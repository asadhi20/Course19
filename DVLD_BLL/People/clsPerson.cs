using System;
using System.Data;
using System.Threading.Tasks;
using DVLD_DAL.People;
using Helper.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVLD_BLL.People
{
    public enum Mode { Add, Update }

    public struct PersonID : IEquatable<PersonID>, IComparable<PersonID>, IComparer<PersonID>
    {
        #region Constructors
        private PersonID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static PersonID Empty => new PersonID(0);
        #endregion


        #region Public Static Methods
        public static PersonID CreateNew(int id) => new PersonID(id);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(PersonID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(PersonID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(PersonID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(PersonID x, PersonID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(PersonID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is PersonID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(PersonID left, PersonID right) => left.Equals(right);
        public static bool operator ==(PersonID left, int @int) => left.Equals(@int);

        public static bool operator !=(PersonID left, PersonID right) => !(left == right);
        public static bool operator !=(PersonID left, int @int) => !(left == @int);


        public static bool operator >(PersonID left, PersonID right) => left.Value > right.Value;
        public static bool operator >(PersonID left, int @int) => left.Value > @int;

        public static bool operator <(PersonID left, PersonID right) => left.Value < right.Value;
        public static bool operator <(PersonID left, int @int) => left.Value < @int;


        public static bool operator <=(PersonID left, PersonID right) => left.Value <= right.Value;
        public static bool operator <=(PersonID left, int @int) => left.Value <= @int;

        public static bool operator >=(PersonID left, PersonID right) => left.Value >= right.Value;
        public static bool operator >=(PersonID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsPerson : IEquatable<clsPerson>, IComparable<clsPerson> 
    {

        #region Private Constructors
        private clsPerson() : this(ID: PersonID.Empty, NationalNo: string.Empty, FirstName: string.Empty, SecondName: string.Empty, ThirdName: string.Empty,
            LastName: string.Empty, DateOfBirth: DateTime.MaxValue, Gender: false, Address: string.Empty, Phone: string.Empty, Email: string.Empty,
            NationalityCountryID: CountryID.Empty, ImagePath: string.Empty, Mode: Mode.Add) { }

        private clsPerson(PersonID ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            bool Gender, string Address, string Phone, string Email, CountryID NationalityCountryID, string ImagePath, Mode Mode) =>
            (this.ID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone,
            this.Email, this.NationalityCountryID, this.ImagePath, _mode) = (ID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, 
            Gender, Address, Phone, Email, NationalityCountryID, ImagePath, Mode);
        #endregion


        #region Public Static Creation Methods
        public static clsPerson CreateNew(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, CountryID NationalityCountryID, string ImagePath) =>
            new clsPerson(ID: PersonID.Empty, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address,
                Phone, Email, NationalityCountryID, ImagePath, Mode: Mode.Add);
        #endregion

        #region Public Internal Creation Methods
        internal static clsPerson CreateFromDB(PersonID ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, 
            DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, CountryID NationalityCountryID, string ImagePath) =>
            new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath, Mode: Mode.Update);
        #endregion



        #region Private Fields
        private Mode _mode { get; set; }
        #endregion


        #region Public Properties
        public PersonID ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public CountryID NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public static clsPerson Empty => new clsPerson();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(clsPerson person1, clsPerson person2) => person1.Equals(person2);
        public static bool operator !=(clsPerson person1, clsPerson person2) => !(person1 == person2);

        public static bool operator >(clsPerson person1, clsPerson person2) => person1.CompareTo(person2) > 0;
        public static bool operator <(clsPerson person1, clsPerson person2) => person1.CompareTo(person2) < 0;

        public static bool operator >=(clsPerson person1, clsPerson person2) => person1.CompareTo(person2) >= 0;
        public static bool operator <=(clsPerson person1, clsPerson person2) => person1.CompareTo(person2) <= 0;
        #endregion


        #region Public Methods Overloaded
        public override bool Equals(object obj) => obj is clsPerson other && this.Equals(other);

        public override int GetHashCode() => (ID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath).GetHashCode();

        public override string ToString() => $"PersonID = {ID}, National No. = {NationalNo}, Name = {FullName()}, Birth Date = {DateOfBirth.ToShortDateString()}, Gender = {(Gender ? "Female" : "Male")}, Address = {Address}, Phone = {Phone}, {(Email.IsNullOrEmpty() ? "\r" : Email + ", ")} Nationality = {clsCountry.GetCountry(NationalityCountryID)}";
        #endregion


        #region Private Methods
        private bool _isLessThan(clsPerson other) =>
            this.IsEmpty() ? !other.IsEmpty() : ID < other.ID
            && NationalNo.CompareTo(other.NationalNo) < 0 && FirstName.CompareTo(other.FirstName) < 0 && SecondName.CompareTo(other.SecondName) < 0
            && ThirdName.CompareTo(other.ThirdName) < 0 && LastName.CompareTo(other.LastName) < 0 && DateOfBirth.CompareTo(other.DateOfBirth) < 0
            && Phone.CompareTo(other.Phone) < 0 && NationalityCountryID < other.NationalityCountryID;
        #endregion


        #region Public Methods
        public int CompareTo(clsPerson other) => this.Equals(other) ? 0 : this._isLessThan(other) ? -1 : 1;

        public bool NotEquals(clsPerson other) => !this.Equals(other);

        public bool Equals(clsPerson other) =>
            this.IsEmpty() ? other.IsEmpty() : ID == other.ID 
            && NationalNo.CompareTo(other.NationalNo) == 0 && FirstName.CompareTo(other.FirstName) == 0 && SecondName .CompareTo(other. SecondName) == 0
            && ThirdName .CompareTo(other. ThirdName) == 0 && LastName .CompareTo(other. LastName) == 0 && DateOfBirth.CompareTo(other.DateOfBirth) == 0
            && Address   .CompareTo(other.   Address) == 0 && Phone    .CompareTo(other.    Phone) == 0 && Email.CompareTo(other.Email) == 0
            && Gender == other.Gender && NationalityCountryID == other.NationalityCountryID && ImagePath.CompareTo(other.ImagePath) == 0;

        public bool IsNotEmpty() => !this.IsEmpty();

        public bool IsEmpty() => ID.IsEmpty()
            || NationalNo.IsNullOrEmptyOrWhiteSpace() 
            || FirstName .IsNullOrEmptyOrWhiteSpace() 
            || SecondName.IsNullOrEmptyOrWhiteSpace() 
            || LastName  .IsNullOrEmptyOrWhiteSpace() 
            || DateOfBirth.Equals(DateTime.MaxValue) 
            || Address   .IsNullOrEmptyOrWhiteSpace() 
            || Phone     .IsNullOrEmptyOrWhiteSpace() 
             ? NationalityCountryID.IsEmpty() : false;


        public string ToShortString() => 
            $"ID = {ID}, Name = {FullName()}, Birth Date = {DateOfBirth.ToShortDateString()}, Gender = {(Gender ? "Female" : "Male")}, Address = {Address}, Phone = {Phone}";

        public string FullName() => FirstName + ' ' + SecondName + ' ' + (ThirdName.IsNullOrEmptyOrWhiteSpace() ? string.Empty : ThirdName + ' ') + LastName;
        #endregion


        #region Public Static Methods
        public static bool IsNotEmpty(clsPerson person) => !(person is null) || person.IsNotEmpty();
        public static bool IsEmpty(clsPerson person) => person is null || person.IsEmpty();
        #endregion



        #region Private Sync Methods
        private bool _addNew() 
        {
            int newID = clsPersonData.AddNew(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID.Value, ImagePath);

            ID = PersonID.CreateNew(newID);
            return ID.Value != -1; 
        }
        
        private bool _update() => clsPersonData.Update(ID.Value, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID.Value, ImagePath);
        #endregion


        #region Public Sync Methods
        //**************   R    **************\\

        public static clsPerson Find(PersonID id)
        {
            int nationalityCountryID = -1;
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.Get(id.Value, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(id, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }

        public static clsPerson FindByNationalNo(string nationalNo)
        {
            int id = -1, nationalityCountryID = -1;
            string firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.GetByNationalNo(ref id, nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }

        public static clsPerson FindByFirstName(string firstName)
        {
            int id = -1, nationalityCountryID = -1;
            string nationalNo = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.GetByFirstName(ref id, ref nationalNo, firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }

        public static clsPerson FindBySecondName(string secondName) 
        {
            int id = -1, nationalityCountryID = -1;
            string nationalNo = "", firstName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.GetBySecondName(ref id, ref nationalNo, ref firstName, secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }

        public static clsPerson FindByThirdName(string thirdName)
        {
            int id = -1, nationalityCountryID = -1;
            string nationalNo = "", firstName = "", secondName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.GetByThirdName(ref id, ref nationalNo, ref firstName, ref secondName, thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }

        public static clsPerson FindByLastName(string lastName)
        {
            int id = -1, nationalityCountryID = -1;
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.GetByLastName(ref id, ref nationalNo, ref firstName, ref secondName, ref thirdName, lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static clsPerson FindByPhone(string phone)
        {
            int id = -1, nationalityCountryID = -1;
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", address = "", lastName = "", email = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.GetByPhone(ref id, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, phone, ref email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static clsPerson FindByEmail(string email)
        {
            int id = -1, nationalityCountryID = -1;
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", imagePath = "";
            bool gender = false;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsPersonData.GetByEmail(ref id, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, email, ref nationalityCountryID, ref imagePath);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        

        public static string GetImagePath(PersonID id) => clsPersonData.GetImagePath(id.Value);

        public static DataTable GetPeople() => clsPersonData.GetPeople();


        public static bool IsExists(PersonID id) => clsPersonData.IsExists(id.Value);

        public static bool IsExistsByNationalNo(string nationalNo) => clsPersonData.IsExistsByNationalNo(nationalNo);
        
        public static bool IsExistsByFullName(string firstName, string secondName, string thirdName, string lastName) => clsPersonData.IsExistsByFullName(firstName, secondName, thirdName, lastName);
        
        public static bool IsExistsByAddress(string address) => clsPersonData.IsExistsByAddress(address);
        
        public static bool IsExistsByPhone(string phone) => clsPersonData.IsExistsByPhone(phone);
        
        public static bool IsExistsByEmail(string email) => clsPersonData.IsExistsByEmail(email);
        
        public static bool IsExistsByImagePath(string imagePath) => clsPersonData.IsExistsByImagePath(imagePath);

        
        //**************  C  U  **************\\

        public bool Save()
        {
            switch (this._mode)
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


        //**************   D    **************\\


        public static bool Delete(PersonID id) => clsPersonData.Delete(id.Value);

        public static bool Delete(string nationalNo) => clsPersonData.Delete(nationalNo);
        
        public static bool Delete(string firstName, string secondName, string thirdName, string lastName) => clsPersonData.Delete(firstName, secondName, thirdName, lastName);
        #endregion


        #region Private Async Methods
        private async Task<bool> _addNewAsync()
        {
            int newID = await clsPersonData.AddNewAsync(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID.Value, ImagePath);

            ID = PersonID.CreateNew(newID);
            return ID.Value != -1;
        }


        private async Task<bool> _updateAsync() =>  await clsPersonData.UpdateAsync(ID.Value, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID.Value, ImagePath);
        #endregion


        #region Public Async Methods
        //**************   R    **************\\
        public static async Task<clsPerson> FindAsync(PersonID id)
        {
            (string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender,
                string address, string phone, string email, int nationalityCountryID, string imagePath, bool isFound) 
                = await clsPersonData.GetAsync(id.Value);

            return isFound ? clsPerson.CreateFromDB(id, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }

        public static async Task<clsPerson> FindByNationalNoAsync(string nationalNo)
        {
            (int id, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender,
                string address, string phone, string email, int nationalityCountryID, string imagePath, bool isFound)
                = await clsPersonData.GetByNationalNoAsync(nationalNo);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static async Task<clsPerson> FindByFirstNameAsync(string firstName)
        {
            (int id, string nationalNo, string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender,
                string address, string phone, string email, int nationalityCountryID, string imagePath, bool isFound)
                = await clsPersonData.GetByFirstNameAsync(firstName);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static async Task<clsPerson> FindBySecondNameAsync(string secondName)
        {
            (int id, string nationalNo, string firstName, string thirdName, string lastName, DateTime dateOfBirth, bool gender,
                string address, string phone, string email, int nationalityCountryID, string imagePath, bool isFound)
                = await clsPersonData.GetBySecondNameAsync(secondName);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static async Task<clsPerson> FindByThirdNameAsync(string thirdName)
        {
            (int id, string firstName, string secondName, string nationalNo, string lastName, DateTime dateOfBirth, bool gender,
                string address, string phone, string email, int nationalityCountryID, string imagePath, bool isFound)
                = await clsPersonData.GetByThirdNameAsync(thirdName);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static async Task<clsPerson> FindByLastNameAsync(string lastName)
        {
            (int id, string nationalNo, string firstName, string secondName, string thirdName, DateTime dateOfBirth, bool gender,
                string address, string phone, string email, int nationalityCountryID, string imagePath, bool isFound)
                = await clsPersonData.GetByLastNameAsync(lastName);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static async Task<clsPerson> FindByPhoneAsync(string phone)
        {
            (int id, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender,
                string address, string email, int nationalityCountryID, string imagePath, bool isFound)
                = await clsPersonData.GetByPhoneAsync(phone);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }
        
        public static async Task<clsPerson> FindByEmailAsync(string email)
        {
            (int id, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender,
                string address, string phone, int nationalityCountryID, string imagePath, bool isFound)
                = await clsPersonData.GetByEmailAsync(email);

            return isFound ? clsPerson.CreateFromDB(PersonID.CreateNew(id), nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, CountryID.CreateNew(nationalityCountryID), imagePath) : clsPerson.Empty;
        }


        public static async Task<string> GetImagePathAsync(PersonID id) => await clsPersonData.GetImagePathAsync(id.Value);

        public static async Task<DataTable> GetPeopleAsync() => await clsPersonData.GetPeopleAsync();


        public static async Task<IEnumerable<(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)>> GetAllAsync() => await clsPersonData.GetAllAsync();

        public static async Task<IEnumerable<clsPerson>> GetAllAsyncAsIEnumerable() => (await clsPersonData.GetAllAsync()).ToIEnumerable();


        public static async Task<bool> IsExistsAsync(PersonID id) => await clsPersonData.IsExistsAsync(id.Value);

        public static async Task<bool> IsExistsByNationalNoAsync(string nationalNo) => await clsPersonData.IsExistsByNationalNoAsync(nationalNo);
        
        public static async Task<bool> IsExistsByFullNameAsync(string firstName, string secondName, string thirdName, string lastName) => await clsPersonData.IsExistsByFullNameAsync(firstName, secondName, thirdName, lastName);
        
        public static async Task<bool> IsExistsByAddressAsync(string address) => await clsPersonData.IsExistsByAddressAsync(address);
        
        public static async Task<bool> IsExistsByPhoneAsync(string phone) => await clsPersonData.IsExistsByPhoneAsync(phone);
        
        public static async Task<bool> IsExistsByEmailAsync(string email) => await clsPersonData.IsExistsByEmailAsync(email);
        
        public static async Task<bool> IsExistsByImagePathAsync(string imagePath) => await clsPersonData.IsExistsByImagePathAsync(imagePath);

        
        //**************  C  U  **************\\

        public async Task<bool> SaveAsync()
        {
            switch (this._mode)
            {
                case Mode.Add:
                    if (await _addNewAsync()) {
                        this._mode = Mode.Update;
                        return true;
                    }
                    else return false;
                case Mode.Update:
                    return await _updateAsync();
            }

            return false;
        }


        //**************   D    **************\\

        public static async Task<bool> DeleteAsync(PersonID id) => await clsPersonData.DeleteAsync(id.Value);

        public static async Task<bool> DeleteAsync(string nationalNo) => await clsPersonData.DeleteAsync(nationalNo);
        
        public static async Task<bool> DeleteAsync(string firstName, string secondName, string thirdName, string lastName) => await clsPersonData.DeleteAsync(firstName, secondName, thirdName, lastName);

        #endregion
    };

    public static class PersonExtensions
    {
        public static IEnumerable<clsPerson> ToIEnumerable(this IEnumerable<(int PersonID, string NationalNo, string FirstName, string SecondName, 
            string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, 
            int NationalityCountryID, string ImagePath)> data)
        {
            foreach ((int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
                        bool Gender,string Address, string Phone, string Email, int NationalityCountryID, string ImagePath) item in data)
            {
                yield return clsPerson.CreateFromDB(PersonID.CreateNew(item.PersonID), item.NationalNo, item.FirstName,
                    item.SecondName,item.ThirdName, item.LastName, item.DateOfBirth, item.Gender, item.Address,
                    item.Phone, item.Email, CountryID.CreateNew(item.NationalityCountryID), item.ImagePath);
            }
        }

        public static List<clsPerson> ToList(this IEnumerable<(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, 
            DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)> data)
        {
            List<clsPerson> result = new List<clsPerson>(data.Count());
            foreach ((int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
                        bool Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath) item in data)
            {
                result.Add(clsPerson.CreateFromDB(PersonID.CreateNew(item.PersonID), item.NationalNo, item.FirstName,
                    item.SecondName, item.ThirdName, item.LastName, item.DateOfBirth, item.Gender, item.Address,
                    item.Phone, item.Email, CountryID.CreateNew(item.NationalityCountryID), item.ImagePath));
            }

            return result;
        }
    }
}
