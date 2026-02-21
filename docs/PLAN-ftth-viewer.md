# PLAN: Watani FTTH Subscription Viewer

## Goal

Recreate a **C# Windows Forms (.NET 8)** desktop application that authenticates against the Watani FTTH API, fetches all subscriptions (~2700+), displays them in an RTL Arabic DataGridView with filtering, supports multi-account credential management, Excel export, and WhatsApp messaging via local API.

## Project Type

**DESKTOP** — C# WinForms (.NET 8), single-project with clean folder structure.

## Success Criteria

- [ ] App authenticates via `POST https://admin.ftth.iq/api/auth/Contractor/token`
- [ ] App loads ALL subscriptions (paginated, 150/page) with retry/error handling
- [ ] DataGridView shows: Customer Name, Phone, Bundle, Expiry Date, Status, Session Status, Subscription Code, Zone
- [ ] Phone numbers fetched from `/api/customers/{id}` (fallback to `secondaryPhone`)
- [ ] Status filter: Active / Expired / All
- [ ] Date range filter on expiry date (client-side)
- [ ] Credential management form (add/edit/delete, persisted to `credentials.json`)
- [ ] WhatsApp message form with templates, placeholders, and send to selected/all
- [ ] Excel export via ClosedXML with alternating row colors
- [ ] Full RTL Arabic interface
- [ ] Graceful error handling (API timeouts, network errors, retry logic)
- [ ] Silent token refresh using stored credentials

## Tech Stack

| Technology | Purpose | Rationale |
|------------|---------|-----------|
| .NET 8 | Runtime | User requirement, LTS |
| Windows Forms | UI | User requirement, matches original app |
| System.Text.Json | JSON parsing | Built-in .NET 8, no extra dependency |
| HttpClient | API calls | Built-in, async support |
| ClosedXML | Excel export | Lightweight, no Office install needed |

### ⚠️ Visual Studio Designer Compatibility (GLOBAL RULE)

**All `.Designer.cs` files MUST be 100% compatible with the Visual Studio WinForms Designer.**

| ❌ FORBIDDEN in .Designer.cs | ✅ ALLOWED in .Designer.cs |
|-------------------------------|-----------------------------|
| Lambda expressions (`(s, e) => ...`) | Named event handlers (`this.btn.Click += new EventHandler(this.btn_Click);`) |
| For/foreach loops | Individual control declarations |
| Dynamic control creation | Static control instantiation |
| Conditional logic | Simple property assignments |
| DataGridView column creation | Basic control layout only |

**Rule**: All dynamic setup (DataGridView columns, alternating row styles, runtime configuration) goes in the **code-behind `.cs` file** — either in the constructor after `InitializeComponent()` or in a dedicated `SetupControls()` method.

## File Structure

```
Watani_FTTH_Subscription_Viewer/
├── WataniFTTH.csproj
├── Program.cs
├── credentials.json              ← already exists
├── settings.json                 ← already exists
│
├── Models/
│   ├── Subscription.cs           # API response DTOs for subscriptions
│   ├── CustomerDetail.cs         # API response DTO for customer phone lookup
│   ├── AuthResponse.cs           # Token response DTO
│   ├── Credential.cs             # Credential model (username, password, agent)
│   └── AppSettings.cs            # Settings model (WhatsApp URL, templates)
│
├── Services/
│   ├── AuthService.cs            # Login, token management, silent refresh
│   ├── SubscriptionService.cs    # Fetch subscriptions (paginated, retry)
│   ├── CustomerService.cs        # Fetch customer phone by ID (with cache)
│   ├── WhatsAppService.cs        # Send WhatsApp messages via GET API
│   ├── CredentialManager.cs      # CRUD credentials.json
│   ├── SettingsManager.cs        # Load/save settings.json
│   └── ExcelExporter.cs          # Export DataGridView to .xlsx
│
├── Forms/
│   ├── MainForm.cs               # Main subscription viewer form
│   ├── MainForm.Designer.cs      # Designer-generated layout
│   ├── MainForm.resx
│   ├── CredentialsForm.cs        # Credential management dialog
│   ├── CredentialsForm.Designer.cs
│   ├── CredentialsForm.resx
│   ├── WhatsAppForm.cs           # WhatsApp message sender dialog
│   ├── WhatsAppForm.Designer.cs
│   └── WhatsAppForm.resx
│
└── Helpers/
    └── PhoneHelper.cs            # Format phone: remove leading 0, prepend 964
```

---

## Detailed Specifications (From Q&A)

### Display Rules

| Field | Source | Display Format |
|-------|--------|----------------|
| اسم المشترك (Customer Name) | `customer.displayValue` | As-is (Arabic) |
| رقم الهاتف (Phone) | `/api/customers/{id}` → `model.primaryContact.mobile` (fallback: `secondaryPhone`) | As-is from API |
| الباقة (Bundle) | `services[].id` where `productType.displayValue == "Base"` | e.g., "FIBER 35" |
| تاريخ انتهاء الاشتراك (Expiry) | `expires` (UTC → **+3h** Baghdad) | `yyyy-MM-dd hh:mm:ss tt` |
| الحالة (Status) | `status` | "Active" → **"نشط"**, "Expired" → **"منتهية الصلاحية"** |
| حالة الجلسة (Session) | `hasActiveSession` | `true` → **"متصل"**, `false` → **"غير متصل"** |
| رمز الاشتراك (Sub Code) | `username` | As-is |
| المنطقة (Zone) | `zone.displayValue` | As-is |

### Selection & Interaction

- **Row selection**: Native multi-select (Ctrl+Click, Shift+Click) — `SelectionMode = FullRowSelect`, `MultiSelect = true`
- **No checkbox column** needed
- **Alternating row colors** in DataGridView (simple alternation, no conditional coloring)

### Phone Number Fetching

- **Strategy**: Background fetch AFTER grid loads (Option C)
- **Concurrency**: 2 concurrent requests at a time (SemaphoreSlim(2))
- **Fallback**: `primaryContact.mobile` → `secondaryPhone` → skip (empty cell)
- **Cache**: Dictionary<string, string> — same customer ID won't be re-fetched
- **Grid update**: Phone column fills in progressively as results arrive

### WhatsApp Messaging

- **API**: `GET http://{url}/send?number={phone}&message={urlEncodedMessage}`
- **Phone format**: Strip leading `0`, prepend `964` (`07735638225` → `9647735638225`)
- **Selection**: "Send to All" checkbox → all rows. Unchecked → only `SelectedRows` in DataGridView
- **Template**: Active subscribers get `template_active`, Expired get `template_expired`
- **Placeholders**: `%CustomerName%`, `%Expiration%`, `%BundleName%`, `%يوم%`, `%ساعة%`, `%دقيقة%`
- **Time calc**: `expires (UTC+3) - DateTime.Now` → absolute value for days/hours/minutes
- **Failure report**: On completion, show summary dialog: "Sent: X, Failed: Y" with list of failed customers (name + reason: no phone / API error)

### Token Management

- **Silent re-login**: If token expired (401 response or elapsed > 3500s), silently re-authenticate using stored credential
- **No user prompt** for session expiry

### Excel Export

- **Columns**: All columns visible in DataGridView
- **Style**: Alternating row colors (match grid appearance)
- **RTL**: Sheet direction right-to-left
- **Save**: `SaveFileDialog` to choose output path

### Credential Management

- **Auto-select**: Last used credential remembered (saved to `settings.json` as `last_credential`)
- **Persist**: `credentials.json` array format (already exists)

---

## Tasks

### Phase 1: Foundation (Project Setup)

- [ ] **T1: Create .NET 8 WinForms project**
  - Run `dotnet new winforms -n WataniFTTH --framework net8.0` in project dir
  - Add ClosedXML NuGet: `dotnet add package ClosedXML`
  - Set `credentials.json` and `settings.json` to Copy to Output Directory
  - → Verify: `dotnet build` succeeds

### Phase 2: Models & Services (Backend Logic)

- [ ] **T2: Create all model classes**
  - `Models/AuthResponse.cs` — `access_token`, `refresh_token`, `expires_in`, `token_type`
  - `Models/Subscription.cs` — Full DTO matching API JSON (nested: partner, customer, services, zone, bundle, activeSession, self). **`expires` comes as UTC — convert to UTC+3 on parse.**
  - `Models/SubscriptionPage.cs` — `totalCount` + `items[]`
  - `Models/CustomerDetail.cs` — Nested model with `primaryContact.mobile` + `secondaryPhone`
  - `Models/Credential.cs` — `Username`, `Password`, `Agent`
  - `Models/AppSettings.cs` — `WhatsAppApi`, `TemplateActive`, `TemplateExpired`, `LastCredential`
  - → Verify: Project compiles with no errors

- [ ] **T3: Create AuthService**
  - `POST` to `https://admin.ftth.iq/api/auth/Contractor/token`
  - Body: `grant_type=password&scope=openid profile&client_id=&username={u}&password={p}`
  - Content-Type: `application/x-www-form-urlencoded`
  - Store access_token + login timestamp for expiry tracking
  - **Silent refresh**: If token age > 3500s or 401 received → re-login with stored creds
  - → Verify: Successful login returns token

- [ ] **T4: Create SubscriptionService**
  - `GET https://admin.ftth.iq/api/subscriptions?pageSize=150&pageNumber={n}&sortCriteria.property=expires&sortCriteria.direction=asc&status={status}&fromExpirationDate={from}&toExpirationDate={to}&hierarchyLevel=0`
  - Bearer token in Authorization header
  - **Date params (server-side filter)**: Convert local dates (UTC+3) to UTC for API:
    - `fromExpirationDate`: Local start date midnight → subtract 3h → `yyyy-MM-ddT21:00:00.000Z` (previous day)
    - `toExpirationDate`: Local end date end-of-day → subtract 3h → `yyyy-MM-ddT20:59:59.999Z`
    - Example: Local 2026-02-19 → `fromExpirationDate=2026-02-18T21:00:00.000Z&toExpirationDate=2026-02-19T20:59:59.999Z`
  - Paginate: loop pages until all items fetched (totalCount / 150)
  - **Retry logic**: 3 retries with exponential backoff on failure
  - **Status param**: "Active", "Expired", or omit for All
  - Report progress callback for UI (e.g., "Page 5/19 loaded")
  - → Verify: Returns full list of subscriptions

- [ ] **T5: Create CustomerService**
  - `GET https://admin.ftth.iq/api/customers/{customerId}`
  - Parse `model.primaryContact.mobile` → fallback `model.primaryContact.secondaryPhone`
  - **Cache**: `Dictionary<string, string>` keyed by customer ID
  - **Concurrency**: `SemaphoreSlim(2)` — only 2 concurrent requests
  - Handle errors gracefully (return empty string on failure)
  - → Verify: Returns phone number for a given customer ID

- [ ] **T6: Create CredentialManager**
  - Load/Save `credentials.json` (array of `Credential`)
  - Add, Update, Delete credentials
  - → Verify: CRUD ops persist to file

- [ ] **T7: Create SettingsManager**
  - Load/Save `settings.json`
  - Include `last_credential` field for auto-select
  - → Verify: Settings round-trip correctly

- [ ] **T8: Create WhatsAppService**
  - `GET http://{apiUrl}/send?number={phone}&message={encodedMessage}`
  - Phone formatting via PhoneHelper
  - Template placeholder replacement: `%CustomerName%`, `%Expiration%`, `%BundleName%`, `%يوم%`, `%ساعة%`, `%دقيقة%`
  - Time remaining calculation: `|expires(UTC+3) - DateTime.Now|` → days, hours, minutes
  - **Return result**: success/failure per message for final report
  - → Verify: Correctly formats phone and message

- [ ] **T9: Create ExcelExporter**
  - Export DataGridView contents to `.xlsx` using ClosedXML
  - Include all visible columns
  - **Alternating row colors** matching DataGridView
  - RTL sheet direction
  - SaveFileDialog for output path
  - → Verify: Generated Excel opens with correct data and alternating colors

- [ ] **T10: Create PhoneHelper**
  - `FormatPhone(string raw)` → removes leading 0, prepends 964
  - Handle edge cases (null, empty, already formatted)
  - → Verify: `"07735638225"` → `"9647735638225"`

### Phase 3: Forms (UI)

- [ ] **T11: Create MainForm**
  - **RTL Layout**: `RightToLeft = Yes`, `RightToLeftLayout = true`
  - **Top Section**: Credential dropdown (ComboBox) with auto-select last used, "إدارة بيانات الدخول" button
  - **Filter Section**: Start Date, End Date, Status dropdown (نشط / منتهية الصلاحية / الكل), "تحميل الاشتراكات" button
  - **Date filter**: Server-side — convert local dates to UTC before passing to API as `fromExpirationDate`/`toExpirationDate`
  - **DataGridView**:
    - Columns: اسم المشترك, رقم الهاتف, الباقة, تاريخ انتهاء الاشتراك, الحالة, حالة الجلسة, رمز الاشتراك, المنطقة
    - `SelectionMode = FullRowSelect`, `MultiSelect = true`
    - `AlternatingRowsDefaultCellStyle` for alternating colors
    - Date format: `yyyy-MM-dd hh:mm:ss tt`
  - **Progress**: ProgressBar + label showing fetch progress
  - **Bottom**: "تصدير الى ملف اكسل", "إرسال رسالة على الواتساب", "إجمالي: {count}" label
  - **Loading flow**: تحميل click → login → fetch all pages with date+status filters (show progress) → populate grid → background-fetch phone numbers (2 concurrent, progressive fill)
  - → Verify: App starts, loads data, phone numbers fill progressively, dates in UTC+3

- [ ] **T12: Create CredentialsForm**
  - **RTL Layout**
  - **Right panel**: ListBox with saved credential usernames
  - **Left panel**: TextBoxes for Username, Password, Agent Name
  - **Buttons**: إضافة جديد (Add), حفظ التغييرات (Save), حذف المحدد (Delete), إغلاق (Close)
  - **Flow**: Select from list → populate edit fields. Add/Save/Delete → update list + persist JSON
  - On close, refresh MainForm credential dropdown
  - → Verify: Add, edit, delete credentials; changes persist after restart

- [ ] **T13: Create WhatsAppForm**
  - **RTL Layout**
  - **Top**: TextBox for WhatsApp API URL (editable, saved to settings)
  - **Tabs**: TabControl — "رسالة للمشتركين النشطين" / "رسالة للمنتهية صلاحيتهم"
  - **Each tab**: RichTextBox with message template (loaded from settings, editable, saved on change)
  - **Bottom**: "إرسال إلى الجميع" CheckBox, "إرسال" (Send), "إغلاق" (Close)
  - **Send logic**:
    - "Send to All" checked → all rows. Unchecked → `SelectedRows` only
    - Template selection: Active sub → `template_active`, Expired → `template_expired`
    - Replace all placeholders with actual values
    - **Failure handling**: Skip customers with no phone, log failures, continue
    - **Final report dialog**: "Sent: X, Failed: Y" + list of failed (customer name + reason)
  - → Verify: Templates editable, sends messages, shows completion report

### Phase 4: Polish & Integration

- [ ] **T14: Wire everything together**
  - MainForm loads credentials on start → auto-select last used (from `settings.json`)
  - Save `last_credential` to settings when user changes selection
  - Login on "تحميل" click (or on first load)
  - **Silent token refresh**: Track token age, re-login if expired (no user prompt)
  - Proper async/await throughout (no UI freezing)
  - → Verify: Full end-to-end workflow works

- [ ] **T15: Error handling & edge cases**
  - API timeout handling (HttpClient timeout = 30s per request)
  - Network disconnection → friendly Arabic error message
  - Empty results → show "لا توجد اشتراكات"
  - Invalid credentials → show error, don't crash
  - Partial load failure → show what was loaded + error count
  - Phone fetch failure → empty cell, log error
  - → Verify: App doesn't crash on any error scenario

---

## API Reference (Quick)

| Endpoint | Method | Auth | Purpose |
|----------|--------|------|---------|
| `/api/auth/Contractor/token` | POST | None | Login, get access_token |
| `/api/subscriptions?pageSize=150&pageNumber={n}&status={s}&sortCriteria.property=expires&sortCriteria.direction=asc&fromExpirationDate={utc}&toExpirationDate={utc}&hierarchyLevel=0` | GET | Bearer | Fetch subscriptions (date filter server-side) |
| `/api/customers/{id}` | GET | Bearer | Get customer phone (mobile → secondaryPhone) |
| `{whatsappUrl}/send?number={n}&message={m}` | GET | None | Send WhatsApp message |

## Key Design Decisions

1. **Page size 150** for subscriptions (as specified by user)
2. **Phone numbers cached** (`Dictionary<string,string>`) — same customer ID won't be re-fetched
3. **Phone fetch concurrency**: 2 concurrent requests (`SemaphoreSlim(2)`), background after grid loads
4. **Phone fallback**: `primaryContact.mobile` → `secondaryPhone` → skip
5. **Retry with exponential backoff** on API failures (3 retries)
6. **Server-side date filtering** — API supports `fromExpirationDate`/`toExpirationDate` in UTC. Local dates (UTC+3) converted to UTC before sending (subtract 3h)
7. **UTC+3 timezone conversion** — API returns `expires` in UTC. All dates converted to Baghdad time (UTC+3) on parse. Display format: `yyyy-MM-dd hh:mm:ss tt`
8. **Silent token refresh** — re-login with stored credentials when token expires (no user prompt)
9. **Row selection**: Native multi-select (Ctrl/Shift+Click), no checkbox column
10. **Status display**: Active → "نشط", Expired → "منتهية الصلاحية"
11. **Session display**: `hasActiveSession` → "متصل" / "غير متصل"
12. **Excel export**: All visible columns + alternating row colors
13. **WhatsApp failure report**: Summary dialog on completion with sent/failed counts + failed customer list
14. **Last credential auto-select**: Saved in `settings.json` as `last_credential`

## Done When

- [ ] App builds and runs on .NET 8
- [ ] Login works with real credentials
- [ ] All ~2700 subscriptions load with progress indicator
- [ ] Phone numbers fill in progressively in background (2 concurrent)
- [ ] Dates display in `yyyy-MM-dd hh:mm:ss tt` format (UTC+3)
- [ ] Status shows "نشط" / "منتهية الصلاحية"
- [ ] Session shows "متصل" / "غير متصل"
- [ ] Filter by Active/Expired/All works
- [ ] Date range filter works
- [ ] Token silently refreshes when expired
- [ ] Credential management (add/edit/delete) persists correctly
- [ ] Last used credential auto-selected on startup
- [ ] Excel export generates .xlsx with alternating row colors
- [ ] WhatsApp messages send with formatted phone (964...) + template placeholders
- [ ] WhatsApp completion report shows sent/failed counts
- [ ] App handles API errors without crashing
- [ ] Full RTL Arabic interface matches original screenshots
