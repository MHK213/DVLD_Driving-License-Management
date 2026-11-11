using System.Windows.Forms;
using DVLD.People;
using DVLD_Business;

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID;
        private clsApplications _ApplicationInfo;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void _ResetDefaultValues()
        {
            _ApplicationID = -1;
            _ApplicationInfo = null;

            lblApplicationID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text = "[???]";
            lblType.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblDate.Text = "[??/??/????]";
            lblStatusDate.Text = "[??/??/????]";
            lblCreatedByUser.Text = "[???]";

            llViewPersonInfo.Enabled = false;
        }

        private void _LoadData()
        {
            _ApplicationID = _ApplicationInfo.ApplicationID;

            lblApplicationID.Text = _ApplicationInfo.ApplicationID.ToString();
            lblStatus.Text = _ApplicationInfo.ApplicationStatusName;
            lblFees.Text = _ApplicationInfo.PaidFees.ToString("N2");
            lblType.Text = _ApplicationInfo.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = _ApplicationInfo.ApplicantFullName;
            lblDate.Text = _ApplicationInfo.ApplicationDate.ToString("dd/MM/yyyy");
            lblStatusDate.Text = _ApplicationInfo.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = clsUser.FindByUserID(_ApplicationInfo.CreatedByUserID).UserName;
            
            llViewPersonInfo.Enabled = true;
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_ApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();

            LoadApplicationInfo(_ApplicationID);
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationInfo = clsApplications.FindBaseApplication(ApplicationID);

            if (_ApplicationInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show($"Application With Application ID {ApplicationID} not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadData();
        }
    }
}