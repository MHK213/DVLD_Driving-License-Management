using DVLD.People.Controls;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmAddUpdateUser : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew, Update }
        private enMode _Mode;

        clsUser _UserInfo;
        int _UserID;

        clsPerson _PersonInfo;

        public frmAddUpdateUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int userID)
        {
            InitializeComponent();

            _UserID = userID;

            _Mode = enMode.Update;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                this.Text = "Add New User";

                lblTitle.Text = "Add New User";

                tpLoginInfo.Enabled = false;
                btnSave.Enabled = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                this.Text = "Update User";

                lblTitle.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            lblUserID.Text = "[????]";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _UserInfo = clsUser.FindByUserID(_UserID);

            ctrlPersonCardWithFilter1.ShowFilter = false;

            if (_UserInfo == null)
            {
                MessageBox.Show($" No User With ID {_UserID}", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }

            _PersonInfo = clsPerson.Find(_UserInfo.PersonID);

            ctrlPersonCardWithFilter1.LoadPersonInfo(_UserInfo.PersonID);

            lblUserID.Text = _UserInfo.UserID.ToString();
            txtUserName.Text = _UserInfo.UserName;
            txtPassword.Text = _UserInfo.Password;
            txtConfirmPassword.Text = _UserInfo.Password;
            chkIsActive.Checked = _UserInfo.IsActive;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update) _LoadData();
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            _PersonInfo = ctrlPersonCardWithFilter1.SelectedPersonInfo;

            if (_PersonInfo == null || _PersonInfo.PersonID == -1)
            {
                MessageBox.Show("Please select a person first!", "Select Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }

            if (clsUser.IsUserExistForPersonID(_PersonInfo.PersonID))
            {
                MessageBox.Show("Selected person already has a user.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;

                _UserInfo = new clsUser();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valide!, put the mouse on the red icon", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _UserInfo.PersonID = _PersonInfo.PersonID;
            _UserInfo.UserName = txtUserName.Text;
            _UserInfo.Password = txtPassword.Text;
            _UserInfo.IsActive = chkIsActive.Checked;

            if (_UserInfo.Save())
            {
                lblUserID.Text = _UserInfo.UserID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _UserInfo.PersonID);
            }
            else
            {
                MessageBox.Show("Error Saving Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "This field is required!");
                return;
            }

            if (confirmPassword != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            string userName = txtUserName.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This field is required!");

                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if (_Mode == enMode.AddNew && clsUser.IsUserExist(userName))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "username is used by another user!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, "");
            }

            if (_Mode == enMode.Update && _UserInfo.UserName != userName && clsUser.IsUserExist(userName))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "username is used by another user!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password is required!");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }
    }
}