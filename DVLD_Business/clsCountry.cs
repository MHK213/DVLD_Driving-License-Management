using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsCountry
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }

        clsCountry(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }

        static public clsCountry Find(int countryID)
        {
            string countryName = "";

            if (clsCountryData.GetCountryByID(countryID, ref countryName))
            {
                return new clsCountry(countryID, countryName);
            }
            else
            {
                return null;
            }

        }

        static public clsCountry Find(string countryName)
        {
            int countryID = -1;

            if (clsCountryData.GetCountryByName(countryName, ref countryID))
            {
                return new clsCountry(countryID, countryName);
            }
            else
            {
                return null;
            }

        }

        static public DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountriesList();
        }
    }
}