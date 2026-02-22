using WataniFTTH.Services;

namespace WataniFTTH.Forms;

public class SendReportForm : Form
{
    private readonly List<WhatsAppSendResult> _results;
    private readonly int _sent;
    private readonly int _failed;
    private readonly List<SendTarget> _filteredOut;

    public SendReportForm(List<WhatsAppSendResult> results, int sent, int failed, List<SendTarget> filteredOut)
    {
        _results = results;
        _sent = sent;
        _failed = failed;
        _filteredOut = filteredOut;
        BuildUI();
    }

    private void BuildUI()
    {
        Text = "تقرير الإرسال";
        Size = new Size(600, 550);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        RightToLeft = RightToLeft.Yes;
        Font = new Font("Segoe UI", 10F);

        // Summary panel
        var pnlSummary = new Panel
        {
            Dock = DockStyle.Top,
            Height = _filteredOut.Count > 0 ? 78 : 55,
            Padding = new Padding(12, 8, 12, 8)
        };

        var lblSent = new Label
        {
            Text = $"✅ تم الإرسال: {_sent}",
            ForeColor = Color.FromArgb(46, 139, 87),
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(12, 10)
        };

        var lblFailed = new Label
        {
            Text = $"❌ فشل الإرسال: {_failed}",
            ForeColor = _failed > 0 ? Color.FromArgb(200, 50, 50) : Color.Gray,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(12, 32)
        };

        pnlSummary.Controls.Add(lblSent);
        pnlSummary.Controls.Add(lblFailed);

        if (_filteredOut.Count > 0)
        {
            var lblFiltered = new Label
            {
                Text = $"⛔ تم استبعادهم بالفلتر: {_filteredOut.Count}",
                ForeColor = Color.FromArgb(180, 120, 0),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(12, 54)
            };
            pnlSummary.Controls.Add(lblFiltered);
        }

        // TabControl to separate failed and filtered-out views
        var tabResults = new TabControl
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 9.5F)
        };

        // ── Tab 1: Failed results ──
        var tabFailed = new TabPage
        {
            Text = $"فشل الإرسال ({_results.Count(r => !r.Success)})",
            Padding = new Padding(4)
        };

        var dgvFailed = CreateDataGridView();
        dgvFailed.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colName",
            HeaderText = "اسم المشترك",
            FillWeight = 40
        });
        dgvFailed.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPhone",
            HeaderText = "رقم الهاتف",
            FillWeight = 30
        });
        dgvFailed.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colError",
            HeaderText = "سبب الفشل",
            FillWeight = 30
        });

        var failedResults = _results.Where(r => !r.Success).ToList();
        foreach (var r in failedResults)
        {
            dgvFailed.Rows.Add(r.CustomerName, r.Phone, r.ErrorMessage);
        }

        tabFailed.Controls.Add(dgvFailed);

        // ── Tab 2: Filtered-out targets ──
        var tabFiltered = new TabPage
        {
            Text = $"مستبعدون بالفلتر ({_filteredOut.Count})",
            Padding = new Padding(4)
        };

        var dgvFiltered = CreateDataGridView();
        dgvFiltered.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colName",
            HeaderText = "اسم المشترك",
            FillWeight = 35
        });
        dgvFiltered.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPhone",
            HeaderText = "رقم الهاتف",
            FillWeight = 25
        });
        dgvFiltered.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colUsername",
            HeaderText = "اسم المستخدم",
            FillWeight = 25
        });
        dgvFiltered.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSuffix",
            HeaderText = "اللاحقة",
            FillWeight = 15
        });

        foreach (var t in _filteredOut)
        {
            var suffix = GetSuffixAfterLastDash(t.DeviceUsername);
            dgvFiltered.Rows.Add(t.CustomerName, t.Phone, t.DeviceUsername, suffix);
        }

        tabFiltered.Controls.Add(dgvFiltered);

        tabResults.TabPages.Add(tabFailed);
        tabResults.TabPages.Add(tabFiltered);

        // If there are filtered-out items but no failures, default to the filtered tab
        if (failedResults.Count == 0 && _filteredOut.Count > 0)
            tabResults.SelectedIndex = 1;

        // Bottom panel with buttons
        var pnlBottom = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 50,
            Padding = new Padding(12, 8, 12, 8)
        };

        var btnCopy = new Button
        {
            Text = "نسخ الفاشلين",
            Size = new Size(130, 34),
            Location = new Point(12, 8),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(52, 120, 198),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            Enabled = failedResults.Count > 0
        };
        btnCopy.Click += (_, _) =>
        {
            var lines = new List<string> { "اسم المشترك\tرقم الهاتف\tسبب الفشل" };
            foreach (var r in failedResults)
            {
                lines.Add($"{r.CustomerName}\t{r.Phone}\t{r.ErrorMessage}");
            }
            Clipboard.SetText(string.Join(Environment.NewLine, lines));
            MessageBox.Show($"تم نسخ {failedResults.Count} سجل إلى الحافظة", "تم النسخ",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        var btnCopyFiltered = new Button
        {
            Text = "نسخ المستبعدين",
            Size = new Size(140, 34),
            Location = new Point(148, 8),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(180, 120, 0),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            Enabled = _filteredOut.Count > 0
        };
        btnCopyFiltered.Click += (_, _) =>
        {
            var lines = new List<string> { "اسم المشترك\tرقم الهاتف\tاسم المستخدم\tاللاحقة" };
            foreach (var t in _filteredOut)
            {
                var suffix = GetSuffixAfterLastDash(t.DeviceUsername);
                lines.Add($"{t.CustomerName}\t{t.Phone}\t{t.DeviceUsername}\t{suffix}");
            }
            Clipboard.SetText(string.Join(Environment.NewLine, lines));
            MessageBox.Show($"تم نسخ {_filteredOut.Count} سجل إلى الحافظة", "تم النسخ",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        var btnClose = new Button
        {
            Text = "إغلاق",
            Size = new Size(90, 34),
            Location = new Point(294, 8),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9.5F)
        };
        btnClose.Click += (_, _) => Close();

        pnlBottom.Controls.Add(btnCopy);
        pnlBottom.Controls.Add(btnCopyFiltered);
        pnlBottom.Controls.Add(btnClose);

        Controls.Add(tabResults);
        Controls.Add(pnlSummary);
        Controls.Add(pnlBottom);
    }

    private static DataGridView CreateDataGridView()
    {
        var dgv = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = true,
            RowHeadersVisible = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 9.5F),
            RightToLeft = RightToLeft.Yes
        };

        dgv.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv.ColumnHeadersHeight = 35;
        dgv.RowTemplate.Height = 30;
        dgv.EnableHeadersVisualStyles = false;

        return dgv;
    }

    private static string GetSuffixAfterLastDash(string username)
    {
        if (string.IsNullOrEmpty(username))
            return string.Empty;

        var lastDash = username.LastIndexOf('-');
        return lastDash >= 0 && lastDash < username.Length - 1
            ? username[(lastDash + 1)..]
            : username;
    }
}
