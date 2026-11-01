using DVLD_DataAccess;
using System.Data;

namespace DVLD_Business
{
    public class clsTestType
    {
        public enum enTestType
        {
            VisionTest = 1,
            WrittenTest = 2,
            StreetTest = 3
        }

        public clsTestType.enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestType(enTestType testTypeID, string testTypeTitle, string testTypeDescription, decimal testTypeFees)
        {
            this.TestTypeID = testTypeID;
            this.TestTypeTitle = testTypeTitle;
            this.TestTypeDescription = testTypeDescription;
            this.TestTypeFees = testTypeFees;
        }

        public bool UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        static public clsTestType Find(enTestType testTypeID)
        {
            string testTypeTitle = "";
            string testTypeDescription = "";
            decimal testTypeFees = 0.0m;

            bool isFound = clsTestTypeData.GetTestTypeInfoByID((int)testTypeID, ref testTypeTitle, ref testTypeDescription, ref testTypeFees);

            if (isFound)
                return new clsTestType(testTypeID, testTypeTitle, testTypeDescription, testTypeFees);
            else
                return null;
        }

        static public DataTable GetAllTestTypesList()
        {
            return clsTestTypeData.GetAllTestTypesList();
        }
    }
}