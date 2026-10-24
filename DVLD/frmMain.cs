using DVLD.User;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
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

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
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