using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelperClasses.Extensions;
using DVLD_DAL.Users;
using DVLD_BLL.People;

namespace DVLD_BLL.Users
{
    public struct UserID : IEquatable<UserID>, IComparable<UserID>, IComparer<UserID>
    {
        #region Constructors
        private UserID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static UserID Empty => new UserID(0);
        #endregion


        #region Public Static Methods
        public static UserID CreateNew(int id) => new UserID(id);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(UserID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(UserID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(UserID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(UserID x, UserID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(UserID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is UserID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(UserID left, UserID right) => left.Equals(right);
        public static bool operator ==(UserID left, int @int) => left.Equals(@int);

        public static bool operator !=(UserID left, UserID right) => !(left == right);
        public static bool operator !=(UserID left, int @int) => !(left == @int);


        public static bool operator >(UserID left, UserID right) => left.Value > right.Value;
        public static bool operator >(UserID left, int @int) => left.Value > @int;

        public static bool operator <(UserID left, UserID right) => left.Value < right.Value;
        public static bool operator <(UserID left, int @int) => left.Value < @int;


        public static bool operator <=(UserID left, UserID right) => left.Value <= right.Value;
        public static bool operator <=(UserID left, int @int) => left.Value <= @int;

        public static bool operator >=(UserID left, UserID right) => left.Value >= right.Value;
        public static bool operator >=(UserID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsUser : IEquatable<clsUser>, IComparable<clsUser>
    {

        #region Private Constructores
        private clsUser() : this(id: UserID.Empty, person: clsPerson.Empty, userName: string.Empty, password: string.Empty, isActive: false, mode: Mode.Add) { }

        private clsUser(UserID id, clsPerson person, string userName, string password, bool isActive, Mode mode)
        {
            this.ID = id;
            this.Person = person;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            _mode = mode;
        }
        #endregion


        #region Static Creation Methods
        public static clsUser CreateNew(clsPerson person, string userName, string password, bool isActive) =>
            new clsUser(id: UserID.Empty, person: person, userName: userName, password: password, isActive: isActive, mode: Mode.Add);

        internal static clsUser CreateFromDB(UserID ID, clsPerson Person, string UserName, string Password, bool IsActive) =>
            new clsUser(id: ID, person: Person, userName: UserName, password: Password, isActive: IsActive, mode: Mode.Update);
        #endregion


        #region Private Fields
        private Mode _mode { get; set; }
        #endregion

        #region Public Properties
        public UserID ID { get; private set; }
        public clsPerson Person { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public static clsUser Empty => new clsUser();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(clsUser user1, clsUser user2) => user1.Equals(user2);
        public static bool operator !=(clsUser user1, clsUser user2) => !(user1 == user2);

        public static bool operator >(clsUser user1, clsUser user2) => user1.CompareTo(user2) > 0;
        public static bool operator <(clsUser user1, clsUser user2) => user1.CompareTo(user2) < 0;

        public static bool operator >=(clsUser user1, clsUser user2) => user1.CompareTo(user2) >= 0;
        public static bool operator <=(clsUser user1, clsUser user2) => user1.CompareTo(user2) <= 0;
        #endregion


        #region Overridden Methods
        public override bool Equals(object obj) => obj is clsUser other && this.Equals(other);

        public override int GetHashCode() => (ID, Person, UserName, Password, IsActive).GetHashCode();

        public override string ToString() => $"UserID = {ID.Value}, PersonID = {Person.ID}, Username = {UserName}, Password = {Password}, IsActive = {(IsActive ? "Yes" : "No")}";
        #endregion


        #region Private Methods
        private bool _isLessThan(clsUser other) => this.IsEmpty() ? other.IsNotEmpty() : ID < other.ID && Person.ID < other.Person.ID && UserName.CompareTo(other.UserName) < 0 && Password.CompareTo(other.Password) < 0;
        #endregion


        #region Public Methods
        public int CompareTo(clsUser other) => this.Equals(other) ? 0 : this._isLessThan(other) ? -1 : 1;

        public bool NotEquals(clsUser other) => !this.Equals(other);

        public bool Equals(clsUser other) => this.IsEmpty() ? other.IsEmpty() : ID == other.ID && Person.Equals(other.Person) && UserName.CompareTo(other.UserName) == 0 && Password.CompareTo(other.Password) == 0 && IsActive == other.IsActive;

        public static bool IsNotEmpty(clsUser user) => !(user is null) || user.IsNotEmpty();
        public static bool IsEmpty(clsUser user) => user is null || user.IsEmpty();

        public bool IsNotEmpty() => !this.IsEmpty();
        public bool IsEmpty() => ID.IsEmpty() || Person.IsEmpty() || UserName.IsNullOrEmptyOrWhiteSpace() || Password.IsNullOrEmptyOrWhiteSpace();
        #endregion


        #region Private Sync Methods
        private bool _addNew() 
        {
            int newID = clsUserData.AddNew(Person.ID.Value, UserName, Password, IsActive);
            this.ID = UserID.CreateNew(newID);
            return this.ID.Value > 0; 
        }
        
        private bool _update() => clsUserData.Update(ID.Value, Person.ID.Value, UserName, Password, IsActive);
        #endregion

        #region Public Sync Methods
        //******************   R    ******************\\
        public static clsUser Find(UserID UserID)
        {
            int personID = -1; string userName = "", password = ""; bool isActive = false;

            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.MaxValue;
            bool gender = false, isFound;
            int nationalityCountryID = -1;

            isFound = clsUserData.Get(UserID.Value, ref personID, ref userName, ref password, ref isActive, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsUser.CreateFromDB(ID: UserID, Person: clsPerson.CreateFromDB(PersonID.CreateNew(personID), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath), UserName: userName, Password: password, IsActive: isActive) : clsUser.Empty;
        }
        
        public static clsUser Find(PersonID PersonID)
        {
            int userID = -1; string userName = "", password = ""; bool isActive = false;

            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.MaxValue;
            bool gender = false, isFound;
            int nationalityCountryID = -1;

            isFound = clsUserData.Get(ref userID, PersonID.Value, ref userName, ref password, ref isActive, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsUser.CreateFromDB(ID: UserID.CreateNew(userID), Person: clsPerson.CreateFromDB(PersonID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath), UserName: userName, Password: password, IsActive: isActive) : clsUser.Empty;
        }

        public static clsUser Find(string UserName, string Password)
        {
            int userID = -1, personID = -1; bool isActive = false;

            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.MaxValue;
            bool gender = false, isFound;
            int nationalityCountryID = -1;

            isFound = clsUserData.Get(ref userID, ref personID, UserName, Password, ref isActive, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsUser.CreateFromDB(ID: UserID.CreateNew(userID), Person: clsPerson.CreateFromDB(PersonID.CreateNew(personID), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath), UserName: UserName, Password: Password, IsActive: isActive) : clsUser.Empty;
        }


        public static DataTable GetUsersByIsActive(bool IsActive) => clsUserData.GetUsersByIsActive(IsActive);

        public static DataTable GetUsers() => clsUserData.GetUsers();


        public static bool IsExsits(UserID UserID) => clsUserData.IsExsits(UserID.Value);
        
        public static bool IsExsitsByPersonID(PersonID PersonID) => clsUserData.IsExsitsByPersonID(PersonID.Value);
        
        public static bool IsExsits(string UserName) => clsUserData.IsExsits(UserName);

        public static bool IsExsits(string UserName, string Password) => clsUserData.IsExsits(UserName, Password);

        public static (bool IsExsits, bool IsActive) IsExsitsAndActive(string UserName, string Password) => clsUserData.IsExsitsAndActive(UserName, Password);


        //******************  C U  ******************\\

        public bool Save()
        {
            switch(this._mode)
            {
                case Mode.Add:
                    if (_addNew()) {
                        this._mode = Mode.Update;
                        return true;
                    }
                    else return false;
                case Mode.Update:
                    return _update();
            }

            return false;
        }


        //******************   D   ******************\\

        public static bool Delete(UserID ID) => clsUserData.Delete(ID.Value);

        public static bool Delete(string UserName, string Password) => clsUserData.Delete(UserName, Password);
        #endregion


        #region Private Async Methods 
        private async Task<bool> _addNewAsync()
        {
            int newID = await clsUserData.AddNewAsync(Person.ID.Value, UserName, Password, IsActive);
            this.ID = UserID.CreateNew(newID);
            return this.ID.Value > 0;
        }

        private async Task<bool> _updateAsync() => await clsUserData.UpdateAsync(ID.Value, Person.ID.Value, UserName, Password, IsActive);
        #endregion

        #region Public Async Methods
        //******************   R    ******************\\
        public static async Task<clsUser> FindAsync(UserID UserID)
        {
            (int personID, string userName, string password, bool isActive, string nationalNo, string firstName,
            string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender, string address,
            string phone, string email, int nationalityCountryID, string imagePath, bool isFound) = await clsUserData.GetAsync(UserID: UserID.Value);

            return isFound ? clsUser.CreateFromDB(ID: UserID, Person: clsPerson.CreateFromDB(PersonID.CreateNew(personID), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath), UserName: userName, Password: password, IsActive: isActive) : clsUser.Empty;
        }

        public static async Task<clsUser> FindAsync(PersonID PersonID)
        {
            (int userID, string userName, string password, bool isActive, string nationalNo, string firstName,
            string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender, string address,
            string phone, string email, int nationalityCountryID, string imagePath, bool isFound) = await clsUserData.GetByPersonIDAsync(PersonID: PersonID.Value);

            return isFound ? clsUser.CreateFromDB(ID: UserID.CreateNew(userID), Person: clsPerson.CreateFromDB(PersonID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath), UserName: userName, Password: password, IsActive: isActive) : clsUser.Empty;
        }

        public static async Task<clsUser> FindAsync(string UserName, string Password)
        {
            (int userID, int personID, bool isActive, string nationalNo, string firstName,
            string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender, string address,
            string phone, string email, int nationalityCountryID, string imagePath, bool isFound) = await clsUserData.GetAsync(UserName, Password);

            return isFound ? clsUser.CreateFromDB(ID: UserID.CreateNew(userID), Person: clsPerson.CreateFromDB(PersonID.CreateNew(personID), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath), UserName: UserName, Password: Password, IsActive: isActive) : clsUser.Empty;
        }


        public static async Task<DataTable> GetUsersAsync(bool IsActive) => await clsUserData.GetUsersAsync(IsActive);

        public static async Task<DataTable> GetUsersAsync() => await clsUserData.GetUsersAsync();
        

        public static async Task<IEnumerable<(int UserID, int PersonID, string UserName, string Password, bool IsActive, string NationalNo, 
            string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, 
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)>> GetAllAsync() => await clsUserData.GetAllAsync();
        
        public static async Task<IEnumerable<clsUser>> GetAllAsyncAsEnumerable() => (await clsUserData.GetAllAsync()).ToIEnumerable();


        public static async Task<bool> IsExsitsAsync(UserID UserID) => await clsUserData.IsExsitsAsync(UserID: UserID.Value);
        public static async Task<bool> IsExsitsAsync(PersonID PersonID) => await clsUserData.IsExsitsByPersonIDAsync(PersonID: PersonID.Value);

        public static async Task<bool> IsExsitsAsync(string UserName, string Password) => await clsUserData.IsExsitsAsync(UserName, Password);

        public static async Task<(bool IsExsits, bool IsActive)> IsExsitsAndActiveAsync(string UserName, string password) => 
            await clsUserData.IsExsitsAndActiveAsync(UserName, password);

        //******************  C U  ******************\\

        public async Task<bool> SaveAsync()
        {
            switch (this._mode)
            {
                case Mode.Add:
                    if (await _addNewAsync())
                    {
                        this._mode = Mode.Update;
                        return true;
                    }
                    else return false;
                case Mode.Update:
                    return await _updateAsync();
            }

            return false;
        }


        //******************   D   ******************\\

        public static async Task<bool> DeleteAsync(UserID ID) => await clsUserData.DeleteAsync(ID.Value);

        public static async Task<bool> DeleteAsync(string UserName, string Password) => await clsUserData.DeleteAsync(UserName, Password);
        #endregion
    }

    public static class UserExtensions
    {
        public static IEnumerable<clsUser> ToIEnumerable(this IEnumerable<(int UserID, int PersonID, string UserName, string Password, 
            bool IsActive, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, 
            bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)> data)
        {
            clsPerson person;

            foreach ((int UserID, int PersonID, string UserName, string Password, bool IsActive, string NationalNo, string FirstName,
                      string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
                      string Address, string Phone, string Email, int NationalityCountryID, string ImagePath) item in data)
            {
                person = clsPerson.CreateFromDB(PersonID.CreateNew(item.PersonID), item.NationalNo, item.FirstName, item.SecondName,
                        item.ThirdName, item.LastName, item.DateOfBirth, item.Gender, item.Address, item.Phone,
                        item.Email, CountryID.CreateNew(item.NationalityCountryID), item.ImagePath);

                yield return clsUser.CreateFromDB(ID: UserID.CreateNew(item.UserID), Person: person,
                    UserName: item.UserName, Password: item.Password, IsActive: item.IsActive);
            }
        }

        public static List<clsUser> ToList(this IEnumerable<(int UserID, int PersonID, string UserName, string Password,
            bool IsActive, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)> data)
        {
            List<clsUser> result = new List<clsUser>(data.Count());
            clsPerson person;

            foreach ((int UserID, int PersonID, string UserName, string Password, bool IsActive, string NationalNo, string FirstName,
                      string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
                      string Address, string Phone, string Email, int NationalityCountryID, string ImagePath) item in data)
            {
                person = clsPerson.CreateFromDB(PersonID.CreateNew(item.PersonID), item.NationalNo, item.FirstName, item.SecondName,
                        item.ThirdName, item.LastName, item.DateOfBirth, item.Gender, item.Address, item.Phone,
                        item.Email, CountryID.CreateNew(item.NationalityCountryID), item.ImagePath);

                result.Add(clsUser.CreateFromDB(ID: UserID.CreateNew(item.UserID), Person: person,
                    UserName: item.UserName, Password: item.Password, IsActive: item.IsActive));
            }

            return result;
        }
    }
}
