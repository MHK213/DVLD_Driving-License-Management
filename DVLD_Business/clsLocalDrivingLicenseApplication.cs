using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode mode { get; private set; } = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassID;
        public clsLicenseClass LicenseClassInfo { get; private set; }

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;

            LicenseClassInfo = null;

            mode = enMode.AddNew;
        }

        public clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int licenseClassID, int applicationID, int applicantPersonID, DateTime applicationDate, 
            int applicationTypeID, enApplicationStatus applicationStatus, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(licenseClassID);

            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = (int)applicationTypeID;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;

            mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(ApplicationID, LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }

        static public clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int localDrivingLicenseApplicationID)
        {
            int applicationID = -1, licenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(localDrivingLicenseApplicationID, ref applicationID, ref licenseClassID);

            if(isFound)
            {
                clsApplications baseApplication = clsApplications.FindBaseApplication(applicationID);

                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, licenseClassID, baseApplication.ApplicationID, baseApplication.ApplicantPersonID,
                    baseApplication.ApplicationDate, baseApplication.ApplicationTypeID, baseApplication.ApplicationStatus, baseApplication.LastStatusDate, baseApplication.PaidFees,
                    baseApplication.CreatedByUserID);
            }
            else
                return null;
        }

        static public clsLocalDrivingLicenseApplication FindByBaseApplicationID(int applicationID)
        {
            int localDrivingLicenseApplicationID = -1, licenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(applicationID, ref localDrivingLicenseApplicationID, ref licenseClassID);

            if (isFound)
            {
                clsApplications baseApplication = clsApplications.FindBaseApplication(applicationID);

                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, licenseClassID, baseApplication.ApplicationID, baseApplication.ApplicantPersonID,
                    baseApplication.ApplicationDate, baseApplication.ApplicationTypeID, baseApplication.ApplicationStatus, baseApplication.LastStatusDate, baseApplication.PaidFees,
                    baseApplication.CreatedByUserID);
            }
            else
                return null;
        }

        public bool DeleteLocalDrivingLicenseApplication()
        {
            bool IsDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
            bool IsBaseApplicationDeleted = false;

            if (!IsDrivingLicenseApplicationDeleted) return false;

            IsBaseApplicationDeleted = base.DeleteApplication();

            return IsBaseApplicationDeleted;
        }

        public bool Save()
        {
            base.mode = (clsApplications.enMode)mode;
            if(!base.Save()) return false;


            switch (mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else return false;

                case enMode.Update:
                    return (_UpdateLocalDrivingLicenseApplication());

                default:
                    return false;
            }
        }

        static public DataTable GetAllLocalDrivingLicenseApplicationsList()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplicationList();
        }

        public bool DoesPassTestType(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)testType);
        }

        static public bool DoesPassTestType(int localdrivinglicenseapplicationid, clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(localdrivinglicenseapplicationid, (int)testType);
        }

        public bool DoesPassPreviousTestType(clsTestType.enTestType testType)
        {
            switch(testType)
            {
                case clsTestType.enTestType.VisionTest:
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    return DoesPassTestType(clsTestType.enTestType.VisionTest);
                case clsTestType.enTestType.StreetTest:
                    return DoesPassTestType(clsTestType.enTestType.WrittenTest);
                default:
                    return false;
            }
        }

        public bool DoesAttendTestType(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(LocalDrivingLicenseApplicationID, (int)testType);
        }

        public int TotalTrialsPerTest(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)testType);
        }

        static public int TotalTrialsPerTest(int localdrivinglicenseapplicationid, clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(localdrivinglicenseapplicationid, (int)testType);
        }

        public bool AttendedTest(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)testType) > 0;
        }

        static public bool AttendedTest(int localdrivinglicenseapplicationid, clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(localdrivinglicenseapplicationid, (int)testType) > 0;
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)testType);
        }

        static public bool IsThereAnActiveScheduledTest(int localdrivinglicenseapplicationid, clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(localdrivinglicenseapplicationid, (int)testType);
        }
    
        
    }
}