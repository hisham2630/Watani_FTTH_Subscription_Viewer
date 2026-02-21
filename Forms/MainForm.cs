using WataniFTTH.Models;
using WataniFTTH.Services;

namespace WataniFTTH.Forms;

public partial class MainForm : Form
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;
    private readonly SubscriptionService _subscriptionService;
    private readonly CustomerService _customerService;
    private readonly CredentialManager _credentialManager;
    private readonly SettingsManager _settingsManager;

    private List<Subscription> _subscriptions = new();
    private CancellationTokenSource? _cts;

    public MainForm()
    {
        InitializeComponent();
        SetupDataGridView();
        SetupStatusCombo();

        _httpClient = AuthService.CreateHttpClient();
        _authService = new AuthService(_httpClient);
        _subscriptionService = new SubscriptionService(_httpClient, _authService);
        _customerService = new CustomerService(_httpClient, _authService);

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _credentialManager = new CredentialManager(Path.Combine(basePath, "credentials.json"));
        _settingsManager = new SettingsManager(Path.Combine(basePath, "settings.json"));

        LoadCredentialsToCombo();

        dtpStartDate.Value = DateTime.Today;
        dtpEndDate.Value = DateTime.Today;
    }

    private void SetupDataGridView()
    {
        dgvSubscriptions.Columns.Clear();

        dgvSubscriptions.Columns.Add("colCustomerName", "اسم المشترك");
        dgvSubscriptions.Columns.Add("colPhone", "رقم الهاتف");
        dgvSubscriptions.Columns.Add("colBundle", "الباقة");
        dgvSubscriptions.Columns.Add("colExpiry", "تاريخ انتهاء الاشتراك");
        dgvSubscriptions.Columns.Add("colStatus", "الحالة");
        dgvSubscriptions.Columns.Add("colSession", "حالة الجلسة");
        dgvSubscriptions.Columns.Add("colSubCode", "رمز الاشتراك");
        dgvSubscriptions.Columns.Add("colZone", "المنطقة");

        dgvSubscriptions.Columns["colCustomerName"]!.Width = 200;
        dgvSubscriptions.Columns["colPhone"]!.Width = 120;
        dgvSubscriptions.Columns["colBundle"]!.Width = 90;
        dgvSubscriptions.Columns["colExpiry"]!.Width = 190;
        dgvSubscriptions.Columns["colStatus"]!.Width = 80;
        dgvSubscriptions.Columns["colSession"]!.Width = 90;
        dgvSubscriptions.Columns["colSubCode"]!.Width = 180;
        dgvSubscriptions.Columns["colZone"]!.Width = 100;

        dgvSubscriptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgvSubscriptions.EnableHeadersVisualStyles = false;
        dgvSubscriptions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
        dgvSubscriptions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvSubscriptions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvSubscriptions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvSubscriptions.ColumnHeadersHeight = 35;

        dgvSubscriptions.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        dgvSubscriptions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 250);
        dgvSubscriptions.RowTemplate.Height = 28;
    }

    private void SetupStatusCombo()
    {
        cmbStatus.Items.Clear();
        cmbStatus.Items.Add("نشط");
        cmbStatus.Items.Add("منتهية الصلاحية");
        cmbStatus.Items.Add("الكل");
        cmbStatus.SelectedIndex = 0;
    }

    private void LoadCredentialsToCombo()
    {
        cmbCredentials.Items.Clear();
        foreach (var cred in _credentialManager.Credentials)
        {
            cmbCredentials.Items.Add(cred.Username);
        }

        var lastUsed = _settingsManager.Settings.LastCredential;
        if (!string.IsNullOrEmpty(lastUsed))
        {
            var idx = cmbCredentials.Items.IndexOf(lastUsed);
            if (idx >= 0) cmbCredentials.SelectedIndex = idx;
        }
        else if (cmbCredentials.Items.Count > 0)
        {
            cmbCredentials.SelectedIndex = 0;
        }
    }

    private void cmbCredentials_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbCredentials.SelectedItem is string username)
        {
            _settingsManager.Settings.LastCredential = username;
            _settingsManager.Save();
        }
    }

    private async void btnLoad_Click(object? sender, EventArgs e)
    {
        if (cmbCredentials.SelectedIndex < 0)
        {
            MessageBox.Show("الرجاء اختيار بيانات الدخول", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var credential = _credentialManager.Credentials[cmbCredentials.SelectedIndex];

        SetLoadingState(true);
        dgvSubscriptions.Rows.Clear();
        lblTotal.Text = "";
        _cts = new CancellationTokenSource();
        bool shouldFetchPhones = false;

        try
        {
            // Step 1: Login
            UpdateProgress(0, 1, "جاري تسجيل الدخول...");
            var loggedIn = await _authService.LoginAsync(credential.Username, credential.Password);
            if (!loggedIn)
            {
                MessageBox.Show($"فشل تسجيل الدخول:\n{_authService.LastError}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Step 2: Build status filter
            string? statusParam = cmbStatus.SelectedIndex switch
            {
                0 => "Active",
                1 => "Expired",
                _ => null
            };

            // Step 3: Show URL being fetched (for debugging)
            var debugUrl = SubscriptionService.BuildUrl(1, statusParam, dtpStartDate.Value, dtpEndDate.Value);
            UpdateProgress(0, 1, "تم تسجيل الدخول بنجاح، جاري تحميل الاشتراكات...");

            // Step 4: Fetch subscriptions
            var progress = new Progress<(int current, int total, string message)>(p =>
            {
                if (p.total > 0)
                {
                    progressBar.Maximum = p.total;
                    progressBar.Value = Math.Min(p.current, p.total);
                }
                lblProgress.Text = p.message;
            });

            _subscriptions = await _subscriptionService.FetchAllAsync(
                statusParam, dtpStartDate.Value, dtpEndDate.Value, progress, _cts.Token);

            // Step 5: Populate grid
            PopulateGrid();
            lblTotal.Text = $"إجمالي: {_subscriptions.Count}";

            if (_subscriptions.Count == 0)
            {
                UpdateProgress(1, 1, "لا توجد اشتراكات في النطاق المحدد");
                MessageBox.Show("لا توجد اشتراكات تطابق المعايير المحددة.\n" +
                    "حاول تغيير نطاق التاريخ أو حالة الاشتراك.", "نتيجة البحث",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                UpdateProgress(1, 1, $"تم تحميل {_subscriptions.Count} اشتراك بنجاح");
                shouldFetchPhones = true;
            }
        }
        catch (OperationCanceledException)
        {
            UpdateProgress(0, 1, "تم الإلغاء");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"حدث خطأ أثناء تحميل البيانات:\n{ex.Message}", "خطأ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetLoadingState(false);
        }

        // Step 6: Fetch phones AFTER loading state is reset
        if (shouldFetchPhones)
        {
            await FetchPhoneNumbersAsync(_cts!.Token);
        }
    }

    private void UpdateProgress(int current, int total, string message)
    {
        pnlProgress.Visible = true;
        if (total > 0)
        {
            progressBar.Maximum = total;
            progressBar.Value = Math.Min(current, total);
        }
        lblProgress.Text = message;
        Application.DoEvents();
    }

    private void PopulateGrid()
    {
        dgvSubscriptions.SuspendLayout();
        dgvSubscriptions.Rows.Clear();

        foreach (var sub in _subscriptions)
        {
            var statusText = sub.Status == "Active" ? "نشط" : "منتهية الصلاحية";
            var sessionText = sub.HasActiveSession ? "متصل" : "غير متصل";

            var rowIndex = dgvSubscriptions.Rows.Add(
                sub.Customer?.DisplayValue ?? "",
                sub.PhoneNumber,
                sub.BaseServiceName,
                "\u200E" + sub.ExpiresLocal.ToString("yyyy-MM-dd hh:mm:ss tt"),
                statusText,
                sessionText,
                sub.Username,
                sub.Zone?.DisplayValue ?? ""
            );

            dgvSubscriptions.Rows[rowIndex].Tag = sub;
        }

        dgvSubscriptions.ResumeLayout();
    }

    private async Task FetchPhoneNumbersAsync(CancellationToken ct)
    {
        var subsWithCustomer = new List<(int index, Subscription sub, string customerId)>();

        for (int i = 0; i < _subscriptions.Count; i++)
        {
            var sub = _subscriptions[i];
            var customerId = sub.Customer?.Id;
            if (!string.IsNullOrEmpty(customerId))
                subsWithCustomer.Add((i, sub, customerId));
        }

        if (subsWithCustomer.Count == 0)
            return;

        // Show progress bar for phone fetching
        pnlProgress.Visible = true;
        progressBar.Maximum = subsWithCustomer.Count;
        progressBar.Value = 0;

        for (int j = 0; j < subsWithCustomer.Count; j++)
        {
            ct.ThrowIfCancellationRequested();

            var (index, sub, customerId) = subsWithCustomer[j];
            var customerName = sub.Customer?.DisplayValue ?? "";

            lblProgress.Text = $"جاري جلب رقم: {customerName} — {j + 1}/{subsWithCustomer.Count}";
            progressBar.Value = j;

            try
            {
                var phone = await _customerService.GetPhoneAsync(customerId, ct);
                sub.PhoneNumber = phone;
                UpdatePhoneCell(index, phone);
            }
            catch (OperationCanceledException) { throw; }
            catch { /* skip failed phone lookups */ }
        }

        progressBar.Value = subsWithCustomer.Count;
        lblProgress.Text = $"تم جلب {subsWithCustomer.Count} رقم هاتف";

        // Hide after a short delay
        await Task.Delay(2000, CancellationToken.None);
        pnlProgress.Visible = false;
    }

    private void UpdatePhoneCell(int rowIndex, string phone)
    {
        if (rowIndex >= 0 && rowIndex < dgvSubscriptions.Rows.Count)
        {
            dgvSubscriptions.Rows[rowIndex].Cells["colPhone"].Value = phone;
        }
    }

    private void SetLoadingState(bool loading)
    {
        pnlProgress.Visible = loading;
        btnLoad.Enabled = !loading;
        cmbCredentials.Enabled = !loading;
        cmbStatus.Enabled = !loading;
        dtpStartDate.Enabled = !loading;
        dtpEndDate.Enabled = !loading;

        if (loading)
        {
            progressBar.Value = 0;
            lblProgress.Text = "";
        }
    }

    private void btnManageCredentials_Click(object? sender, EventArgs e)
    {
        using var form = new CredentialsForm(_credentialManager);
        form.ShowDialog(this);
        LoadCredentialsToCombo();
    }

    private async void btnExportExcel_Click(object? sender, EventArgs e)
    {
        if (dgvSubscriptions.Rows.Count == 0)
        {
            MessageBox.Show("لا توجد بيانات للتصدير", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var sfd = new SaveFileDialog
        {
            Filter = "Excel Files (*.xlsx)|*.xlsx",
            FileName = $"Subscriptions_{DateTime.Now:yyyy-MM-dd_HHmmss}.xlsx"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        // Extract data from DGV on UI thread
        var headers = new List<string>();
        for (int i = 0; i < dgvSubscriptions.Columns.Count; i++)
            headers.Add(dgvSubscriptions.Columns[i].HeaderText);

        var rows = new List<string[]>();
        for (int r = 0; r < dgvSubscriptions.Rows.Count; r++)
        {
            var row = new string[headers.Count];
            for (int c = 0; c < headers.Count; c++)
                row[c] = dgvSubscriptions.Rows[r].Cells[c].Value?.ToString() ?? string.Empty;
            rows.Add(row);
        }

        var filePath = sfd.FileName;

        // Show progress and disable export button
        pnlProgress.Visible = true;
        btnExportExcel.Enabled = false;
        progressBar.Value = 0;

        var progress = new Progress<(int current, int total, string message)>(p =>
        {
            if (p.total > 0)
            {
                progressBar.Maximum = p.total;
                progressBar.Value = Math.Min(p.current, p.total);
            }
            lblProgress.Text = p.message;
        });

        try
        {
            await Task.Run(() => ExcelExporter.Export(headers, rows, filePath, progress));
            MessageBox.Show("تم التصدير بنجاح!", "نجاح",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"فشل التصدير:\n{ex.Message}", "خطأ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnExportExcel.Enabled = true;
            await Task.Delay(1500);
            pnlProgress.Visible = false;
        }
    }

    private void btnWhatsApp_Click(object? sender, EventArgs e)
    {
        var targets = new List<SendTarget>();
        var selectedRows = dgvSubscriptions.SelectedRows;

        foreach (DataGridViewRow row in selectedRows)
        {
            if (row.Tag is Subscription sub)
            {
                targets.Add(new SendTarget
                {
                    CustomerName = sub.Customer?.DisplayValue ?? "",
                    Phone = sub.PhoneNumber,
                    BundleName = sub.BaseServiceName,
                    ExpiresLocal = sub.ExpiresLocal,
                    Status = sub.Status
                });
            }
        }

        var allTargets = new List<SendTarget>();
        foreach (DataGridViewRow row in dgvSubscriptions.Rows)
        {
            if (row.Tag is Subscription sub)
            {
                allTargets.Add(new SendTarget
                {
                    CustomerName = sub.Customer?.DisplayValue ?? "",
                    Phone = sub.PhoneNumber,
                    BundleName = sub.BaseServiceName,
                    ExpiresLocal = sub.ExpiresLocal,
                    Status = sub.Status
                });
            }
        }

        using var form = new WhatsAppForm(_settingsManager, targets, allTargets);
        form.ShowDialog(this);
    }
}
