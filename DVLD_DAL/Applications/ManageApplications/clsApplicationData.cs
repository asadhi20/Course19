using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helper.Classes;
using System.Linq;
using System.Text;
using System.Net;
using DVLD_DAL.People;

namespace DVLD_DAL.Applications.ManageApplications
{
    public static class clsApplicationData
    {
        #region Public Sync Methods
        //************** C **************\\
        public static int Add(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            const string query = "SP_AddNewApplication";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },

                new SqlParameter("@NewApplicationID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int newApplicationID = -1;
            const int appicationIDParamIndex = 7;

            try
            {
                int rowsAffected = clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure);
                if (rowsAffected > 0)
                {
                    newApplicationID = parameters[appicationIDParamIndex].Value != DBNull.Value
                        ? Convert.ToInt32(parameters[appicationIDParamIndex].Value) : -1;
                }
            }
            catch { }

            return newApplicationID;
        }


        //************** R **************\\
        public static bool Get(int ApplicationID, ref int ApplicantPersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address, ref string Phone,
            ref string Email, ref int NationalityCountryID, ref string ImagePath, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            const string query = "SP_GetSingleApplicationDetails_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure))
            {
                if (reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ApplicantPersonID = reader.GetInt32(reader.GetOrdinal("ApplicantPersonID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                    ThirdName = reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName);
                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    Email = reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail);
                    NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                    ImagePath = reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath);
                    ApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate"));
                    ApplicationTypeID = reader.GetInt32(reader.GetOrdinal("ApplicationTypeID"));
                    ApplicationStatus = reader.GetByte(reader.GetOrdinal("ApplicationStatus"));
                    LastStatusDate = reader.GetDateTime(reader.GetOrdinal("LastStatusDate"));
                    PaidFees = (float)reader.GetDecimal(reader.GetOrdinal("PaidFees"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));

                    return true;
                }
            }
            return false;
        }
        
        public static bool Get(ref int ApplicationID, int ApplicantPersonID, ref string NationalNo, ref string FirstName, ref string SecondName, 
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address, ref string Phone, 
            ref string Email, ref int NationalityCountryID, ref string ImagePath, ref DateTime ApplicationDate, ref int ApplicationTypeID, 
            ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, int CreatedByUserID)
        {
            const string query = "SP_SingleGetApplicationDetails_By_ApplicantPersonID_CreatedByUserID";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID }
            };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure))
            {
                if (reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                    ThirdName = reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName);
                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    Email = reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail);
                    NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                    ImagePath = reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath);
                    ApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate"));
                    ApplicationTypeID = reader.GetInt32(reader.GetOrdinal("ApplicationTypeID"));
                    ApplicationStatus = reader.GetByte(reader.GetOrdinal("ApplicationStatus"));
                    LastStatusDate = reader.GetDateTime(reader.GetOrdinal("LastStatusDate"));
                    PaidFees = (float)reader.GetDecimal(reader.GetOrdinal("PaidFees"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));

                    return true;
                }
            }

            return false;
        }

        public static DataTable GetApplications()
        {
            const string query = "SP_GetAllApplications";

            DataTable testTypes = new DataTable();

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, commandType: CommandType.StoredProcedure))
            {
                if (!(reader is null) && reader.HasRows) testTypes.Load(reader);
            }

            return testTypes;
        }



        //************** U **************\\
        public static bool Update(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            const string query = "SP_UpdateApplication_By_ID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }


        //************** D **************\\
        public static bool Delete(int ID)
        {
            const string query = "SP_DeleteApplication_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ID } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }
        #endregion


        #region Public Async Methods
        //************** C **************\\
        public static async Task<int> AddAsync(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            const string query = "SP_AddNewApplication";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },

                new SqlParameter("@NewApplicationID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int newApplicationID = -1;
            const int appicationIDParamIndex = 7;

            try
            {
                int rowsAffected = await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure);
                if (rowsAffected > 0)
                {
                    newApplicationID = parameters[appicationIDParamIndex].Value != DBNull.Value
                        ? Convert.ToInt32(parameters[appicationIDParamIndex].Value) : -1;
                }
            }
            catch { }

            return newApplicationID;
        }


        //************** R **************\\
        public static async Task<(int ApplicantPersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, bool IsFound)> GetAsync(int ID)
        {
            const string query = "SP_GetSingleApplicationDetails_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure))
            {
                if (await reader.ReadAsync())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ApplicantPersonID: reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName: reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName: reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName),
                        LastName: reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Phone: reader.GetString(reader.GetOrdinal("Phone")),
                        Email: reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath),
                        ApplicationDate: reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                        ApplicationTypeID: reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                        ApplicationStatus: reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                        LastStatusDate: reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                        CreatedByUserID: reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                        IsFound: true);
                }
            }
            
            return (ApplicantPersonID: -1, NationalNo: "", FirstName: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", ApplicationDate: DateTime.MaxValue, 
                ApplicationTypeID: -1, ApplicationStatus: 0, LastStatusDate: DateTime.MaxValue, PaidFees: .0f, CreatedByUserID: -1, IsFound: false);
        }
        
        public static async Task<(int ApplicationID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, bool IsFound)> GetAsync(int ApplicantPersonID, int CreatedByUserID)
        {
            const string query = "SP_SingleGetApplicationDetails_By_ApplicantPersonID_CreatedByUserID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID }
            };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure))
            {
                if (await reader.ReadAsync())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ApplicationID: reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName: reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName: reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName),
                        LastName: reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Phone: reader.GetString(reader.GetOrdinal("Phone")),
                        Email: reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath),
                        ApplicationDate: reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                        ApplicationTypeID: reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                        ApplicationStatus: reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                        LastStatusDate: reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                        IsFound: true);
                }
            }
            
            return (ApplicationID: -1, NationalNo: "", FirstName: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue, 
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", ApplicationDate: DateTime.MaxValue, 
                ApplicationTypeID: -1, ApplicationStatus: 0, LastStatusDate: DateTime.MaxValue, PaidFees: .0f, IsFound: false);
        }


        public static async Task<IEnumerable<(int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone,
            string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)>> GetAllAsync()
        {
            const string query = "SP_GetAllApplicationsDetails";

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, commandType: CommandType.StoredProcedure))
            {
                if (reader is null || !reader.HasRows)
                    return new List<(int, int, string, string, string, string, string, DateTime, bool, string, string, string, int, string, DateTime, int, byte, DateTime, float, int)>();

                const int minimumNumberOfApplications = 20;

                List<(int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                    DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate,
                    int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)> applications =
                new List<(int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                    DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate,
                    int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)>(minimumNumberOfApplications);

                int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                while (await reader.ReadAsync())
                {
                    applications.Add((ApplicationID: reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                        ApplicantPersonID: reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName: reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName: reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName),
                        LastName: reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Phone: reader.GetString(reader.GetOrdinal("Phone")),
                        Email: reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath),
                        ApplicationDate: reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                        ApplicationTypeID: reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                        ApplicationStatus: reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                        LastStatusDate: reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                        CreatedByUserID: reader.GetInt32(reader.GetOrdinal("CreatedByUserID")))
                        );
                }

                return applications;
            }
        }

        public static async Task<DataTable> GetApplicationsAsync()
        {
            const string query = "SP_GetAllApplications";

            DataTable testTypes = new DataTable();

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(
                clsDBSettings.CnnString("DVLD_DB"), 
                query, commandType: CommandType.StoredProcedure))
            {
                if (!(reader is null) && reader.HasRows) testTypes.Load(reader);
            }

            return testTypes;
        }



        //************** U **************\\
        public static async Task<bool> UpdateAsync(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            const string query = "SP_UpdateApplication_By_ID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }


        //************** D **************\\
        public static async Task<bool> DeleteAsync(int ID)
        {
            const string query = "SP_DeleteApplication_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }
        #endregion
    }
}
