using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper.Classes;

namespace DVLD_DAL.Applications.DrivingLicenseServices
{
    public static class clsLicenseClassData
    {
        #region Sync Methods
        //************** R **************\\
        public static DataTable GetLicenseClasses()
        {
            const string query = "SELECT ID = LicenseClassID, Name = ClassName, Description = ClassDescription, Fees = ClassFees FROM LicenseClasses;";

            DataTable testTypes = new DataTable();

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) testTypes.Load(reader);
            }

            return testTypes;
        }

        public static bool Get(ref int LicenseClassID, string ClassName, ref string ClassDescription, ref int MiniumnAllowedAge, ref int DefaultValidityLength, ref float Fees)
        {
            const string query = "SELECT LicenseClassID, ClassDescription, MiniumnAllowedAge, DefaultValidityLength, ClassFees FROM LicenseClasses WHERE ClassName = @ClassName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader.Read())
                {
                    LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));
                    ClassDescription = reader.GetString(reader.GetOrdinal("ClassDescription"));
                    MiniumnAllowedAge = reader.GetByte(reader.GetOrdinal("MinimumAllowedAge"));
                    DefaultValidityLength = reader.GetByte(reader.GetOrdinal("DefaultValidityLength"));
                    Fees = (float)reader.GetDecimal(reader.GetOrdinal("ClassFees"));
                }
            }

            return false;
        }

        public static bool Get(int ID, ref string ClassName, ref string ClassDescription, ref int MiniumnAllowedAge, ref int DefaultValidityLength, ref float Fees)
        {
            const string query = "SELECT ClassName, ClassDescription, MiniumnAllowedAge, DefaultValidityLength, ClassFees FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader.Read())
                {
                    ClassName = reader.GetString(reader.GetOrdinal("ClassName"));
                    ClassDescription = reader.GetString(reader.GetOrdinal("ClassDescription"));
                    MiniumnAllowedAge = reader.GetByte(reader.GetOrdinal("MinimumAllowedAge"));
                    DefaultValidityLength = reader.GetByte(reader.GetOrdinal("DefaultValidityLength"));
                    Fees = (float)reader.GetDecimal(reader.GetOrdinal("ClassFees"));
                }
            }

            return false;
        }


        //************** U **************\\
        public static bool Update(int ID, string ClassName, string Description, float Fees)
        {
            const string query = @"UPDATE LicenseClasses
                                   SET ClassName        = @ClassName, 
                                       ClassDescription = @ClassDescription,
                                       ClassFees        = @ClassFees
                                   WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName },
                new SqlParameter("@ClassDescription", SqlDbType.NVarChar, 500) { Value = Description },
                new SqlParameter("@ClassFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Update(int ID, string ClassName, string Description, int MinimumAllowedAge, int DefaultValidityLength, float Fees)
        {
            const string query = @"UPDATE LicenseClasses
                                   SET ClassName        = @ClassName, 
                                       ClassDescription = @ClassDescription,
                                       ClassFees        = @ClassFees,
                                       MinimumAllowedAge = @MinimumAllowedAge,
                                       DefaultValidityLength = @DefaultValidityLength
                                   WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName },
                new SqlParameter("@ClassDescription", SqlDbType.NVarChar, 500) { Value = Description },
                new SqlParameter("@MinimumAllowedAge", SqlDbType.TinyInt) { Value = MinimumAllowedAge },
                new SqlParameter("@DefaultValidityLength", SqlDbType.TinyInt) { Value = DefaultValidityLength },
                new SqlParameter("@ClassFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }


        //************** D **************\\
        public static bool Delete(int ID)
        {
            const string query = "DELETE FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Delete(string ClassName)
        {
            const string query = "DELETE FROM LicenseClasses WHERE ClassName = @ClassName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion


        #region Async Methods
        //************** R **************\\
        public static async Task<DataTable> GetLicenseClassesAsync()
        {
            const string query = "SELECT ID = LicenseClassID, Name = ClassName, Description = ClassDescription, Fees = ClassFees FROM LicenseClasses;";

            DataTable testTypes = new DataTable();

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) testTypes.Load(reader);
            }

            return testTypes;
        }

        public static async Task<IEnumerable<(int ID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees)>> GetAllAsync()
        {
            const string query = "SELECT LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees FROM LicenseClasses;";

            const int numberOfLicenseClasses = 7;

            List<(int ID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees)> licenseClasses =
                new List<(int ID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees)>(numberOfLicenseClasses);

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (reader is null || !reader.HasRows) return licenseClasses;

                while (await reader.ReadAsync())
                {
                    licenseClasses.Add((ID: reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        ClassName: reader.GetString(reader.GetOrdinal("ClassName")),
                        ClassDescription: reader.GetString(reader.GetOrdinal("ClassDescription")),
                        MinimumAllowedAge: reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                        DefaultValidityLength: reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                        Fees: (float)reader.GetDecimal(reader.GetOrdinal("ClassFees"))
                        ));
                }
            }

            return licenseClasses;
        }

        public static async Task<(int LicenseClassID, string ClassDescription, int MiniumnAllowedAge, int DefaultValidityLength, float Fees, bool IsFound)> GetAsync(string ClassName)
        {
            const string query = "SELECT LicenseClassID, ClassDescription, MiniumnAllowedAge, DefaultValidityLength, ClassFees FROM LicenseClasses WHERE ClassName = @ClassName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (await reader.ReadAsync())
                    return (LicenseClassID: reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        ClassDescription: reader.GetString(reader.GetOrdinal("ClassDescription")),
                        MiniumnAllowedAge: reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                        DefaultValidityLength: reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                        Fees: (float)reader.GetDecimal(reader.GetOrdinal("ClassFees")),
                        IsFound: true);
            }

            return (LicenseClassID: -1, ClassDescription: string.Empty, MiniumnAllowedAge: -1, DefaultValidityLength: -1, Fees: .0f, IsFound: false);
        }
        
        public static async Task<(string ClassName, string ClassDescription, int MiniumnAllowedAge, int DefaultValidityLength, float Fees, bool IsFound)> GetAsync(int ID)
        {
            const string query = "SELECT ClassName, ClassDescription, MiniumnAllowedAge, DefaultValidityLength, ClassFees FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (await reader.ReadAsync())
                    return (ClassName: reader.GetString(reader.GetOrdinal("ClassName")),
                        ClassDescription: reader.GetString(reader.GetOrdinal("ClassDescription")),
                        MiniumnAllowedAge: reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                        DefaultValidityLength: reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                        Fees: (float)reader.GetDecimal(reader.GetOrdinal("ClassFees")),
                        IsFound: true);
            }

            return (ClassName: "", ClassDescription: "", MiniumnAllowedAge: -1, DefaultValidityLength: -1, Fees: .0f, IsFound: false);
        }


        //************** U **************\\
        public static async Task<bool> UpdateAsync(int ID, string ClassName, string Description, float Fees)
        {
            const string query = @"UPDATE LicenseClasses
                                   SET ClassName        = @ClassName, 
                                       ClassDescription = @ClassDescription,
                                       ClassFees        = @ClassFees
                                   WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName },
                new SqlParameter("@ClassDescription", SqlDbType.NVarChar, 500) { Value = Description },
                new SqlParameter("@ClassFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static async Task<bool> UpdateAsync(int ID, string ClassName, string Description, int MinimumAllowedAge, int DefaultValidityLength, float Fees)
        {
            const string query = @"UPDATE LicenseClasses
                                   SET ClassName        = @ClassName, 
                                       ClassDescription = @ClassDescription,
                                       ClassFees        = @ClassFees,
                                       MinimumAllowedAge = @MinimumAllowedAge,
                                       DefaultValidityLength = @DefaultValidityLength
                                   WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName },
                new SqlParameter("@ClassDescription", SqlDbType.NVarChar, 500) { Value = Description },
                new SqlParameter("@MinimumAllowedAge", SqlDbType.TinyInt) { Value = MinimumAllowedAge },
                new SqlParameter("@DefaultValidityLength", SqlDbType.TinyInt) { Value = DefaultValidityLength },
                new SqlParameter("@ClassFees", SqlDbType.SmallMoney) { Value = Fees }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }


        //************** D **************\\
        public static async Task<bool> DeleteAsync(int ID)
        {
            const string query = "DELETE FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = ID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> DeleteAsync(string ClassName)
        {
            const string query = "DELETE FROM LicenseClasses WHERE ClassName = @ClassName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClassName", SqlDbType.NVarChar, 50) { Value = ClassName } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion
    }
}
