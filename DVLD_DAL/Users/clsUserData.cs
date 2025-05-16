using System;
using System.Data;
using System.Data.SqlClient;
using Helper.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using DVLD_DAL.People;

namespace DVLD_DAL.Users
{
    public static class clsUserData
    {
        #region Internal Const Fields
        internal const int _UserNameColumnSize = 20;
        internal const int _PasswordColumnSize = 20;
        #endregion

        #region Synchronous Methods
        //************** C **************\\
        /// <returns>If returned -1 then user is not added, Otherwise added.</returns>
        public static int AddNew(int PersonID, string UserName, string Password, bool IsActive)
        {
            const string query = @"INSERT INTO Users (PersonID, UserName, Password, IsActive) 
                                       VALUES (@PersonID, @UserName, @Password, @IsActive);
                                   SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID },
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive }
            };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);
            return result != null && int.TryParse(result.ToString(), out int insertedID) ? insertedID : -1;
        }


        //************** R **************\\

        public static bool Get(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive, ref string NationalNo, 
            ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, 
            ref bool Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = @"SELECT u.PersonID, u.UserName, u.Password, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, 
                                   	   p.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID
                                   WHERE UserID = @UserID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    UserName = reader.GetString(reader.GetOrdinal("UserName"));
                    Password = reader.GetString(reader.GetOrdinal("Password"));
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
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
            }

            return false;
        }

        public static bool Get(ref int UserID, int PersonID, ref string UserName, ref string Password, ref bool IsActive, ref string NationalNo, 
            ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, 
            ref bool Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = @"SELECT u.UserID, u.UserName, u.Password, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, 
                                   	   p.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID
                                   WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID } };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    UserName = reader.GetString(reader.GetOrdinal("UserName"));
                    Password = reader.GetString(reader.GetOrdinal("Password"));
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
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
            }

            return false;
        }

        public static bool Get(ref int UserID, ref int PersonID, string UserName, string Password, ref bool IsActive, ref string NationalNo, 
            ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, 
            ref bool Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            const string query = @"SELECT u.UserID, u.PersonID, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, 
                                   	   p.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID 
                                   WHERE UserName = @UserName AND Password = @Password;";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName }, 
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password } 
            };

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
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
            }

            return false;
        }
        

        public static DataTable GetUsersByIsActive(bool IsActive)
        {
            const string query = @"SELECT u.UserID, u.PersonID,
                                   	FullName = CONCAT(p.FirstName, ' ', p.SecondName, ' ', COALESCE(p.ThirdName + ' ', ''), p.LastName),
                                   	u.UserName, u.IsActive 
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID WHERE u.IsActive = @IsActive;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive } };

            DataTable users = new DataTable();

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) users.Load(reader);
            }

            return users;
        }

        public static DataTable GetUsers()
        {
            const string query = @"SELECT u.UserID, u.PersonID,
	                                   FullName = CONCAT(p.FirstName, ' ', p.SecondName, ' ', COALESCE(p.ThirdName + ' ', ''), p.LastName),
	                                   u.UserName, u.IsActive 
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID;";

            DataTable users = new DataTable();

            using (SqlDataReader reader = clsSqlDBExecutor.ExecuteReader(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) users.Load(reader);
            }

            return users;
        }
        
        
        public static bool IsExsits(int UserID)
        {
            const string query = "SELECT 1 FROM Users WHERE UserID = @UserID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }
        
        public static bool IsExsitsByPersonID(int PersonID)
        {
            const string query = "SELECT 1 FROM Users WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static bool IsExsits(string UserName)
        {
            const string query = "SELECT 1 FROM Users WHERE UserName = @UserName;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName } };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static bool IsExsits(string UserName, string Password)
        {
            const string query = "SELECT 1 FROM Users WHERE UserName = @UserName AND Password = @Password;";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };

            return clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static (bool IsExsits, bool IsActive) IsExsitsAndActive(string UserName, string Password)
        {
            const string query = @"DECLARE @USERID INT = (SELECT UserID FROM Users WHERE UserName = @UserName AND Password = @Password);
                                   IF @USERID > 0 BEGIN
                                       SET @USERID *= (SELECT IsActive FROM Users WHERE UserID = @USERID); 
                                       SELECT CASE WHEN @USERID > 0 THEN 3 ELSE 2 END;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },  
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },  
            };

            object result = clsSqlDBExecutor.ExecuteScalar(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return int.TryParse(result?.ToString(), out int num)
                ? (IsExsits: (num & 2) == 2, IsActive: (num & 1) == 1)
                : (IsExsits: false, IsActive: false);
        }



        //************** U **************\\

        public static bool Update(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            const string query = @"UPDATE Users
                                   SET PersonID = @PersonID,
                                       UserName = @UserName,
                                       Password = @Password,
                                       IsActive = @IsActive
                                   WHERE UserID = @UserID;
                                   SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID },
                new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID },
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive }
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }


        public static bool Activate(int UserID)
        {
            const string query = "UPDATE Users SET IsActive = 1 WHERE UserID = @UserID; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Deactivate(int UserID)
        {
            const string query = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static bool Activate(string UserName, string Password)
        {
            const string query = "UPDATE Users SET IsActive = 1 WHERE UserName = @UserName AND Password = @Password; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };
            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static bool Deactivate(string UserName, string Password)
        {
            const string query = "UPDATE Users SET IsActive = 0 WHERE UserName = @UserName AND Password = @Password; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };
            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        

        //************** D **************\\

        public static bool Delete(int UserID)
        {
            const string query = "DELETE FROM Users WHERE UserID = @UserID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static bool Delete(string UserName, string Password)
        {
            const string query = "DELETE FROM Users WHERE UserName = @UserName AND Password = @Password;";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };

            return clsSqlDBExecutor.ExecuteNonQuery(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion


        #region Asynchronous Methods

        //************** C **************\\

        /// <returns>If returned -1 then user is not added, Otherwise added.</returns>
        public static async Task<int> AddNewAsync(int PersonID, string UserName, string Password, bool IsActive)
        {
            const string query = @"INSERT INTO Users (PersonID, UserName, Password, IsActive) 
                                       VALUES (@PersonID, @UserName, @Password, @IsActive);
                                   SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID },
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive }
            };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);
            return result != null && int.TryParse(result.ToString(), out int insertedID) ? insertedID : -1;
        }


        //************** R **************\\

        public static async Task<(int PersonID, string UserName, string Password, bool IsActive, string NationalNo,
            string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, 
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetAsync(int UserID)
        {
            const string query = @"SELECT p.PersonID, u.UserName, u.Password, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName, 
                                   	   p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID 
                                   WHERE UserID = @UserID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (PersonID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        UserName: reader.GetString(reader.GetOrdinal("UserName")),
                        Password: reader.GetString(reader.GetOrdinal("Password")),
                        IsActive: reader.GetBoolean(reader.GetOrdinal("IsActive")),
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
                        IsFound: true);
                }
            }

            return (PersonID: -1, UserName: string.Empty, Password: string.Empty, IsActive: false, NationalNo: string.Empty, 
                   FirstName: string.Empty, SecondName: string.Empty, ThirdName: string.Empty, LastName: string.Empty, 
                 DateOfBirth: DateTime.MaxValue, Gender: false, Address: string.Empty, Phone: string.Empty, 
                       Email: string.Empty, NationalityCountryID: -1, ImagePath: string.Empty, IsFound: false);
        }


        public static async Task<(int UserID, string UserName, string Password, bool IsActive, string NationalNo,
            string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetByPersonIDAsync(int PersonID)
        {
            const string query = @"SELECT u.UserID, u.UserName, u.Password, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName, 
                                   	   p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID 
                                   WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID } };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (UserID: reader.GetInt32(reader.GetOrdinal("UserID")),
                        UserName: reader.GetString(reader.GetOrdinal("UserName")),
                        Password: reader.GetString(reader.GetOrdinal("Password")),
                        IsActive: reader.GetBoolean(reader.GetOrdinal("IsActive")),
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
                        IsFound: true);
                }
            }

            return (UserID: -1, UserName: string.Empty, Password: string.Empty, IsActive: false, NationalNo: string.Empty, FirstName: string.Empty, SecondName: string.Empty, ThirdName: string.Empty, LastName: string.Empty, DateOfBirth: DateTime.MaxValue, Gender: false, Address: string.Empty, Phone: string.Empty, Email: string.Empty, NationalityCountryID: -1, ImagePath: string.Empty, IsFound: false);
        }


        public static async Task<(int UserID, int PersonID, bool IsActive, string NationalNo, string FirstName, 
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, 
            string Phone, string Email, int NationalityCountryID, string ImagePath, bool IsFound)> GetAsync(string UserName, string Password)
        {
            const string query = @"SELECT u.UserID, u.PersonID, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, 
                                   	   p.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID 
                                   WHERE UserName = @UserName AND Password = @Password;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password }
            };

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                    return (UserID: reader.GetInt32(reader.GetOrdinal("UserID")),
                        PersonID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        IsActive: reader.GetBoolean(reader.GetOrdinal("IsActive")),
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
                        IsFound: true);
                }
            }

            return (UserID: -1, PersonID: -1, IsActive: false, NationalNo: string.Empty, FirstName: string.Empty, SecondName: string.Empty, ThirdName: string.Empty, LastName: string.Empty, DateOfBirth: DateTime.MaxValue, Gender: false, Address: string.Empty, Phone: string.Empty, Email: string.Empty, NationalityCountryID: -1, ImagePath: string.Empty, IsFound: false);
        }


        public static async Task<IEnumerable<(int UserID, int PersonID, string UserName, string Password, bool IsActive, string NationalNo,
            string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)>> GetAllAsync()
        {
            const string query = @"SELECT u.UserID, p.PersonID, u.UserName, u.Password, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, 
                                  p.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
                           FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID;";

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (reader is null || !reader.HasRows)
                    return new List<(int UserID, int PersonID, string UserName, string Password, bool IsActive, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)>();

                const int minimumNumberOfUsers = 20;
                List<(int UserID, int PersonID, string UserName, string Password, bool IsActive, string NationalNo, string FirstName, string SecondName, string ThirdName,
                    string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)> users =
                new List<(int UserID, int PersonID, string UserName, string Password, bool IsActive, string NationalNo, string FirstName, string SecondName, string ThirdName,
                    string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)>(minimumNumberOfUsers);

                int indThirdName = reader.GetOrdinal("ThirdName"), indEmail = reader.GetOrdinal("Email"), indImagePath = reader.GetOrdinal("ImagePath");

                while (await reader.ReadAsync())
                {
                    users.Add((UserID: reader.GetInt32(reader.GetOrdinal("UserID")),
                        PersonID: reader.GetInt32(reader.GetOrdinal("PersonID")),
                        UserName: reader.GetString(reader.GetOrdinal("UserName")),
                        Password: reader.GetString(reader.GetOrdinal("Password")),
                        IsActive: reader.GetBoolean(reader.GetOrdinal("IsActive")),
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
                        ImagePath: reader.IsDBNull(indImagePath) ? string.Empty : reader.GetString(indImagePath)
                        ));
                }

                return users;
            }
        }


        public static async Task<DataTable> GetUsersAsync(bool IsActive)
        {
            const string query = @"SELECT u.UserID, u.PersonID,
                                   	FullName = CONCAT(p.FirstName, ' ', p.SecondName, ' ', COALESCE(p.ThirdName + ' ', ''), p.LastName),
                                   	u.UserName, u.IsActive 
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID WHERE u.IsActive = @IsActive;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive } };

            DataTable users = new DataTable();

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) users.Load(reader);
            }

            return users;
        }

        public static async Task<DataTable> GetUsersAsync()
        {
            const string query = @"SELECT u.UserID, u.PersonID,
	                                   FullName = CONCAT(p.FirstName, ' ', p.SecondName, ' ', COALESCE(p.ThirdName + ' ', ''), p.LastName),
	                                   u.UserName, u.IsActive 
                                   FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID;";

            DataTable users = new DataTable();

            using (SqlDataReader reader = await clsSqlDBExecutor.ExecuteReaderAsync(clsDBSettings.CnnString("DVLD_DB"), query))
            {
                if (!(reader is null) && reader.HasRows) users.Load(reader);
            }

            return users;
        }


        public static async Task<bool> IsExsitsAsync(int UserID)
        {
            const string query = "SELECT 1 FROM Users WHERE UserID = @UserID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }
        
        public static async Task<bool> IsExsitsByPersonIDAsync(int PersonID)
        {
            const string query = "SELECT 1 FROM Users WHERE PersonID = @PersonID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID } };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<bool> IsExsitsAsync(string UserName, string Password)
        {
            const string query = "SELECT 1 FROM Users WHERE UserName = @UserName AND Password = @Password;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };

            return await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) != null;
        }

        public static async Task<(bool IsExsits, bool IsActive)> IsExsitsAndActiveAsync(string UserName, string Password)
        {
            const string query = @"DECLARE @USERID INT = (SELECT UserID FROM Users WHERE UserName = @UserName AND Password = @Password);
                                   IF @USERID > 0 BEGIN
                                       SET @USERID *= (SELECT IsActive FROM Users WHERE UserID = @USERID); 
                                       SELECT CASE WHEN @USERID > 0 THEN 3 ELSE 2 END;
                                   END;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };

            object result = await clsSqlDBExecutor.ExecuteScalarAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters);

            return int.TryParse(result?.ToString(), out int num)
                ? (IsExsits: (num & 2) == 2, IsActive: (num & 1) == 1)
                : (IsExsits: false, IsActive: false);
        }


        //************** U **************\\

        public static async Task<bool> UpdateAsync(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            const string query = @"UPDATE Users
                                   SET PersonID = @PersonID,
                                       UserName = @UserName,
                                       Password = @Password,
                                       IsActive = @IsActive
                                   WHERE UserID = @UserID;
                                   SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID },
                new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID },
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive }
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }


        public static async Task<bool> ActivateAsync(int UserID)
        {
            const string query = "UPDATE Users SET IsActive = 1 WHERE UserID = @UserID; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> DeactivateAsync(int UserID)
        {
            const string query = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> ActivateAsync(string UserName, string Password)
        {
            const string query = "UPDATE Users SET IsActive = 1 WHERE UserName = @UserName AND Password = @Password; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };
            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }

        public static async Task<bool> DeactivateAsync(string UserName, string Password)
        {
            const string query = "UPDATE Users SET IsActive = 0 WHERE UserName = @UserName AND Password = @Password; SELECT @@ROWCOUNT;";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };
            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }


        //************** D **************\\

        public static async Task<bool> DeleteAsync(int UserID)
        {
            const string query = "DELETE FROM Users WHERE UserID = @UserID;";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID } };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        
        public static async Task<bool> DeleteAsync(string UserName, string Password)
        {
            const string query = "DELETE FROM Users WHERE UserName = @UserName AND Password = @Password;";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, _UserNameColumnSize) { Value = UserName },
                new SqlParameter("@Password", SqlDbType.NVarChar, _PasswordColumnSize) { Value = Password },
            };

            return await clsSqlDBExecutor.ExecuteNonQueryAsync(clsDBSettings.CnnString("DVLD_DB"), query, parameters) > 0;
        }
        #endregion
    }
}
