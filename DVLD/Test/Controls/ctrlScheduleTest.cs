using DVLD.GlobalClass;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD.Test.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 }
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestAppointment _TestAppointmentInfo;
        private int _TestAppointmentID = 1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        private int _LocalDrivingLicenseApplicationID = 1;

        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set 
            { 
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Properties.Resources.Vision_512;
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                        break;
                    case clsTestType.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                        break;
                }
            }
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int localDrivingLicenseApplicationID, int appointmentID = -1)
        {
            if (appointmentID == -1) _Mode = enMode.AddNew;
            else _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _TestAppointmentID = appointmentID;
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show("Invalid Local Driving License Application ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;

                return;
            }

            if (_LocalDrivingLicenseApplicationInfo.DoesAttendTestType(_TestTypeID)) _CreationMode = enCreationMode.RetakeTestSchedule;
            else _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                gbRetakeTestInfo.Enabled = true;

                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplications.enApplicationType.RetakeTest).ApplicationFees.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;

                lblTitle.Text = "Schedule Test";
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplicationInfo.ApplicantFullName;

            lblTrial.Text = _LocalDrivingLicenseApplicationInfo.TotalTrialsPerTest(TestTypeID).ToString();

            if(_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointmentInfo = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData()) return;
            }

            lblTotalFees.Text = (Convert.ToDecimal(lblFees.Text) + Convert.ToDecimal(lblRetakeAppFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint()) return;

            if (!_HandleAppointmentLockedConstraint()) return;

            if (!_HandlePrviousTestConstraint()) return;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;

                return false;
            }

            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if(_TestAppointmentInfo.IsLocked)
            {
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                lblUserMessage.Visible = true;
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;

                return false;
            }
            else lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    if (_LocalDrivingLicenseApplicationInfo.DoesPassPreviousTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Visible = false;
                        dtpTestDate.Enabled = true;
                        btnSave.Enabled = true;

                        return true;
                    }
                    else
                    {
                        lblUserMessage.Text = "Cannot Schedule, Vision Test Should be Passed First.";
                        lblUserMessage.Visible = true;
                        dtpTestDate.Enabled = false;
                        btnSave.Enabled = false;

                        return false;
                    }

                case clsTestType.enTestType.StreetTest:
                    if (_LocalDrivingLicenseApplicationInfo.DoesPassPreviousTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Visible = false;
                        dtpTestDate.Enabled = true;
                        btnSave.Enabled = true;

                        return true;
                    }
                    else
                    {
                        lblUserMessage.Text = "Cannot Schedule, Written Test Should be Passed First.";
                        lblUserMessage.Visible = true;
                        dtpTestDate.Enabled = false;
                        btnSave.Enabled = false;

                        return false;
                    }
            }

            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointmentInfo = clsTestAppointment.FindByTestAppointmentID(_TestAppointmentID);
            
            if(_TestAppointmentInfo == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;

                return false;
            }

            lblFees.Text = _TestAppointmentInfo.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointmentInfo.AppointmentDate) < 0) dtpTestDate.MinDate = DateTime.Now;
            else dtpTestDate.MinDate = _TestAppointmentInfo.AppointmentDate;

            dtpTestDate.Value = _TestAppointmentInfo.AppointmentDate;

            if(_TestAppointmentInfo.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointmentInfo.RetakeTestApplicationInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointmentInfo.RetakeTestApplicationID.ToString();
            }

            return true;
        }

        private bool _HandleRetakeApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplications Application = new clsApplications();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplicationInfo.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplications.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointmentInfo.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointmentInfo.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication()) return;

            _TestAppointmentInfo.TestTypeID = _TestTypeID;
            _TestAppointmentInfo.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID;
            _TestAppointmentInfo.AppointmentDate = dtpTestDate.Value;
            _TestAppointmentInfo.PaidFees = Convert.ToDecimal(lblFees.Text);

            if (_TestAppointmentInfo.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}