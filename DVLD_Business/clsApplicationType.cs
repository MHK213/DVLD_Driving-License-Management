using DVLD_DataAccess;
using System.Data;

namespace DVLD_Business
{
    internal class clsApplicationType
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        public clsApplicationType(int applicationTypeID, string applicationTypeTitle, decimal applicationFees)
        {
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitle = applicationTypeTitle;
            this.ApplicationFees = applicationFees;
        }

        public bool UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        static public clsApplicationType Find(int applicationTypeID)
        {
            string applicationTypeTitle = "";
            decimal applicationFees = 0.0m;

            bool isFound = clsApplicationTypesData.GetApplicationTypesInfoByID(applicationTypeID, ref applicationTypeTitle, ref applicationFees);

            if (isFound)
                return new clsApplicationType(applicationTypeID, applicationTypeTitle, applicationFees);
            else
                return null;
        }

        static public DataTable GetAllApplicationTypesList()
        {
            return clsApplicationTypesData.GetAllApplicationTypesList();
        }
    }
}