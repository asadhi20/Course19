using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Helper.Extensions;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.Applications.DrivingLicenseServices;
using DVLD_BLL.Applications.ManageApplicationTypes;
using DVLD_BLL.Applications.ManageApplications;
using DVLD_BLL.Users;
using DVLD_BLL.People;
using DVLD_BLL;

namespace DVLDWinForms.Applications.DivingLicenseServices.NewDrivingLicense
{
    public partial class frmNewLocalDrivingLicenseApplication: Form
    {
        #region Constructors
        private frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            //_licenseClasses = clsSingleton.Instance.GetClassLicenses;
            //_loadLicenseClassesFormDB().SafeFireAndForget();
        }
        #endregion

        #region Public Creation Methods
        public static frmNewLocalDrivingLicenseApplication CreateNew(clsLocalDrivingLicenseApplication LDLApplication, clsUser User)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDLApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDLApplication.LDLApplication = LDLApplication;
            frmNewLocalDLApplication.CurrentUser = User;
            return frmNewLocalDLApplication;
        }

        public static async Task<frmNewLocalDrivingLicenseApplication> CreateNewAsync(clsLocalDrivingLicenseApplication LDLApplication, string UserName, string Password)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDLApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDLApplication.LDLApplication = LDLApplication;
            frmNewLocalDLApplication.CurrentUser = await clsUser.FindAsync(UserName, Password);
            return frmNewLocalDLApplication;
        }

        public static async Task<frmNewLocalDrivingLicenseApplication> CreateNewAsync(LocalDrivingLicenseApplicationID LDLApplicationID, clsUser User)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDLApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDLApplication.LDLApplication = await clsLocalDrivingLicenseApplication.FindAsync(LDLApplicationID);
            frmNewLocalDLApplication.CurrentUser = User;
            return frmNewLocalDLApplication;
        }

        public static async Task<frmNewLocalDrivingLicenseApplication> CreateNewAsync(LocalDrivingLicenseApplicationID LDLApplicationID, string UserName, string Password)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDLApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDLApplication.LDLApplication = await clsLocalDrivingLicenseApplication.FindAsync(LDLApplicationID);
            frmNewLocalDLApplication.CurrentUser = await clsUser.FindAsync(UserName, Password);
            return frmNewLocalDLApplication;
        }
        #endregion


        #region Private Fields
        private bool _isAddingMode { get; set; }

        private readonly string _unknownLableValue = "[?????]";
        private readonly string _updateApplicationFormTitle = "Update Local Driving License Application";
        private readonly string _addApplicatioFormTitle = "New Local Driving License Application";

        private ApplicationTypeID _applicationTypeID => ApplicationTypeID.CreateNew(1); // ApplicationTitle: New Local Driving License Service

        private static IEnumerable<clsLicenseClass> _licenseClasses;
        #endregion

        #region Public Properties
        public clsLocalDrivingLicenseApplication LDLApplication { get; set; }
        public clsUser CurrentUser { get; set; }
        public clsPerson CurrentPerson { get; set; }

        public bool IsAnyChangedHappened { get; set; }
        #endregion


        #region Private Helper Methods
        private (DateTime ApplicationDate, int LicenseClassID, string CreatedByUserName, float Fees) _getLDLApplicationInfo() => 
            (ApplicationDate: Convert.ToDateTime(lblApplicationDate.Text), LicenseClassID: cmbLicenseClasses.SelectedIndex + 1, 
            CreatedByUserName: lblCreatedBy.Text, Fees: Convert.ToSingle(lblApplicationFees.Text));
        
        private (int LicenseClassID, string CreatedByUserName, float Fees) _getNewLDLApplicationInfo() => 
            (LicenseClassID: cmbLicenseClasses.SelectedIndex + 1, CreatedByUserName: lblCreatedBy.Text, Fees: Convert.ToSingle(lblApplicationFees.Text));

        private bool _isPersonIsEmpty() => clsPerson.IsEmpty(ucPersonInfoWithFilter1.ucPersonCard1.Person);
        #endregion


        #region Private Helper UI Methods
        private clsApplication _reloadApplicationInfoToApplicationForEditingMode(clsApplication application, clsPerson person, DateTime applicationDate, float ApplicationFees, clsUser createdByUser)
        {
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = ApplicationFees;
            return application;
        }


        private void _fullComboBoxWithNamesOfLicencesClasses() =>
            cmbLicenseClasses.Items.AddRange(_licenseClasses
                    .Select(licClass => licClass.ClassName)
                    .ToArray()
                    );

        private static async Task _loadLicenseClassesFormDB() => _licenseClasses = await clsLazySingleton.Instance.GetClassLicensesAsync();


        private void _loadFormInAddingMode()
        {
            tabControl1.SelectedTab = tabPagePersonalInfo;

            lblTitel.Text = _addApplicatioFormTitle;
            lblLDLApplicationID.Text = _unknownLableValue;

            lblApplicationDate.Text = DateTime.Today.ToString();
            cmbLicenseClasses.SelectedIndex = 2;

            lblCreatedBy.Text = CurrentUser.UserName;

            _isAddingMode = true;
        }

        private async Task _loadFormInEditingMode()
        {
            tabControl1.SelectedTab = tabPageApplicationInfo;

            lblTitel.Text = _updateApplicationFormTitle;
            lblLDLApplicationID.Text = LDLApplication.ID.ToString();

            cmbLicenseClasses.SelectedIndex = LDLApplication.LicenseID.Value - 1;
            lblApplicationDate.Text = LDLApplication.Application.ApplicationDate.ToString();

            clsUser user = await clsUser.FindAsync(UserID: LDLApplication.Application.CreatedByUserID);
            lblCreatedBy.Text = user.UserName;

            ucPersonInfoWithFilter1.gpbFilter.Enabled = false;
            ucPersonInfoWithFilter1.mtxtFilter.Enabled = false;

            if (clsPerson.IsEmpty(CurrentPerson)) CurrentPerson = await clsPerson.FindAsync(LDLApplication.Application.ApplicantPerson.ID);

            await ucPersonInfoWithFilter1.LoadPersonInfo(person: CurrentPerson);

            _isAddingMode = false;
        }

        private bool _isPersonCanTakeThisLicense(clsPerson person, LicenseID licenseID) =>
            _licenseClasses.First(licClass => licClass.LicenseID == licenseID)
            .MinimumAllowedAge <= (DateTime.UtcNow.Year - person.DateOfBirth.Year);


        private async Task<clsLocalDrivingLicenseApplication> _saveAsAddingMode()
        {
            (int licenseClassID, string createdByUserName, float applicationFees) = _getNewLDLApplicationInfo();

            LicenseID licenseID = LicenseID.CreateNew(licenseClassID);

            if (!_isPersonCanTakeThisLicense(CurrentPerson, licenseID))
            {
                DialogResult.ShowMessageBoxErrorDial("This person's age does not satisfy the minimum age requirement for the license.", "Error");
                return LDLApplication;
            }

            ApplicationID AppID = await clsLocalDrivingLicenseApplication.GetApplicationIDWhenStatusNewOrCompletedAsync(CurrentPerson.ID, licenseID);

            const int MaximinNotAllowedIntegerAsID = 0;
            if (AppID.Value > MaximinNotAllowedIntegerAsID)
            {
                DialogResult.ShowMessageBoxErrorDial(
                    $"Choose another license class, the selected person already have an another application for the selected license class with id = {AppID}.", 
                    "Invalid Choose");
                IsAnyChangedHappened = false;
                return LDLApplication;
            }

            clsApplication newApplication = clsApplication.CreateNew(CurrentPerson, ApplicationDate: DateTime.Now, _applicationTypeID, clsApplication.enApplicationStatus.New, LastStatusDate: DateTime.Now, applicationFees, CurrentUser.ID);

            clsLocalDrivingLicenseApplication newLDLApplication = clsLocalDrivingLicenseApplication.CreateNew(newApplication, licenseID);

            IsAnyChangedHappened = await newLDLApplication.SaveAsync();

            if (IsAnyChangedHappened)
            {
                DialogResult.ShowMessageBoxInfoDial($"Application with application id = {newLDLApplication.ID} has been added.", "Adding Application");
                _isAddingMode = false;
            }
            else DialogResult.ShowMessageBoxErrorDial($"Application has been not added.", "Error");

            return newLDLApplication;
        }

        private async Task<clsLocalDrivingLicenseApplication> _saveAsEditingMode()
        {
            (DateTime applicationDate, int licenseClassID, string createdBy, float applicationFees) = _getLDLApplicationInfo();

            if (LDLApplication.LicenseID.Value == licenseClassID)
            {
                DialogResult.ShowMessageBoxWarningDial($"Person with person id = {CurrentUser.ID} you have not enter a new info to it.", "User Info Not Saved");
                IsAnyChangedHappened = false;
                return LDLApplication;
            }

            LicenseID licenseID = LicenseID.CreateNew(licenseClassID);
            
            if (_isPersonCanTakeThisLicense(CurrentPerson, licenseID))
            {
                DialogResult.ShowMessageBoxErrorDial("This person's age does not satisfy the minimum age requirement for the license.", "Error");
                return LDLApplication;
            }

            bool IsHasNewOrCompletedApp = await clsLocalDrivingLicenseApplication.IsHasNewOrCompletedAppAsync(CurrentPerson.ID, licenseID);

            if (IsHasNewOrCompletedApp)
            {
                DialogResult.ShowMessageBoxErrorDial(
                    $"Choose another license class, the selected person already have an another application for the selected license class with id = {LDLApplication.ID}.",
                    "Invalid Choose");
                IsAnyChangedHappened = false;
                return LDLApplication;
            }

            _reloadApplicationInfoToApplicationForEditingMode(application: LDLApplication.Application, person: CurrentPerson, 
                applicationDate: applicationDate, ApplicationFees: applicationFees, createdByUser: CurrentUser);

            LDLApplication.LicenseID = licenseID;
            IsAnyChangedHappened = await LDLApplication.SaveAsync();

            if (IsAnyChangedHappened)
            {
                DialogResult.ShowMessageBoxInfoDial($"Application with id = {LDLApplication.ID} has been updated.", "Update User Info");
            }
            else
            {
                DialogResult.ShowMessageBoxErrorDial($"Application with id = {LDLApplication.ID} has been not updated.", "An Error Occurreded");
            }

            return LDLApplication;
        }
        #endregion


        #region Main UI Methods
        private async void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            if (_licenseClasses is null) await _loadLicenseClassesFormDB();
            if (cmbLicenseClasses.Items.Count is 0) _fullComboBoxWithNamesOfLicencesClasses();

            lblApplicationFees.Text = 15.ToString();

            if (LDLApplication.ID.IsEmpty()) _loadFormInAddingMode();
            else await _loadFormInEditingMode();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            const int tabPagePersonalInfoIndex = 0;
            if (_isPersonIsEmpty())
            {
                DialogResult.ShowMessageBoxErrorDial("Person is not found!", "Faild To Find Person");
                tabControl1.SelectedIndex = tabPagePersonalInfoIndex;
                return;
            }

            this.CurrentPerson = ucPersonInfoWithFilter1.ucPersonCard1.Person;

            this.LDLApplication = _isAddingMode ? await _saveAsAddingMode() : await _saveAsEditingMode();

            if (IsAnyChangedHappened) frmNewLocalDrivingLicenseApplication_Load(this, EventArgs.Empty);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            const int tabPageApplicationInfoIndex = 1;
            if (!_isAddingMode)
            {
                tabControl1.SelectedIndex = tabPageApplicationInfoIndex;
                return;
            }

            if (_isPersonIsEmpty())
            {
                DialogResult.ShowMessageBoxErrorDial("There is no person selected.", "Error");
                return;
            }

            tabControl1.SelectedIndex = tabPageApplicationInfoIndex;
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
        #endregion
    }
}
