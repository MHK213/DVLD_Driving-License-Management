using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode mode { get; private set; }

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {SecondName} {ThirdName} {LastName}"; }
        }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        private string _ImagePath;
        public string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                _ImagePath = value;
            }
        }

        public clsCountry CountryInfo;

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            mode = enMode.AddNew;
        }

        public clsPerson(int personID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, byte gender, string address, string phone, string email,
            int nationalityByCountryID, string imagePath)
        {
            this.PersonID = personID;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityByCountryID;
            this.CountryInfo = clsCountry.Find(nationalityByCountryID);
            this.ImagePath = imagePath;

            mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone,
                this.Email, this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone,
                this.Email, this.NationalityCountryID, this.ImagePath);
        }

        static public clsPerson Find(int personID)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            byte gender = 0;
            int nationalityByCountryID = 0;

            bool isFound = clsPersonData.GetPersonInfoByID(personID, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email,
                ref nationalityByCountryID, ref imagePath);

            if (isFound)
                return new clsPerson(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, nationalityByCountryID, imagePath);
            else
                return null;
        }

        static public clsPerson Find(string nationalNo)
        {
            int personID = -1;
            string firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            byte gender = 0;
            int nationalityByCountryID = 0;

            bool isFound = clsPersonData.GetPersonInfoByNationalNo(nationalNo, ref personID, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email,
                ref nationalityByCountryID, ref imagePath);

            if (isFound)
                return new clsPerson(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, nationalityByCountryID, imagePath);
            else
                return null;
        }

        static public bool DeletePerson(int PersonID)
        {
            return (clsPersonData.DeletePerson(PersonID));
        }

        static public bool IsPersonExist(int PersonID)
        {
            return (clsPersonData.IsPersonExist(PersonID));
        }

        static public bool IsPersonExist(string nationalNo)
        {
            return (clsPersonData.IsPersonExist(nationalNo));
        }

        static public DataTable GetAllPeopleList()
        {
            return clsPersonData.GetAllPeopleList();
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return (_UpdatePerson());
                default:
                    return false;
            }
        }
    }
}