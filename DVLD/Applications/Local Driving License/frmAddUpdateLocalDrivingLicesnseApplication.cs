using DVLD.GlobalClass;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode mode;

        private clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;
        private int LocalDrivingLicenseApplicationID;

        private int ApplicantPersonID;

        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();

            mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicesnseApplication(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();

            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;

            mode = enMode.Update;
        }

        private void _FillLicenseClassesInComboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach(DataRow rox in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(rox["ClassName"]);
            }
        }

        private void _ResetDefaultValues()
        {
            _FillLicenseClassesInComboBox();

            if(mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Local Driving License Application";

                LocalDrivingLicenseApplicationInfo = new clsLocalDrivingLicenseApplication();

                ctrlPersonCardWithFilter1.FilterFocus();

                tpApplicationInfo.Enabled = false;

                cbLicenseClass.SelectedIndex = 2;

                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblFees.Text = clsApplicationType.Find((int)clsApplications.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }

        }

        private void _LoadData()
        {
            LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            
            if(LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show($"No Application With ID = {LocalDrivingLicenseApplicationID}", "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(LocalDrivingLicenseApplicationInfo.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = LocalDrivingLicenseApplicationInfo.ApplicationDate.ToShortDateString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(LocalDrivingLicenseApplicationInfo.LicenseClassID).ClassName);
            lblFees.Text = LocalDrivingLicenseApplicationInfo.PaidFees.ToString();
            lblCreatedByUser.Text = LocalDrivingLicenseApplicationInfo.CreatedByUserInfo.UserName;
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            ApplicantPersonID = PersonID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(PersonID);
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select an applicant first.", "Applicant Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }

            btnSave.Enabled = true;
            tpApplicationInfo.Enabled = true;
            tcApplicationInfo.SelectedTab = tpApplicationInfo;
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if(mode == enMode.Update) _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            if (mode == enMode.AddNew)
            {
                int ActiveApplicationID = clsApplications.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, clsApplications.enApplicationType.NewDrivingLicense, LicenseClassID);

                if (ActiveApplicationID != -1)
                {
                    MessageBox.Show("The applicant already has an active application for the selected class.", "Active Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LocalDrivingLicenseApplicationInfo.LicenseClassID = LicenseClassID;
                LocalDrivingLicenseApplicationInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                LocalDrivingLicenseApplicationInfo.CreatedByUserInfo = clsGlobal.CurrentUser;
                LocalDrivingLicenseApplicationInfo.PaidFees = Convert.ToDecimal(lblFees.Text);
                LocalDrivingLicenseApplicationInfo.LastStatusDate = DateTime.Now;
                LocalDrivingLicenseApplicationInfo.ApplicationTypeID = (int)clsApplications.enApplicationType.NewDrivingLicense;
                LocalDrivingLicenseApplicationInfo.ApplicationStatus = clsApplications.enApplicationStatus.New;
                LocalDrivingLicenseApplicationInfo.ApplicationDate = DateTime.Now;
                LocalDrivingLicenseApplicationInfo.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            }
            else
            {
                LocalDrivingLicenseApplicationInfo.LicenseClassID = LicenseClassID;
                LocalDrivingLicenseApplicationInfo.PaidFees = Convert.ToDecimal(lblFees.Text);
                LocalDrivingLicenseApplicationInfo.LastStatusDate = DateTime.Now;
                LocalDrivingLicenseApplicationInfo.ApplicationStatus = clsApplications.enApplicationStatus.New;
            }


            if (LocalDrivingLicenseApplicationInfo.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();

                MessageBox.Show("Applicant saved successfully.", "Applicant Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID;

                lblTitle.Text = "Update Local Driving License Application";
                mode = enMode.Update;
            }
            else
            {
                MessageBox.Show("Failed to save applicant.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            if (mode == enMode.AddNew) ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            ApplicantPersonID = obj;
        }
    }
}