using DVLD_DataAccess;
using System.Data;

namespace DVLD_Business
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode { get; private set; } = enMode.AddNew;

        private const int RequiredTestsCount = 3;

        public int TestID { get; private set;}

        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { get; private set; }

        public bool TestResult { get; set; }
        public string Notes { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; private set; }

        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;

            TestAppointmentInfo = clsTestAppointment.FindByTestAppointmentID(testAppointmentID);
            CreatedByUserInfo = clsUser.FindByUserID(createdByUserID);

            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            TestID = clsTestData.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);

            return (TestID != -1);
        }

        private bool _Update()
        {
            return clsTestData.UpdateTest(TestID, TestAppointmentID, TestResult, Notes);
        }

        static public clsTest FindByTestID(int testID)
        {
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = "";
            int createdByUserID = -1;

            if (clsTestData.GetTestInfoByID(testID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTest(testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        static public clsTest GetLastTestByPersonAndTestTypeAndLicenseClass(int personID, int testTypeID, int licenseClassID)
        {
            int testID = -1;
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = "";
            int createdByUserID = -1;

            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(personID, testTypeID, licenseClassID, ref testID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTest(testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;

                case enMode.Update:

                    return _Update();

            }

            return false;
        }

        public static DataTable GetAllTestsList()
        {
            return clsTestData.GetAllTestsList();
        }

        public static byte GetPassedTestCount(int localDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(localDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int localDrivingLicenseApplicationID)
        {
            return (GetPassedTestCount(localDrivingLicenseApplicationID) == RequiredTestsCount);
        }
    }
}