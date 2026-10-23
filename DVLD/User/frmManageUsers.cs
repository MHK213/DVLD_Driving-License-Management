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
        private static DataTable _dtAllUsers = clsUser.GetAllUsersList();

        private DataTable _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID",
                                                       "FullName", "UserName", "IsActive");

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

            _RefreshTotalRecords();
        }
    }
}