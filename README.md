# Watani FTTH Subscription Viewer

A Windows Forms desktop application for managing and monitoring FTTH (Fiber-to-the-Home) subscriptions via the Watani admin API (`admin.ftth.iq`).

## Features

- **Subscription Monitoring** — Load, filter, and view all FTTH subscriptions (Active / Expired / All)
- **Multi-Credential Support** — Save and switch between multiple login credentials
- **Phone Number Lookup** — Automatically fetches customer phone numbers in the background
- **Excel Export** — Export subscription data to `.xlsx` with RTL formatting and alternating row colors
- **WhatsApp Messaging** — Send customizable template messages to subscribers via WhatsApp API with location support
- **Full RTL Arabic Interface** — Right-to-left layout throughout the application
- **Silent Token Refresh** — Automatic re-authentication when tokens expire

## Tech Stack

| Technology | Purpose |
|---|---|
| .NET 8 | Runtime |
| Windows Forms | UI Framework |
| System.Text.Json | JSON parsing |
| ClosedXML | Excel export |
| HttpClient | Async API calls |

## Project Structure

```
├── Forms/
│   ├── MainForm.cs              # Main subscription viewer
│   ├── CredentialsForm.cs       # Credential management dialog
│   └── WhatsAppForm.cs          # WhatsApp message sender
├── Models/
│   ├── Subscription.cs          # Subscription DTOs
│   ├── CustomerDetail.cs        # Customer phone lookup DTO
│   ├── AuthResponse.cs          # Token response DTO
│   ├── Credential.cs            # Stored credential model
│   └── AppSettings.cs           # Application settings model
├── Services/
│   ├── AuthService.cs           # API authentication
│   ├── SubscriptionService.cs   # Subscription data fetching
│   ├── CustomerService.cs       # Customer detail lookup
│   ├── CredentialManager.cs     # Credential persistence
│   ├── SettingsManager.cs       # Settings persistence
│   ├── ExcelExporter.cs         # Excel export with ClosedXML
│   └── WhatsAppService.cs       # WhatsApp API integration
├── Helpers/
│   └── PhoneHelper.cs           # Phone number formatting (964 prefix)
├── credentials.json             # Login credentials (⚠️ not tracked by git)
├── settings.json                # App settings & message templates
└── WataniFTTH.sln
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (Windows)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/hisham2630/Watani_FTTH_Subscription_Viewer.git
   cd Watani_FTTH_Subscription_Viewer
   ```

2. Create a `credentials.json` file in the project root:
   ```json
   [
     {
       "username": "your_username",
       "password": "your_password",
       "agent": "your_agent_name"
     }
   ]
   ```

3. Build and run:
   ```bash
   dotnet run
   ```

### Publish (Self-Contained)

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## Configuration

`settings.json` contains:

| Key | Description |
|---|---|
| `whatsapp_api` | WhatsApp API URL template with `{phone}` and `{message}` placeholders |
| `template_active` | Message template for active subscribers |
| `template_expired` | Message template for expired subscribers |

### Template Variables

| Variable | Replaced With |
|---|---|
| `%CustomerName%` | Customer name |
| `%Expiration%` | Subscription expiration date |
| `%BundleName%` | Subscription bundle name |
| `%يوم%` | Days remaining/elapsed |
| `%ساعة%` | Hours remaining/elapsed |
| `%دقيقة%` | Minutes remaining/elapsed |

## License

Private — All rights reserved.
