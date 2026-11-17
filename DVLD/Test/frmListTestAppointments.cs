using DVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Test
{
    public partial class frmListTestAppointments : Form
    {
        static DataTable _dtLicenseTestAppointments;

        private int _LocalDrivingLicanseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        public frmListTestAppointments(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            InitializeComponent();

            _LocalDrivingLicanseApplicationID = localDrivingLicenseApplicationID;
            _TestTypeID = testTypeID;
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test";
                    pbTestTypeImage.Image = Properties.Resources.Vision_512;
                    this.Text = lblTitle.Text;
                    break;
                case clsTestType.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test";
                    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                    this.Text = lblTitle.Text;
                    break;
                case clsTestType.enTestType.StreetTest:
                    lblTitle.Text = "Street Test";
                    pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                    this.Text = lblTitle.Text;
                    break;
            }
        }

        private void _RefreshTotalRecords()
        {
            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();
        }

        private void _FillTestAppointmentsList()
        {
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicanseApplicationID, _TestTypeID);
            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;

            if(dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }

            _RefreshTotalRecords();
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicanseApplicationID);

            _FillTestAppointmentsList();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicanseApplicationID);

            if(localDrivingLicenseApplication != null && localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicanseApplicationID, _TestTypeID); 

            frmScheduleTest.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicanseApplicationID, _TestTypeID, Convert.ToInt32(dgvLicenseTestAppointments.CurrentRow.Cells[0].Value));

            frmScheduleTest.ShowDialog();
            _FillTestAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}