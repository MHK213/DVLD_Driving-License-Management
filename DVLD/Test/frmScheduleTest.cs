using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD.Test
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicanseApplicationID = -1;
        private int _AppointmentID = -1;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        public frmScheduleTest(int localDrivingLicanseApplicationID, clsTestType.enTestType testType, int appointmentID = -1)
        {
            InitializeComponent();

            _LocalDrivingLicanseApplicationID = localDrivingLicanseApplicationID;
            _TestType = testType;
            _AppointmentID = appointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestType;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicanseApplicationID, _AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}