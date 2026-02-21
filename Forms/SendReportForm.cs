using WataniFTTH.Services;

namespace WataniFTTH.Forms;

public class SendReportForm : Form
{
    private readonly List<WhatsAppSendResult> _results;
    private readonly int _sent;
    private readonly int _failed;

    public SendReportForm(List<WhatsAppSendResult> results, int sent, int failed)
    {
        _results = results;
        _sent = sent;
        _failed = failed;
        BuildUI();
    }

    private void BuildUI()
    {
        Text = "تقرير الإرسال";
        Size = new Size(600, 450);
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
            Height = 55,
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

        // DataGridView for failed results
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

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colName",
            HeaderText = "اسم المشترك",
            FillWeight = 40
        });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPhone",
            HeaderText = "رقم الهاتف",
            FillWeight = 30
        });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colError",
            HeaderText = "سبب الفشل",
            FillWeight = 30
        });

        var failedResults = _results.Where(r => !r.Success).ToList();
        foreach (var r in failedResults)
        {
            dgv.Rows.Add(r.CustomerName, r.Phone, r.ErrorMessage);
        }

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

        var btnClose = new Button
        {
            Text = "إغلاق",
            Size = new Size(90, 34),
            Location = new Point(148, 8),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9.5F)
        };
        btnClose.Click += (_, _) => Close();

        pnlBottom.Controls.Add(btnCopy);
        pnlBottom.Controls.Add(btnClose);

        Controls.Add(dgv);
        Controls.Add(pnlSummary);
        Controls.Add(pnlBottom);
    }
}
