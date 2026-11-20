using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {
        public static bool GetDriverInfoByDriverID(int driverID, ref int personID, ref int createdByUserID, ref DateTime createdDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", driverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personID = (int)reader["PersonID"];
                    createdByUserID = (int)reader["CreatedByUserID"];
                    createdDate = (DateTime)reader["CreatedDate"];

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

        public static bool GetDriverInfoByPersonID(int personID, ref int driverID, ref int createdByUserID, ref DateTime createdDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    driverID = (int)reader["DriverID"];
                    createdByUserID = (int)reader["CreatedByUserID"];
                    createdDate = (DateTime)reader["CreatedDate"];

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

        public static int AddNewDriver(int personID, int createdByUserID)
        {
            int driverID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                                VALUES (@PersonID, @CreatedByUserID, @CreatedDate)
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) driverID = insertedID;
            }
            catch {}
            finally
            {
                connection.Close();
            }

            return driverID;
        }

        //public static bool UpdateDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        //{
        //    int rowsAffected = 0;

        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"UPDATE Drivers SET PersonID = @PersonID, CreatedByUserID = @CreatedByUserID, CreatedDate = @CreatedDate
        //                    WHERE DriverID = @DriverID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@DriverID", driverID);
        //    command.Parameters.AddWithValue("@PersonID", personID);
        //    command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
        //    command.Parameters.AddWithValue("@CreatedDate", createdDate);

        //    try
        //    {
        //        connection.Open();

        //        rowsAffected = command.ExecuteNonQuery();
        //    }
        //    catch { }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return (rowsAffected > 0);
        //}

        public static DataTable GetAllDriversList()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers_View ORDER BY FullName;";

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
    }
}