using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HelperClasses.Extensions;

namespace DVLD_DAL.Applications.ManageApplicationTypes
{
    public static class clsApplicationTypeData
    {
        #region Sync Methods
        //************** R **************\\
        public static int Get(string Title)
        {
            const string query = @"SELECT ApplicationTypeID FROM ApplicationTypes WHERE ApplicationTypeTitle = @ApplicationTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationTypeTitle", SqlDbType.NVarChar, 150) { Value = Title } };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int ApplicationTypeID) ? ApplicationTypeID : -1;
        }

        public static string Get(int ID)
        {
            const string query = @"SELECT ApplicationTypeTitle FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ID } };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result?.ToString() ?? string.Empty;
        }

        public static DataTable GetApplicationTypes()
        {
            const string query = "SELECT ID = ApplicationTypeID, Title = ApplicationTypeTitle, Fees = ApplicationFees FROM ApplicationTypes;";

            DataTable applicationTypes = new DataTable();

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) applicationTypes.Load(reader);
            }

            return applicationTypes;
        }


        //************** U **************\\
        public static bool Update(int ID, float Fees)
        {
            const string query = "UPDATE ApplicationTypes SET ApplicationFees = @ApplicationFees WHERE ApplicationTypeID = @ApplicationTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Update(string Title, float Fees)
        {
            const string query = "UPDATE ApplicationTypes SET ApplicationFees = @ApplicationFees WHERE ApplicationTypeTitle = @ApplicationTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationTypeTitle", SqlDbType.NVarChar, 150) { Value = Title },
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Update(int ID, string Title, float Fees)
        {
            const string query = @"UPDATE ApplicationTypes 
                                   SET ApplicationTypeTitle = @ApplicationTypeTitle, 
                                       ApplicationFees = @ApplicationFees 
                                   WHERE ApplicationTypeID = @ApplicationTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ApplicationTypeTitle", SqlDbType.NVarChar, 150) { Value = Title },
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion


        #region Async Methods
        //************** R **************\\
        public static async Task<int> GetAsync(string Title)
        {
            const string query = @"SELECT ApplicationTypeID FROM ApplicationTypes WHERE ApplicationTypeTitle = @ApplicationTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationTypeTitle", SqlDbType.NVarChar, 150) { Value = Title } };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int ApplicationTypeID) ? ApplicationTypeID : -1;
        }

        public static async Task<string> GetAsync(int ID)
        {
            const string query = @"SELECT ApplicationTypeTitle FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ID } };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result?.ToString() ?? string.Empty;
        }

        public static async Task<DataTable> GetApplicationTypesAsync()
        {
            const string query = "SELECT ID = ApplicationTypeID, Title = ApplicationTypeTitle, Fees = ApplicationFees FROM ApplicationTypes;";

            DataTable applicationTypes = new DataTable();

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) applicationTypes.Load(reader);
            }

            return applicationTypes;
        }


        //************** U **************\\
        public static async Task<bool> UpdateAsync(int ID, float Fees)
        {
            const string query = "UPDATE ApplicationTypes SET ApplicationFees = @ApplicationFees WHERE ApplicationTypeID = @ApplicationTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> UpdateAsync(string Title, float Fees)
        {
            const string query = "UPDATE ApplicationTypes SET ApplicationFees = @ApplicationFees WHERE ApplicationTypeTitle = @ApplicationTypeTitle;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationTypeTitle", SqlDbType.NVarChar, 150) { Value = Title },
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> UpdateAsync(int ID, string Title, float Fees)
        {
            const string query = @"UPDATE ApplicationTypes 
                                   SET ApplicationTypeTitle = @ApplicationTypeTitle, 
                                       ApplicationFees = @ApplicationFees 
                                   WHERE ApplicationTypeID = @ApplicationTypeID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ApplicationTypeTitle", SqlDbType.NVarChar, 150) { Value = Title },
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion
    }
}
