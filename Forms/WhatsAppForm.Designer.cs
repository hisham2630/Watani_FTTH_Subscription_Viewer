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
        this.components = new System.ComponentModel.Container();

        this.lblApiUrl = new System.Windows.Forms.Label();
        this.txtApiUrl = new System.Windows.Forms.TextBox();
        this.lblApiHint = new System.Windows.Forms.Label();
        this.tabControl = new System.Windows.Forms.TabControl();
        this.tabActive = new System.Windows.Forms.TabPage();
        this.tabExpired = new System.Windows.Forms.TabPage();
        this.rtbActiveTemplate = new System.Windows.Forms.RichTextBox();
        this.rtbExpiredTemplate = new System.Windows.Forms.RichTextBox();
        this.chkSendAll = new System.Windows.Forms.CheckBox();
        this.btnSend = new System.Windows.Forms.Button();
        this.btnPause = new System.Windows.Forms.Button();
        this.btnStop = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.progressBar = new System.Windows.Forms.ProgressBar();
        this.lblProgress = new System.Windows.Forms.Label();
        this.chkLocation = new System.Windows.Forms.CheckBox();
        this.lblCoords = new System.Windows.Forms.Label();
        this.txtCoords = new System.Windows.Forms.TextBox();

        this.tabControl.SuspendLayout();
        this.tabActive.SuspendLayout();
        this.tabExpired.SuspendLayout();
        this.SuspendLayout();

        //
        // lblApiUrl
        //
        this.lblApiUrl.AutoSize = true;
        this.lblApiUrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.lblApiUrl.Location = new System.Drawing.Point(15, 10);
        this.lblApiUrl.Name = "lblApiUrl";
        this.lblApiUrl.Text = "قالب عنوان API";

        //
        // txtApiUrl
        //
        this.txtApiUrl.Anchor = System.Windows.Forms.AnchorStyles.Top
            | System.Windows.Forms.AnchorStyles.Left
            | System.Windows.Forms.AnchorStyles.Right;
        this.txtApiUrl.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.txtApiUrl.Location = new System.Drawing.Point(15, 30);
        this.txtApiUrl.Name = "txtApiUrl";
        this.txtApiUrl.Size = new System.Drawing.Size(700, 30);

        //
        // lblApiHint
        //
        this.lblApiHint.Anchor = System.Windows.Forms.AnchorStyles.Top
            | System.Windows.Forms.AnchorStyles.Left
            | System.Windows.Forms.AnchorStyles.Right;
        this.lblApiHint.Font = new System.Drawing.Font("Segoe UI", 8F);
        this.lblApiHint.ForeColor = System.Drawing.Color.Gray;
        this.lblApiHint.Location = new System.Drawing.Point(15, 62);
        this.lblApiHint.Name = "lblApiHint";
        this.lblApiHint.Size = new System.Drawing.Size(700, 18);
        this.lblApiHint.Text = "المتغيرات المتاحة: {phone} = رقم الهاتف  |  {message} = نص الرسالة";

        //
        // chkLocation
        //
        this.chkLocation.AutoSize = true;
        this.chkLocation.Checked = true;
        this.chkLocation.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.chkLocation.Location = new System.Drawing.Point(15, 85);
        this.chkLocation.Name = "chkLocation";
        this.chkLocation.Size = new System.Drawing.Size(120, 24);
        this.chkLocation.Text = "إرسال الموقع";
        this.chkLocation.CheckedChanged += new System.EventHandler(this.chkLocation_CheckedChanged);

        //
        // lblCoords
        //
        this.lblCoords.AutoSize = true;
        this.lblCoords.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblCoords.Location = new System.Drawing.Point(590, 87);
        this.lblCoords.Name = "lblCoords";
        this.lblCoords.Text = "الإحداثيات:";

        //
        // txtCoords
        //
        this.txtCoords.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.txtCoords.Location = new System.Drawing.Point(150, 84);
        this.txtCoords.Name = "txtCoords";
        this.txtCoords.Size = new System.Drawing.Size(430, 27);
        this.txtCoords.RightToLeft = System.Windows.Forms.RightToLeft.No;

        //
        // tabControl
        //
        this.tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top
            | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left
            | System.Windows.Forms.AnchorStyles.Right;
        this.tabControl.Controls.Add(this.tabActive);
        this.tabControl.Controls.Add(this.tabExpired);
        this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.tabControl.Location = new System.Drawing.Point(15, 120);
        this.tabControl.Name = "tabControl";
        this.tabControl.SelectedIndex = 0;
        this.tabControl.Size = new System.Drawing.Size(700, 270);

        //
        // tabActive
        //
        this.tabActive.Controls.Add(this.rtbActiveTemplate);
        this.tabActive.Location = new System.Drawing.Point(4, 32);
        this.tabActive.Name = "tabActive";
        this.tabActive.Padding = new System.Windows.Forms.Padding(5);
        this.tabActive.Size = new System.Drawing.Size(692, 264);
        this.tabActive.Text = "رسالة للمشتركين النشطين";

        //
        // tabExpired
        //
        this.tabExpired.Controls.Add(this.rtbExpiredTemplate);
        this.tabExpired.Location = new System.Drawing.Point(4, 32);
        this.tabExpired.Name = "tabExpired";
        this.tabExpired.Padding = new System.Windows.Forms.Padding(5);
        this.tabExpired.Size = new System.Drawing.Size(692, 264);
        this.tabExpired.Text = "رسالة للمنتهية صلاحيتهم";

        //
        // rtbActiveTemplate
        //
        this.rtbActiveTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
        this.rtbActiveTemplate.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.rtbActiveTemplate.Location = new System.Drawing.Point(5, 5);
        this.rtbActiveTemplate.Name = "rtbActiveTemplate";
        this.rtbActiveTemplate.Size = new System.Drawing.Size(682, 254);

        //
        // rtbExpiredTemplate
        //
        this.rtbExpiredTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
        this.rtbExpiredTemplate.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.rtbExpiredTemplate.Location = new System.Drawing.Point(5, 5);
        this.rtbExpiredTemplate.Name = "rtbExpiredTemplate";
        this.rtbExpiredTemplate.Size = new System.Drawing.Size(682, 254);

        //
        // progressBar
        //
        this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left
            | System.Windows.Forms.AnchorStyles.Right;
        this.progressBar.Location = new System.Drawing.Point(15, 400);
        this.progressBar.Name = "progressBar";
        this.progressBar.Size = new System.Drawing.Size(500, 23);
        this.progressBar.Visible = false;

        //
        // lblProgress
        //
        this.lblProgress.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblProgress.Location = new System.Drawing.Point(520, 400);
        this.lblProgress.Name = "lblProgress";
        this.lblProgress.Size = new System.Drawing.Size(195, 23);
        this.lblProgress.Text = "";
        this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        //
        // chkSendAll
        //
        this.chkSendAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.chkSendAll.AutoSize = true;
        this.chkSendAll.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.chkSendAll.Location = new System.Drawing.Point(520, 435);
        this.chkSendAll.Name = "chkSendAll";
        this.chkSendAll.Size = new System.Drawing.Size(150, 27);
        this.chkSendAll.Text = "إرسال إلى الجميع";

        //
        // btnSend
        //
        this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.btnSend.BackColor = System.Drawing.Color.FromArgb(46, 139, 87);
        this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.btnSend.ForeColor = System.Drawing.Color.White;
        this.btnSend.Location = new System.Drawing.Point(115, 470);
        this.btnSend.Name = "btnSend";
        this.btnSend.Size = new System.Drawing.Size(120, 38);
        this.btnSend.Text = "إرسال";
        this.btnSend.UseVisualStyleBackColor = false;
        this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

        //
        // btnPause
        //
        this.btnPause.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.btnPause.BackColor = System.Drawing.Color.FromArgb(200, 160, 40);
        this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnPause.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.btnPause.ForeColor = System.Drawing.Color.White;
        this.btnPause.Location = new System.Drawing.Point(245, 470);
        this.btnPause.Name = "btnPause";
        this.btnPause.Size = new System.Drawing.Size(120, 38);
        this.btnPause.Text = "إيقاف مؤقت";
        this.btnPause.UseVisualStyleBackColor = false;
        this.btnPause.Visible = false;
        this.btnPause.Click += new System.EventHandler(this.btnPause_Click);

        //
        // btnStop
        //
        this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.btnStop.BackColor = System.Drawing.Color.FromArgb(200, 50, 50);
        this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnStop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.btnStop.ForeColor = System.Drawing.Color.White;
        this.btnStop.Location = new System.Drawing.Point(375, 470);
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new System.Drawing.Size(90, 38);
        this.btnStop.Text = "إيقاف";
        this.btnStop.UseVisualStyleBackColor = false;
        this.btnStop.Visible = false;
        this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

        //
        // btnClose
        //
        this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.btnClose.Location = new System.Drawing.Point(15, 470);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(90, 38);
        this.btnClose.Text = "إغلاق";
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

        //
        // WhatsAppForm
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(730, 525);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnStop);
        this.Controls.Add(this.btnPause);
        this.Controls.Add(this.btnSend);
        this.Controls.Add(this.chkSendAll);
        this.Controls.Add(this.lblProgress);
        this.Controls.Add(this.progressBar);
        this.Controls.Add(this.tabControl);
        this.Controls.Add(this.txtCoords);
        this.Controls.Add(this.lblCoords);
        this.Controls.Add(this.chkLocation);
        this.Controls.Add(this.lblApiHint);
        this.Controls.Add(this.txtApiUrl);
        this.Controls.Add(this.lblApiUrl);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "WhatsAppForm";
        this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "إرسال رسالة على الواتساب";

        this.tabControl.ResumeLayout(false);
        this.tabActive.ResumeLayout(false);
        this.tabExpired.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label lblApiUrl;
    private System.Windows.Forms.TextBox txtApiUrl;
    private System.Windows.Forms.Label lblApiHint;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabActive;
    private System.Windows.Forms.TabPage tabExpired;
    private System.Windows.Forms.RichTextBox rtbActiveTemplate;
    private System.Windows.Forms.RichTextBox rtbExpiredTemplate;
    private System.Windows.Forms.CheckBox chkSendAll;
    private System.Windows.Forms.Button btnSend;
    private System.Windows.Forms.Button btnPause;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label lblProgress;
    private System.Windows.Forms.CheckBox chkLocation;
    private System.Windows.Forms.Label lblCoords;
    private System.Windows.Forms.TextBox txtCoords;
}
