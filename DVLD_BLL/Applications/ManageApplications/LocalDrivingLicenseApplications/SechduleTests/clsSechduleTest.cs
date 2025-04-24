using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using DVLD_DAL.Applications.ManageApplications.LocalDrivingLicenseApplications.SechduleTests;
using DVLD_BLL.Users;
using System.Data;
using DVLD_BLL.Applications.ManageApplicationTypes;
using DVLD_BLL.People;
using static DVLD_BLL.Applications.ManageApplications.clsApplication;
using DVLD_BLL.Applications.DrivingLicenseServices;

namespace DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications.SechduleTests
{
    public enum Mode { Add, Update };

    public struct SechduleTestID : IEquatable<SechduleTestID>, IComparable<SechduleTestID>, IComparer<SechduleTestID>
    {
        #region Constructors
        private SechduleTestID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static SechduleTestID Empty => new SechduleTestID(0);
        #endregion


        #region Public Static Methods
        public static SechduleTestID NewID(int ID) => new SechduleTestID(ID);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(SechduleTestID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(SechduleTestID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(SechduleTestID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(SechduleTestID x, SechduleTestID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(SechduleTestID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is SechduleTestID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(SechduleTestID left, SechduleTestID right) => left.Equals(right);
        public static bool operator ==(SechduleTestID left, int @int) => left.Equals(@int);
        
        public static bool operator !=(SechduleTestID left, SechduleTestID right) => !(left == right);
        public static bool operator !=(SechduleTestID left, int @int) => !(left == @int);


        public static bool operator >(SechduleTestID left, SechduleTestID right) => left.Value > right.Value;
        public static bool operator >(SechduleTestID left, int @int) => left.Value > @int;
        
        public static bool operator <(SechduleTestID left, SechduleTestID right) => left.Value < right.Value;
        public static bool operator <(SechduleTestID left, int @int) => left.Value < @int;


        public static bool operator <=(SechduleTestID left, SechduleTestID right) => left.Value <= right.Value;
        public static bool operator <=(SechduleTestID left, int @int) => left.Value <= @int;

        public static bool operator >=(SechduleTestID left, SechduleTestID right) => left.Value >= right.Value;
        public static bool operator >=(SechduleTestID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsSechduleTest : IEquatable<clsSechduleTest>, IComparable<clsSechduleTest>
    {
        public enum enTestAppointmentType { None = 0, Vision = 1, Written = 2, Street = 3 }

        #region Private Constructors
        private clsSechduleTest() : this(ID: SechduleTestID.Empty, TestAppointmentType: enTestAppointmentType.None, LocalDLApplication: clsLocalDrivingLicenseApplication.Empty, AppointmentDate: DateTime.MaxValue, PaidFees: .0f, CreatedByUserID: UserID.Empty, IsLocked: false, Mode: Mode.Add) { }

        private clsSechduleTest(SechduleTestID ID, enTestAppointmentType TestAppointmentType, clsLocalDrivingLicenseApplication LocalDLApplication, DateTime AppointmentDate, float PaidFees, UserID CreatedByUserID, bool IsLocked, Mode Mode) =>
            (this.ID, this.TestAppointmentType, this.LDLApplication, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, 
                this.IsLocked, _mode, _isAppointmentDateFieldUpdated, _isIsLockedFieldUpdated) = 
                    (ID, TestAppointmentType, LocalDLApplication, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, Mode, false, false);
        #endregion

        #region Static Creation Methods
        public static clsSechduleTest CreateNew(enTestAppointmentType TestAppointmentType, clsLocalDrivingLicenseApplication LocalDLApplication, DateTime AppointmentDate, float PaidFees, UserID CreatedByUserID, bool IsLocked) =>
            new clsSechduleTest(ID: SechduleTestID.Empty, TestAppointmentType: TestAppointmentType, LocalDLApplication: LocalDLApplication, AppointmentDate: AppointmentDate, PaidFees: PaidFees, CreatedByUserID: CreatedByUserID, IsLocked: IsLocked, Mode: Mode.Add);

        internal static clsSechduleTest CreateFromDB(SechduleTestID SechduleTestID, enTestAppointmentType TestAppointmentType, clsLocalDrivingLicenseApplication LocalDLApplication, DateTime AppointmentDate, float PaidFees, UserID CreatedByUserID, bool IsLocked) =>
            new clsSechduleTest(ID: SechduleTestID, TestAppointmentType: TestAppointmentType, LocalDLApplication: LocalDLApplication, AppointmentDate: AppointmentDate, PaidFees: PaidFees, CreatedByUserID: CreatedByUserID, IsLocked: IsLocked, Mode: Mode.Update);
        #endregion


        #region Private Fields
        private Mode _mode { get; set; }

        private DateTime _appointmentDate;
        private bool _isAppointmentDateFieldUpdated = false;

        private bool _isLocked;
        private bool _isIsLockedFieldUpdated = false;
        #endregion

        #region Public Properties
        public SechduleTestID ID { get; set; }
        public enTestAppointmentType TestAppointmentType { get; set; }
        public clsLocalDrivingLicenseApplication LDLApplication { get; private set; }
        public DateTime AppointmentDate {
            get => _appointmentDate;
            set {
                if (_appointmentDate == value) return;

                _isAppointmentDateFieldUpdated = true;
                _appointmentDate = value;
            }
        }
        public float PaidFees { get; set; }
        public UserID CreatedByUserID { get; set; }
        public bool IsLocked {
            get => _isLocked;
            set {
                if (_isLocked == value) return;

                _isIsLockedFieldUpdated = true;
                _isLocked = value;
            }
        }

        public static clsSechduleTest Empty => new clsSechduleTest();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(clsSechduleTest left, clsSechduleTest right) => left.Equals(right);
        public static bool operator !=(clsSechduleTest left, clsSechduleTest right) => !(left == right);

        public static bool operator >(clsSechduleTest left, clsSechduleTest right) => left.CompareTo(right) > 0;
        public static bool operator <(clsSechduleTest left, clsSechduleTest right) => left.CompareTo(right) < 0;

        public static bool operator >=(clsSechduleTest left, clsSechduleTest right) => left.CompareTo(right) >= 0;
        public static bool operator <=(clsSechduleTest left, clsSechduleTest right) => left.CompareTo(right) <= 0;
        #endregion


        #region Overridden Methods
        public override bool Equals(object obj) => obj is clsSechduleTest other && this.Equals(other);

        public override int GetHashCode() => (ID, TestAppointmentType, LDLApplication, AppointmentDate, PaidFees, CreatedByUserID, IsLocked).GetHashCode();
        #endregion


        #region Private Methods
        private bool _isLessThan(clsSechduleTest other) =>
            this.IsEmpty() ? other.IsNotEmpty() : ID < other.ID && AppointmentDate < other.AppointmentDate
            && PaidFees < other.PaidFees && CreatedByUserID < other.CreatedByUserID;
        #endregion


        #region Public Methods
        public int CompareTo(clsSechduleTest other) => this.Equals(other) ? 0 : this._isLessThan(other) ? -1 : 1;

        public bool NotEquals(clsSechduleTest other) => !this.Equals(other);
        public bool Equals(clsSechduleTest other) =>
            this.IsEmpty() ? other.IsEmpty() : ID.Equals(other.ID) && TestAppointmentType == other.TestAppointmentType 
            && LDLApplication.Equals(other.LDLApplication) && AppointmentDate.Equals(other.AppointmentDate)
            && PaidFees.Equals(other.PaidFees) && CreatedByUserID.Equals(other.CreatedByUserID);

        public bool IsNotEmpty() => !this.IsEmpty();
        public bool IsEmpty() =>
            ID.IsEmpty() || TestAppointmentType == enTestAppointmentType.None || LDLApplication.IsEmpty() 
            || AppointmentDate.Equals(DateTime.MaxValue) || PaidFees < 1.0f || CreatedByUserID.IsEmpty();
        #endregion

        #region Public Static Methods
        public static bool IsNotEmpty(clsSechduleTest SechduleTest) => !(SechduleTest is null) || SechduleTest.IsNotEmpty();
        public static bool IsEmpty(clsSechduleTest SechduleTest) => SechduleTest is null || SechduleTest.IsEmpty();
        #endregion


        #region Private Sync Methods
        private bool _addNew()
        {
            int newID = clsSechduleTestData.Add((byte)TestAppointmentType, LDLApplication.ID.Value, AppointmentDate, PaidFees, CreatedByUserID.Value, IsLocked);

            this.ID = SechduleTestID.NewID(newID);
            return this.ID.Value != -1;
        }

        private bool _update() => 
            clsSechduleTestData.Update(ID.Value, (byte)TestAppointmentType, LDLApplication.ID.Value, AppointmentDate, PaidFees, CreatedByUserID.Value, IsLocked);
        private bool _updateAppointmentDateOnly() => clsSechduleTestData.UpdateAppointmentDateOnly(ID.Value, AppointmentDate);
        private bool _updateIsLockedOnly() => clsSechduleTestData.UpdateIsLockedOnly(ID.Value, IsLocked);
        #endregion

        #region Public Sync Methods
        //************** R **************\\
        public static clsSechduleTest Find(SechduleTestID ID)
        {
            bool isLocked = false;
            byte testAppointmentType = 0;
            int localDLAppID = -1, createdByUserID = -1;
            float paidFees = 0.0f;
            DateTime appointmentDate = DateTime.MaxValue;

            int applicationID = -1, applicantPersonID = -1, applicationTypeID = -1, applicationCreatedByUserID = -1, licenseID = -1;
            byte applicationStatus = 0;
            float applicationPaidFees = 0.0f;
            DateTime applicationDate = DateTime.MaxValue, lastStatusDate = DateTime.MaxValue;

            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            int nationalityCountryID = -1;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsSechduleTestData.Get(ID: ID.Value, TestTypeID: ref testAppointmentType, LocalDLApplicationID: ref localDLAppID,
                ApplicationID: ref applicationID, ApplicantPersonID: ref applicantPersonID, NationalNo: ref nationalNo, FirstName: ref firstName,
                SecondName: ref secondName, ThirdName: ref thirdName, LastName: ref lastName, DateOfBirth: ref dateOfBirth,
                Gender: ref gender, Address: ref address, Phone: ref phone, Email: ref email, NationalityCountryID: ref nationalityCountryID,
                ImagePath: ref imagePath, ApplicationDate: ref applicationDate, ApplicationTypeID: ref applicationTypeID,
                ApplicationStatus: ref applicationStatus, LastStatusDate: ref lastStatusDate, ApplicationPaidFees: ref applicationPaidFees,
                ApplicationCreatedByUserID: ref applicationCreatedByUserID, LicenseID: ref licenseID, AppointmentDate: ref appointmentDate,
                PaidFees: ref paidFees, CreatedByUserID: ref createdByUserID, IsLocked: ref isLocked);

            if (isFound)
            {
                clsPerson person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName,
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath);

                clsApplication application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID), ApplicantPerson: person,
                    ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                    ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: applicationPaidFees,
                    CreatedByUserID: UserID.CreateNew(applicationCreatedByUserID));

                clsLocalDrivingLicenseApplication lDLApp = clsLocalDrivingLicenseApplication.CreateFromDB(
                    ID: LocalDrivingLicenseApplicationID.CreateNew(localDLAppID), Application: application, LicenseID: LicenseID.CreateNew(licenseID));

                return clsSechduleTest.CreateFromDB(SechduleTestID: ID, TestAppointmentType: (enTestAppointmentType)testAppointmentType,
                    LocalDLApplication: lDLApp, AppointmentDate: appointmentDate, PaidFees: paidFees,
                    CreatedByUserID: UserID.CreateNew(createdByUserID), IsLocked: isLocked);
            }

            return clsSechduleTest.Empty;
        }

        public static clsSechduleTest Find(SechduleTestID ID, enTestAppointmentType TestAppointmentType)
        {
            bool isLocked = false;
            int localDLAppID = -1, createdByUserID = -1;
            float paidFees = 0.0f;
            DateTime appointmentDate = DateTime.MaxValue;

            byte applicationStatus = 0;
            int applicationID = -1, applicantPersonID = -1, applicationTypeID = -1, applicationCreatedByUserID = -1, licenseID = -1;
            float applicationPaidFees = 0.0f;
            DateTime applicationDate = DateTime.MaxValue, lastStatusDate = DateTime.MaxValue;

            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            bool gender = false;
            int nationalityCountryID = -1;
            DateTime dateOfBirth = DateTime.MaxValue;

            bool isFound = clsSechduleTestData.Get(ID: ID.Value, TestTypeID: (byte)TestAppointmentType, LocalDLApplicationID: ref localDLAppID, 
                ApplicationID: ref applicationID, ApplicantPersonID: ref applicantPersonID, NationalNo: ref nationalNo, FirstName: ref firstName,
                SecondName: ref secondName, ThirdName: ref thirdName, LastName: ref lastName, DateOfBirth: ref dateOfBirth, 
                Gender: ref gender, Address: ref address, Phone: ref phone, Email: ref email, NationalityCountryID: ref nationalityCountryID,
                ImagePath: ref imagePath, ApplicationDate: ref applicationDate, ApplicationTypeID: ref applicationTypeID, 
                ApplicationStatus: ref applicationStatus, LastStatusDate: ref lastStatusDate, ApplicationPaidFees: ref applicationPaidFees, 
                ApplicationCreatedByUserID: ref applicationCreatedByUserID, LicenseID: ref licenseID, AppointmentDate: ref appointmentDate, 
                PaidFees: ref paidFees, CreatedByUserID: ref createdByUserID, IsLocked: ref isLocked);

            if (isFound)
            {
                clsPerson person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName,
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath);

                clsApplication application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID), ApplicantPerson: person,
                    ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                    ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: applicationPaidFees,
                    CreatedByUserID: UserID.CreateNew(applicationCreatedByUserID));

                clsLocalDrivingLicenseApplication lDLApp = clsLocalDrivingLicenseApplication.CreateFromDB(
                    ID: LocalDrivingLicenseApplicationID.CreateNew(localDLAppID), Application: application, LicenseID: LicenseID.CreateNew(licenseID));

                return clsSechduleTest.CreateFromDB(SechduleTestID: ID, TestAppointmentType: (enTestAppointmentType)TestAppointmentType,
                        LocalDLApplication: lDLApp, AppointmentDate: appointmentDate, PaidFees: paidFees,
                        CreatedByUserID: UserID.CreateNew(createdByUserID), IsLocked: isLocked);
            }

            return clsSechduleTest.Empty;
        }

        public static IEnumerable<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate,
            float PaidFees, string FullName, bool IsLocked)> GetAll() => clsSechduleTestData.GetAll();

        public static DataTable GetAllTestAppointments(enTestAppointmentType TestAppointmentType, LocalDrivingLicenseApplicationID LocalDLAppID) => 
            clsSechduleTestData.GetAllTestAppointments((byte)TestAppointmentType, LocalDLAppID.Value);


        public static DataTable GetAllTestAppointments(enTestAppointmentType TestAppointmentType) => 
            clsSechduleTestData.GetAllTestAppointments((byte)TestAppointmentType);


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

                case Mode.Update: 
                    if (_isAppointmentDateFieldUpdated) 
                    {
                        _isAppointmentDateFieldUpdated = !_updateAppointmentDateOnly();
                        return !_isAppointmentDateFieldUpdated;
                    }
                    else if (_isIsLockedFieldUpdated)
                    {
                        _isIsLockedFieldUpdated = !_updateIsLockedOnly();
                        return !_isIsLockedFieldUpdated;
                    }
                    else return _update();
            }

            return false;
        }


        //************** D **************\\
        public static bool Delete(SechduleTestID ID) => clsSechduleTestData.Delete(ID.Value);
        #endregion


        #region Private Async Methods
        private async Task<bool> _addNewAsync()
        {
            int newID = await clsSechduleTestData.AddAsync((byte)TestAppointmentType, LDLApplication.ID.Value, AppointmentDate, PaidFees, CreatedByUserID.Value, IsLocked);

            this.ID = SechduleTestID.NewID(newID);
            return this.ID.Value != -1;
        }

        private async Task<bool> _updateAsync() => await clsSechduleTestData.UpdateAsync(ID.Value, (byte)TestAppointmentType, LDLApplication.ID.Value, AppointmentDate, (byte)PaidFees, CreatedByUserID.Value, IsLocked);

        private async Task<bool> _updateAppointmentDateOnlyAsync() => await clsSechduleTestData.UpdateAppointmentDateOnlyAsync(ID.Value, AppointmentDate);

        private async Task<bool> _updateIsLockedOnlyAsync() => await clsSechduleTestData.UpdateIsLockedOnlyAsync(ID.Value, IsLocked);
        #endregion

        #region Public Async Methods
        //************** R **************\\
        public static async Task<clsSechduleTest> FindAsync(SechduleTestID ID)
        {
            (byte testAppointmentType, int localDLApplicationID, int applicationID, int applicantPersonID, string nationalNo, string firstName,
                string secondName, string thirdName, string lastName, DateTime dateOfBirth, bool gender, string address, string phone, 
                string email, int nationalityCountryID, string imagePath, DateTime applicationDate, int applicationTypeID, byte applicationStatus,
                DateTime lastStatusDate, float applicationPaidFees, int applicationCreatedByUserID, int licenseID, DateTime appointmentDate,
                float paidFees, int createdByUserID, bool isLocked, bool isFound) = await clsSechduleTestData.GetAsync(ID.Value);

            if (isFound)
            {
                clsPerson person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName,
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath);

                clsApplication application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID), ApplicantPerson: person,
                    ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                    ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: applicationPaidFees,
                    CreatedByUserID: UserID.CreateNew(applicationCreatedByUserID));

                clsLocalDrivingLicenseApplication lDLApp = clsLocalDrivingLicenseApplication.CreateFromDB(
                    ID: LocalDrivingLicenseApplicationID.CreateNew(localDLApplicationID), Application: application, LicenseID: LicenseID.CreateNew(licenseID));

                return clsSechduleTest.CreateFromDB(SechduleTestID: ID, TestAppointmentType: (enTestAppointmentType)testAppointmentType,
                    LocalDLApplication: lDLApp, AppointmentDate: appointmentDate, PaidFees: paidFees,
                    CreatedByUserID: UserID.CreateNew(createdByUserID), IsLocked: isLocked);
            }

            return clsSechduleTest.Empty;
        }

        public static async Task<clsSechduleTest> FindAsync(SechduleTestID ID, enTestAppointmentType testAppointmentType)
        {
            (int localDLApplicationID, int applicationID, int applicantPersonID, string nationalNo, string firstName, string secondName,
                string thirdName, string lastName, DateTime dateOfBirth, bool gender, string address, string phone, string email,
                int nationalityCountryID, string imagePath, DateTime applicationDate, int applicationTypeID, byte applicationStatus,
                DateTime lastStatusDate, float applicationPaidFees, int applicationCreatedByUserID, int licenseID, DateTime appointmentDate,
                float paidFees, int createdByUserID, bool isLocked, bool isFound) = 
                await clsSechduleTestData.GetAsync(ID.Value, (byte)testAppointmentType);

            if (isFound)
            {
                clsPerson person = clsPerson.CreateFromDB(ID: PersonID.CreateNew(applicantPersonID), NationalNo: nationalNo, FirstName: firstName,
                    SecondName: secondName, ThirdName: thirdName, LastName: lastName, DateOfBirth: dateOfBirth, Gender: gender, Address: address,
                    Phone: phone, Email: email, NationalityCountryID: CountryID.CreateNew(nationalityCountryID), ImagePath: imagePath);

                clsApplication application = clsApplication.CreateFromDB(ApplicationID: ApplicationID.CreateNew(applicationID), ApplicantPerson: person,
                    ApplicationDate: applicationDate, ApplicationTypeID: ApplicationTypeID.CreateNew(applicationTypeID),
                    ApplicationStatus: (enApplicationStatus)applicationStatus, LastStatusDate: lastStatusDate, PaidFees: applicationPaidFees,
                    CreatedByUserID: UserID.CreateNew(applicationCreatedByUserID));

                clsLocalDrivingLicenseApplication lDLApp = clsLocalDrivingLicenseApplication.CreateFromDB(
                    ID: LocalDrivingLicenseApplicationID.CreateNew(localDLApplicationID), Application: application, LicenseID: LicenseID.CreateNew(licenseID));

                return clsSechduleTest.CreateFromDB(SechduleTestID: ID, TestAppointmentType: (enTestAppointmentType)testAppointmentType,
                        LocalDLApplication: lDLApp, AppointmentDate: appointmentDate, PaidFees: paidFees,
                        CreatedByUserID: UserID.CreateNew(createdByUserID), IsLocked: isLocked);
            }

            return clsSechduleTest.Empty;
        }


        public static async Task<IEnumerable<(int ID, string ClassName, int LocalDLApplicationID, string TestTypeTitle, DateTime AppointmentDate,
            float PaidFees, string FullName, bool IsLocked)>> GetAllAsync() => await clsSechduleTestData.GetAllAsync();

        public static async Task<DataTable> GetAllTestAppointmentsAsync(LocalDrivingLicenseApplicationID LDLAppID, enTestAppointmentType TestAppointmentType) =>
            await clsSechduleTestData.GetAllTestAppointmentsAsync(LDLAppID.Value, (byte)TestAppointmentType);

        
        public static async Task<DataTable> GetAllTestAppointmentsAsync(enTestAppointmentType TestAppointmentType) => 
            await clsSechduleTestData.GetAllTestAppointmentsAsync((byte)TestAppointmentType);

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

                case Mode.Update:
                    if (_isAppointmentDateFieldUpdated)
                    {
                        _isAppointmentDateFieldUpdated = !await _updateAppointmentDateOnlyAsync();
                        return !_isAppointmentDateFieldUpdated;
                    }
                    else if (_isIsLockedFieldUpdated)
                    {
                        _isIsLockedFieldUpdated = !await _updateIsLockedOnlyAsync();
                        return !_isIsLockedFieldUpdated;
                    }
                    else return await _updateAsync();
            }

            return false;
        }


        //************** D **************\\
        public static async Task<bool> DeleteAsync(SechduleTestID ID) => await clsSechduleTestData.DeleteAsync(ID.Value);
        #endregion
    }
}
