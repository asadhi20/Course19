using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HelperClasses.Extensions;

namespace DVLD_DAL.Applications.ManageTestTypes
{
    public static class clsTestTypeData
    {
        #region Sync Methods
        //************** R **************\\
        public static DataTable GetTestTypes()
        {
            const string query = "SELECT ID = TestTypeID, Title = TestTypeTitle, Description = TestTypeDescription, Fees = TestTypeFees FROM TestTypes;";

            DataTable testTypes = new DataTable();

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) testTypes.Load(reader);
            }

            return testTypes;
        }

        public static int GetTestType(string Title)
        {
            const string query = "SELECT TestTypeID FROM TestTypes WHERE TestTypeTitle = @TestTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestTypeTitle", SqlDbType.NVarChar, 150) { Value = Title } };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int TestTypeID) ? TestTypeID : -1;
        }

        public static string GetTestType(int ID)
        {
            const string query = "SELECT TestTypeTitle FROM TestTypes WHERE TestTypeID = @TestTypeID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = ID } };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result.ToString() ?? string.Empty;
        }


        //************** U **************\\
        public static bool Update(int ID, float Fees)
        {
            const string query = "UPDATE TestTypes SET TestTypeFees  = @TestTypeFees WHERE TestTypeID  = @TestTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@TestTypeFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Update(string Title, float Fees)
        {
            const string query = "UPDATE TestTypes SET TestTypeFees = @TestTypeFees WHERE TestTypeTitle = @TestTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestTypeTitle", SqlDbType.NVarChar, 100) { Value = Title },
                new SqlParameter("@TestTypeFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Update(int ID, string Title, string Description, float Fees)
        {
            const string query = @"UPDATE TestTypes 
                                   SET TestTypeFees = @TestTypeFees, 
                                       TestTypeTitle = @TestTypeTitle
                                       TestTypeDescription = @TestTypeDescription
                                   WHERE TestTypeID = @TestTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = Title },
                new SqlParameter("@TestTypeTitle", SqlDbType.NVarChar, 100) { Value = Title },
                new SqlParameter("@TestTypeDescription", SqlDbType.NVarChar, 500) { Value = Description },
                new SqlParameter("@TestTypeFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion


        #region Async Methods
        //************** R **************\\
        public static async Task<DataTable> GetTestTypesAsync()
        {
            const string query = "SELECT ID = TestTypeID, Title = TestTypeTitle, Description = TestTypeDescription, Fees = TestTypeFees FROM TestTypes;";

            DataTable testTypes = new DataTable();

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) testTypes.Load(reader);
            }

            return testTypes;
        }

        public static async Task<int> GetTestTypeAsync(string Title)
        {
            const string query = "SELECT TestTypeID FROM TestTypes WHERE TestTypeTitle = @TestTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestTypeTitle", SqlDbType.NVarChar, 100) { Value = Title } };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int TestTypeID) ? TestTypeID : -1;
        }

        public static async Task<string> GetTestTypeAsync(int ID)
        {
            const string query = "SELECT TestTypeTitle FROM TestTypes WHERE TestTypeID = @TestTypeID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = ID } };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result.ToString() ?? "";
        }


        //************** U **************\\
        public static async Task<bool> UpdateAsync(int ID, float Fees)
        {
            const string query = "UPDATE TestTypes SET TestTypeFees  = @TestTypeFees WHERE TestTypeID  = @TestTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@TestTypeFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> UpdateAsync(string Title, float Fees)
        {
            const string query = "UPDATE TestTypes SET TestTypeFees = @TestTypeFees WHERE TestTypeTitle = @TestTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestTypeTitle", SqlDbType.NVarChar, 100) { Value = Title },
                new SqlParameter("@TestTypeFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> UpdateAsync(int ID, string Title, string Description, float Fees)
        {
            const string query = @"UPDATE TestTypes 
                                   SET TestTypeFees = @TestTypeFees, 
                                       TestTypeTitle = @TestTypeTitle
                                       TestTypeDescription = @TestTypeDescription
                                   WHERE TestTypeID = @TestTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = Title },
                new SqlParameter("@TestTypeTitle", SqlDbType.NVarChar, 100) { Value = Title },
                new SqlParameter("@TestTypeDescription", SqlDbType.NVarChar, 500) { Value = Description },
                new SqlParameter("@TestTypeFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion
    }
}
