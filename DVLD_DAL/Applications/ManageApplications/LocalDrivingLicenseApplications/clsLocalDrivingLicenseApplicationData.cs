using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Helper.Classes;
using DVLD_DAL.People;

namespace DVLD_DAL.Applications.ManageApplications.LocalDrivingLicenseApplications
{
    public static class clsLocalDrivingLicenseApplicationData
    {
        #region Public Sync Methods
        //************** C **************\\
        public static (int ApplicationID, int LocalDrivingLicenseApplicationID) Add(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            const string query = "SP_AddNewLocalDLApplication";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID },
                
                new SqlParameter("@NewApplicationID", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@NewLocalDrivingLicenseApplicationID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int lDLApplicationID = -1, applicationID = -1;
            const int applicationIDParamIndex = 8, localDLApplicationIDParamIndex = 9;

            try
            {
                int rowAffected = clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure);
                if (rowAffected > 0)
                {
                    applicationID = parameters[applicationIDParamIndex].Value != DBNull.Value
                        ? Convert.ToInt32(parameters[applicationIDParamIndex].Value) : -1;

                    lDLApplicationID = parameters[localDLApplicationIDParamIndex].Value != DBNull.Value
                        ? Convert.ToInt32(parameters[localDLApplicationIDParamIndex].Value) : -1;
                }
            }
            catch { }

            return (ApplicationID: applicationID, LocalDrivingLicenseApplicationID: lDLApplicationID);
        }


        //************** R **************\\
        public static DataTable GetAllLDLApplications()
        {
            const string query = "SP_GetAllLcoalDLApplications_View";

            DataTable lDLApplications = new DataTable();

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, null, CommandType.StoredProcedure))
            {
                if (!(reader is null) && reader.HasRows) lDLApplications.Load(reader);
            }

            return lDLApplications;
        }

        public static bool Get(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int ApplicantPersonID,ref string NationalNo, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address, ref string Phone,
            ref string Email, ref int NationalityCountryID, ref string ImagePath, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID, ref int LicenseClassID)
        {
            const string query = "SP_GetLocalDLApplication_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDrivingLicenseApplicationID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure, CommandBehavior.SingleRow))
            {
                if (reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
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
                    PaidFees = (float)reader.GetDecimal(reader.GetOrdinal("ApplicationPaidFees"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("ApplicationCreatedByUserID"));
                    LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));

                    return true;
                }
            }

            return false;
        }

        public static bool GetSingleLcoalDLApplications_View(int ID,ref string DrivingClass, ref string NationalNo, ref string FullName,
            ref DateTime ApplicationDate, ref int PassedTests, ref string Status)
        {
            const string query = "SP_GetSingleLcoalDLApplications_View";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure, CommandBehavior.SingleResult))
            {
                if (reader.Read())
                {
                    DrivingClass = reader.GetString(reader.GetOrdinal("DrivingClass"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    ApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate"));
                    PassedTests = reader.GetInt32(reader.GetOrdinal("PassedTests"));
                    Status = reader.GetString(reader.GetOrdinal("Status"));

                    return true;
                }
            }

            return false;
        }
        
        public static int GetApplicationIDWhenStatusNewOrCompleted(int ApplicantPersonID, int LicenseClassID)
        {
            const string query = "SP_GetSingleApplicationIDWhenStatusNewOrCompleted_By_ApplicantPersonID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure);

            return !(result is null) && int.TryParse(result.ToString(), out int applicationID) ? applicationID : -1;
        }


        public static bool IsExists(int LDLApplicationID, int ApplicationID, int ApplicantPersonID, int LicenseClassID)
        {
            const string query = "SP_IsExistsLocalDLApplication_By_LDLAppID_AppID_ApplicantPersonID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LDLApplicationID },
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return clsSqlDBExecutor.ExecuteScalar(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }

        public static bool IsExists(int LDLApplicationID, int ApplicationID, int LicenseClassID)
        {
            const string query = "SP_IsExistsLocalDLApplication_By_LDLAppID_AppID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LDLApplicationID },
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return clsSqlDBExecutor.ExecuteScalar(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }

        public static bool IsExists(int ApplicantPersonID, byte ApplicationStatus, int LicenseClassID)
        {
            const string query = "SP_IsExistsLocalDLApplication_By_ApplicantPersonID_AppStatus_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return clsSqlDBExecutor.ExecuteScalar(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }

        public static bool IsHasNewOrCompletedApp(int ApplicantPersonID, int LicenseClassID)
        {
            const string query = "SP_IsHasNewOrCompletedLocalDLApplication_By_ApplicantPersonID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return clsSqlDBExecutor.ExecuteScalar(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }



        //************** U **************\\
        public static bool Update(int LDLApplicationID, int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            const string query = "SP_UpdateLocalDLApplication_By_LDLAppID_AppID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LDLApplicationID },
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) > 0;
        }

        public static bool UpdateApplicationStatus(int LocalDLAppID, byte ApplicationStatus)
        {
            const string query = "SP_UpdateLocalDLApplication_ApplicationStatusOnly_By_LocalDLAppID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDLAppID },
                new SqlParameter("@NewApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus } 
            };

            return clsSqlDBExecutor.ExecuteNonQuery(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) > 0;
        }


        //************** D **************\\
        public static bool Delete(int ID)
        {
            const string query = "SP_DeleteLocalDLApplication_By_LDLAppID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = ID } };

            return clsSqlDBExecutor.ExecuteNonQuery(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) > 0;
        }
        #endregion


        #region Public Async Methods
        //************** C **************\\
        public static async Task<(int ApplicationID, int LocalDrivingLicenseApplicationID)> AddAsync(int ApplicantPersonID, DateTime ApplicationDate, 
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            const string query = "SP_AddNewLocalDLApplication";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID },

                new SqlParameter("@NewApplicationID", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@NewLocalDrivingLicenseApplicationID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int lDLApplicationID = -1, applicationID = -1;
            const int applicationIDParamIndex = 8, localDLApplicationIDParamIndex = 9;

            try
            {
                int rowAffected = await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure);
                if (rowAffected > 0)
                {
                    applicationID = parameters[applicationIDParamIndex].Value != DBNull.Value 
                        ? Convert.ToInt32(parameters[applicationIDParamIndex].Value) : -1;

                    lDLApplicationID = parameters[localDLApplicationIDParamIndex].Value != DBNull.Value
                        ? Convert.ToInt32(parameters[localDLApplicationIDParamIndex].Value) : -1;
                }
            }
            catch { }

            return (ApplicationID: applicationID, LocalDrivingLicenseApplicationID: lDLApplicationID);
        }


        //************** R **************\\
        public static async Task<DataTable> GetAllLDLApplicationsAsync()
        {
            const string query = "SP_GetAllLcoalDLApplications_View";

            DataTable lDLApplications = new DataTable();

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, commandType: CommandType.StoredProcedure))
            {
                if (!(reader is null) && reader.HasRows) lDLApplications.Load(reader);
            }

            return lDLApplications;
        }

        public static async Task<IEnumerable<(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID, string NationalNo,
            string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone,
            string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseID)>> GetAllAsync()
        {
            const string query = "SP_GetAllLocalDLApplicationInfo";

            const int minimumNumberOfApplications = 20;

            List<(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName, 
                string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, 
                string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
                byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseID)> applications = 
            new List<(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName, 
                string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, 
                string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
                byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseID)>(minimumNumberOfApplications);

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, commandType: CommandType.StoredProcedure))
            {
                if (reader is null || !reader.HasRows) return applications;

                int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                while (await reader.ReadAsync())
                {
                    applications.Add((LocalDrivingLicenseApplicationID: reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                        ApplicationID: reader.GetInt32(reader.GetOrdinal("ApplicationID")),
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
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("ApplicationPaidFees")),
                        CreatedByUserID: reader.GetInt32(reader.GetOrdinal("ApplicationCreatedByUserID")),
                        LicenseID: reader.GetInt32(reader.GetOrdinal("LicenseClassID"))
                        ));
                }
            }

            return applications;
        }


        public static async Task<(int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName, string SecondName, 
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, 
            int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseID, bool IsFound)> GetAsync(int ID)
        {
            const string query = "SP_GetLocalDLApplication_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(
                clsDBSettings.CnnString("DVLD_DB"), 
                query, parameters, CommandType.StoredProcedure, CommandBehavior.SingleRow))
            {
                if (await reader.ReadAsync())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ApplicationID: reader.GetInt32(reader.GetOrdinal("ApplicationID")),
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
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("ApplicationPaidFees")),
                        CreatedByUserID: reader.GetInt32(reader.GetOrdinal("ApplicationCreatedByUserID")),
                        LicenseID: reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        IsFound: true);
                }
            }

            return (ApplicationID: -1, ApplicantPersonID: -1, NationalNo: "", FirstName: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", ApplicationDate: DateTime.MaxValue, ApplicationTypeID: -1,
                ApplicationStatus: (byte)0, LastStatusDate: DateTime.MaxValue, PaidFees: .0f, CreatedByUserID: -1, LicenseID: -1, IsFound: false);
        }


        public static async Task<(string DrivingClass, string NationalNo, string FullName, DateTime ApplicationDate, 
            int PassedTests, string Status, bool IsFound)> GetSingleLcoalDLApplications_ViewAsync(int ID)
        {
            const string query = "SP_GetSingleLcoalDLApplications_View_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure, CommandBehavior.SingleRow))
            {
                if (await reader.ReadAsync())
                {
                    return (DrivingClass: reader.GetString(reader.GetOrdinal("DrivingClass")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FullName: reader.GetString(reader.GetOrdinal("FullName")),
                        ApplicationDate: reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                        PassedTests: reader.GetInt32(reader.GetOrdinal("PassedTests")),
                        Status: reader.GetString(reader.GetOrdinal("Status")),
                        IsFound: true);
                }
            }

            return (DrivingClass: string.Empty, NationalNo: string.Empty, FullName: string.Empty, ApplicationDate: DateTime.MaxValue, PassedTests: -1, Status: string.Empty, IsFound: false);
        }

        public static async Task<int> GetApplicationIDWhenStatusNewOrCompletedAsync(int ApplicantPersonID, int LicenseClassID)
        {
            const string query = "SP_GetSingleApplicationIDWhenStatusNewOrCompleted_By_ApplicantPersonID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure);

            return !(result is null) && int.TryParse(result.ToString(), out int applicationID) ? applicationID : -1;
        }


        public static async Task<bool> IsExistsAsync(int LDLApplicationID, int ApplicationID, int ApplicantPersonID, int LicenseClassID)
        {
            const string query = "SP_IsExistsLocalDLApplication_By_LDLAppID_AppID_ApplicantPersonID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LDLApplicationID },
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return await clsSqlDBExecutor.ExecuteScalarAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }

        public static async Task<bool> IsExistsAsync(int LDLApplicationID, int ApplicationID, int LicenseClassID)
        {
            const string query = "SP_IsExistsLocalDLApplication_By_LDLAppID_AppID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LDLApplicationID },
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return await clsSqlDBExecutor.ExecuteScalarAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }
        
        public static async Task<bool> IsExistsAsync(int ApplicantPersonID, byte ApplicationStatus, int LicenseClassID)
        {
            const string query = "SP_IsExistsLocalDLApplication_By_ApplicantPersonID_AppStatus_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return await clsSqlDBExecutor.ExecuteScalarAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }
        
        public static async Task<bool> IsHasNewOrCompletedAppAsync(int ApplicantPersonID, int LicenseClassID)
        {
            const string query = "SP_IsHasNewOrCompletedLocalDLApplication_By_ApplicantPersonID_LicClassID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return await clsSqlDBExecutor.ExecuteScalarAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) != null;
        }



        //************** U **************\\
        public static async Task<bool> UpdateAsync(int LDLApplicationID, int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            const string query = "SP_UpdateLocalDLApplication_By_LDLAppID_AppID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LDLApplicationID },
                new SqlParameter("@ApplicationID", SqlDbType.Int) { Value = ApplicationID },
                new SqlParameter("@ApplicantPersonID", SqlDbType.Int) { Value = ApplicantPersonID },
                new SqlParameter("@ApplicationDate", SqlDbType.DateTime) { Value = ApplicationDate },
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) { Value = ApplicationTypeID },
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus },
                new SqlParameter("@LastStatusDate", SqlDbType.DateTime) { Value = LastStatusDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@LicenseClassID", SqlDbType.Int) { Value = LicenseClassID }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) > 0;
        }

        public static async Task<bool> UpdateApplicationStatusAsync(int LocalDLAppID, byte ApplicationStatus)
        {
            const string query = "SP_UpdateLocalDLApplication_ApplicationStatusOnly_By_LocalDLAppID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDLAppID },
                new SqlParameter("@NewApplicationStatus", SqlDbType.TinyInt) { Value = ApplicationStatus }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) > 0;
        }


        //************** D **************\\
        public static async Task<bool> DeleteAsync(int ID)
        {
            const string query = "SP_DeleteLocalDLApplication_By_LDLAppID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = ID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(
                clsDBSettings.CnnString("DVLD_DB"),
                query, parameters, CommandType.StoredProcedure) > 0;
        }
        #endregion
    }
}
