using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int personID, ref string nationalNo, ref string firstName, ref string secondName, ref string thirdName, ref string lastName,
            ref DateTime dateOfBirth, ref byte gender, ref string address, ref string phone, ref string email, ref int nationalityCountryID, ref string imagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    nationalNo = (string)reader["NationalNo"];
                    firstName = (string)reader["FirstName"];
                    secondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] == DBNull.Value) thirdName = "";
                    else thirdName = (string)reader["ThirdName"];

                    lastName = (string)reader["LastName"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gender = (byte)reader["Gendor"];
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];

                    if (reader["Email"] == DBNull.Value) email = "";
                    else email = (string)reader["Email"];


                    nationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] == DBNull.Value) imagePath = "";
                    else imagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(string nationalNo, ref int personID, ref string firstName, ref string secondName, ref string thirdName, ref string lastName,
            ref DateTime dateOfBirth, ref byte gender, ref string address, ref string phone, ref string email, ref int nationalityCountryID, ref string imagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", nationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personID = (int)reader["PersonID"];
                    firstName = (string)reader["FirstName"];
                    secondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] == DBNull.Value) thirdName = "";
                    else thirdName = (string)reader["ThirdName"];

                    lastName = (string)reader["LastName"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gender = (byte)reader["Gendor"];
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];

                    if (reader["Email"] == DBNull.Value) email = "";
                    else email = (string)reader["Email"];


                    nationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] == DBNull.Value) imagePath = "";
                    else imagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewPerson(string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, byte gender,
            string address, string phone, string email, int nationalityCountryID, string imagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, 
                                ImagePath)
                                VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, 
                                @ImagePath)
                                SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@SecondName", secondName);

            if (string.IsNullOrEmpty(thirdName) || thirdName.ToUpper() == "NULL") command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else command.Parameters.AddWithValue("@ThirdName", thirdName);

            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gendor", gender);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);

            if (string.IsNullOrEmpty(email) || email.ToUpper() == "NULL") command.Parameters.AddWithValue("@Email", DBNull.Value);
            else command.Parameters.AddWithValue("@Email", email);

            command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);

            if (string.IsNullOrEmpty(imagePath) || imagePath.ToUpper() == "NULL") command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else command.Parameters.AddWithValue("@ImagePath", imagePath);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) PersonID = insertedID;
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePerson(int personID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth,
            byte gender, string address, string phone, string email, int nationalityCountryID, string imagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE People SET NationalNo = @NationalNo, FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName, LastName = @LastName, 
                            DateOfBirth = @DateOfBirth, Gendor = @Gendor, Address = @Address, Phone = @Phone, Email = @Email, NationalityCountryID = @NationalityCountryID, 
                            ImagePath = @ImagePath 
                            WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@SecondName", secondName);

            if (string.IsNullOrEmpty(thirdName) || thirdName.ToUpper() == "NULL") command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else command.Parameters.AddWithValue("@ThirdName", thirdName);

            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gendor", gender);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);

            if (string.IsNullOrEmpty(email) || email.ToUpper() == "NULL") command.Parameters.AddWithValue("@Email", DBNull.Value);
            else command.Parameters.AddWithValue("@Email", email);

            command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);

            if (string.IsNullOrEmpty(imagePath) || imagePath.ToUpper() == "NULL") command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else command.Parameters.AddWithValue("@ImagePath", imagePath);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM People
                            WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllPeopleList()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.Gendor, 
                             CASE People.Gendor
                                WHEN 0 THEN 'Male'
                                ELSE 'Female'
                            END AS GendorCaption,
                            People.DateOfBirth, People.NationalityCountryID, Countries.CountryName, People.Phone, People.Email, People.ImagePath
                            FROM People
                            INNER JOIN Countries
                            ON People.NationalityCountryID = Countries.CountryID
                            ORDER BY People.FirstName;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) dt.Load(reader);

                reader.Close();
            }
            catch { }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsPersonExist(int personID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null) isFound = true;
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPersonExist(string nationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", nationalNo);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null) isFound = true;
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}