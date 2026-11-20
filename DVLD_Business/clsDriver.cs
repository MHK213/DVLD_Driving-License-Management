using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode { get; private set; } = enMode.AddNew;

        public int DriverID { get; private set; }
        public int PersonID { get; private set; }
        public clsPerson PersonInfo { get; private set; }

        public int CreatedByUserID { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.MinValue;

            Mode = enMode.AddNew;
        }

        public clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;

            PersonInfo = clsPerson.Find(personID);

            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            DriverID = clsDriverData.AddNewDriver(PersonID, CreatedByUserID);

            return (DriverID != -1);
        }

        //private bool _Update()
        //{
        //    return clsDriverData.UpdateDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
        //}

        public static clsDriver FindByDriverID(int driverID)
        {
            int personID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.MinValue;

            bool isFound = clsDriverData.GetDriverInfoByDriverID(driverID, ref personID, ref createdByUserID, ref createdDate);

            if (isFound)
            {
                return new clsDriver(driverID, personID, createdByUserID, createdDate);
            }
            else
            {
                return null;
            }
        }

        public static clsDriver FindByPersonID(int personID)
        {
            int driverID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.MinValue;

            bool isFound = clsDriverData.GetDriverInfoByPersonID(personID, ref driverID, ref createdByUserID, ref createdDate);

            if (isFound)
            {
                return new clsDriver(driverID, personID, createdByUserID, createdDate);
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
                    else
                    {
                        return false;
                    }
                //case enMode.Update:
                //    return _Update();
                default:
                    return false;
            }
        }

        public static DataTable GetAllDriversList()
        {
            return clsDriverData.GetAllDriversList();
        }
    }
}