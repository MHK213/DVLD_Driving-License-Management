using DVLD.People;
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
    public partial class frmManageUsers : Form
    {
        private static DataTable _dtAllUsers;

        private DataTable _dtUsers;

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshTotalRecords()
        {
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsersList();
            _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID",
                                                       "FullName", "UserName", "IsActive");

            dgvUsers.DataSource = _dtUsers;
            cbFilterBy.SelectedIndex = 0;
            _RefreshTotalRecords();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();

            cbFilterBy.SelectedIndex = 0;

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 80;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 80;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 200;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 150;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 80;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                _RefreshTotalRecords();

                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "UserID")
            {
                _dtUsers.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtUsers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }

            _RefreshTotalRecords();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = false;

                _RefreshUsersList();
            }
            else if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;

                cbIsActive.Focus();
            }
            else
            {
                txtFilterValue.Visible = true;
                cbIsActive.Visible = false;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsActive.Text == "All")
                _dtUsers.DefaultView.RowFilter = "";
            else if (cbIsActive.Text == "Yes")
                _dtUsers.DefaultView.RowFilter = string.Format("IsActive = true");
            else
                _dtUsers.DefaultView.RowFilter = string.Format("IsActive = false");

            _RefreshTotalRecords();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isNumericFilter = (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID");

            e.Handled = isNumericFilter && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();

            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void ShowDetailstoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));

            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();

            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));

            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;

            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshUsersList();
            }

            else
                MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void changePasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));

            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void sendEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not implemented yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not implemented yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}