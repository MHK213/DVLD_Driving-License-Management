using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    internal class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo;

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            IsActive = true;

            mode = enMode.AddNew;
        }

        public clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;

            PersonInfo = clsPerson.Find(personID);

            mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUserInfo()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }

        private bool _UpdateUserPassword()
        {
            return clsUserData.ChangePassword(this.UserID, this.Password);
        }

        static public clsUser FindByUserID(int userID)
        {
            string userName = "", password = "";
            bool isActive = true;
            int personID = -1;

            bool isFound = clsUserData.GetUserInfoByUserID(userID, ref personID, ref userName, ref password, ref isActive);

            if (isFound)
                return new clsUser(userID, personID, userName, password, isActive);
            else
                return null;
        }

        static public clsUser FindByPersonID(int personID)
        {
            string userName = "", password = "";
            bool isActive = true;
            int userID = -1;

            bool isFound = clsUserData.GetUserInfoByPersonID(personID, ref userID, ref userName, ref password, ref isActive);

            if (isFound)
                return new clsUser(userID, personID, userName, password, isActive);
            else
                return null;
        }

        static public clsUser FindByUserNameAndPassword(string userName, string password)
        {
            bool isActive = true;
            int userID = -1, personID = -1;

            bool isFound = clsUserData.GetUserInfoByUserNameAndPassword(userName, password, ref userID, ref personID, ref isActive);

            if (isFound)
                return new clsUser(userID, personID, userName, password, isActive);
            else
                return null;
        }

        static public bool DeleteUser(int userID)
        {
            return (clsUserData.DeleteUser(userID));
        }

        static public bool IsUserExist(int userID)
        {
            return (clsUserData.IsUserExist(userID));
        }

        static public bool IsUserExist(string userName)
        {
            return (clsUserData.IsUserExist(userName));
        }

        static public bool IsUserExistForPersonID(int personID)
        {
            return (clsUserData.IsUserExistForPersonID(personID));
        }

        static public DataTable GetAllUsersList()
        {
            return clsUserData.GetAllUsersList();
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return (_UpdateUserInfo());
                default:
                    return false;
            }
        }
    }
}