using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Classes;


namespace DVLD_DAL.Applications.ManageApplications.LocalDrivingLicenseApplications.SechduleTests
{
    public static class clsSechduleTestData
    {
        #region Internal Const Fields
        internal const int LockedTestAppointmentValue = 1;
        #endregion

        #region Public Sync Methods
        //************** C **************\\
        public static int Add(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked)
        {
            const string query = "SP_AddNewTestAppointment";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestTypeID", SqlDbType.TinyInt) { Value = TestTypeID },
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDrivingLicenseApplicationID },
                new SqlParameter("@AppointmentDate", SqlDbType.SmallDateTime) { Value = AppointmentDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = IsLocked },

                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int testAppointmentID = -1;
			const int testAppointmentIDParamIndex = 6;

			try
			{
                int rowAffected = clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure);
				if (rowAffected > 0)
				{
                    testAppointmentID = parameters[testAppointmentIDParamIndex].Value != DBNull.Value 
                        ? Convert.ToInt32(parameters[testAppointmentIDParamIndex].Value) : -1;
				}
			}
			catch { }

			return testAppointmentID;
        }


        //************** R **************\\
        public static bool Get(int ID, byte TestTypeID, ref int LocalDLApplicationID, ref int ApplicationID, ref int ApplicantPersonID,
            ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref bool Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath,
            ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref float ApplicationPaidFees, ref int ApplicationCreatedByUserID, ref int LicenseID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked)
        {
            const string query = "SP_GetSingleTestAppointmentWithLDLAppInfo_By_TestAppointmentID_TestTypeID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = TestTypeID }
            };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    LocalDLApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
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
                    ApplicationPaidFees = (float)reader.GetDouble(reader.GetOrdinal("ApplicationPaidFees"));
                    ApplicationCreatedByUserID = reader.GetInt32(reader.GetOrdinal("ApplicationCreatedByUserID"));
                    LicenseID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));
                    AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                    PaidFees = (float)reader.GetDouble(reader.GetOrdinal("PaidFees"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                    IsLocked = reader.GetByte(reader.GetOrdinal("IsLocked")) is LockedTestAppointmentValue;

                    return true;
                }
            }

            return false;
        }
        
        public static bool Get(int ID, ref byte TestTypeID, ref int LocalDLApplicationID, ref int ApplicationID, ref int ApplicantPersonID,
            ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref bool Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath,
            ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref float ApplicationPaidFees, ref int ApplicationCreatedByUserID, ref int LicenseID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked)
        {
            const string query = "SP_GetTestAppointmentWithLDLAppInfo_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    TestTypeID = reader.GetByte(reader.GetOrdinal("TestTypeID"));
                    LocalDLApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
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
                    ApplicationPaidFees = (float)reader.GetDouble(reader.GetOrdinal("ApplicationPaidFees"));
                    ApplicationCreatedByUserID = reader.GetInt32(reader.GetOrdinal("ApplicationCreatedByUserID"));
                    LicenseID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));
                    AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                    PaidFees = (float)reader.GetDouble(reader.GetOrdinal("PaidFees"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                    IsLocked = reader.GetByte(reader.GetOrdinal("IsLocked")) is LockedTestAppointmentValue;

                    return true;
                }
            }

            return false;
        }


        public static IEnumerable<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate,
            float PaidFees, string FullName, bool IsLocked)> GetAll()
        {
            const string query = "SP_GetAllTestAppointments_View";

            const int minimumNumberOfTestApplointments = 20;

            List<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate, float PaidFees, string FullName, bool IsLocked)> testAppointments =
                new List<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate, float PaidFees, string FullName, bool IsLocked)>(minimumNumberOfTestApplointments);

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (reader is null || !reader.HasRows) return testAppointments;

                while (reader.Read())
                {
                    testAppointments.Add((ID: reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                        ClassName: reader.GetString(reader.GetOrdinal("ClassName")),
                        LocalDLApplicationID: reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                        TestTypeTitle: reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                        AppointmentDate: reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                        FullName: reader.GetString(reader.GetOrdinal("FullName")),
                        IsLocked: reader.GetByte(reader.GetOrdinal("IsLocked")) is LockedTestAppointmentValue
                        ));
                }
            }

            return testAppointments;
        }


        public static DataTable GetAllTestAppointments(byte TestTypeID, int LoaclDLAppID)
        {
            const string query = "SP_GetAllTestAppointments_By_LocalDLAppID_TestTypeID";

            DataTable testAppointments = new DataTable();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = TestTypeID },
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LoaclDLAppID }
            };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure))
            {
                if (!(reader is null) && reader.HasRows) testAppointments.Load(reader);
            }

            return testAppointments;
        }


        public static DataTable GetAllTestAppointments(byte TestTypeID)
        {
            const string query = "SP_GetTestAppointment_By_TestTypeID";

            DataTable testAppointments = new DataTable();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = TestTypeID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (!(reader is null) && reader.HasRows) testAppointments.Load(reader);
            }

            return testAppointments;
        }


        //************** U **************\\
        public static bool Update(int ID, byte TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked)
        {
            const string query = "SP_UpdateTestAppointment_By_ID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@TestTypeID", SqlDbType.TinyInt) { Value = TestTypeID },
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDrivingLicenseApplicationID },
                new SqlParameter("@AppointmentDate", SqlDbType.SmallDateTime) { Value = AppointmentDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = IsLocked }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static bool UpdateAppointmentDateOnly(int ID, DateTime AppointmentDate)
        {
            const string query = "SP_UpdateTestAppointment_AppointmetDateOnly_By_ID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@AppointmentDate", SqlDbType.SmallDateTime) { Value = AppointmentDate },
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }

        public static bool UpdateIsLockedOnly(int ID, bool IsLocked)
        {
            const string query = "SP_UpdateTestAppointment_IsLockedOnly_By_ID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = IsLocked }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }


        //************** D **************\\
        public static bool Delete(int ID)
        {
            const string query = "SP_DeleteTestAppointment_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion


        #region Public Async Methods
        //************** C **************\\
        public static async Task<int> AddAsync(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked)
        {
            const string query = "SP_AddNewTestAppointment";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestTypeID", SqlDbType.TinyInt) { Value = TestTypeID },
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDrivingLicenseApplicationID },
                new SqlParameter("@AppointmentDate", SqlDbType.SmallDateTime) { Value = AppointmentDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = IsLocked },

                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int testAppointmentID = -1;
            const int testAppointmentIDParamIndex = 6;
			
			try
			{
                int rowAffected = await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure);
                if (rowAffected > 0)
                {
                    testAppointmentID = parameters[testAppointmentIDParamIndex].Value != DBNull.Value
                        ? Convert.ToInt32(parameters[testAppointmentIDParamIndex].Value) : -1;
                }
			}
			catch { }

			return testAppointmentID;
        }


        //************** R **************\\
        public static async Task<(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID, string NationalNo,
            string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float ApplicationPaidFees, int ApplicationCreatedByUserID, int LicenseID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, bool IsFound)> GetAsync(int ID, byte TestTypeID)
        {
            const string query = "SP_GetSingleTestAppointmentWithLDLAppInfo_By_TestAppointmentID_TestTypeID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = TestTypeID }
            };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (LocalDrivingLicenseApplicationID: reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
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
                        ApplicationPaidFees: (float)reader.GetDecimal(reader.GetOrdinal("ApplicationPaidFees")),
                        ApplicationCreatedByUserID: reader.GetInt32(reader.GetOrdinal("ApplicationCreatedByUserID")),
                        LicenseID: reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        AppointmentDate: reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                        CreatedByUserID: reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                        IsLocked: reader.GetBoolean(reader.GetOrdinal("IsLocked")),
                        IsFound: true);
                }
            }

            return (LocalDrivingLicenseApplicationID: -1, ApplicationID: -1, ApplicantPersonID: -1, NationalNo: "", FirstName: "",
                SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue, Gender: false, Address: "", Phone: "", Email: "",
                NationalityCountryID: -1, ImagePath: "", ApplicationDate: DateTime.MaxValue, ApplicationTypeID: -1, ApplicationStatus: 0,
                LastStatusDate: DateTime.MaxValue, ApplicationPaidFees: .0f, ApplicationCreatedByUserID: -1, LicenseID: -1,
                AppointmentDate: DateTime.MaxValue, PaidFees: .0f, CreatedByUserID: -1, IsLocked: false, IsFound: false);
        }


        public static async Task<(byte TestTypeID, int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID, 
            string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath,
            DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float ApplicationPaidFees, 
            int ApplicationCreatedByUserID, int LicenseID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, 
            bool IsLocked, bool IsFound)> GetAsync(int ID)
        {
            const string query = "SP_GetTestAppointmentWithLDLAppInfo_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (
                        TestTypeID: reader.GetByte(reader.GetOrdinal("TestTypeID")),
                        LocalDrivingLicenseApplicationID: reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
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
                        ApplicationPaidFees: (float)reader.GetDecimal(reader.GetOrdinal("ApplicationPaidFees")),
                        ApplicationCreatedByUserID: reader.GetInt32(reader.GetOrdinal("ApplicationCreatedByUserID")),
                        LicenseID: reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        AppointmentDate: reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                        PaidFees: (float)reader.GetDouble(reader.GetOrdinal("PaidFees")),
                        CreatedByUserID: reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                        IsLocked: reader.GetBoolean(reader.GetOrdinal("IsLocked")),
                        IsFound: true
                    );
                }
            }

            return (TestTypeID: 0, LocalDrivingLicenseApplicationID: -1, ApplicationID: -1, ApplicantPersonID: -1, NationalNo: "", FirstName: "",
                SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue, Gender: false, Address: "", Phone: "", Email: "",
                NationalityCountryID: -1, ImagePath: "", ApplicationDate: DateTime.MaxValue, ApplicationTypeID: -1, ApplicationStatus: 0,
                LastStatusDate: DateTime.MaxValue, ApplicationPaidFees: .0f, ApplicationCreatedByUserID: -1, LicenseID: -1,
                AppointmentDate: DateTime.MaxValue, PaidFees: .0f, CreatedByUserID: -1, IsLocked: false, IsFound: false);
        }


        public static async Task<IEnumerable<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate,
            float PaidFees, string FullName, bool IsLocked)>> GetAllAsync()
        {
            const string query = "SP_GetAllTestAppointments_View";

            const int minimumNumberOfTestApplointments = 20;

            List<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate, float PaidFees, string FullName, bool IsLocked)> testAppointments =
                new List<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate, float PaidFees, string FullName, bool IsLocked)>(minimumNumberOfTestApplointments);

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (reader is null || !reader.HasRows) return testAppointments;

                while (await reader.ReadAsync())
                {
                    testAppointments.Add((ID: reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                        ClassName: reader.GetString(reader.GetOrdinal("ClassName")),
                        LocalDLApplicationID: reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                        TestTypeTitle: reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                        AppointmentDate: reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                        PaidFees: (float)reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                        FullName: reader.GetString(reader.GetOrdinal("FullName")),
                        IsLocked: reader.GetByte(reader.GetOrdinal("IsLocked")) is LockedTestAppointmentValue
                        ));
                }
            }

            return testAppointments;
        }


        public static async Task<DataTable> GetAllTestAppointmentsAsync(int LocalDLAppID, byte TestTypeID)
        {
            const string query = "SP_GetAllTestAppointments_By_LocalDLAppID_TestTypeID";

            DataTable testAppointments = new DataTable();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = TestTypeID },
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDLAppID }
            };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure))
            {
                if (!(reader is null) && reader.HasRows) testAppointments.Load(reader);
            }

            return testAppointments;
        }


        public static async Task<DataTable> GetAllTestAppointmentsAsync(byte TestTypeID)
        {
            const string query = "SP_GetTestAppointment_By_TestTypeID";

            DataTable testAppointments = new DataTable();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = TestTypeID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (!(reader is null) && reader.HasRows) testAppointments.Load(reader);
            }

            return testAppointments;
        }
        

        //************** U **************\\
        public static async Task<bool> UpdateAsync(int ID, byte TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked)
        {
            const string query = "SP_UpdateTestAppointment_ID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@TestTypeID", SqlDbType.TinyInt) { Value = TestTypeID },
                new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int) { Value = LocalDrivingLicenseApplicationID },
                new SqlParameter("@AppointmentDate", SqlDbType.SmallDateTime) { Value = AppointmentDate },
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) { Value = PaidFees },
                new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = CreatedByUserID },
                new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = IsLocked }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> UpdateAppointmentDateOnlyAsync(int ID, DateTime AppointmentDate)
        {
            const string query = "SP_UpdateTestAppointment_AppointmetDateOnly_By_ID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@AppointmentDate", SqlDbType.SmallDateTime) { Value = AppointmentDate },
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }
        
        public static async Task<bool> UpdateIsLockedOnlyAsync(int ID, bool IsLocked)
        {
            const string query = "SP_UpdateTestAppointment_IsLockedOnly_By_ID";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = IsLocked }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters, CommandType.StoredProcedure) > 0;
        }


        //************** D **************\\
        public static async Task<bool> DeleteAsync(int ID)
        {
            const string query = "SP_DeleteTestAppointment_By_ID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestAppointmentID", SqlDbType.Int) { Value = ID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion
    }
}
