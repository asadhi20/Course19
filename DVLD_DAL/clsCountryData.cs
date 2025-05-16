using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
using Helper.Classes;
using System.Collections.Generic;

namespace DVLD_DAL
{
    public sealed class clsCountryData
    {
        #region Sync Methods
        public static int GetCountry(string Name)
        {
            string query = @"SELECT CountryID FROM Countries WHERE CountryName = @CountryName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CountryName", SqlDbType.NVarChar, 50) { Value = Name } };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int CountryID) ? CountryID : -1;
        }
        
        public static string GetCountry(int ID)
        {
            string query = @"SELECT CountryName FROM Countries WHERE CountryID = @CountryID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CountryID", SqlDbType.Int) { Value = ID } };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result.ToString() ?? string.Empty;
        }

        public static string[] GetCountriesNames()
        {
            string query = "SELECT CountryName FROM Countries;";
            DataTable countries = clsSqlDBExecutor.ExecuteDataAdapter(clsDBSettings.CnnString("DVLD_DB"), query);

            string[] CountriesName = new string[countries.Rows.Count];

            for (int i = 0; i < countries.Rows.Count; i++) CountriesName[i] = countries.Rows[i][0].ToString();

            return CountriesName;
        }


        public static IEnumerable<(int ID, string Name)> GetCountries()
        {
            string query = "SELECT CountryID, CountryName FROM Countries;";

            const int numberOfCountryes = 193;
            (int ID, string Name)[] countries = new (int ID, string Name)[numberOfCountryes];

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(ConfigurationManager.ConnectionStrings["DVLD"].ConnectionString, query))
            {
                for (int i = 0; reader.Read(); i++)
                {
                    countries[i] = (ID: reader.GetInt32(reader.GetOrdinal("CountryID")), Name: reader.GetString(reader.GetOrdinal("CountryName")));
                }
            }

            foreach ((int ID, string Name) country in countries) yield return country;
        }
        #endregion


        #region Async Methods
        public static async Task<int> GetCountryAsync(string Name)
        {
            string query = @"SELECT CountryID FROM Countries WHERE CountryName = @CountryName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CountryName", SqlDbType.NVarChar, 50) { Value = Name } };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int CountryID) ? CountryID : -1;
        }
        
        public static async Task<string> GetCountryAsync(int ID)
        {
            string query = @"SELECT CountryName FROM Countries WHERE CountryID = @CountryID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CountryID", SqlDbType.Int) { Value = ID } };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result.ToString() ?? "";
        }

        public static async Task<string[]> GetCountriesNamesAsync()
        {
            string query = "SELECT CountryName FROM Countries;";

            DataTable countries = await clsSqlDBExecutor.ExecuteDataAdapterAsync(clsDBSettings.CnnString("DVLD_DB"), query);

            string[] CountriesName = new string[countries.Rows.Count];

            for (int i = 0; i < countries.Rows.Count; i++) CountriesName[i] = countries.Rows[i][0].ToString();

            return CountriesName;
        }


        public static async Task<IEnumerable<(int ID, string Name)>> GetCountriesAsync()
        {
            string query = "SELECT CountryID, CountryName FROM Countries;";

            const int numberOfCountryes = 193;
            List<(int ID, string Name)> countries = new List<(int ID, string Name)>(numberOfCountryes);

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                while (await reader.ReadAsync())
                {
                    countries.Add((ID: reader.GetInt32(reader.GetOrdinal("CountryID")), Name: reader.GetString(reader.GetOrdinal("CountryName"))));
                }
            }

            return countries;
        }
        #endregion
    }
}
