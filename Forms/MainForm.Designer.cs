namespace WataniFTTH.Forms;

partial class MainForm
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
        this.components = new System.ComponentModel.Container();

        this.pnlTop = new System.Windows.Forms.Panel();
        this.lblLoginInfo = new System.Windows.Forms.Label();
        this.cmbCredentials = new System.Windows.Forms.ComboBox();
        this.btnManageCredentials = new System.Windows.Forms.Button();

        this.pnlFilter = new System.Windows.Forms.Panel();
        this.lblStartDate = new System.Windows.Forms.Label();
        this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
        this.lblEndDate = new System.Windows.Forms.Label();
        this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
        this.lblStatus = new System.Windows.Forms.Label();
        this.cmbStatus = new System.Windows.Forms.ComboBox();
        this.btnLoad = new System.Windows.Forms.Button();

        this.pnlProgress = new System.Windows.Forms.Panel();
        this.progressBar = new System.Windows.Forms.ProgressBar();
        this.lblProgress = new System.Windows.Forms.Label();

        this.dgvSubscriptions = new System.Windows.Forms.DataGridView();

        this.pnlBottom = new System.Windows.Forms.Panel();
        this.btnExportExcel = new System.Windows.Forms.Button();
        this.btnWhatsApp = new System.Windows.Forms.Button();
        this.lblTotal = new System.Windows.Forms.Label();

        this.pnlTop.SuspendLayout();
        this.pnlFilter.SuspendLayout();
        this.pnlProgress.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvSubscriptions)).BeginInit();
        this.pnlBottom.SuspendLayout();
        this.SuspendLayout();

        //
        // pnlTop
        //
        this.pnlTop.Controls.Add(this.btnManageCredentials);
        this.pnlTop.Controls.Add(this.cmbCredentials);
        this.pnlTop.Controls.Add(this.lblLoginInfo);
        this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlTop.Location = new System.Drawing.Point(0, 0);
        this.pnlTop.Name = "pnlTop";
        this.pnlTop.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
        this.pnlTop.Size = new System.Drawing.Size(1100, 42);

        //
        // lblLoginInfo
        //
        this.lblLoginInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.lblLoginInfo.AutoSize = true;
        this.lblLoginInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblLoginInfo.Location = new System.Drawing.Point(975, 10);
        this.lblLoginInfo.Name = "lblLoginInfo";
        this.lblLoginInfo.Text = "بيانات الدخول:";

        //
        // cmbCredentials
        //
        this.cmbCredentials.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.cmbCredentials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbCredentials.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.cmbCredentials.Location = new System.Drawing.Point(785, 7);
        this.cmbCredentials.Name = "cmbCredentials";
        this.cmbCredentials.Size = new System.Drawing.Size(180, 28);
        this.cmbCredentials.SelectedIndexChanged += new System.EventHandler(this.cmbCredentials_SelectedIndexChanged);

        //
        // btnManageCredentials
        //
        this.btnManageCredentials.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btnManageCredentials.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
        this.btnManageCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnManageCredentials.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnManageCredentials.ForeColor = System.Drawing.Color.White;
        this.btnManageCredentials.Location = new System.Drawing.Point(615, 5);
        this.btnManageCredentials.Name = "btnManageCredentials";
        this.btnManageCredentials.Size = new System.Drawing.Size(160, 32);
        this.btnManageCredentials.Text = "إدارة بيانات الدخول";
        this.btnManageCredentials.UseVisualStyleBackColor = false;
        this.btnManageCredentials.Click += new System.EventHandler(this.btnManageCredentials_Click);

        //
        // pnlFilter
        //
        this.pnlFilter.Controls.Add(this.btnLoad);
        this.pnlFilter.Controls.Add(this.cmbStatus);
        this.pnlFilter.Controls.Add(this.lblStatus);
        this.pnlFilter.Controls.Add(this.dtpEndDate);
        this.pnlFilter.Controls.Add(this.lblEndDate);
        this.pnlFilter.Controls.Add(this.dtpStartDate);
        this.pnlFilter.Controls.Add(this.lblStartDate);
        this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlFilter.Location = new System.Drawing.Point(0, 42);
        this.pnlFilter.Name = "pnlFilter";
        this.pnlFilter.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
        this.pnlFilter.Size = new System.Drawing.Size(1100, 42);

        //
        // lblStartDate
        //
        this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.lblStartDate.AutoSize = true;
        this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblStartDate.Location = new System.Drawing.Point(1010, 12);
        this.lblStartDate.Name = "lblStartDate";
        this.lblStartDate.Text = "تاريخ البدء:";

        //
        // dtpStartDate
        //
        this.dtpStartDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dtpStartDate.Location = new System.Drawing.Point(870, 8);
        this.dtpStartDate.Name = "dtpStartDate";
        this.dtpStartDate.Size = new System.Drawing.Size(130, 27);

        //
        // lblEndDate
        //
        this.lblEndDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.lblEndDate.AutoSize = true;
        this.lblEndDate.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblEndDate.Location = new System.Drawing.Point(770, 12);
        this.lblEndDate.Name = "lblEndDate";
        this.lblEndDate.Text = "تاريخ الانتهاء:";

        //
        // dtpEndDate
        //
        this.dtpEndDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dtpEndDate.Location = new System.Drawing.Point(630, 8);
        this.dtpEndDate.Name = "dtpEndDate";
        this.dtpEndDate.Size = new System.Drawing.Size(130, 27);

        //
        // lblStatus
        //
        this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.lblStatus.AutoSize = true;
        this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblStatus.Location = new System.Drawing.Point(530, 12);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Text = "حالة الاشتراك:";

        //
        // cmbStatus
        //
        this.cmbStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.cmbStatus.Location = new System.Drawing.Point(380, 8);
        this.cmbStatus.Name = "cmbStatus";
        this.cmbStatus.Size = new System.Drawing.Size(140, 28);

        //
        // btnLoad
        //
        this.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
        this.btnLoad.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.btnLoad.ForeColor = System.Drawing.Color.White;
        this.btnLoad.Location = new System.Drawing.Point(10, 3);
        this.btnLoad.Name = "btnLoad";
        this.btnLoad.Size = new System.Drawing.Size(180, 36);
        this.btnLoad.Text = "تحميل الاشتراكات";
        this.btnLoad.UseVisualStyleBackColor = false;
        this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

        //
        // pnlProgress
        //
        this.pnlProgress.Controls.Add(this.progressBar);
        this.pnlProgress.Controls.Add(this.lblProgress);
        this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlProgress.Location = new System.Drawing.Point(0, 84);
        this.pnlProgress.Name = "pnlProgress";
        this.pnlProgress.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
        this.pnlProgress.Size = new System.Drawing.Size(1100, 30);
        this.pnlProgress.Visible = false;

        //
        // progressBar
        //
        this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
        this.progressBar.Location = new System.Drawing.Point(10, 3);
        this.progressBar.Name = "progressBar";
        this.progressBar.Size = new System.Drawing.Size(880, 24);

        //
        // lblProgress
        //
        this.lblProgress.Dock = System.Windows.Forms.DockStyle.Left;
        this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblProgress.Location = new System.Drawing.Point(10, 3);
        this.lblProgress.Name = "lblProgress";
        this.lblProgress.Size = new System.Drawing.Size(450, 24);
        this.lblProgress.Text = "";
        this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        //
        // dgvSubscriptions
        //
        this.dgvSubscriptions.AllowUserToAddRows = false;
        this.dgvSubscriptions.AllowUserToDeleteRows = false;
        this.dgvSubscriptions.AllowUserToResizeRows = false;
        this.dgvSubscriptions.BackgroundColor = System.Drawing.SystemColors.Window;
        this.dgvSubscriptions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.dgvSubscriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvSubscriptions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvSubscriptions.Location = new System.Drawing.Point(0, 84);
        this.dgvSubscriptions.MultiSelect = true;
        this.dgvSubscriptions.Name = "dgvSubscriptions";
        this.dgvSubscriptions.ReadOnly = true;
        this.dgvSubscriptions.RowHeadersVisible = false;
        this.dgvSubscriptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvSubscriptions.Size = new System.Drawing.Size(1100, 480);

        //
        // pnlBottom
        //
        this.pnlBottom.Controls.Add(this.lblTotal);
        this.pnlBottom.Controls.Add(this.btnWhatsApp);
        this.pnlBottom.Controls.Add(this.btnExportExcel);
        this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlBottom.Location = new System.Drawing.Point(0, 564);
        this.pnlBottom.Name = "pnlBottom";
        this.pnlBottom.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
        this.pnlBottom.Size = new System.Drawing.Size(1100, 46);

        //
        // btnExportExcel
        //
        this.btnExportExcel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(46, 139, 87);
        this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnExportExcel.ForeColor = System.Drawing.Color.White;
        this.btnExportExcel.Location = new System.Drawing.Point(920, 7);
        this.btnExportExcel.Name = "btnExportExcel";
        this.btnExportExcel.Size = new System.Drawing.Size(170, 32);
        this.btnExportExcel.Text = "تصدير الى ملف اكسل";
        this.btnExportExcel.UseVisualStyleBackColor = false;
        this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

        //
        // btnWhatsApp
        //
        this.btnWhatsApp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btnWhatsApp.BackColor = System.Drawing.Color.FromArgb(46, 139, 87);
        this.btnWhatsApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnWhatsApp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnWhatsApp.ForeColor = System.Drawing.Color.White;
        this.btnWhatsApp.Location = new System.Drawing.Point(710, 7);
        this.btnWhatsApp.Name = "btnWhatsApp";
        this.btnWhatsApp.Size = new System.Drawing.Size(200, 32);
        this.btnWhatsApp.Text = "إرسال رسالة على الواتساب";
        this.btnWhatsApp.UseVisualStyleBackColor = false;
        this.btnWhatsApp.Click += new System.EventHandler(this.btnWhatsApp_Click);

        //
        // lblTotal
        //
        this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
        this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblTotal.Location = new System.Drawing.Point(13, 10);
        this.lblTotal.Name = "lblTotal";
        this.lblTotal.Size = new System.Drawing.Size(160, 25);
        this.lblTotal.Text = "إجمالي: 0";
        this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        //
        // MainForm
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1100, 610);
        this.Controls.Add(this.dgvSubscriptions);
        this.Controls.Add(this.pnlProgress);
        this.Controls.Add(this.pnlFilter);
        this.Controls.Add(this.pnlTop);
        this.Controls.Add(this.pnlBottom);
        this.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.MinimumSize = new System.Drawing.Size(900, 500);
        this.Name = "MainForm";
        this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "FTTH Subscription Viewer ⚡";

        this.pnlTop.ResumeLayout(false);
        this.pnlTop.PerformLayout();
        this.pnlFilter.ResumeLayout(false);
        this.pnlFilter.PerformLayout();
        this.pnlProgress.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvSubscriptions)).EndInit();
        this.pnlBottom.ResumeLayout(false);
        this.pnlBottom.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblLoginInfo;
    private System.Windows.Forms.ComboBox cmbCredentials;
    private System.Windows.Forms.Button btnManageCredentials;

    private System.Windows.Forms.Panel pnlFilter;
    private System.Windows.Forms.Label lblStartDate;
    private System.Windows.Forms.DateTimePicker dtpStartDate;
    private System.Windows.Forms.Label lblEndDate;
    private System.Windows.Forms.DateTimePicker dtpEndDate;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ComboBox cmbStatus;
    private System.Windows.Forms.Button btnLoad;

    private System.Windows.Forms.Panel pnlProgress;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label lblProgress;

    private System.Windows.Forms.DataGridView dgvSubscriptions;

    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Button btnExportExcel;
    private System.Windows.Forms.Button btnWhatsApp;
    private System.Windows.Forms.Label lblTotal;
}
