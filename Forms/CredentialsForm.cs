using WataniFTTH.Models;
using WataniFTTH.Services;

namespace WataniFTTH.Forms;

public partial class CredentialsForm : Form
{
    private readonly CredentialManager _credentialManager;

    public CredentialsForm(CredentialManager credentialManager)
    {
        InitializeComponent();
        _credentialManager = credentialManager;
        RefreshList();
    }

    private void RefreshList()
    {
        lstCredentials.Items.Clear();
        foreach (var cred in _credentialManager.Credentials)
        {
            lstCredentials.Items.Add(cred.Username);
        }
    }

    private void lstCredentials_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstCredentials.SelectedIndex >= 0 &&
            lstCredentials.SelectedIndex < _credentialManager.Credentials.Count)
        {
            var cred = _credentialManager.Credentials[lstCredentials.SelectedIndex];
            txtUsername.Text = cred.Username;
            txtPassword.Text = cred.Password;
            txtAgent.Text = cred.Agent;
        }
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            MessageBox.Show("الرجاء إدخال اسم المستخدم", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _credentialManager.Add(new Credential
        {
            Username = txtUsername.Text.Trim(),
            Password = txtPassword.Text.Trim(),
            Agent = txtAgent.Text.Trim()
        });

        RefreshList();
        ClearFields();
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (lstCredentials.SelectedIndex < 0)
        {
            MessageBox.Show("الرجاء اختيار بيانات من القائمة", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _credentialManager.Update(lstCredentials.SelectedIndex, new Credential
        {
            Username = txtUsername.Text.Trim(),
            Password = txtPassword.Text.Trim(),
            Agent = txtAgent.Text.Trim()
        });

        RefreshList();
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (lstCredentials.SelectedIndex < 0)
        {
            MessageBox.Show("الرجاء اختيار بيانات من القائمة", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show("هل أنت متأكد من حذف هذه البيانات?", "تأكيد الحذف",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            _credentialManager.Delete(lstCredentials.SelectedIndex);
            RefreshList();
            ClearFields();
        }
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        this.Close();
    }

    private void ClearFields()
    {
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtAgent.Text = "";
    }
}
