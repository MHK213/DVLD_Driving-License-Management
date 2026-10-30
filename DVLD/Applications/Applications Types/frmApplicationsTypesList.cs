using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.Applications.Applications_Types
{
    public partial class frmApplicationsTypesList : Form
    {
        private DataTable dtAllApplicationTypes;

        public frmApplicationsTypesList()
        {
            InitializeComponent();
        }

        private void _RefershTotalRecords()
        {
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        private void _RefreshApplicationTypes()
        {
            dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypesList();

            dgvApplicationTypes.DataSource = dtAllApplicationTypes;
            _RefershTotalRecords();
        }

        private void frmApplicationsTypesList_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypes();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns["ApplicationTypeID"].HeaderText = "ID";
                dgvApplicationTypes.Columns["ApplicationTypeID"].Width = 110;

                dgvApplicationTypes.Columns["ApplicationTypeTitle"].HeaderText = "Title";
                dgvApplicationTypes.Columns["ApplicationTypeTitle"].Width = 400;

                dgvApplicationTypes.Columns["ApplicationFees"].HeaderText = "Fees";
                dgvApplicationTypes.Columns["ApplicationFees"].Width = 100;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells["ApplicationTypeID"].Value);

            frm.ShowDialog();
            frmApplicationsTypesList_Load(null, null);
        }
    }
}