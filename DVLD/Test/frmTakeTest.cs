using DVLD.GlobalClass;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD.Test
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private clsTestType.enTestType _TestTypeID;
        private clsTest _TestInfo;


        public frmTakeTest(int testAppointmentID, clsTestType.enTestType testTypeID)
        {
            InitializeComponent();

            _TestAppointmentID = testAppointmentID;
            _TestTypeID = testTypeID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.TestTypeID = _TestTypeID;
            ctrlSecheduledTest1.LoadInfo(_TestAppointmentID);

            btnSave.Enabled = (ctrlSecheduledTest1.TestAppointmentID == -1) ? false : true;

            int testID = ctrlSecheduledTest1.TestID;

            if (testID != -1)
            {
                _TestInfo = clsTest.FindByTestID(testID);

                if (_TestInfo == null)
                {
                    MessageBox.Show("Error: Unable to load test information.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnSave.Enabled = false;
                    return;
                }

                if (_TestInfo.TestResult)
                    rbPass.Checked = true;
                else
                {
                    rbFail.Checked = true;
                    txtNotes.Text = _TestInfo.Notes;
                }

                lblUserMessage.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
            }
            else _TestInfo = new clsTest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            _TestInfo.TestAppointmentID = _TestAppointmentID;
            _TestInfo.TestResult = rbPass.Checked;
            _TestInfo.Notes = txtNotes.Text.Trim();
            _TestInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_TestInfo.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
