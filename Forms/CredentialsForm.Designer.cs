namespace WataniFTTH.Forms;

partial class CredentialsForm
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

        this.lblSavedCreds = new System.Windows.Forms.Label();
        this.lstCredentials = new System.Windows.Forms.ListBox();
        this.lblEditTitle = new System.Windows.Forms.Label();
        this.lblUsername = new System.Windows.Forms.Label();
        this.txtUsername = new System.Windows.Forms.TextBox();
        this.lblPassword = new System.Windows.Forms.Label();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.lblAgent = new System.Windows.Forms.Label();
        this.txtAgent = new System.Windows.Forms.TextBox();
        this.btnAdd = new System.Windows.Forms.Button();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnDelete = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.SuspendLayout();

        //
        // lblSavedCreds
        //
        this.lblSavedCreds.AutoSize = true;
        this.lblSavedCreds.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        this.lblSavedCreds.Location = new System.Drawing.Point(400, 15);
        this.lblSavedCreds.Name = "lblSavedCreds";
        this.lblSavedCreds.Text = "بيانات الدخول المحفوظة:";

        //
        // lstCredentials
        //
        this.lstCredentials.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.lstCredentials.ItemHeight = 25;
        this.lstCredentials.Location = new System.Drawing.Point(380, 45);
        this.lstCredentials.Name = "lstCredentials";
        this.lstCredentials.Size = new System.Drawing.Size(230, 254);
        this.lstCredentials.SelectedIndexChanged += new System.EventHandler(this.lstCredentials_SelectedIndexChanged);

        //
        // lblEditTitle
        //
        this.lblEditTitle.AutoSize = true;
        this.lblEditTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        this.lblEditTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);
        this.lblEditTitle.Location = new System.Drawing.Point(80, 15);
        this.lblEditTitle.Name = "lblEditTitle";
        this.lblEditTitle.Text = "تعديل بيانات الدخول المحددة";

        //
        // lblUsername
        //
        this.lblUsername.AutoSize = true;
        this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblUsername.Location = new System.Drawing.Point(120, 60);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Text = "اسم المستخدم:";

        //
        // txtUsername
        //
        this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.txtUsername.Location = new System.Drawing.Point(50, 85);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(280, 30);

        //
        // lblPassword
        //
        this.lblPassword.AutoSize = true;
        this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblPassword.Location = new System.Drawing.Point(135, 130);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Text = "كلمة المرور:";

        //
        // txtPassword
        //
        this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.txtPassword.Location = new System.Drawing.Point(50, 155);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.Size = new System.Drawing.Size(280, 30);

        //
        // lblAgent
        //
        this.lblAgent.AutoSize = true;
        this.lblAgent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblAgent.Location = new System.Drawing.Point(130, 200);
        this.lblAgent.Name = "lblAgent";
        this.lblAgent.Text = "اسم الوكيل:";

        //
        // txtAgent
        //
        this.txtAgent.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.txtAgent.Location = new System.Drawing.Point(50, 225);
        this.txtAgent.Name = "txtAgent";
        this.txtAgent.Size = new System.Drawing.Size(280, 30);

        //
        // btnAdd
        //
        this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnAdd.ForeColor = System.Drawing.Color.White;
        this.btnAdd.Location = new System.Drawing.Point(490, 310);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(120, 35);
        this.btnAdd.Text = "إضافة جديد";
        this.btnAdd.UseVisualStyleBackColor = false;
        this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

        //
        // btnSave
        //
        this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 139, 87);
        this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnSave.ForeColor = System.Drawing.Color.White;
        this.btnSave.Location = new System.Drawing.Point(360, 310);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(120, 35);
        this.btnSave.Text = "حفظ التغييرات";
        this.btnSave.UseVisualStyleBackColor = false;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

        //
        // btnDelete
        //
        this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
        this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnDelete.ForeColor = System.Drawing.Color.White;
        this.btnDelete.Location = new System.Drawing.Point(230, 310);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new System.Drawing.Size(120, 35);
        this.btnDelete.Text = "حذف المحدد";
        this.btnDelete.UseVisualStyleBackColor = false;
        this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

        //
        // btnClose
        //
        this.btnClose.BackColor = System.Drawing.SystemColors.Control;
        this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnClose.Location = new System.Drawing.Point(15, 310);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(90, 35);
        this.btnClose.Text = "إغلاق";
        this.btnClose.UseVisualStyleBackColor = false;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

        //
        // CredentialsForm
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(630, 360);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnDelete);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.txtAgent);
        this.Controls.Add(this.lblAgent);
        this.Controls.Add(this.txtPassword);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.txtUsername);
        this.Controls.Add(this.lblUsername);
        this.Controls.Add(this.lblEditTitle);
        this.Controls.Add(this.lstCredentials);
        this.Controls.Add(this.lblSavedCreds);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "CredentialsForm";
        this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "إدارة بيانات الدخول";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label lblSavedCreds;
    private System.Windows.Forms.ListBox lstCredentials;
    private System.Windows.Forms.Label lblEditTitle;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label lblAgent;
    private System.Windows.Forms.TextBox txtAgent;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnClose;
}
