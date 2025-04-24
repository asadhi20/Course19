using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using DVLD_DAL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.Applications.DrivingLicenseServices;
using DVLD_BLL.Applications.ManageApplicationTypes;
using DVLD_BLL.People;
using DVLD_BLL.Users;
using static DVLD_BLL.Applications.ManageApplications.clsApplication;

namespace DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications
{
    public struct LocalDrivingLicenseApplicationID : IEquatable<LocalDrivingLicenseApplicationID>, IComparable<LocalDrivingLicenseApplicationID>, IComparer<LocalDrivingLicenseApplicationID>
    {
        #region Constructors
        private LocalDrivingLicenseApplicationID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static LocalDrivingLicenseApplicationID Empty => new LocalDrivingLicenseApplicationID(0);
        #endregion


        #region Public Static Methods
        public static LocalDrivingLicenseApplicationID CreateNew(int id) => new LocalDrivingLicenseApplicationID(id);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(LocalDrivingLicenseApplicationID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(LocalDrivingLicenseApplicationID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(LocalDrivingLicenseApplicationID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(LocalDrivingLicenseApplicationID x, LocalDrivingLicenseApplicationID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(LocalDrivingLicenseApplicationID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is LocalDrivingLicenseApplicationID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(LocalDrivingLicenseApplicationID left, LocalDrivingLicenseApplicationID right) => left.Equals(right);
        public static bool operator ==(LocalDrivingLicenseApplicationID left, int @int) => left.Equals(@int);

        public static bool operator !=(LocalDrivingLicenseApplicationID left, LocalDrivingLicenseApplicationID right) => !(left == right);
        public static bool operator !=(LocalDrivingLicenseApplicationID left, int @int) => !(left == @int);


        public static bool operator >(LocalDrivingLicenseApplicationID left, LocalDrivingLicenseApplicationID right) => left.Value > right.Value;
        public static bool operator >(LocalDrivingLicenseApplicationID left, int @int) => left.Value > @int;

        public static bool operator <(LocalDrivingLicenseApplicationID left, LocalDrivingLicenseApplicationID right) => left.Value < right.Value;
        public static bool operator <(LocalDrivingLicenseApplicationID left, int @int) => left.Value < @int;


        public static bool operator <=(LocalDrivingLicenseApplicationID left, LocalDrivingLicenseApplicationID right) => left.Value <= right.Value;
        public static bool operator <=(LocalDrivingLicenseApplicationID left, int @int) => left.Value <= @int;

        public static bool operator >=(LocalDrivingLicenseApplicationID left, LocalDrivingLicenseApplicationID right) => left.Value >= right.Value;
        public static bool operator >=(LocalDrivingLicenseApplicationID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsLocalDrivingLicenseApplication
    {
        #region Private Constructors
        private clsLocalDrivingLicenseApplication() : 
            this(id: LocalDrivingLicenseApplicationID.Empty, application: clsApplication.Empty,  licenseID: LicenseID.Empty, mode: Mode.Add) { }

        private clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID id, clsApplication application, LicenseID licenseID, Mode mode) =>
            (this.ID, this.Application, this.LicenseID, _mode) = (id, application, licenseID, mode);
        #endregion


        #region Public Static Creation Methods
        public static clsLocalDrivingLicenseApplication CreateNew(clsApplication application, LicenseID licenseID) =>
            new clsLocalDrivingLicenseApplication(id: LocalDrivingLicenseApplicationID.Empty, application: application, licenseID: licenseID, mode: Mode.Add);
        #endregion

        #region Internal Static Creation Methods
        internal static clsLocalDrivingLicenseApplication CreateFromDB(LocalDrivingLicenseApplicationID ID, clsApplication Application, LicenseID LicenseID) =>
            new clsLocalDrivingLicenseApplication(id: ID, application: Application, licenseID: LicenseID, mode: Mode.Update);
        #endregion


        #region Private Fields
        private Mode _mode { get; set; }
        #endregion

        #region Public Properties
        public LocalDrivingLicenseApplicationID ID { get; private set; }
        public clsApplication Application { get; set; }
        public LicenseID LicenseID { get; set; }

        public static clsLocalDrivingLicenseApplication Empty => new clsLocalDrivingLicenseApplication();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(clsLocalDrivingLicenseApplication left, clsLocalDrivingLicenseApplication right) => left.Equals(right);
        public static bool operator !=(clsLocalDrivingLicenseApplication left, clsLocalDrivingLicenseApplication right) => !(left == right);

        public static bool operator >(clsLocalDrivingLicenseApplication left, clsLocalDrivingLicenseApplication right) => left.CompareTo(right) > 0;
        public static bool operator <(clsLocalDrivingLicenseApplication left, clsLocalDrivingLicenseApplication right) => left.CompareTo(right) < 0;

        public static bool operator >=(clsLocalDrivingLicenseApplication left, clsLocalDrivingLicenseApplication right) => left.CompareTo(right) >= 0;
        public static bool operator <=(clsLocalDrivingLicenseApplication left, clsLocalDrivingLicenseApplication right) => left.CompareTo(right) <= 0;
        #endregion


        #region Overridden Methods
        public override bool Equals(object obj) => obj is clsLocalDrivingLicenseApplication other && this.Equals(other);

        public override int GetHashCode() => (ID, Application, LicenseID).GetHashCode();
        #endregion


        #region Private Methods
        private bool _isLessThan(clsLocalDrivingLicenseApplication other) =>
            this.IsEmpty() ? other.IsNotEmpty() : ID < other.ID && Application < other.Application && LicenseID < other.LicenseID;
        #endregion


        #region Public Methods
        public int CompareTo(clsLocalDrivingLicenseApplication other) => 
            this.Equals(other) ? 0 : this._isLessThan(other) ? -1 : 1;

        public bool NotEquals(clsLocalDrivingLicenseApplication other) => !this.Equals(other);

        public bool Equals(clsLocalDrivingLicenseApplication other) =>
            this.IsEmpty() ? other.IsEmpty() : ID == other.ID && Application == other.Application && LicenseID == other.LicenseID;

        public bool IsNotEmpty() => !this.IsEmpty();
        public bool IsEmpty() => ID.IsEmpty() || Application.IsEmpty() || LicenseID.IsEmpty();
        #endregion

        #region Public Static Methods
        public static bool IsNotEmpty(clsLocalDrivingLicenseApplication LDLApplication) => !(LDLApplication is null) || LDLApplication.IsNotEmpty();
        public static bool IsEmpty(clsLocalDrivingLicenseApplication LDLApplication) => LDLApplication is null || LDLApplication.IsEmpty();
        #endregion


        #region Private Sync Methods
        private bool _addNew()
        {
            (int applicationID, int localDLApplicationID) = clsLocalDrivingLicenseApplicationData.Add(Application.ApplicantPerson.ID.Value,
                Application.ApplicationDate, Application.ApplicationTypeID.Value, (byte)Application.ApplicationStatus,
                Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID.Value, LicenseID.Value);

            if (applicationID > 0 && localDLApplicationID > 0)
            {
                this.ID = LocalDrivingLicenseApplicationID.CreateNew(localDLApplicationID);

                this.Application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID),
                    ApplicantPerson: Application.ApplicantPerson, ApplicationDate: Application.ApplicationDate,
                    Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreatedByUserID);

                return true;
            }

            return false;
        }

        private bool _update() => clsLocalDrivingLicenseApplicationData.Update(ID.Value, Application.ID.Value, Application.ApplicantPerson.ID.Value,
            Application.ApplicationDate, Application.ApplicationTypeID.Value, (byte)Application.ApplicationStatus, Application.LastStatusDate,
            Application.PaidFees, Application.CreatedByUserID.Value, LicenseID.Value);
        #endregion

        #region Public Sync Methods
        //************** R **************\\
        public static DataTable GetLDLApplications() => clsLocalDrivingLicenseApplicationData.GetAllLDLApplications();

        public static clsLocalDrivingLicenseApplication Find(LocalDrivingLicenseApplicationID ID)
        {
            byte applicationStatus = 0;
            int applicationID = -1, applicantPersonID = -1, applicationTypeID = -1, applicationCreatedByUserID = -1, licenseID = -1;
            DateTime applicationDate = DateTime.MaxValue, lastStatusDate = DateTime.MaxValue;
            float applicationPaidFees = .0f;

            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            int nationalityCountryID = -1;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsLocalDrivingLicenseApplicationData.Get(LocalDrivingLicenseApplicationID: ID.Value, ApplicationID: ref applicationID, 
                ApplicantPersonID: ref applicantPersonID, NationalNo: ref nationalNo, FirstName: ref firstName, SecondName: ref secondName,
                ThirdName: ref thirdName, LastName: ref lastName, DateOfBirth: ref dateOfBirth, Gender: ref gender, Address: ref address,
                Phone: ref phone, Email: ref email, NationalityCountryID: ref nationalityCountryID, ImagePath: ref imagePath,
                ApplicationDate: ref applicationDate, ApplicationTypeID: ref applicationTypeID, ApplicationStatus: ref applicationStatus, 
                LastStatusDate: ref lastStatusDate, PaidFees: ref applicationPaidFees, CreatedByUserID: ref applicationCreatedByUserID, ref licenseID);

            if (isFound)
            {
                clsPerson person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName,
                        SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                        Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath);

                clsApplication application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID),
                    ApplicantPerson: person, ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                    ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate,
                    PaidFees: applicationPaidFees, CreatedByUserID: UserID.CreateNew(applicationCreatedByUserID));

                return clsLocalDrivingLicenseApplication.CreateFromDB(ID: ID, Application: application, LicenseID: LicenseID.CreateNew(licenseID));
            }

            return clsLocalDrivingLicenseApplication.Empty;
        }

        public static ApplicationID GetApplicationIDWhenStatusNewOrCompleted(PersonID ApplicantPersonID, LicenseID LicenseClassID) =>
            ApplicationID.CreateNew(clsLocalDrivingLicenseApplicationData.GetApplicationIDWhenStatusNewOrCompleted(ApplicantPersonID.Value, LicenseClassID.Value));

        public static bool GetSingleLcoalDLApplications_ViewAsync(LocalDrivingLicenseApplicationID ID, ref string DrivingClass, ref string NationalNo, 
            ref string FullName, ref DateTime ApplicationDate, ref int PassedTests, ref string Status) =>
            clsLocalDrivingLicenseApplicationData.GetSingleLcoalDLApplications_View(ID.Value, ref DrivingClass, ref NationalNo, 
                ref FullName, ref ApplicationDate, ref PassedTests, ref Status);
        

        public static bool IsExists(LocalDrivingLicenseApplicationID ID, ApplicationID ApplicationID, PersonID ApplicantPersonID, LicenseID LicenseClassID) =>
            clsLocalDrivingLicenseApplicationData.IsExists(ID.Value, ApplicationID.Value, ApplicantPersonID.Value, LicenseClassID.Value);

        public static bool IsExists(LocalDrivingLicenseApplicationID ID, ApplicationID ApplicationID, LicenseID LicenseClassID) =>
            clsLocalDrivingLicenseApplicationData.IsExists(ID.Value, ApplicationID.Value, LicenseClassID.Value);

        public static bool IsExists(PersonID ApplicantPersonID, enApplicationStatus ApplicationStatus, LicenseID LicenseClassID) =>
            clsLocalDrivingLicenseApplicationData.IsExists(ApplicantPersonID.Value, (byte)ApplicationStatus, LicenseClassID.Value);

        public static bool IsHasNewOrCompletedApp(PersonID ApplicantPersonID, LicenseID LicenseClassID) =>
            clsLocalDrivingLicenseApplicationData.IsHasNewOrCompletedApp(ApplicantPersonID.Value, LicenseClassID.Value);


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

        public static bool UpdateApplicationStatus(LocalDrivingLicenseApplicationID LocalDLAppID, enApplicationStatus ApplicationStatus) => 
            clsLocalDrivingLicenseApplicationData.UpdateApplicationStatus(LocalDLAppID.Value, (byte)ApplicationStatus);


        //************** D **************\\
        public static bool Delete(LocalDrivingLicenseApplicationID ID) => clsLocalDrivingLicenseApplicationData.Delete(ID.Value);
        #endregion


        #region Private Async Methods
        private async Task<bool> _addNewAsync()
        {
            (int applicationID, int localDLApplicationID) = await clsLocalDrivingLicenseApplicationData.AddAsync(Application.ApplicantPerson.ID.Value,
                Application.ApplicationDate, Application.ApplicationTypeID.Value, (byte)Application.ApplicationStatus, Application.LastStatusDate,
                Application.PaidFees, Application.CreatedByUserID.Value, LicenseID.Value);

            if (applicationID > 0 && localDLApplicationID > 0)
            {
                this.ID = LocalDrivingLicenseApplicationID.CreateNew(localDLApplicationID);

                this.Application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID),
                    ApplicantPerson: Application.ApplicantPerson, ApplicationDate: Application.ApplicationDate,
                    Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreatedByUserID);

                return true;
            }

            return false;
        }

        private async Task<bool> _updateAsync() => await clsLocalDrivingLicenseApplicationData.UpdateAsync(ID.Value, Application.ID.Value,
            Application.ApplicantPerson.ID.Value, Application.ApplicationDate, Application.ApplicationTypeID.Value, (byte)Application.ApplicationStatus,
            Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID.Value, LicenseID.Value);
        #endregion

        #region Public Async Methods
        //************** R **************\\
        public static async Task<clsLocalDrivingLicenseApplication> FindAsync(LocalDrivingLicenseApplicationID ID)
        {
            (int applicationID, int applicantPersonID, string nationalNo, string firstName, string secondName, string thirdName, string lastName,
                DateTime dateOfBirth, bool gender, string address, string phone, string email, int nationalityCountryID, string imagePath,
                DateTime applicationDate, int applicationTypeID, byte applicationStatus, DateTime lastStatusDate, float applicationPaidFees,
                int applicationCreatedByUserID, int licenseID, bool isFound) = await clsLocalDrivingLicenseApplicationData.GetAsync(ID.Value);

            if (isFound)
            {
                clsPerson person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName,
                        SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                        Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath);

                clsApplication application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID),
                    ApplicantPerson: person, ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                    ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate,
                    PaidFees: applicationPaidFees, CreatedByUserID: UserID.CreateNew(applicationCreatedByUserID));

                return clsLocalDrivingLicenseApplication.CreateFromDB(ID: ID, Application: application, LicenseID: LicenseID.CreateNew(licenseID));
            }

            return clsLocalDrivingLicenseApplication.Empty;
        }

        public static async Task<ApplicationID> GetApplicationIDWhenStatusNewOrCompletedAsync(PersonID ApplicantPersonID, LicenseID LicenseClassID) =>
            ApplicationID.CreateNew(await clsLocalDrivingLicenseApplicationData.GetApplicationIDWhenStatusNewOrCompletedAsync(ApplicantPersonID.Value, LicenseClassID.Value));

        public static async Task<(string DrivingClass, string NationalNo, string FullName, DateTime ApplicationDate, 
            int PassedTests, string Status)> GetSingleLcoalDLApplications_ViewAsync(LocalDrivingLicenseApplicationID ID)
        {
            (string DrivingClass, string NationalNo, string FullName, DateTime ApplicationDate, int PassedTests, string Status, bool isFound) = 
                await clsLocalDrivingLicenseApplicationData.GetSingleLcoalDLApplications_ViewAsync(ID.Value);

            return isFound ? (DrivingClass, NationalNo, FullName, ApplicationDate, PassedTests, Status) : (null, null, null, DateTime.MaxValue, -1, null);
        }


        public static async Task<DataTable> GetLDLApplicationsAsync() =>
            await clsLocalDrivingLicenseApplicationData.GetAllLDLApplicationsAsync();

        public static async Task<IEnumerable<clsLocalDrivingLicenseApplication>> GetAllAsIEnumerableAsync() =>
            (await clsLocalDrivingLicenseApplicationData.GetAllAsync()).ToIEnumerable();
        
        public static async Task<List<clsLocalDrivingLicenseApplication>> GetAllAsListAsync() =>
            (await clsLocalDrivingLicenseApplicationData.GetAllAsync()).ToList();


        public static async Task<bool> IsExistsAsync(LocalDrivingLicenseApplicationID ID, ApplicationID ApplicationID, PersonID ApplicantPersonID, LicenseID LicenseClassID) =>
            await clsLocalDrivingLicenseApplicationData.IsExistsAsync(ID.Value, ApplicationID.Value, ApplicantPersonID.Value, LicenseClassID.Value);

        public static async Task<bool> IsExistsAsync(LocalDrivingLicenseApplicationID ID, ApplicationID ApplicationID, LicenseID LicenseClassID) =>
            await clsLocalDrivingLicenseApplicationData.IsExistsAsync(ID.Value, ApplicationID.Value, LicenseClassID.Value);

        public static async Task<bool> IsExistsAsync(PersonID ApplicantPersonID, enApplicationStatus ApplicationStatus, LicenseID LicenseClassID) =>
            await clsLocalDrivingLicenseApplicationData.IsExistsAsync(ApplicantPersonID.Value, (byte)ApplicationStatus, LicenseClassID.Value);
        
        public static async Task<bool> IsHasNewOrCompletedAppAsync(PersonID ApplicantPersonID, LicenseID LicenseClassID) =>
            await clsLocalDrivingLicenseApplicationData.IsHasNewOrCompletedAppAsync(ApplicantPersonID.Value, LicenseClassID.Value);


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

        public static async Task<bool> UpdateApplicationStatusAsync(LocalDrivingLicenseApplicationID LocalDLAppID, enApplicationStatus ApplicationStatus) =>
            await clsLocalDrivingLicenseApplicationData.UpdateApplicationStatusAsync(LocalDLAppID.Value, (byte)ApplicationStatus);


        //************** D **************\\
        public static async Task<bool> DeleteAsync(LocalDrivingLicenseApplicationID ID) => await clsLocalDrivingLicenseApplicationData.DeleteAsync(ID.Value);
        #endregion
    }

    public static class LocalDrivingLicenseApplicationExtensions
    {
        public static IEnumerable<clsLocalDrivingLicenseApplication> ToIEnumerable(this IEnumerable<(int LocalDrivingLicenseApplicationID, int ApplicationID,
            int ApplicantPersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)> data)
        {
            clsPerson person; clsApplication application;

            foreach (var item in data)
            {
                person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(item.ApplicantPersonID), NationalNo: item.NationalNo, FirstName: item.FirstName,
                    SecondName: item.SecondName, ThirdName: item.ThirdName, LastName: item.LastName, DateOfBirth: item.DateOfBirth, Gender: item.Gender,
                    Address: item.Address, Phone: item.Phone, Email: item.Email, NationalityCountryID: CountryID.CreateNew(item.NationalityCountryID), ImagePath: item.ImagePath);

                application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(item.ApplicationID), ApplicantPerson: person,
                    ApplicationDate: item.ApplicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(item.ApplicationTypeID),
                    ApplicationStatus: (enApplicationStatus)item.ApplicationStatus, LastStatusDate: item.LastStatusDate,
                    PaidFees: item.PaidFees, CreatedByUserID: UserID.CreateNew(item.CreatedByUserID));

                yield return clsLocalDrivingLicenseApplication.CreateNew(application: application, licenseID: LicenseID.CreateNew(item.LicenseClassID));
            }
        }

        public static List<clsLocalDrivingLicenseApplication> ToList(this IEnumerable<(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, bool Gender, string Address, string Phone,
            string Email, int NationalityCountryID, string ImagePath, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID, int LicenseClassID)> data)
        {
            List<clsLocalDrivingLicenseApplication> result = new List<clsLocalDrivingLicenseApplication>(data.Count());
            clsPerson person; clsApplication application;

            foreach (var item in data)
            {
                person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(item.ApplicantPersonID), NationalNo: item.NationalNo, FirstName: item.FirstName,
                    SecondName: item.SecondName, ThirdName: item.ThirdName, LastName: item.LastName, DateOfBirth: item.DateOfBirth, Gender: item.Gender,
                    Address: item.Address, Phone: item.Phone, Email: item.Email, NationalityCountryID: CountryID.CreateNew(item.NationalityCountryID), ImagePath: item.ImagePath);

                application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(item.ApplicationID), ApplicantPerson: person, 
                    ApplicationDate: item.ApplicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(item.ApplicationTypeID),
                    ApplicationStatus: (enApplicationStatus)item.ApplicationStatus, LastStatusDate: item.LastStatusDate,
                    PaidFees: item.PaidFees, CreatedByUserID: UserID.CreateNew(item.CreatedByUserID));

                result.Add(clsLocalDrivingLicenseApplication.CreateNew(application: application, licenseID: LicenseID.CreateNew(item.LicenseClassID)));
            }

            return result;
        }
    }
}
