using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD
{
    public partial class frmPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeopleList();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeopleList();

            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");


            dgvPeople.DataSource = _dtPeople;
            _RefreshTotalRecords();
        }

        private void _RefreshTotalRecords()
        {
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        public frmPeople()
        {
            InitializeComponent();
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFiltreBy.SelectedIndex = 0;
            _RefreshTotalRecords();

            if (dgvPeople.Rows.Count > 0)
            {

                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 90;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 110;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 90;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 110;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 70;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 100;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 90;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 80;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 110;
            }
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFiltreBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(tbFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                _RefreshTotalRecords();

                return;
            }

            if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbFilterBy.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbFilterBy.Text.Trim());

            _RefreshTotalRecords();
        }

        private void cbFiltreBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilterBy.Visible = (cbFiltreBy.Text != "None");

            if (tbFilterBy.Visible)
            {
                tbFilterBy.Text = "";
                tbFilterBy.Focus();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int personId = Convert.ToInt32(dgvPeople.CurrentRow.Cells["Person ID"].Value);

            //frmPersonDetails personDetails = new frmPersonDetails(personId);

            //personDetails.ShowDialog();

            //_RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int personId = Convert.ToInt32(dgvPeople.CurrentRow.Cells["Person ID"].Value);

            //frmAddEditPerson addUpdatePerson = new frmAddEditPerson(personId);
            //addUpdatePerson.ShowDialog();

            //_RefreshPeopleList();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not implemented yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not implemented yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);

            if (MessageBox.Show($"Are you sure you want to delete person [{personId}]?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(personId)) MessageBox.Show("Person deleted successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            _RefreshPeopleList();
        }

        private void _AddPerson()
        {
            //frmAddEditPerson addUpdatePerson = new frmAddEditPerson(-1);
            //addUpdatePerson.ShowDialog();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            _AddPerson();
            _RefreshPeopleList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddPerson();
            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {

        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool IsNumericFilter = (cbFiltreBy.Text == "Person ID");

            e.Handled = (IsNumericFilter && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }
    }
}