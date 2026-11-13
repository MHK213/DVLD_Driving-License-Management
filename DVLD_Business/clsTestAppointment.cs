using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; private set; }

        public int TestAppointmentID { get; private set; }
        public clsTestType.enTestType TestTypeID { get; private set; }
        public int LocalDrivingLicenseApplicationID { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public decimal PaidFees { get; private set; }
        public int CreatedByUserID { get; private set; }
        public byte IsLocked { get; private set; }

        public int RetakeTestApplicationID { get; private set; }
        public clsApplications RetakeTestApplicationInfo { get; private set; }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestType.WrittenTest;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0.0m;
            CreatedByUserID = -1;
            IsLocked = 0;
            RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        public clsTestAppointment(int testAppointmentID, clsTestType.enTestType testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate,decimal paidFees, int createdByUserID, byte isLocked, 
            int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplications.FindBaseApplication(retakeTestApplicationID);

            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, 
                RetakeTestApplicationID);

            return (TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(TestAppointmentID, (int)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, 
                IsLocked, RetakeTestApplicationID);
        }

        static public clsTestAppointment FindByTestAppointmentID(int testAppointmentID)
        {
            int localDrivingLicenseApplicationID = -1, createdByUserID = -1, retakeTestApplicationID = -1, testTypeID = -1;
            DateTime appointmentDate = DateTime.Now;
            decimal paidFees = 0.0m;
            byte isLocked = 0;

            bool isFound = clsTestAppointmentData.GetTestAppointmentInfoByID(testAppointmentID, ref localDrivingLicenseApplicationID, ref testTypeID, 
                ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked, ref retakeTestApplicationID);

            if (isFound)
                return new clsTestAppointment(testAppointmentID, (clsTestType.enTestType)testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLocked, retakeTestApplicationID);
            else
                return null;
        }

        static public clsTestAppointment LastTestAppointment(int localDrivingLicenseApplicationID, int testTypeID)
        {
            int testAppointmentID = -1, createdByUserID = -1, retakeTestApplicationID = -1;
            DateTime appointmentDate = DateTime.Now;
            decimal paidFees = 0.0m;
            byte isLocked = 0;

            bool isFound = clsTestAppointmentData.GetLastTestAppointment(localDrivingLicenseApplicationID, testTypeID, ref testAppointmentID,
                ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked, ref retakeTestApplicationID);

            if (isFound)
                return new clsTestAppointment(testAppointmentID, (clsTestType.enTestType)testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLocked, retakeTestApplicationID);
            else
                return null;
        }

        static public DataTable GetAllTestAppointmentsList()
        {
            return clsTestAppointment.GetAllTestAppointmentsList();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return (_UpdateTestAppointment());
                default:
                    return false;
            }
        }

        public DataTable GetApplicationTestAppointmentsPerTestType(int testTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, testTypeID);
        }

        static public DataTable GetApplicationTestAppointmentsPerTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(localDrivingLicenseApplicationID, testTypeID);
        }
    }
}