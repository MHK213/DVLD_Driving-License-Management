using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode { get; private set; } = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementForDamaged = 3, ReplacementForLost = 4 }
        public enIssueReason IssueReason { get; private set; } = enIssueReason.FirstTime;
        public string IssueReasonText {
            get
            {
                return GetIssueReasonText(IssueReason);
            }
        }

        public int LicenseID { get; private set; }
        public int ApplicationID { get; private set; }

        public int DriverID { get; private set; }
        public clsDriver DriverInfo { get; private set; }

        public int LicenseClassID { get; private set; }
        public clsLicenseClass LicenseClass { get; private set; }

        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public string Notes { get; private set; }
        public decimal PaidFees { get; private set; }
        public bool IsActive { get; private set; }
        public int CreatedByUserID { get; private set; }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = false;
            IssueReason = enIssueReason.FirstTime;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, 
            enIssueReason issueReason, int createdByUser)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClassID = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUser;

            DriverInfo = clsDriver.FindByDriverID(DriverID);
            LicenseClass = clsLicenseClass.Find(LicenseClassID);

            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            LicenseID = clsLicenseData.AddNewLicense(ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);

            return (LicenseID != -1);
        }

        private bool _Update()
        {
            return clsLicenseData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);
        }

        public static string GetIssueReasonText(enIssueReason issueReason)
        {
            switch(issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.ReplacementForDamaged:
                    return "Replacement For Damaged";
                case enIssueReason.ReplacementForLost:
                    return "Replacement For Lost";
                default:
                    return "First Time";
            }
        }

        public static clsLicense FindLicenseByID(int licenseID)
        {
            int applicationID = -1, driverID = -1, licenseClassID = -1, createdByUserID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = "";
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;

            bool isFound = clsLicenseData.GetLicenseInfoByLicenseID(licenseID, ref applicationID, ref driverID, ref licenseClassID, ref issueDate, ref expirationDate, ref notes, ref paidFees,
                ref isActive, ref issueReason, ref createdByUserID);

            if (isFound)
                return new clsLicense(licenseID, applicationID, driverID, licenseClassID, issueDate, expirationDate, notes, paidFees, isActive, (enIssueReason) issueReason, createdByUserID);
            else
                return null;
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
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();
                default:
                    return false;
            }
        }

        public static DataTable GetAllLicensesList()
        {
            return clsLicenseData.GetAllLicensesList();
        }

        public static int GetActiveLicenseIDByPersonID(int personID, int licenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(personID, licenseClassID);
        }

        public static bool IsLicenseExistByPersonID(int personID, int licenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(personID, licenseClassID) != -1);
        }

        public static DataTable GetDriverLicenses(int driverID)
        {
            return clsLicenseData.GetDriverLicensesList(driverID);
        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool DeactivateCurrentLicense(int licenseID)
        {
            return clsLicenseData.DeactivateLicense(licenseID);
        }

        public clsLicense RenewLicense(string notes, int createdByUserID)
        {
            clsApplications newApplication = new clsApplications();

            newApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            newApplication.ApplicationDate = DateTime.Now;
            newApplication.ApplicationTypeID = (int)clsApplications.enApplicationType.RenewDrivingLicense;
            newApplication.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = clsApplicationType.Find((int)clsApplications.enApplicationType.RenewDrivingLicense).ApplicationFees;
            newApplication.CreatedByUserID = createdByUserID;

            if (!newApplication.Save()) return null;

            clsLicense newLicense = new clsLicense();

            newLicense.ApplicationID = newApplication.ApplicationID;
            newLicense.DriverID = this.DriverID;
            newLicense.LicenseClassID = this.LicenseClassID;
            newLicense.IssueDate = DateTime.Now;

            int defaultValidityLength = this.LicenseClass.DefaultValidityLength;

            newLicense.ExpirationDate = DateTime.Now.AddYears(defaultValidityLength);
            newLicense.Notes = notes;
            newLicense.PaidFees = this.LicenseClass.ClassFees;
            newLicense.IsActive = true;
            newLicense.IssueReason = enIssueReason.Renew;
            newLicense.CreatedByUserID = createdByUserID;

            if (!newLicense.Save()) return null;

            DeactivateCurrentLicense(this.LicenseID);

            return newLicense;
        }

        public clsLicense ReplaceLicense(enIssueReason issueReason, int createdByUserID)
        {
            clsApplications newApplication = new clsApplications();

            newApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            newApplication.ApplicationDate = DateTime.Now;
            newApplication.ApplicationTypeID = (issueReason == enIssueReason.ReplacementForDamaged) ?
                (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;

            newApplication.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = clsApplicationType.Find(newApplication.ApplicationTypeID).ApplicationFees;
            newApplication.CreatedByUserID = createdByUserID;

            if (!newApplication.Save()) return null;

            clsLicense newLicense = new clsLicense();

            newLicense.ApplicationID = newApplication.ApplicationID;
            newLicense.DriverID = this.DriverID;
            newLicense.LicenseClassID = this.LicenseClassID;
            newLicense.IssueDate = DateTime.Now;
            newLicense.ExpirationDate = this.ExpirationDate;
            newLicense.Notes = this.Notes;
            newLicense.PaidFees = 0;
            newLicense.IsActive = true;
            newLicense.IssueReason = issueReason;
            newLicense.CreatedByUserID = createdByUserID;

            if (!newLicense.Save()) return null;

            DeactivateCurrentLicense(this.LicenseID);

            return newLicense;
        }
    }
}