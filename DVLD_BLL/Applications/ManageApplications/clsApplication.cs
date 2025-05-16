using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Helper.Extensions;
using DVLD_DAL.Applications.ManageApplications;
using DVLD_BLL.Applications.ManageApplicationTypes;
using DVLD_BLL.People;
using DVLD_BLL.Users;
using static DVLD_BLL.Applications.ManageApplications.clsApplication;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Security.Policy;

namespace DVLD_BLL.Applications.ManageApplications
{
    public struct ApplicationID : IEquatable<ApplicationID>, IComparable<ApplicationID>, IComparer<ApplicationID>
    {
        #region Constructors
        private ApplicationID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static ApplicationID Empty => new ApplicationID(0);
        #endregion


        #region Public Static Methods
        public static ApplicationID CreateNew(int id) => new ApplicationID(id);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(ApplicationID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(ApplicationID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(ApplicationID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(ApplicationID x, ApplicationID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(ApplicationID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is ApplicationID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(ApplicationID left, ApplicationID right) => left.Equals(right);
        public static bool operator ==(ApplicationID left, int @int) => left.Equals(@int);

        public static bool operator !=(ApplicationID left, ApplicationID right) => !(left == right);
        public static bool operator !=(ApplicationID left, int @int) => !(left == @int);


        public static bool operator >(ApplicationID left, ApplicationID right) => left.Value > right.Value;
        public static bool operator >(ApplicationID left, int @int) => left.Value > @int;

        public static bool operator <(ApplicationID left, ApplicationID right) => left.Value < right.Value;
        public static bool operator <(ApplicationID left, int @int) => left.Value < @int;


        public static bool operator <=(ApplicationID left, ApplicationID right) => left.Value <= right.Value;
        public static bool operator <=(ApplicationID left, int @int) => left.Value <= @int;

        public static bool operator >=(ApplicationID left, ApplicationID right) => left.Value >= right.Value;
        public static bool operator >=(ApplicationID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsApplication : IEquatable<clsApplication>, IComparable<clsApplication>
    {
        public enum enApplicationStatus { None = 0, New = 1, Cancelled = 2, Completed = 3 }

        #region Private Constructors
        //private clsApplication() : this(ID: ApplicationID.Empty, ApplicantPersonID: PersonID.Empty, ApplicationDate: DateTime.MaxValue, ApplicationTypeID: ApplicationTypeID.Empty, ApplicationStatus: enApplicationStatus.None, LastStatusDate: DateTime.MaxValue, PaidFees: .0f, CreatedByUserID: UserID.Empty, Mode: Mode.Add) { }

        //private clsApplication(ApplicationID ID, PersonID ApplicantPersonID, DateTime ApplicationDate, ApplicationTypeID ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, UserID CreatedByUserID, Mode Mode) =>
        //    (this.ID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID, _mode) = (ID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID, Mode);


        private clsApplication() : this(ID: ApplicationID.Empty, ApplicantPerson: clsPerson.Empty, ApplicationDate: DateTime.MaxValue, 
            ApplicationTypeID: ApplicationTypeID.Empty, ApplicationStatus: enApplicationStatus.None, LastStatusDate: DateTime.MaxValue, 
            PaidFees: .0f, CreatedByUserID: UserID.Empty, Mode: Mode.Add) { }

        private clsApplication(ApplicationID ID, clsPerson ApplicantPerson, DateTime ApplicationDate, ApplicationTypeID ApplicationTypeID, 
            enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, UserID CreatedByUserID, Mode Mode) =>
            (this.ID, this.ApplicantPerson, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID, _mode) = 
                (ID, ApplicantPerson, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID, Mode);
        #endregion

        #region Public Static Creation Methods
        public static clsApplication CreateNew(clsPerson ApplicantPerson, DateTime ApplicationDate, ApplicationTypeID ApplicationTypeID, 
            enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, UserID CreatedByUserID) =>
            new clsApplication(ID: ApplicationID.Empty, ApplicantPerson: ApplicantPerson, ApplicationDate: ApplicationDate, ApplicationTypeID: ApplicationTypeID,
                ApplicationStatus: ApplicationStatus, LastStatusDate: LastStatusDate, PaidFees: PaidFees, CreatedByUserID: CreatedByUserID, Mode: Mode.Add);
        #endregion

        #region Public Internal Creation Methods
        internal static clsApplication CreateFromDB(ApplicationID ApplicationID, clsPerson ApplicantPerson, DateTime ApplicationDate, 
            ApplicationTypeID ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, UserID CreatedByUserID) =>
            new clsApplication(ID: ApplicationID, ApplicantPerson: ApplicantPerson, ApplicationDate: ApplicationDate, ApplicationTypeID: ApplicationTypeID,
                ApplicationStatus: ApplicationStatus, LastStatusDate: LastStatusDate, PaidFees: PaidFees, CreatedByUserID: CreatedByUserID, Mode: Mode.Update);
        #endregion


        #region Private Fields
        private Mode _mode { get; set; }
        #endregion

        #region Public Properties
        public ApplicationID ID { get; set; }
        public clsPerson ApplicantPerson { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationTypeID ApplicationTypeID { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public UserID CreatedByUserID { get; set; }

        public static clsApplication Empty => new clsApplication();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(clsApplication left, clsApplication right) => left.Equals(right);
        public static bool operator !=(clsApplication left, clsApplication right) => !(left == right);

        public static bool operator >(clsApplication left, clsApplication right) => left.CompareTo(right) > 0;
        public static bool operator <(clsApplication left, clsApplication right) => left.CompareTo(right) < 0;

        public static bool operator >=(clsApplication left, clsApplication right) => left.CompareTo(right) >= 0;
        public static bool operator <=(clsApplication left, clsApplication right) => left.CompareTo(right) <= 0;
        #endregion


        #region Overridden Methods
        public override bool Equals(object obj) => obj is clsApplication other && this.Equals(other);

        public override int GetHashCode() => (ID, ApplicantPerson, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID).GetHashCode();
        #endregion


        #region Private Methods
        private bool _isLessThan(clsApplication other) => 
            this.IsEmpty() ? other.IsNotEmpty() : ID < other.ID 
            && ApplicantPerson < other.ApplicantPerson && ApplicationDate < other.ApplicationDate 
            && ApplicationTypeID < other.ApplicationTypeID && LastStatusDate < other.LastStatusDate
            && PaidFees < other.PaidFees && CreatedByUserID < other.CreatedByUserID;
        #endregion


        #region Public Methods
        public int CompareTo(clsApplication other) => this.Equals(other) ? 0 : this._isLessThan(other) ? -1 : 1;

        public bool NotEquals(clsApplication other) => !this.Equals(other);
        public bool Equals(clsApplication other) =>
            this.IsEmpty() ? other.IsEmpty() : ID == other.ID 
            && ApplicantPerson.Equals(other.ApplicantPerson) && ApplicationDate.Equals(other.ApplicationDate)
            && ApplicationTypeID.Equals(other.ApplicationTypeID) && LastStatusDate.Equals(other.LastStatusDate)
            && PaidFees.Equals(other.PaidFees) && CreatedByUserID.Equals(other.CreatedByUserID);

        public bool IsNotEmpty() => !this.IsEmpty();
        public bool IsEmpty() => 
            ID.IsEmpty() || ApplicantPerson.IsEmpty() || ApplicationDate.Equals(DateTime.MaxValue) 
            || LastStatusDate.Equals(DateTime.MaxValue) || ApplicationTypeID.IsEmpty() || PaidFees < 1 || CreatedByUserID.IsEmpty();
        #endregion

        #region Public Static Methods
        public static bool IsNotEmpty(clsApplication application) => !(application is null) || application.IsNotEmpty();
        public static bool IsEmpty(clsApplication application) => application is null || application.IsEmpty();
        #endregion


        #region Private Sync Methods
        private bool _addNew()
        {
            int newID = clsApplicationData.Add(ApplicantPerson.ID.Value, ApplicationDate, ApplicationTypeID.Value, (byte)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID.Value);

            this.ID = ApplicationID.CreateNew(newID);
            return this.ID.Value != -1;
        }

        private bool _update() => clsApplicationData.Update(ID.Value, ApplicantPerson.ID.Value, ApplicationDate, ApplicationTypeID.Value, (byte)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID.Value);
        #endregion

        #region Public Sync Methods
        //************** R **************\\
        public static DataTable GetApplications() => clsApplicationData.GetApplications();

        public static clsApplication Find(ApplicationID ID)
        {
            byte applicationStatus = 0;
            int applicantPersonID = -1, applicationTypeID = -1, createdByUserID = -1;
            DateTime applicationDate = DateTime.MaxValue, lastStatusDate = DateTime.MaxValue;
            float paidFees = .0f;

            bool gender = false;
            int countryID = -1;
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsApplicationData.Get(ID.Value, ref applicantPersonID, ref nationalNo, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref countryID, ref imagePath,
                ref applicationDate, ref applicationTypeID, ref applicationStatus, ref lastStatusDate, ref paidFees, ref createdByUserID);

            return isFound ? clsApplication.CreateFromDB(ApplicationID: ID, 
                ApplicantPerson: clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName,
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(countryID), ImagePath: imagePath
                    ),
                ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID), 
                ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: paidFees, 
                CreatedByUserID: UserID.CreateNew(createdByUserID)) : clsApplication.Empty;
        }

        public static clsApplication Find(PersonID ApplicantPersonID, UserID CreatedByUserID)
        {
            byte applicationStatus = 0;
            int applicationID = -1, applicationTypeID = -1;
            DateTime applicationDate = DateTime.MaxValue, lastStatusDate = DateTime.MaxValue;
            float paidFees = .0f;

            bool gender = false;
            int countryID = -1;
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.MaxValue;


            bool isFound = clsApplicationData.Get(ref applicationID, ApplicantPersonID.Value, ref nationalNo, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref countryID, ref imagePath,
                ref applicationDate, ref applicationTypeID, ref applicationStatus, ref lastStatusDate, ref paidFees, CreatedByUserID.Value);

            return isFound ? clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID),
                ApplicantPerson: clsPerson.CreateFromDB(ID: ApplicantPersonID, NationalNo: nationalNo, FirstName: firstName,
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(countryID), ImagePath: imagePath
                    ),
                ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: paidFees,
                CreatedByUserID: CreatedByUserID) : clsApplication.Empty;
        }


        //************** C U **************\\
        public bool Save()
        {
            switch (_mode)
            {
                case Mode.Add:
                    if (_addNew())
                    {
                        _mode = Mode.Update;
                        return true;
                    }
                    else return false;
                case Mode.Update: return _update();
            }

            return false;
        }


        //************** D **************\\
        public static bool Delete(ApplicationID ID) => clsApplicationData.Delete(ID.Value);
        #endregion


        #region Private Async Methods
        private async Task<bool> _addNewAsync()
        {
            int newID = await clsApplicationData.AddAsync(ApplicantPerson.ID.Value, ApplicationDate, ApplicationTypeID.Value, (byte)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID.Value);

            this.ID = ApplicationID.CreateNew(newID);
            return this.ID.Value != -1;
        }

        private async Task<bool> _updateAsync() => await clsApplicationData.UpdateAsync(ID.Value, ApplicantPerson.ID.Value, ApplicationDate, ApplicationTypeID.Value, (byte)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID.Value);
        #endregion

        #region Public Async Methods
        //************** R **************\\
        public static async Task<DataTable> GetApplicationsAsync() => await clsApplicationData.GetApplicationsAsync();

        public static async Task<IEnumerable<clsApplication>> GetAllAsync() =>
            (await clsApplicationData.GetAllAsync()).ToIEnumerable();


        public static async Task<clsApplication> FindAsync(ApplicationID ID)
        {
            (int applicantPersonID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth,
                bool gender, string address, string phone, string email, int nationalityCountryID, string imagePath, DateTime applicationDate,
                int applicationTypeID, byte applicationStatus, DateTime lastStatusDate, float paidFees, int createdByUserID, bool isFound)
                = await clsApplicationData.GetAsync(ID.Value);

            return isFound ? clsApplication.CreateFromDB(ApplicationID: ID, 
                ApplicantPerson: clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName, 
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address, 
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath
                    ),
                ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: paidFees, 
                CreatedByUserID: UserID.CreateNew(createdByUserID)) : clsApplication.Empty;
        }

        public static async Task<clsApplication> FindAsync(PersonID ApplicantPersonID, UserID CreatedByUserID)
        {
            (int applicationID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth,
                bool gender, string address, string phone, string email, int nationalityCountryID, string imagePath, DateTime applicationDate,
                int applicationTypeID, byte applicationStatus, DateTime lastStatusDate, float paidFees, bool isFound) 
                = await clsApplicationData.GetAsync(ApplicantPersonID.Value, CreatedByUserID.Value);

            return isFound ? clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID), 
                ApplicantPerson: clsPerson.CreateFromDB(ID: ApplicantPersonID, NationalNo: nationalNo, FirstName: firstName,
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath
                    ),
                ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: paidFees, CreatedByUserID) 
                : clsApplication.Empty;
        }

        //************** C U **************\\
        public async Task<bool> SaveAsync()
        {
            switch (_mode)
            {
                case Mode.Add:
                    if (await _addNewAsync())
                    {
                        _mode = Mode.Update;
                        return true;
                    }
                    else return false;
                case Mode.Update: return await _updateAsync();
            }

            return false;
        }


        //************** D **************\\
        public static async Task<bool> DeleteAsync(ApplicationID ID) => await clsApplicationData.DeleteAsync(ID.Value);
        #endregion
    }

    public static class ApplicationExtensions
    {
        public static IEnumerable<clsApplication> ToIEnumerable(this IEnumerable<(int ApplicationID, int ApplicantPersonID,
            string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, 
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)> data)
        {
            clsPerson person;

            foreach ((int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName, string SecondName, string ThirdName,
                string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID,
                string ImagePath, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
                float PaidFees, int CreatedByUserID) item in data)
            {
                person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(item.ApplicantPersonID), NationalNo: item.NationalNo,
                    FirstName: item.FirstName, SecondName: item.SecondName, ThirdName: item.ThirdName, LastName: item.LastName,
                    DateOfBirth: item.DateOfBirth, Gender: item.Gender, Address: item.Address, Phone: item.Phone, Email: item.Email,
                    NationalityCountryID: CountryID.CreateNew(item.NationalityCountryID), ImagePath: item.ImagePath);

                yield return clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(item.ApplicationID), ApplicantPerson: person,
                    ApplicationDate: item.ApplicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(item.ApplicationTypeID),
                    ApplicationStatus: (enApplicationStatus)item.ApplicationStatus, LastStatusDate: item.LastStatusDate,
                    PaidFees: item.PaidFees, CreatedByUserID: UserID.CreateNew(item.CreatedByUserID));
            }
        }

        public static List<clsApplication> ToList(this IEnumerable<(int ApplicationID, int ApplicantPersonID, string NationalNo, 
            string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, 
            string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID, 
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)> data)
        {
            List<clsApplication> result = new List<clsApplication>(data.Count());
            clsPerson person; clsApplication application;

            foreach ((int ApplicationID, int ApplicantPersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, 
                string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone, string Email, int NationalityCountryID, 
                string ImagePath, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, 
                float PaidFees, int CreatedByUserID) item in data)
            {
                person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(item.ApplicantPersonID), NationalNo: item.NationalNo,
                    FirstName: item.FirstName, SecondName: item.SecondName, ThirdName: item.ThirdName, LastName: item.LastName,
                    DateOfBirth: item.DateOfBirth, Gender: item.Gender, Address: item.Address, Phone: item.Phone, Email: item.Email,
                    NationalityCountryID: CountryID.CreateNew(item.NationalityCountryID), ImagePath: item.ImagePath);

                application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(item.ApplicationID), ApplicantPerson: person,
                    ApplicationDate: item.ApplicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(item.ApplicationTypeID),
                    ApplicationStatus: (enApplicationStatus)item.ApplicationStatus, LastStatusDate: item.LastStatusDate,
                    PaidFees: item.PaidFees, CreatedByUserID: UserID.CreateNew(item.CreatedByUserID));

                result.Add(application);
            }

            return result;
        }
    }
}
