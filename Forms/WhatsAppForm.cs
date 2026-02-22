using WataniFTTH.Services;

namespace WataniFTTH.Forms;

public partial class WhatsAppForm : Form
{
    private readonly SettingsManager _settingsManager;
    private readonly List<SendTarget> _selectedTargets;
    private readonly List<SendTarget> _allTargets;
    private readonly WhatsAppService _whatsAppService;

    private CancellationTokenSource? _cts;
    private bool _paused;

    public WhatsAppForm(
        SettingsManager settingsManager,
        List<SendTarget> selectedTargets,
        List<SendTarget> allTargets)
    {
        InitializeComponent();

        _settingsManager = settingsManager;
        _selectedTargets = selectedTargets;
        _allTargets = allTargets;
        _whatsAppService = new WhatsAppService(new HttpClient { Timeout = TimeSpan.FromSeconds(15) });

        LoadSettings();
        LoadSuffixes();
        LoadBlockedSuffixes();
        tabFilter.SelectedIndex = _settingsManager.Settings.FilterTabIndex;

        this.SizeChanged += WhatsAppForm_SizeChanged;
    }

    private void WhatsAppForm_SizeChanged(object? sender, EventArgs e)
    {
        if (Owner == null) return;

        if (WindowState == FormWindowState.Minimized)
        {
            Owner.WindowState = FormWindowState.Minimized;
        }
        else if (WindowState == FormWindowState.Normal)
        {
            Owner.WindowState = FormWindowState.Normal;
        }
    }

    private void LoadSettings()
    {
        txtApiUrl.Text = _settingsManager.Settings.WhatsAppApi;
        rtbActiveTemplate.Text = _settingsManager.Settings.TemplateActive;
        rtbExpiredTemplate.Text = _settingsManager.Settings.TemplateExpired;
        chkLocation.Checked = _settingsManager.Settings.LocationEnabled;
        txtCoords.Text = _settingsManager.Settings.LocationCoords;
        ToggleLocationFields(chkLocation.Checked);
    }

    private void SaveSettings()
    {
        _settingsManager.Settings.WhatsAppApi = txtApiUrl.Text.Trim();
        _settingsManager.Settings.TemplateActive = rtbActiveTemplate.Text;
        _settingsManager.Settings.TemplateExpired = rtbExpiredTemplate.Text;
        _settingsManager.Settings.LocationEnabled = chkLocation.Checked;
        _settingsManager.Settings.LocationCoords = txtCoords.Text.Trim();
        SaveSuffixes();
        SaveBlockedSuffixes();
        _settingsManager.Settings.FilterTabIndex = tabFilter.SelectedIndex;
        _settingsManager.Save();
    }

    // ──────────────────────────────────────────
    //  Allowed suffix filter (whitelist)
    // ──────────────────────────────────────────

    private void LoadSuffixes()
    {
        lstSuffixes.Items.Clear();
        foreach (var s in _settingsManager.Settings.AllowedSuffixes)
            lstSuffixes.Items.Add(s);

        ToggleFilterControls(chkFilterEnabled.Checked);
    }

    private void SaveSuffixes()
    {
        _settingsManager.Settings.AllowedSuffixes.Clear();
        foreach (var item in lstSuffixes.Items)
            _settingsManager.Settings.AllowedSuffixes.Add(item.ToString()!);
    }

    private void btnAddSuffix_Click(object? sender, EventArgs e) => AddToList(txtNewSuffix, lstSuffixes);

    private void txtNewSuffix_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; AddToList(txtNewSuffix, lstSuffixes); }
    }

    private void btnRemoveSuffix_Click(object? sender, EventArgs e) => RemoveSelected(lstSuffixes);

    private void btnResetSuffixes_Click(object? sender, EventArgs e)
    {
        var result = MessageBox.Show("هل تريد إعادة تعيين القائمة إلى القيم الافتراضية؟",
            "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            lstSuffixes.Items.Clear();
            var defaults = new[] { "NOMMOR", "NOMOO", "NOMOON", "NOMOOR", "NOMOORD", "NOMOR" };
            foreach (var d in defaults)
                lstSuffixes.Items.Add(d);
        }
    }

    private void chkFilterEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        ToggleFilterControls(chkFilterEnabled.Checked);
    }

    private void ToggleFilterControls(bool enabled)
    {
        lstSuffixes.Enabled = enabled;
        txtNewSuffix.Enabled = enabled;
        btnAddSuffix.Enabled = enabled;
        btnRemoveSuffix.Enabled = enabled;
        btnResetSuffixes.Enabled = enabled;
    }

    // ──────────────────────────────────────────
    //  Blocked suffix filter (blacklist)
    // ──────────────────────────────────────────

    private void LoadBlockedSuffixes()
    {
        lstBlocked.Items.Clear();
        foreach (var s in _settingsManager.Settings.BlockedSuffixes)
            lstBlocked.Items.Add(s);

        ToggleBlockControls(chkBlockEnabled.Checked);
    }

    private void SaveBlockedSuffixes()
    {
        _settingsManager.Settings.BlockedSuffixes.Clear();
        foreach (var item in lstBlocked.Items)
            _settingsManager.Settings.BlockedSuffixes.Add(item.ToString()!);
    }

    private void btnAddBlocked_Click(object? sender, EventArgs e) => AddToList(txtNewBlocked, lstBlocked);

    private void txtNewBlocked_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; AddToList(txtNewBlocked, lstBlocked); }
    }

    private void btnRemoveBlocked_Click(object? sender, EventArgs e) => RemoveSelected(lstBlocked);

    private void btnClearBlocked_Click(object? sender, EventArgs e)
    {
        var result = MessageBox.Show("هل تريد إعادة تعيين القائمة إلى القيم الافتراضية؟",
            "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            lstBlocked.Items.Clear();
            var defaults = new[] { "ALZAEIM", "ALZAIEM", "ALZIAEM", "DUKAN", "GNEL",
                "GNRL", "MUS", "OM", "TMT", "VAIO", "ZAIN" };
            foreach (var d in defaults)
                lstBlocked.Items.Add(d);
        }
    }

    private void chkBlockEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        ToggleBlockControls(chkBlockEnabled.Checked);
    }

    private void ToggleBlockControls(bool enabled)
    {
        lstBlocked.Enabled = enabled;
        txtNewBlocked.Enabled = enabled;
        btnAddBlocked.Enabled = enabled;
        btnRemoveBlocked.Enabled = enabled;
        btnClearBlocked.Enabled = enabled;
    }

    // ──────────────────────────────────────────
    //  Shared helpers for list management
    // ──────────────────────────────────────────

    private static void AddToList(TextBox txtInput, ListBox lst)
    {
        var value = txtInput.Text.Trim().ToUpperInvariant();
        if (string.IsNullOrEmpty(value))
        {
            MessageBox.Show("الرجاء إدخال لاحقة", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        foreach (var item in lst.Items)
        {
            if (string.Equals(item.ToString(), value, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("هذه اللاحقة موجودة بالفعل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInput.Clear();
                return;
            }
        }

        lst.Items.Add(value);
        txtInput.Clear();
        txtInput.Focus();
    }

    private static void RemoveSelected(ListBox lst)
    {
        if (lst.SelectedIndex < 0)
        {
            MessageBox.Show("الرجاء تحديد لاحقة لحذفها", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        lst.Items.RemoveAt(lst.SelectedIndex);
    }

    // ──────────────────────────────────────────
    //  Filter logic
    // ──────────────────────────────────────────

    /// <summary>
    /// Extracts the suffix after the last dash in a device username.
    /// E.g., "ABC-DEF-NOMMOR" → "NOMMOR"
    /// </summary>
    private static string GetSuffixAfterLastDash(string username)
    {
        if (string.IsNullOrEmpty(username))
            return string.Empty;

        var lastDash = username.LastIndexOf('-');
        return lastDash >= 0 && lastDash < username.Length - 1
            ? username[(lastDash + 1)..]
            : username;
    }

    /// <summary>
    /// Applies both whitelist and blacklist filters:
    ///   1. Whitelist (allowed): keep only matching suffixes
    ///   2. Blacklist (blocked): remove matching suffixes
    /// </summary>
    private List<SendTarget> ApplyFilter(List<SendTarget> targets)
    {
        var result = targets;

        // Step 1: Whitelist — keep only allowed suffixes
        if (chkFilterEnabled.Checked && lstSuffixes.Items.Count > 0)
        {
            var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in lstSuffixes.Items)
                allowed.Add(item.ToString()!);

            result = result.Where(t =>
            {
                var suffix = GetSuffixAfterLastDash(t.DeviceUsername);
                return allowed.Contains(suffix);
            }).ToList();
        }

        // Step 2: Blacklist — remove blocked suffixes
        if (chkBlockEnabled.Checked && lstBlocked.Items.Count > 0)
        {
            var blocked = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in lstBlocked.Items)
                blocked.Add(item.ToString()!);

            result = result.Where(t =>
            {
                var suffix = GetSuffixAfterLastDash(t.DeviceUsername);
                return !blocked.Contains(suffix);
            }).ToList();
        }

        return result;
    }

    /// <summary>
    /// Returns true if any filter is active.
    /// </summary>
    private bool IsAnyFilterActive =>
        (chkFilterEnabled.Checked && lstSuffixes.Items.Count > 0) ||
        (chkBlockEnabled.Checked && lstBlocked.Items.Count > 0);

    // ──────────────────────────────────────────
    //  Location
    // ──────────────────────────────────────────

    private void chkLocation_CheckedChanged(object? sender, EventArgs e)
    {
        ToggleLocationFields(chkLocation.Checked);
    }

    private void ToggleLocationFields(bool enabled)
    {
        txtCoords.Enabled = enabled;
        lblCoords.Enabled = enabled;
    }

    // ──────────────────────────────────────────
    //  Send
    // ──────────────────────────────────────────

    private async void btnSend_Click(object? sender, EventArgs e)
    {
        SaveSettings();

        var rawTargets = chkSendAll.Checked ? _allTargets : _selectedTargets;
        var targets = ApplyFilter(rawTargets);
        var filteredOut = IsAnyFilterActive
            ? rawTargets.Except(targets).ToList()
            : new List<SendTarget>();

        if (rawTargets.Count > 0 && targets.Count == 0 && IsAnyFilterActive)
        {
            MessageBox.Show(
                $"تم استبعاد جميع المشتركين ({rawTargets.Count}) بواسطة الفلتر.\n" +
                "تحقق من إعدادات الفلتر أو قم بتعطيله.",
                "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (targets.Count == 0)
        {
            MessageBox.Show("لا يوجد مشتركين مختارين للإرسال", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Inform user about filtered count
        if (IsAnyFilterActive && targets.Count < rawTargets.Count)
        {
            var skipped = rawTargets.Count - targets.Count;
            var confirm = MessageBox.Show(
                $"سيتم الإرسال إلى {targets.Count} مشترك (تم استبعاد {skipped} بواسطة الفلتر).\nمتابعة؟",
                "تأكيد الفلتر", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;
        }

        var apiUrl = txtApiUrl.Text.Trim();
        if (string.IsNullOrEmpty(apiUrl))
        {
            MessageBox.Show("الرجاء إدخال قالب عنوان API", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!apiUrl.Contains("{phone}") || !apiUrl.Contains("{message}"))
        {
            MessageBox.Show("قالب العنوان يجب أن يحتوي على {phone} و {message}", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Setup UI for sending
        btnSend.Enabled = false;
        btnClose.Enabled = false;
        btnPause.Visible = true;
        btnStop.Visible = true;
        progressBar.Visible = true;
        progressBar.Maximum = targets.Count;
        progressBar.Value = 0;

        _cts = new CancellationTokenSource();
        _paused = false;

        var results = new List<WhatsAppSendResult>();
        int sent = 0;
        int failed = 0;
        var rng = new Random();
        bool stopped = false;

        for (int i = 0; i < targets.Count; i++)
        {
            // Check for stop
            if (_cts.IsCancellationRequested)
            {
                stopped = true;
                break;
            }

            // Check for pause
            while (_paused && !_cts.IsCancellationRequested)
            {
                lblProgress.Text = $"⏸ متوقف مؤقتاً — {sent} من {targets.Count}";
                await Task.Delay(300);
            }

            if (_cts.IsCancellationRequested)
            {
                stopped = true;
                break;
            }

            var target = targets[i];

            var template = target.Status == "Active"
                ? rtbActiveTemplate.Text
                : rtbExpiredTemplate.Text;

            string? location = null;
            if (chkLocation.Checked)
            {
                var coords = txtCoords.Text.Trim().Replace(" ", "");
                if (!string.IsNullOrEmpty(coords))
                    location = coords;
            }

            var result = await _whatsAppService.SendAsync(
                apiUrl,
                target.Phone,
                target.CustomerName,
                template,
                target.BundleName,
                target.ExpiresLocal,
                location);

            results.Add(result);

            if (result.Success)
                sent++;
            else
                failed++;

            progressBar.Value = i + 1;
            lblProgress.Text = $"تم إرسال {sent} من {targets.Count}";

            // Random delay between messages (skip after last)
            if (i < targets.Count - 1 && !_cts.IsCancellationRequested)
            {
                var delaySec = rng.Next(3, 11);
                for (int s = delaySec; s > 0; s--)
                {
                    if (_cts.IsCancellationRequested) break;

                    // Pause during delay too
                    while (_paused && !_cts.IsCancellationRequested)
                    {
                        lblProgress.Text = $"⏸ متوقف مؤقتاً — {sent} من {targets.Count}";
                        await Task.Delay(300);
                    }

                    if (_cts.IsCancellationRequested) break;

                    lblProgress.Text = $"تم إرسال {sent} من {targets.Count} — الانتظار {s} ثانية";
                    await Task.Delay(1000);
                }
            }
        }

        // Reset UI
        btnSend.Enabled = true;
        btnClose.Enabled = true;
        btnPause.Visible = false;
        btnStop.Visible = false;
        progressBar.Visible = false;
        lblProgress.Text = "";

        if (stopped)
        {
            lblProgress.Text = $"تم الإيقاف — أُرسل {sent} من أصل {targets.Count}";
        }

        ShowReport(results, sent, failed, filteredOut);
    }

    private void btnPause_Click(object? sender, EventArgs e)
    {
        _paused = !_paused;
        btnPause.Text = _paused ? "استمرار" : "إيقاف مؤقت";
        btnPause.BackColor = _paused
            ? Color.FromArgb(46, 139, 87)
            : Color.FromArgb(200, 160, 40);
    }

    private void btnStop_Click(object? sender, EventArgs e)
    {
        _paused = false;
        _cts?.Cancel();
    }

    private static void ShowReport(List<WhatsAppSendResult> results, int sent, int failed, List<SendTarget> filteredOut)
    {
        using var reportForm = new SendReportForm(results, sent, failed, filteredOut);
        reportForm.ShowDialog();
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        SaveSettings();
        this.Close();
    }
}
