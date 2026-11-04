using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode mode { get ; private set; } = enMode.AddNew;

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }
        public enApplicationStatus ApplicationStatus { get; set; }
        public string ApplicationStatusName
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public string ApplicationFullName
        {
            get
            {
                return ApplicantPersonInfo?.FullName ?? string.Empty;
            }
        }
        public clsPerson ApplicantPersonInfo { get; private set; }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo { get; private set; }

        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; private set; }

        public clsApplications()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;

            ApplicationStatus = enApplicationStatus.New;
            mode = enMode.AddNew;
        }

        public clsApplications(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, enApplicationStatus applicationStatus, 
            DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
           ApplicationID = applicationID;
           ApplicantPersonID = applicantPersonID;
           ApplicationDate = applicationDate;
           ApplicationTypeID = applicationTypeID;
           ApplicationStatus = applicationStatus;
           LastStatusDate = lastStatusDate;
           PaidFees = paidFees;
           CreatedByUserID = createdByUserID;
            
           ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
           ApplicationTypeInfo = clsApplicationType.Find(applicationTypeID);
           CreatedByUserInfo = clsUser.FindByUserID(createdByUserID);

            mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            ApplicationID = clsApplicationsData.AddNewApplication(ApplicantPersonID, ApplicationDate, ApplicationTypeID, (byte)ApplicationStatus, 
                LastStatusDate, PaidFees, CreatedByUserID);

            return (ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, 
                (byte)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
        }

        static public clsApplications FindBaseApplication(int applicationID)
        {
            int applicantPersonID = -1;
            DateTime applicationDate = DateTime.Now;
            int applicationTypeID = -1;
            byte applicationStatus = 0;
            DateTime lastStatusDate = DateTime.Now;
            decimal paidFees = 0;
            int createdByUserID = -1;

            bool isFound = clsApplicationsData.GetApplicationInfoByApplicationID(applicationID, ref applicantPersonID, ref applicationDate, ref applicationTypeID, 
                ref applicationStatus, ref lastStatusDate, ref paidFees, ref createdByUserID);

            if (isFound)
                return new clsApplications(applicationID, applicantPersonID, applicationDate, applicationTypeID, (enApplicationStatus)applicationStatus, lastStatusDate, paidFees, createdByUserID);
            else
                return null;
        }

        public bool DeleteApplication()
        {
            return (clsApplicationsData.DeleteApplication(ApplicationID));
        }

        public bool Cancel()
        {
            return (clsApplicationsData.UpdateStatus(ApplicationID, 2));
        }

        public bool SetComplete()
        {
            return (clsApplicationsData.UpdateStatus(ApplicationID, 3));
        }

        public int GetActiveApplicationID(enApplicationType applicationTypeID)
        {
            return clsApplicationsData.GetActiveApplicationID(ApplicantPersonID, (int)applicationTypeID);
        }

        static public int GetActiveApplicationID(int applicantPersonID, enApplicationType applicationTypeID)
        {
            return clsApplicationsData.GetActiveApplicationID(applicantPersonID, (int)applicationTypeID);
        }

        public bool DoesApplicantHaveActiveApplication(int applicationTypeID)
        {
            return (clsApplicationsData.DoesApplicantHaveActiveApplication(ApplicantPersonID, applicationTypeID));
        }

        static public bool DoesApplicantHaveActiveApplication(int applicantPersonID, int applicationTypeID)
        {
            return (clsApplicationsData.DoesApplicantHaveActiveApplication(applicantPersonID, applicationTypeID));
        }

        static public int GetActiveApplicationIDForLicenseClass(int applicantPersonID, enApplicationType applicationTypeID, int licenseClassID)
        {
            return clsApplicationsData.GetActiveApplicationIDForLicenseClass(applicantPersonID, (int)applicationTypeID, licenseClassID);
        }

        static public bool IsApplicationExist(int applicationID)
        {
            return (clsApplicationsData.IsApplicationExist(applicationID));
        }

        static public DataTable GetAllApplicationsList()
        {
            return clsApplicationsData.GetAllApplicationsList();
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return (_UpdateApplication());
                default:
                    return false;
            }
        }
    }
}