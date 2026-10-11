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
    }
}