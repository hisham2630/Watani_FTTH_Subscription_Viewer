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
        _settingsManager.Save();
    }

    private void chkLocation_CheckedChanged(object? sender, EventArgs e)
    {
        ToggleLocationFields(chkLocation.Checked);
    }

    private void ToggleLocationFields(bool enabled)
    {
        txtCoords.Enabled = enabled;
        lblCoords.Enabled = enabled;
    }

    private async void btnSend_Click(object? sender, EventArgs e)
    {
        SaveSettings();

        var targets = chkSendAll.Checked ? _allTargets : _selectedTargets;

        if (targets.Count == 0)
        {
            MessageBox.Show("لا يوجد مشتركين مختارين للإرسال", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        ShowReport(results, sent, failed);
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

    private static void ShowReport(List<WhatsAppSendResult> results, int sent, int failed)
    {
        using var reportForm = new SendReportForm(results, sent, failed);
        reportForm.ShowDialog();
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        SaveSettings();
        this.Close();
    }
}
