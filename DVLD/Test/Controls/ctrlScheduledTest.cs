using DVLD_Business;
using System.Windows.Forms;

namespace DVLD.Test.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private clsTestType.enTestType _TestTypeID;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        pbTestTypeImage.Image = Properties.Resources.Vision_512;
                        gbTestType.Text = "Vision Test";
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                        gbTestType.Text = "Written Test";
                        break;
                    case clsTestType.enTestType.StreetTest:
                        pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                        gbTestType.Text = "Street Test";
                        break;
                }
            }
        }

        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointmentInfo;
        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;

        private int _TestID = -1;
        public int TestID
        {
            get
            {
                return _TestID;
            }
        }

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        private void _Reset()
        {
            _TestAppointmentID = -1;
            _TestID = -1;
            _LocalDrivingLicenseApplicationID = -1;

            _TestAppointmentInfo = null;
            _LocalDrivingLicenseApplicationInfo = null;
        }

        public void LoadInfo(int testAppointmentID)
        {
            _Reset();

            _TestAppointmentInfo = clsTestAppointment.FindByTestAppointmentID(testAppointmentID);

            if (_TestAppointmentInfo == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + testAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestAppointmentID = testAppointmentID;

            _TestID = _TestAppointmentInfo.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointmentInfo.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _TestAppointmentID = -1;
                _LocalDrivingLicenseApplicationID = -1;
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _TestAppointmentInfo.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplicationInfo.ApplicantFullName;

            lblTrial.Text = _LocalDrivingLicenseApplicationInfo.TotalTrialsPerTest(_TestTypeID).ToString();

            lblDate.Text = _TestAppointmentInfo.AppointmentDate.ToString("d");
            lblFees.Text = _TestAppointmentInfo.PaidFees.ToString();

            lblTestID.Text = (_TestID == -1) ? "Not Taken Yet" : _TestID.ToString();
        }
    }
}