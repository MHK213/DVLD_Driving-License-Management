using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {
        public static bool GetLicenseInfoByLicenseID(int licenseID, ref int applicationID, ref int driverID, ref int licenseClass, ref DateTime issueDate, ref DateTime expirationDate, ref string notes,
            ref decimal paidFees, ref bool isActive, ref byte issueReason, ref int createdByUser)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    applicationID = (int)reader["ApplicationID"];
                    driverID = (int)reader["DriverID"];
                    licenseClass = (int)reader["LicenseClass"];
                    issueDate = (DateTime)reader["IssueDate"];
                    expirationDate = (DateTime)reader["ExpirationDate"];

                    notes = (reader["Notes"] == DBNull.Value) ? "" : (string)reader["Notes"];

                    paidFees = (decimal)reader["PaidFees"];
                    isActive = (bool)reader["IsActive"];
                    issueReason = (byte)reader["IssueReason"];
                    createdByUser = (int)reader["CreatedByUser"];

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

        public static int AddNewLicense(int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, byte issueReason, 
            int createdByUser)
        {
            int licenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUser)
                                VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUser)
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", applicationID);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@LicenseClass", licenseClass);
            command.Parameters.AddWithValue("@IssueDate", issueDate);
            command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

            if(string.IsNullOrWhiteSpace(notes)) command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else command.Parameters.AddWithValue("@Notes", notes);

            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@IsActive", isActive);
            command.Parameters.AddWithValue("@IssueReason", issueReason);
            command.Parameters.AddWithValue("@CreatedByUser", createdByUser);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) licenseID = insertedID;
            }
            catch { }
            finally
            {
                connection.Close();
            }

            return licenseID;
        }

        public static bool UpdateLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, 
            byte issueReason, int createdByUser)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses 
                                SET applicationID = @ApplicationID, DriverID = @DriverID, LicenseClass = @LicenseClass, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, 
                                    Notes = @Notes, PaidFees = @PaidFees, IsActive = @IsActive, IssueReason = @IssueReason, CreatedByUser = @CreatedByUser
                            WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseID);
            command.Parameters.AddWithValue("@ApplicationID", applicationID);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@LicenseClass", licenseClass);
            command.Parameters.AddWithValue("@IssueDate", issueDate);
            command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

            if (string.IsNullOrWhiteSpace(notes)) command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else command.Parameters.AddWithValue("@Notes", notes);

            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@IsActive", isActive);
            command.Parameters.AddWithValue("@IssueReason", issueReason);
            command.Parameters.AddWithValue("@CreatedByUser", createdByUser);

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

        public static DataTable GetAllLicensesList()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses;";

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

        public static DataTable GetDriverLicensesList(int driverID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive 
                             FROM Licenses
                             INNER JOIN LicenseClasses
                                ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                             WHERE DriverID = @DriverID
                             ORDER BY IsActive DESC, ExpirationDate DESC;";

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

        public static int GetActiveLicenseIDByPersonID(int personID, int licenseClassID)
        {
            int activeLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicenseID FROM Licenses
                             INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
                             WHERE Drivers.PersonID = @PersonID AND Licenses.LicenseClass = @LicenseClass AND Licenses.IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@LicenseClass", licenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) activeLicenseID = insertedID;
            }
            catch { }
            finally
            {
                connection.Close();
            }

            return activeLicenseID;
        }

        public static bool DeactivateLicense(int licenseID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses 
                                SET IsActive = 0
                            WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch {}
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}