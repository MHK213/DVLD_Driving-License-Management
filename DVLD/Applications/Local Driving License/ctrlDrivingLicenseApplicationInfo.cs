using DVLD_Business;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;

        private int _LicenseID = -1;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            _LocalDrivingLicenseApplicationID = -1;
            _LocalDrivingLicenseApplicationInfo = null;

            lblLocalDrivingLicenseApplicationID.Text = "[???]";
            lblAppliedFor.Text = "[???]";
            lblPassedTests.Text = "0";

            ctrlApplicationBasicInfo1._ResetDefaultValues();

            llShowLicenceInfo.Enabled = false;
        }
        
        private void _LoadData()
        {
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID;

            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            //lblPassedTests.Text = _LocalDrivingLicenseApplicationInfo.PassedTestsCount.ToString();

            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplicationInfo.ApplicationID);

            llShowLicenceInfo.Enabled = (_LicenseID != -1);
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        public void LoadApplicationInfoByLocalDrivingAppID(int localDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplicationInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show($"Local Driving License Application with ID {localDrivingLicenseApplicationID} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadData();
        }

        public void LoadApplicationInfoByBaseAppID(int applicationID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByBaseApplicationID(applicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show($"Local Driving License Application with Application ID {applicationID} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadData();
        }
    }
}