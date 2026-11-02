using DVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Test.TestTypes
{
    public partial class frmTestTypesList : Form
    {
        private DataTable dtAllTestTypes;

        public frmTestTypesList()
        {
            InitializeComponent();
        }

        private void _RefreshTotalRecords()
        {
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void frmTestTypesList_Load(object sender, EventArgs e)
        {
            dtAllTestTypes = clsTestType.GetAllTestTypesList();
            dgvTestTypes.DataSource = dtAllTestTypes;

            _RefreshTotalRecords();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns["TestTypeID"].HeaderText = "ID";
                dgvTestTypes.Columns["TestTypeID"].Width = 120;

                dgvTestTypes.Columns["TestTypeTitle"].HeaderText = "Title";
                dgvTestTypes.Columns["TestTypeTitle"].Width = 180;

                dgvTestTypes.Columns["TestTypeDescription"].HeaderText = "Description";
                dgvTestTypes.Columns["TestTypeDescription"].Width = 400;

                dgvTestTypes.Columns["TestTypeFees"].HeaderText = "Fees";
                dgvTestTypes.Columns["TestTypeFees"].Width = 110;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frmEdit = new frmEditTestType((clsTestType.enTestType)dgvTestTypes.SelectedRows[0].Cells["TestTypeID"].Value);

            frmEdit.ShowDialog();
            frmTestTypesList_Load(null, null);
        }
    }
}