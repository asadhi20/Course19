using System;
using System.Data;
using System.Data.SqlClient;
using HelperClasses.Extensions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace DVLD_DAL.People
{
    public static class clsPersonData
    {
        #region Internal Const Fields
        internal const int _NationalNoColumnSize = 20;
        internal const int _FirstNameColumnSize = 20;
        internal const int _SecondNameColumnSize = 20;
        internal const int _ThirdNameColumnSize = 20;
        internal const int _LastNameCloumnSize = 20;
        internal const int _AddressColumnSize = 500;
        internal const int _PhoneColumnSize = 20;
        internal const int _EmailColumnSize = 50;
        internal const int _ImagePathColumnSize = 150;
        #endregion

        #region Sync Methods
        //********** C **********\\

        /// <returns>If returned -1 then person is not added, Otherwise added.</returns>
        public static int AddNew(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            const string query = @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, 
                                          Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                                   VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, 
                                          @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                                   SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo },
                new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName },
                new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName },
                new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DateOfBirth },
                new SqlParameter("@Gender", SqlDbType.TinyInt) { Value = Gender },
                new SqlParameter("@Address", SqlDbType.NVarChar, _AddressColumnSize) { Value = Address },
                new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone },
                new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email },
                new SqlParameter("@NationalityCountryID", SqlDbType.Int) { Value = NationalityCountryID },
                new SqlParameter("@ImagePath", SqlDbType.NVarChar, _ImagePathColumnSize) { Value = string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath },
                };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int insertedID) ? insertedID : -1;
        }

        //********** R **********\\

        public static bool Get(int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = "SELECT NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

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
                    return true;
                }
            };

            return false;
        }

        public static bool GetByNationalNo(ref int ID, string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = "SELECT PersonID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
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
                    return true;
                }
            };

            return false;
        }
        
        public static bool GetByFirstName(ref int ID, ref string NationalNo, string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = "SELECT PersonID, NationalNo, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE FirstName = @FirstName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
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
                    return true;
                }
            };

            return false;
        }

        public static bool GetBySecondName(ref int ID, ref string NationalNo, ref string FirstName, string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = "SELECT PersonID, NationalNo, FirstName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE SecondName = @SecondName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    ThirdName = reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName);
                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    Email = reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail);
                    NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                    ImagePath = reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath);
                    return true;
                }
            };

            return false;
        }

        public static bool GetByThirdName(ref int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
            string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = "SELECT PersonID, NationalNo, FirstName, SecondName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE ThirdName = @ThirdName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = ThirdName } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    Email = reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail);
                    NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                    ImagePath = reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath);
                    return true;
                }
            };

            return false;
        }

        public static bool GetByLastName(ref int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = "SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE LastName = @LastName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                    ThirdName = reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName);
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    Email = reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail);
                    NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                    ImagePath = reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath);

                    return true;
                }
            };

            return false;
        }

        public static bool GetByPhone(ref int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = "SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Email, NationalityCountryID, ImagePath FROM People WHERE Phone = @Phone;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                    ThirdName = reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName);
                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    Email = reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail);
                    NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                    ImagePath = reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath);

                    return true;
                }
            };

            return false;
        }

        public static bool GetByEmail(ref int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender, ref string Address,
            ref string Phone, string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = @"IF @Email IS NOT NULL BEGIN
                                       SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, 
                                       Address, Phone, NationalityCountryID, ImagePath FROM People WHERE Email = @Email;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = Email.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)Email.RemoveWhiteSpaces() } };
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indImagePath = reader.GetOrdinal("ImagePath");

                    ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                    ThirdName = reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName);
                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender"));
                    Address = reader.GetString(reader.GetOrdinal("Address"));
                    Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                    ImagePath = reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath);

                    return true;
                }
            };

            return false;
        }


        public static string GetImagePath(int ID)
        {
            const string query = "SELECT ImagePath FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);
            return result?.ToString() ?? string.Empty;
        }

        public static DataTable GetPeople()
        {
            const string query = @"SELECT [Person ID], [National No], [First Name], [Second Name], [Third Name], [Last Name], 
                                          [Date Of Birth], Nationality, Gender, Phone, Email
                                   FROM People_View;";

            DataTable people = new DataTable();
            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query)) 
            { 
                if (!(reader is null) && reader.HasRows) people.Load(reader);
            }

            return people;
        }

        
        public static bool IsExists(int ID)
        {
            const string query = "SELECT 1 FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static bool IsExistsByNationalNo(string NationalNo)
        {
            const string query = "SELECT 1 FROM People WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static bool IsExistsByFullName(string FirstName, string SecondName, string ThirdName, string LastName)
        {
            const string query = "SELECT 1 FROM People WHERE FirstName = @FirstName AND SecondName = @SecondName AND ThirdName = @ThirdName AND LastName = @LastName;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@FirstName" , SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                new SqlParameter("@ThirdName" , SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = ThirdName.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)ThirdName },
                new SqlParameter("@LastName"  , SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName }
            };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static bool IsExistsByAddress(string Address)
        {
            const string query = "SELECT 1 FROM People WHERE Address = @Address;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Address", SqlDbType.NVarChar) { Value = Address } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }
        
        public static bool IsExistsByPhone(string Phone)
        {
            const string query = "SELECT 1 FROM People WHERE Phone = @Phone;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = Phone } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static bool IsExistsByEmail(string Email)
        {
            const string query = @"IF @Email IS NOT NULL BEGIN
                                      SELECT 1 FROM People WHERE Email = @Email;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NVarChar) { Value = Email.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)Email } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static bool IsExistsByImagePath(string ImagePath)
        {
            const string query = @"IF @ImagePath IS NOT NULL BEGIN 
                                        SELECT 1 FROM People WHERE ImagePath = @ImagePath;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ImagePath", SqlDbType.NVarChar) { Value = ImagePath.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)ImagePath } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }
        

        //********** U **********\\

        public static bool Update(int ID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            const string query = @"UPDATE People
                                   SET NationalNo = @NationalNo,
                                       FirstName = @FirstName,
                                       SecondName = @SecondName,
                                       ThirdName = @ThirdName,
                                       LastName = @LastName,
                                       DateOfBirth = @DateOfBirth,
                                       Gender = @Gender,
                                       Address = @Address,
                                       Phone = @Phone,
                                       Email = @Email,
                                       NationalityCountryID = @NationalityCountryID,
                                       ImagePath = @ImagePath
                                   WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo },
                new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName },
                new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName },
                new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DateOfBirth },
                new SqlParameter("@Gender", SqlDbType.TinyInt) { Value = Gender },
                new SqlParameter("@Address", SqlDbType.NVarChar, _AddressColumnSize) { Value = Address },
                new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone },
                new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email },
                new SqlParameter("@NationalityCountryID", SqlDbType.Int) { Value = NationalityCountryID },
                new SqlParameter("@ImagePath", SqlDbType.NVarChar, _ImagePathColumnSize) { Value = string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath },
                };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static bool Update(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            const string query = @"UPDATE People
                                   SET FirstName = @FirstName,
                                       SecondName = @SecondName,
                                       ThirdName = @ThirdName,
                                       LastName = @LastName,
                                       DateOfBirth = @DateOfBirth,
                                       Gender = @Gender,
                                       Address = @Address,
                                       Phone = @Phone,
                                       Email = @Email,
                                       NationalityCountryID = @NationalityCountryID,
                                       ImagePath = @ImagePath
                                   WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo },
                new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName },
                new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName },
                new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DateOfBirth },
                new SqlParameter("@Gender", SqlDbType.TinyInt) { Value = Gender },
                new SqlParameter("@Address", SqlDbType.NVarChar, _AddressColumnSize) { Value = Address },
                new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone },
                new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email },
                new SqlParameter("@NationalityCountryID", SqlDbType.Int) { Value = NationalityCountryID },
                new SqlParameter("@ImagePath", SqlDbType.NVarChar, _ImagePathColumnSize) { Value = string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath },
                };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        

        //********** D **********\\

        public static bool Delete(int ID)
        {
            const string query = "DELETE FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static bool Delete(string NationalNo)
        {
            const string query = "DELETE FROM People WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@NationalNo", SqlDbType.NVarChar) { Value = NationalNo } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static bool Delete(string FirstName, string SecondName, string ThirdName, string LastName)
        {
            const string query = "DELETE FROM People WHERE FirstName = @FirstName AND SecondName = @SecondName AND ThirdName = @ThirdName AND LastName = @LastName;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@FirstName" , SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                new SqlParameter("@ThirdName" , SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = ThirdName.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)ThirdName },
                new SqlParameter("@LastName"  , SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName }
            };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }
        #endregion


        #region Async Methods

        //********** C **********\\

        /// <returns>If returned -1 then person is not added, Otherwise added.</returns>
        public static async Task<int> AddNewAsync(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            const string query = @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, 
                                          Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                                   VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, 
                                          @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                                   SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo },
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                    new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                    new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName },
                    new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName },
                    new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DateOfBirth },
                    new SqlParameter("@Gender", SqlDbType.TinyInt) { Value = Gender },
                    new SqlParameter("@Address", SqlDbType.NVarChar, _AddressColumnSize) { Value = Address },
                    new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone },
                    new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email },
                    new SqlParameter("@NationalityCountryID", SqlDbType.Int) { Value = NationalityCountryID },
                    new SqlParameter("@ImagePath", SqlDbType.NVarChar, _ImagePathColumnSize) { Value = string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath },
                };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result != null && int.TryParse(result.ToString(), out int insertedID) ? insertedID : -1;
        }

        //********** R **********\\

        public static async Task<(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetAsync(int ID)
        {
            const string query = "SELECT NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");
                    return (NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
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
                        IsFound: true);
                }
            };

            return (NationalNo: "", FirstName: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue, 
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }


        public static async Task<(int ID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetByNationalNoAsync(string NationalNo)
        {
            const string query = "SELECT PersonID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ID: reader.GetInt32(reader.GetOrdinal("PersonID")),
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
                        IsFound: true);
                }
            };

            return (ID: -1, FirstName: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }
        

        public static async Task<(int ID, string NationalNo, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetByFirstNameAsync(string FirstName)
        {
            const string query = "SELECT PersonID, NationalNo, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE FirstName = @FirstName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
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
                        IsFound: true);
                }
            };

            return (ID: -1, NationalNo: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }

        public static async Task<(int ID, string NationalNo, string FirstName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetBySecondNameAsync(string SecondName)
        {
            const string query = "SELECT PersonID, NationalNo, FirstName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE SecondName = @SecondName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        ThirdName: reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName),
                        LastName: reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Phone: reader.GetString(reader.GetOrdinal("Phone")),
                        Email: reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath), IsFound: true);
                }
            };

            return (ID: -1, NationalNo: "", FirstName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }

        public static async Task<(int ID, string NationalNo, string FirstName, string SecondName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetByThirdNameAsync(string ThirdName)
        {
            const string query = "SELECT PersonID, NationalNo, FirstName, SecondName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE ThirdName = @ThirdName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = ThirdName } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName: reader.GetString(reader.GetOrdinal("SecondName")),
                        LastName: reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Phone: reader.GetString(reader.GetOrdinal("Phone")),
                        Email: reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath),
                        IsFound: true);
                }
            };

            return (ID: -1, NationalNo: "", FirstName: "", SecondName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }

        public static async Task<(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetByLastNameAsync(string LastName)
        {
            const string query = "SELECT PersonID, FirstName, SecondName, ThirdName, NationalNo, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath FROM People WHERE LastName = @LastName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName: reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName: reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Phone: reader.GetString(reader.GetOrdinal("Phone")),
                        Email: reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath),
                        IsFound: true);
                }
            };

            return (ID: -1, NationalNo: "", FirstName: "", SecondName: "", ThirdName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", Email: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }

        public static async Task<(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            bool Gender, string Address, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetByPhoneAsync(string Phone)
        {
            const string query = "SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Email, NationalityCountryID, ImagePath FROM People WHERE Phone = @Phone;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName: reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName: reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName),
                        LastName: reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Email: reader.IsDBNull(indEmail) ? string.Empty : reader.GetString(indEmail),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath),
                        IsFound: true);
                }
            };

            return (ID: -1, NationalNo: "", FirstName: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Email: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }
        
        public static async Task<(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            bool Gender, string Address, string Phone, int NationalityCountryID, string ImagePath, bool IsFound)> GetByEmailAsync(string Email)
        {
            const string query = @"IF @Email IS NOT NULL BEGIN
                                       SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, 
                                       Address, Phone, NationalityCountryID, ImagePath FROM People WHERE Email = @Email;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = Email } };
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (ID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNo: reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName: reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName: reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName: reader.IsDBNull(indThirdName) ? string.Empty : reader.GetString(indThirdName),
                        LastName: reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender: reader.GetBoolean(reader.GetOrdinal("Gender")),
                        Address: reader.GetString(reader.GetOrdinal("Address")),
                        Phone: reader.GetString(reader.GetOrdinal("Phone")),
                        NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath), 
                        IsFound: true);
                }
            };

            return (ID: -1, NationalNo: "", FirstName: "", SecondName: "", ThirdName: "", LastName: "", DateOfBirth: DateTime.MaxValue,
                Gender: false, Address: "", Phone: "", NationalityCountryID: -1, ImagePath: "", IsFound: false);
        }


        public static async Task<string> GetImagePathAsync(int ID)
        {
            const string query = "SELECT ImagePath FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return result?.ToString() ?? string.Empty;
        }

        public static async Task<DataTable> GetPeopleAsync()
        {
            const string query = @"SELECT [Person ID], [National No], [First Name], [Second Name], [Third Name], [Last Name], 
                                          [Date Of Birth], Nationality, Gender, Phone, Email
                                   FROM People_View;";

            DataTable people = new DataTable();
            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) people.Load(reader);
            }

            return people;
        }
        
        public static async Task<IEnumerable<(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)>> GetAllAsync()
        {
            const string query = @"SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                          DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath
                                   FROM People;";

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (reader is null || !reader.HasRows)
                    return new List<(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
                string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)>();


                const int minimumNumberOfPeople = 20;
                List<(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
                    string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)> people =
                new List<(int PersonID, string NationalNo,string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
                string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)>(minimumNumberOfPeople);

                int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                while (await reader.ReadAsync())
                {
                    people.Add((PersonID: reader.GetInt32(reader.GetOrdinal("PersonID")),
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
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath)));
                }

                return people;
            }
        }



        public static async Task<bool> IsExistsAsync(int ID)
        {
            const string query = "SELECT 1 FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<bool> IsExistsByNationalNoAsync(string NationalNo)
        {
            const string query = "SELECT 1 FROM People WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<bool> IsExistsByFullNameAsync(string FirstName, string SecondName, string ThirdName, string LastName)
        {
            const string query = "SELECT 1 FROM People WHERE FirstName = @FirstName AND SecondName = @SecondName AND ThirdName = @ThirdName AND LastName = @LastName;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@FirstName" , SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                new SqlParameter("@ThirdName" , SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = ThirdName.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)ThirdName },
                new SqlParameter("@LastName"  , SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName }
            };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<bool> IsExistsByAddressAsync(string Address)
        {
            const string query = "SELECT 1 FROM People WHERE Address = @Address;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Address", SqlDbType.NVarChar, _AddressColumnSize) { Value = Address } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<bool> IsExistsByPhoneAsync(string Phone)
        {
            const string query = "SELECT 1 FROM People WHERE Phone = @Phone;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<bool> IsExistsByEmailAsync(string Email)
        {
            const string query = @"IF @Email IS NOT NULL BEGIN
                                      SELECT 1 FROM People WHERE Email = @Email;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = Email.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)Email } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<bool> IsExistsByImagePathAsync(string ImagePath)
        {
            const string query = @"IF @ImagePath IS NOT NULL BEGIN 
                                        SELECT 1 FROM People WHERE ImagePath = @ImagePath;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ImagePath", SqlDbType.NVarChar, _ImagePathColumnSize) { Value = ImagePath.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)ImagePath } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }


        //********** U **********\\

        public static async Task<bool> UpdateAsync(int ID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            const string query = @"UPDATE People
                                   SET NationalNo = @NationalNo,
                                       FirstName = @FirstName,
                                       SecondName = @SecondName,
                                       ThirdName = @ThirdName,
                                       LastName = @LastName,
                                       DateOfBirth = @DateOfBirth,
                                       Gender = @Gender,
                                       Address = @Address,
                                       Phone = @Phone,
                                       Email = @Email,
                                       NationalityCountryID = @NationalityCountryID,
                                       ImagePath = @ImagePath
                                   WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID },
                    new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo },
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                    new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                    new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName },
                    new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName },
                    new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DateOfBirth },
                    new SqlParameter("@Gender", SqlDbType.TinyInt) { Value = Gender },
                    new SqlParameter("@Address", SqlDbType.NVarChar, _AddressColumnSize) { Value = Address },
                    new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone },
                    new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email },
                    new SqlParameter("@NationalityCountryID", SqlDbType.Int) { Value = NationalityCountryID },
                    new SqlParameter("@ImagePath", SqlDbType.NVarChar, _ImagePathColumnSize) { Value = string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath },
                };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> UpdateAsync(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            const string query = @"UPDATE People
                                   SET FirstName = @FirstName,
                                       SecondName = @SecondName,
                                       ThirdName = @ThirdName,
                                       LastName = @LastName,
                                       DateOfBirth = @DateOfBirth,
                                       Gender = @Gender,
                                       Address = @Address,
                                       Phone = @Phone,
                                       Email = @Email,
                                       NationalityCountryID = @NationalityCountryID,
                                       ImagePath = @ImagePath
                                   WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@NationalNo", SqlDbType.NVarChar, _NationalNoColumnSize) { Value = NationalNo },
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                    new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                    new SqlParameter("@ThirdName", SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = string.IsNullOrWhiteSpace(ThirdName) ? DBNull.Value : (object)ThirdName },
                    new SqlParameter("@LastName", SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName },
                    new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DateOfBirth },
                    new SqlParameter("@Gender", SqlDbType.TinyInt) { Value = Gender },
                    new SqlParameter("@Address", SqlDbType.NVarChar, _AddressColumnSize) { Value = Address },
                    new SqlParameter("@Phone", SqlDbType.NVarChar, _PhoneColumnSize) { Value = Phone },
                    new SqlParameter("@Email", SqlDbType.NVarChar, _EmailColumnSize) { Value = string.IsNullOrWhiteSpace(Email) ? DBNull.Value : (object)Email },
                    new SqlParameter("@NationalityCountryID", SqlDbType.Int) { Value = NationalityCountryID },
                    new SqlParameter("@ImagePath", SqlDbType.NVarChar, _ImagePathColumnSize) { Value = string.IsNullOrWhiteSpace(ImagePath) ? DBNull.Value : (object)ImagePath },
                };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }


        //********** D **********\\

        public static async Task<bool> DeleteAsync(int ID)
        {
            const string query = "DELETE FROM People WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = ID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> DeleteAsync(string NationalNo)
        {
            const string query = "DELETE FROM People WHERE NationalNo = @NationalNo;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@NationalNo", SqlDbType.NVarChar) { Value = NationalNo } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> DeleteAsync(string FirstName, string SecondName, string ThirdName, string LastName)
        {
            const string query = "DELETE FROM People WHERE FirstName = @FirstName AND SecondName = @SecondName AND ThirdName = @ThirdName AND LastName = @LastName;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@FirstName" , SqlDbType.NVarChar, _FirstNameColumnSize) { Value = FirstName },
                new SqlParameter("@SecondName", SqlDbType.NVarChar, _SecondNameColumnSize) { Value = SecondName },
                new SqlParameter("@ThirdName" , SqlDbType.NVarChar, _ThirdNameColumnSize) { Value = ThirdName.IsNullOrEmptyOrWhiteSpace() ? DBNull.Value : (object)ThirdName },
                new SqlParameter("@LastName"  , SqlDbType.NVarChar, _LastNameCloumnSize) { Value = LastName }
            };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }
        #endregion
    };

}
