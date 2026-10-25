using DVLD.GlobalClass;
using DVLD.Login;
using DVLD.User;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        private frmLogin _LoginForm;
        public frmMain(frmLogin loginForm)
        {
            InitializeComponent();

            _LoginForm = loginForm;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is frmPeople)
                {
                    child.Activate();
                    return;
                }
            }

            frmPeople people = new frmPeople();

            people.MdiParent = this;

            people.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is frmUserInfo)
                {
                    child.Activate();
                    return;
                }
            }

            frmUserInfo userInfo = new frmUserInfo(clsGlobal.CurrentUser.UserID);

            userInfo.MdiParent = this;
            userInfo.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is frmChangePassword)
                {
                    child.Activate();
                    return;
                }
            }

            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);

            frm.MdiParent = this;
            frm.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            this.Close();
            _LoginForm.Show();
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is frmManageUsers)
                {
                    child.Activate();
                    return;
                }
            }

            frmManageUsers users = new frmManageUsers();

            users.MdiParent = this;
            users.Show();
        }
    }
}