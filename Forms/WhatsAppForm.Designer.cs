namespace WataniFTTH.Forms;

partial class WhatsAppForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        lblApiUrl = new Label();
        txtApiUrl = new TextBox();
        lblApiHint = new Label();
        tabControl = new TabControl();
        tabActive = new TabPage();
        rtbActiveTemplate = new RichTextBox();
        tabExpired = new TabPage();
        rtbExpiredTemplate = new RichTextBox();
        chkSendAll = new CheckBox();
        btnSend = new Button();
        btnPause = new Button();
        btnStop = new Button();
        btnClose = new Button();
        progressBar = new ProgressBar();
        lblProgress = new Label();
        chkLocation = new CheckBox();
        lblCoords = new Label();
        txtCoords = new TextBox();

        // Filter wrapper
        grpFilter = new GroupBox();
        tabFilter = new TabControl();

        // Allowed tab controls
        tabAllowed = new TabPage();
        chkFilterEnabled = new CheckBox();
        lstSuffixes = new ListBox();
        txtNewSuffix = new TextBox();
        btnAddSuffix = new Button();
        btnRemoveSuffix = new Button();
        btnResetSuffixes = new Button();
        lblFilterInfo = new Label();

        // Blocked tab controls
        tabBlocked = new TabPage();
        chkBlockEnabled = new CheckBox();
        lstBlocked = new ListBox();
        txtNewBlocked = new TextBox();
        btnAddBlocked = new Button();
        btnRemoveBlocked = new Button();
        btnClearBlocked = new Button();
        lblBlockInfo = new Label();

        tabControl.SuspendLayout();
        tabActive.SuspendLayout();
        tabExpired.SuspendLayout();
        grpFilter.SuspendLayout();
        tabFilter.SuspendLayout();
        tabAllowed.SuspendLayout();
        tabBlocked.SuspendLayout();
        SuspendLayout();
        // 
        // lblApiUrl
        // 
        lblApiUrl.AutoSize = true;
        lblApiUrl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblApiUrl.Location = new Point(13, 8);
        lblApiUrl.Name = "lblApiUrl";
        lblApiUrl.Size = new Size(84, 15);
        lblApiUrl.TabIndex = 14;
        lblApiUrl.Text = "قالب عنوان API";
        // 
        // txtApiUrl
        // 
        txtApiUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtApiUrl.Font = new Font("Segoe UI", 10F);
        txtApiUrl.Location = new Point(13, 22);
        txtApiUrl.Margin = new Padding(3, 2, 3, 2);
        txtApiUrl.Name = "txtApiUrl";
        txtApiUrl.Size = new Size(613, 25);
        txtApiUrl.TabIndex = 13;
        // 
        // lblApiHint
        // 
        lblApiHint.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblApiHint.Font = new Font("Segoe UI", 8F);
        lblApiHint.ForeColor = Color.Gray;
        lblApiHint.Location = new Point(13, 46);
        lblApiHint.Name = "lblApiHint";
        lblApiHint.Size = new Size(612, 14);
        lblApiHint.TabIndex = 12;
        lblApiHint.Text = "المتغيرات المتاحة: {phone} = رقم الهاتف  |  {message} = نص الرسالة";
        // 
        // tabControl
        // 
        tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tabControl.Controls.Add(tabActive);
        tabControl.Controls.Add(tabExpired);
        tabControl.Font = new Font("Segoe UI", 10F);
        tabControl.Location = new Point(13, 90);
        tabControl.Margin = new Padding(3, 2, 3, 2);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(612, 165);
        tabControl.TabIndex = 8;
        // 
        // tabActive
        // 
        tabActive.Controls.Add(rtbActiveTemplate);
        tabActive.Location = new Point(4, 26);
        tabActive.Margin = new Padding(3, 2, 3, 2);
        tabActive.Name = "tabActive";
        tabActive.Padding = new Padding(4);
        tabActive.Size = new Size(604, 135);
        tabActive.TabIndex = 0;
        tabActive.Text = "رسالة للمشتركين النشطين";
        // 
        // rtbActiveTemplate
        // 
        rtbActiveTemplate.Dock = DockStyle.Fill;
        rtbActiveTemplate.Font = new Font("Segoe UI", 10F);
        rtbActiveTemplate.Location = new Point(4, 4);
        rtbActiveTemplate.Margin = new Padding(3, 2, 3, 2);
        rtbActiveTemplate.Name = "rtbActiveTemplate";
        rtbActiveTemplate.Size = new Size(596, 127);
        rtbActiveTemplate.TabIndex = 0;
        rtbActiveTemplate.Text = "";
        // 
        // tabExpired
        // 
        tabExpired.Controls.Add(rtbExpiredTemplate);
        tabExpired.Location = new Point(4, 26);
        tabExpired.Margin = new Padding(3, 2, 3, 2);
        tabExpired.Name = "tabExpired";
        tabExpired.Padding = new Padding(4);
        tabExpired.Size = new Size(604, 135);
        tabExpired.TabIndex = 1;
        tabExpired.Text = "رسالة للمنتهية صلاحيتهم";
        // 
        // rtbExpiredTemplate
        // 
        rtbExpiredTemplate.Dock = DockStyle.Fill;
        rtbExpiredTemplate.Font = new Font("Segoe UI", 10F);
        rtbExpiredTemplate.Location = new Point(4, 4);
        rtbExpiredTemplate.Margin = new Padding(3, 2, 3, 2);
        rtbExpiredTemplate.Name = "rtbExpiredTemplate";
        rtbExpiredTemplate.Size = new Size(596, 127);
        rtbExpiredTemplate.TabIndex = 0;
        rtbExpiredTemplate.Text = "";
        // 
        // chkLocation
        // 
        chkLocation.AutoSize = true;
        chkLocation.Checked = true;
        chkLocation.CheckState = CheckState.Checked;
        chkLocation.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        chkLocation.Location = new Point(13, 64);
        chkLocation.Margin = new Padding(3, 2, 3, 2);
        chkLocation.Name = "chkLocation";
        chkLocation.Size = new Size(90, 19);
        chkLocation.TabIndex = 11;
        chkLocation.Text = "إرسال الموقع";
        chkLocation.CheckedChanged += chkLocation_CheckedChanged;
        // 
        // lblCoords
        // 
        lblCoords.AutoSize = true;
        lblCoords.Font = new Font("Segoe UI", 9F);
        lblCoords.Location = new Point(516, 65);
        lblCoords.Name = "lblCoords";
        lblCoords.Size = new Size(58, 15);
        lblCoords.TabIndex = 10;
        lblCoords.Text = "الإحداثيات:";
        // 
        // txtCoords
        // 
        txtCoords.Font = new Font("Segoe UI", 9F);
        txtCoords.Location = new Point(131, 63);
        txtCoords.Margin = new Padding(3, 2, 3, 2);
        txtCoords.Name = "txtCoords";
        txtCoords.RightToLeft = RightToLeft.No;
        txtCoords.Size = new Size(377, 23);
        txtCoords.TabIndex = 9;

        // ═══════════════════════════════════════
        //  Filter GroupBox with inner TabControl
        // ═══════════════════════════════════════

        // 
        // grpFilter
        // 
        grpFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpFilter.Controls.Add(tabFilter);
        grpFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpFilter.ForeColor = Color.FromArgb(70, 130, 180);
        grpFilter.Location = new Point(13, 261);
        grpFilter.Margin = new Padding(3, 2, 3, 2);
        grpFilter.Name = "grpFilter";
        grpFilter.Padding = new Padding(3, 2, 3, 2);
        grpFilter.Size = new Size(612, 160);
        grpFilter.TabIndex = 7;
        grpFilter.TabStop = false;
        grpFilter.Text = "فلاتر اسم المستخدم للجهاز (اللاحقة بعد آخر -)";
        // 
        // tabFilter
        // 
        tabFilter.Controls.Add(tabAllowed);
        tabFilter.Controls.Add(tabBlocked);
        tabFilter.Dock = DockStyle.Fill;
        tabFilter.Font = new Font("Segoe UI", 9F);
        tabFilter.ForeColor = SystemColors.ControlText;
        tabFilter.Location = new Point(3, 17);
        tabFilter.Margin = new Padding(0);
        tabFilter.Name = "tabFilter";
        tabFilter.SelectedIndex = 0;
        tabFilter.Size = new Size(606, 141);
        tabFilter.TabIndex = 0;

        // ── Allowed Tab ──
        // 
        // tabAllowed
        // 
        tabAllowed.Controls.Add(chkFilterEnabled);
        tabAllowed.Controls.Add(lstSuffixes);
        tabAllowed.Controls.Add(txtNewSuffix);
        tabAllowed.Controls.Add(btnAddSuffix);
        tabAllowed.Controls.Add(btnRemoveSuffix);
        tabAllowed.Controls.Add(btnResetSuffixes);
        tabAllowed.Controls.Add(lblFilterInfo);
        tabAllowed.Location = new Point(4, 24);
        tabAllowed.Margin = new Padding(3, 2, 3, 2);
        tabAllowed.Name = "tabAllowed";
        tabAllowed.Padding = new Padding(4);
        tabAllowed.Size = new Size(598, 113);
        tabAllowed.TabIndex = 0;
        tabAllowed.Text = "✅ لاحقات مسموحة (القائمة البيضاء)";
        // 
        // chkFilterEnabled
        // 
        chkFilterEnabled.AutoSize = true;
        chkFilterEnabled.Checked = false;
        chkFilterEnabled.Font = new Font("Segoe UI", 9F);
        chkFilterEnabled.ForeColor = SystemColors.ControlText;
        chkFilterEnabled.Location = new Point(468, 6);
        chkFilterEnabled.Margin = new Padding(3, 2, 3, 2);
        chkFilterEnabled.Name = "chkFilterEnabled";
        chkFilterEnabled.Size = new Size(84, 19);
        chkFilterEnabled.TabIndex = 0;
        chkFilterEnabled.Text = "تفعيل الفلتر";
        chkFilterEnabled.CheckedChanged += chkFilterEnabled_CheckedChanged;
        // 
        // lstSuffixes
        // 
        lstSuffixes.Font = new Font("Consolas", 10F);
        lstSuffixes.ForeColor = SystemColors.ControlText;
        lstSuffixes.ItemHeight = 15;
        lstSuffixes.Location = new Point(294, 6);
        lstSuffixes.Margin = new Padding(3, 2, 3, 2);
        lstSuffixes.Name = "lstSuffixes";
        lstSuffixes.RightToLeft = RightToLeft.No;
        lstSuffixes.Size = new Size(167, 94);
        lstSuffixes.TabIndex = 1;
        // 
        // txtNewSuffix
        // 
        txtNewSuffix.Font = new Font("Consolas", 10F);
        txtNewSuffix.ForeColor = SystemColors.ControlText;
        txtNewSuffix.Location = new Point(101, 6);
        txtNewSuffix.Margin = new Padding(3, 2, 3, 2);
        txtNewSuffix.Name = "txtNewSuffix";
        txtNewSuffix.PlaceholderText = "اسم اللاحقة";
        txtNewSuffix.RightToLeft = RightToLeft.No;
        txtNewSuffix.Size = new Size(132, 23);
        txtNewSuffix.TabIndex = 2;
        txtNewSuffix.KeyDown += txtNewSuffix_KeyDown;
        // 
        // btnAddSuffix
        // 
        btnAddSuffix.BackColor = Color.FromArgb(46, 139, 87);
        btnAddSuffix.FlatStyle = FlatStyle.Flat;
        btnAddSuffix.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnAddSuffix.ForeColor = Color.White;
        btnAddSuffix.Location = new Point(9, 6);
        btnAddSuffix.Margin = new Padding(3, 2, 3, 2);
        btnAddSuffix.Name = "btnAddSuffix";
        btnAddSuffix.Size = new Size(83, 23);
        btnAddSuffix.TabIndex = 3;
        btnAddSuffix.Text = "إضافة";
        btnAddSuffix.UseVisualStyleBackColor = false;
        btnAddSuffix.Click += btnAddSuffix_Click;
        // 
        // btnRemoveSuffix
        // 
        btnRemoveSuffix.BackColor = Color.FromArgb(200, 50, 50);
        btnRemoveSuffix.FlatStyle = FlatStyle.Flat;
        btnRemoveSuffix.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRemoveSuffix.ForeColor = Color.White;
        btnRemoveSuffix.Location = new Point(9, 37);
        btnRemoveSuffix.Margin = new Padding(3, 2, 3, 2);
        btnRemoveSuffix.Name = "btnRemoveSuffix";
        btnRemoveSuffix.Size = new Size(83, 25);
        btnRemoveSuffix.TabIndex = 4;
        btnRemoveSuffix.Text = "حذف المحدد";
        btnRemoveSuffix.UseVisualStyleBackColor = false;
        btnRemoveSuffix.Click += btnRemoveSuffix_Click;
        // 
        // btnResetSuffixes
        // 
        btnResetSuffixes.BackColor = Color.FromArgb(100, 100, 100);
        btnResetSuffixes.FlatStyle = FlatStyle.Flat;
        btnResetSuffixes.Font = new Font("Segoe UI", 9F);
        btnResetSuffixes.ForeColor = Color.White;
        btnResetSuffixes.Location = new Point(9, 66);
        btnResetSuffixes.Margin = new Padding(3, 2, 3, 2);
        btnResetSuffixes.Name = "btnResetSuffixes";
        btnResetSuffixes.Size = new Size(83, 26);
        btnResetSuffixes.TabIndex = 5;
        btnResetSuffixes.Text = "إعادة تعيين";
        btnResetSuffixes.UseVisualStyleBackColor = false;
        btnResetSuffixes.Click += btnResetSuffixes_Click;
        // 
        // lblFilterInfo
        // 
        lblFilterInfo.Font = new Font("Segoe UI", 8F);
        lblFilterInfo.ForeColor = Color.Gray;
        lblFilterInfo.Location = new Point(101, 32);
        lblFilterInfo.Name = "lblFilterInfo";
        lblFilterInfo.Size = new Size(184, 64);
        lblFilterInfo.TabIndex = 6;
        lblFilterInfo.Text = "إرسال فقط للأجهزة التي لاحقتها (بعد آخر -) موجودة في هذه القائمة";

        // ── Blocked Tab ──
        // 
        // tabBlocked
        // 
        tabBlocked.Controls.Add(chkBlockEnabled);
        tabBlocked.Controls.Add(lstBlocked);
        tabBlocked.Controls.Add(txtNewBlocked);
        tabBlocked.Controls.Add(btnAddBlocked);
        tabBlocked.Controls.Add(btnRemoveBlocked);
        tabBlocked.Controls.Add(btnClearBlocked);
        tabBlocked.Controls.Add(lblBlockInfo);
        tabBlocked.Location = new Point(4, 24);
        tabBlocked.Margin = new Padding(3, 2, 3, 2);
        tabBlocked.Name = "tabBlocked";
        tabBlocked.Padding = new Padding(4);
        tabBlocked.Size = new Size(598, 113);
        tabBlocked.TabIndex = 1;
        tabBlocked.Text = "⛔ لاحقات محظورة (القائمة السوداء)";
        // 
        // chkBlockEnabled
        // 
        chkBlockEnabled.AutoSize = true;
        chkBlockEnabled.Checked = true;
        chkBlockEnabled.CheckState = CheckState.Checked;
        chkBlockEnabled.Font = new Font("Segoe UI", 9F);
        chkBlockEnabled.ForeColor = SystemColors.ControlText;
        chkBlockEnabled.Location = new Point(468, 6);
        chkBlockEnabled.Margin = new Padding(3, 2, 3, 2);
        chkBlockEnabled.Name = "chkBlockEnabled";
        chkBlockEnabled.Size = new Size(84, 19);
        chkBlockEnabled.TabIndex = 0;
        chkBlockEnabled.Text = "تفعيل الفلتر";
        chkBlockEnabled.CheckedChanged += chkBlockEnabled_CheckedChanged;
        // 
        // lstBlocked
        // 
        lstBlocked.Font = new Font("Consolas", 10F);
        lstBlocked.ForeColor = SystemColors.ControlText;
        lstBlocked.ItemHeight = 15;
        lstBlocked.Location = new Point(294, 6);
        lstBlocked.Margin = new Padding(3, 2, 3, 2);
        lstBlocked.Name = "lstBlocked";
        lstBlocked.RightToLeft = RightToLeft.No;
        lstBlocked.Size = new Size(167, 94);
        lstBlocked.TabIndex = 1;
        // 
        // txtNewBlocked
        // 
        txtNewBlocked.Font = new Font("Consolas", 10F);
        txtNewBlocked.ForeColor = SystemColors.ControlText;
        txtNewBlocked.Location = new Point(101, 6);
        txtNewBlocked.Margin = new Padding(3, 2, 3, 2);
        txtNewBlocked.Name = "txtNewBlocked";
        txtNewBlocked.PlaceholderText = "اسم اللاحقة";
        txtNewBlocked.RightToLeft = RightToLeft.No;
        txtNewBlocked.Size = new Size(132, 23);
        txtNewBlocked.TabIndex = 2;
        txtNewBlocked.KeyDown += txtNewBlocked_KeyDown;
        // 
        // btnAddBlocked
        // 
        btnAddBlocked.BackColor = Color.FromArgb(46, 139, 87);
        btnAddBlocked.FlatStyle = FlatStyle.Flat;
        btnAddBlocked.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnAddBlocked.ForeColor = Color.White;
        btnAddBlocked.Location = new Point(9, 6);
        btnAddBlocked.Margin = new Padding(3, 2, 3, 2);
        btnAddBlocked.Name = "btnAddBlocked";
        btnAddBlocked.Size = new Size(83, 23);
        btnAddBlocked.TabIndex = 3;
        btnAddBlocked.Text = "إضافة";
        btnAddBlocked.UseVisualStyleBackColor = false;
        btnAddBlocked.Click += btnAddBlocked_Click;
        // 
        // btnRemoveBlocked
        // 
        btnRemoveBlocked.BackColor = Color.FromArgb(200, 50, 50);
        btnRemoveBlocked.FlatStyle = FlatStyle.Flat;
        btnRemoveBlocked.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRemoveBlocked.ForeColor = Color.White;
        btnRemoveBlocked.Location = new Point(9, 37);
        btnRemoveBlocked.Margin = new Padding(3, 2, 3, 2);
        btnRemoveBlocked.Name = "btnRemoveBlocked";
        btnRemoveBlocked.Size = new Size(83, 25);
        btnRemoveBlocked.TabIndex = 4;
        btnRemoveBlocked.Text = "حذف المحدد";
        btnRemoveBlocked.UseVisualStyleBackColor = false;
        btnRemoveBlocked.Click += btnRemoveBlocked_Click;
        // 
        // btnClearBlocked
        // 
        btnClearBlocked.BackColor = Color.FromArgb(100, 100, 100);
        btnClearBlocked.FlatStyle = FlatStyle.Flat;
        btnClearBlocked.Font = new Font("Segoe UI", 9F);
        btnClearBlocked.ForeColor = Color.White;
        btnClearBlocked.Location = new Point(9, 66);
        btnClearBlocked.Margin = new Padding(3, 2, 3, 2);
        btnClearBlocked.Name = "btnClearBlocked";
        btnClearBlocked.Size = new Size(83, 26);
        btnClearBlocked.TabIndex = 5;
        btnClearBlocked.Text = "إعادة تعيين";
        btnClearBlocked.UseVisualStyleBackColor = false;
        btnClearBlocked.Click += btnClearBlocked_Click;
        // 
        // lblBlockInfo
        // 
        lblBlockInfo.Font = new Font("Segoe UI", 8F);
        lblBlockInfo.ForeColor = Color.Gray;
        lblBlockInfo.Location = new Point(101, 32);
        lblBlockInfo.Name = "lblBlockInfo";
        lblBlockInfo.Size = new Size(184, 64);
        lblBlockInfo.TabIndex = 6;
        lblBlockInfo.Text = "إرسال للجميع ما عدا الأجهزة التي لاحقتها (بعد آخر -) موجودة في هذه القائمة";

        // ═══════════════════════════════════════
        //  Bottom controls
        // ═══════════════════════════════════════

        // 
        // progressBar
        // 
        progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        progressBar.Location = new Point(13, 432);
        progressBar.Margin = new Padding(3, 2, 3, 2);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(438, 17);
        progressBar.TabIndex = 6;
        progressBar.Visible = false;
        // 
        // lblProgress
        // 
        lblProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lblProgress.Font = new Font("Segoe UI", 9F);
        lblProgress.Location = new Point(455, 432);
        lblProgress.Name = "lblProgress";
        lblProgress.Size = new Size(171, 17);
        lblProgress.TabIndex = 5;
        lblProgress.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // chkSendAll
        // 
        chkSendAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        chkSendAll.AutoSize = true;
        chkSendAll.Font = new Font("Segoe UI", 10F);
        chkSendAll.Location = new Point(457, 452);
        chkSendAll.Margin = new Padding(3, 2, 3, 2);
        chkSendAll.Name = "chkSendAll";
        chkSendAll.Size = new Size(129, 23);
        chkSendAll.TabIndex = 4;
        chkSendAll.Text = "إرسال إلى الجميع";
        // 
        // btnSend
        // 
        btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSend.BackColor = Color.FromArgb(46, 139, 87);
        btnSend.FlatStyle = FlatStyle.Flat;
        btnSend.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSend.ForeColor = Color.White;
        btnSend.Location = new Point(101, 481);
        btnSend.Margin = new Padding(3, 2, 3, 2);
        btnSend.Name = "btnSend";
        btnSend.Size = new Size(105, 28);
        btnSend.TabIndex = 3;
        btnSend.Text = "إرسال";
        btnSend.UseVisualStyleBackColor = false;
        btnSend.Click += btnSend_Click;
        // 
        // btnPause
        // 
        btnPause.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnPause.BackColor = Color.FromArgb(200, 160, 40);
        btnPause.FlatStyle = FlatStyle.Flat;
        btnPause.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnPause.ForeColor = Color.White;
        btnPause.Location = new Point(214, 481);
        btnPause.Margin = new Padding(3, 2, 3, 2);
        btnPause.Name = "btnPause";
        btnPause.Size = new Size(105, 28);
        btnPause.TabIndex = 2;
        btnPause.Text = "إيقاف مؤقت";
        btnPause.UseVisualStyleBackColor = false;
        btnPause.Visible = false;
        btnPause.Click += btnPause_Click;
        // 
        // btnStop
        // 
        btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnStop.BackColor = Color.FromArgb(200, 50, 50);
        btnStop.FlatStyle = FlatStyle.Flat;
        btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStop.ForeColor = Color.White;
        btnStop.Location = new Point(328, 481);
        btnStop.Margin = new Padding(3, 2, 3, 2);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(79, 28);
        btnStop.TabIndex = 1;
        btnStop.Text = "إيقاف";
        btnStop.UseVisualStyleBackColor = false;
        btnStop.Visible = false;
        btnStop.Click += btnStop_Click;
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClose.FlatStyle = FlatStyle.Flat;
        btnClose.Font = new Font("Segoe UI", 10F);
        btnClose.Location = new Point(13, 481);
        btnClose.Margin = new Padding(3, 2, 3, 2);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(79, 28);
        btnClose.TabIndex = 0;
        btnClose.Text = "إغلاق";
        btnClose.Click += btnClose_Click;
        // 
        // WhatsAppForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(639, 522);
        Controls.Add(btnClose);
        Controls.Add(btnStop);
        Controls.Add(btnPause);
        Controls.Add(btnSend);
        Controls.Add(chkSendAll);
        Controls.Add(lblProgress);
        Controls.Add(progressBar);
        Controls.Add(grpFilter);
        Controls.Add(tabControl);
        Controls.Add(txtCoords);
        Controls.Add(lblCoords);
        Controls.Add(chkLocation);
        Controls.Add(lblApiHint);
        Controls.Add(txtApiUrl);
        Controls.Add(lblApiUrl);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Margin = new Padding(3, 2, 3, 2);
        MaximizeBox = false;
        MinimizeBox = true;
        Name = "WhatsAppForm";
        RightToLeft = RightToLeft.Yes;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "إرسال رسالة على الواتساب";
        tabControl.ResumeLayout(false);
        tabActive.ResumeLayout(false);
        tabExpired.ResumeLayout(false);
        tabBlocked.ResumeLayout(false);
        tabBlocked.PerformLayout();
        tabAllowed.ResumeLayout(false);
        tabAllowed.PerformLayout();
        tabFilter.ResumeLayout(false);
        grpFilter.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblApiUrl;
    private TextBox txtApiUrl;
    private Label lblApiHint;
    private TabControl tabControl;
    private TabPage tabActive;
    private TabPage tabExpired;
    private RichTextBox rtbActiveTemplate;
    private RichTextBox rtbExpiredTemplate;
    private CheckBox chkSendAll;
    private Button btnSend;
    private Button btnPause;
    private Button btnStop;
    private Button btnClose;
    private ProgressBar progressBar;
    private Label lblProgress;
    private CheckBox chkLocation;
    private Label lblCoords;
    private TextBox txtCoords;

    // Filter wrapper
    private GroupBox grpFilter;
    private TabControl tabFilter;

    // Allowed (whitelist) tab
    private TabPage tabAllowed;
    private CheckBox chkFilterEnabled;
    private ListBox lstSuffixes;
    private TextBox txtNewSuffix;
    private Button btnAddSuffix;
    private Button btnRemoveSuffix;
    private Button btnResetSuffixes;
    private Label lblFilterInfo;

    // Blocked (blacklist) tab
    private TabPage tabBlocked;
    private CheckBox chkBlockEnabled;
    private ListBox lstBlocked;
    private TextBox txtNewBlocked;
    private Button btnAddBlocked;
    private Button btnRemoveBlocked;
    private Button btnClearBlocked;
    private Label lblBlockInfo;
}
