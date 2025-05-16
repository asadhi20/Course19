using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Helper.Extensions;
using DVLD_BLL.People;
using DVLD_BLL.Users;

namespace DVLDWinForms.Users
{
    public sealed partial class frmAddEditUser : Form
    {
        #region Constructors
        private frmAddEditUser() => InitializeComponent();

        public static frmAddEditUser CreateAsEditingMode(clsUser user)
        {
            frmAddEditUser frmAdd_EditUser = new frmAddEditUser();
            frmAdd_EditUser.User = user;
            frmAdd_EditUser._isAddingMode = false;
            return frmAdd_EditUser;
        }

        public static async Task<frmAddEditUser> CreateAsEditingMode(int userID)
        {
            frmAddEditUser frmAdd_EditUser = new frmAddEditUser();
            frmAdd_EditUser.User = userID < 1 ? clsUser.Empty : await clsUser.FindAsync(UserID: UserID.CreateNew(userID));
            frmAdd_EditUser._isAddingMode = false;
            return frmAdd_EditUser;
        }

        public static frmAddEditUser CreateAsAddingMode(clsPerson person)
        {
            frmAddEditUser frmAdd_EditUser = new frmAddEditUser();
            if (!(person is null) || !person.IsEmpty()) frmAdd_EditUser.Person = person;
            frmAdd_EditUser._isAddingMode = true;
            return frmAdd_EditUser;
        }

        public static async Task<frmAddEditUser> CreateAsAddingMode(PersonID personID)
        {
            frmAddEditUser frmAdd_EditUser = new frmAddEditUser();
            if (personID.Value > 0) frmAdd_EditUser.Person = await clsPerson.FindAsync(personID);
            frmAdd_EditUser._isAddingMode = true;
            return frmAdd_EditUser;
        }
        #endregion

        #region Private Fields
        private bool _isAddingMode { get; set; }

        private readonly string _updateUserFormTitle = "Update User Info";
        private readonly string _addUserFormTitle = "Add New User";
        private clsPerson _person { get; set; }
        #endregion
        
        #region Public Properties
        public clsUser User { get; set; }
        public clsPerson Person 
        {
            get => clsUser.IsEmpty(User) || clsPerson.IsEmpty(User.Person) ? _person : User.Person;
            set => _person = value;
        }
        public bool IsAnyChangedHappened { get; set; }
        #endregion


        #region Private UI Helper Methods
        private clsUser _reloadUserInfoToUser(clsUser user, string userName, string password, bool isActive)
        {
            user.UserName = userName;
            user.Password = password;
            user.IsActive = isActive;
            return user;
        }


        private bool _isNotAllUserInfoValid() =>
            erprovIsValidInput.GetError(txtUserName).NotIsNullOrEmpty() || erprovIsValidInput.GetError(txtPassword).NotIsNullOrEmpty() || erprovIsValidInput.GetError(txtConfirmPassword).NotIsNullOrEmpty();

        private (string UserName, string Password, string ConfirmPassword) _getNewUserInfo() =>
            (UserName: txtUserName.Text.RemoveWhiteSpaces(), Password: txtPassword.Text.RemoveWhiteSpaces(), ConfirmPassword: txtConfirmPassword.Text.RemoveWhiteSpaces());

        private bool _isPersonIsEmpty() => clsPerson.IsEmpty(ucPersonInfoWithFilter1.ucPersonCard1.Person);


        private async Task<clsUser> _saveAsAddingMode()
        {
            if (await clsUser.IsExsitsAsync(PersonID: Person.ID))
            {
                DialogResult.ShowMessageBoxErrorDial($"Person with person id = {Person.ID} is already exsits as an user!", "Error");
                return User;
            }

            (string userName, string password, string confirmPassword) = _getNewUserInfo();

            clsUser user = clsUser.CreateNew(person: Person, userName: userName, password: password, isActive: chkIsActive.Checked);

            IsAnyChangedHappened = await user.SaveAsync();

            if (IsAnyChangedHappened)
            {
                DialogResult.ShowMessageBoxInfoDial($"User with user id = {user.ID} has been added.", "Adding User");
                _isAddingMode = false;
            }
            else DialogResult.ShowMessageBoxErrorDial($"User has been not added.", "Error");

            return user;
        }

        private async Task<clsUser> _saveAsEditingMode()
        {
            (string userName, string password, string confirmPassword) = _getNewUserInfo();

            if (User.IsActive.Equals(chkIsActive.Checked) && User.UserName.Equals(userName) && User.Password.Equals(password))
            {
                DialogResult.ShowMessageBoxWarningDial($"User with user id = {User.ID} you have not enter a new info to it.", "User Info Not Saved");
                return User;
            }

            _reloadUserInfoToUser(user: User, userName: userName, password: password, isActive: chkIsActive.Checked);

            IsAnyChangedHappened = await User.SaveAsync();

            DialogResult.ShowMessageBoxInfoDial($"User with user id = {User.ID} has been updated.", "Update User Info");

            return User;
        }
        #endregion

        #region Public UI Helper Methods
        public void LoadUserInfo(clsUser user)
        {
            txtUserName       .Text = user.UserName;
            txtPassword       .Text = user.Password;
            txtConfirmPassword.Text = user.Password;
            chkIsActive    .Checked = user.IsActive;
        }
        #endregion

        #region Main UI Methods
        private async void frmAddEditUser_Load(object sender, EventArgs e)
        {
            if (_isAddingMode)
            {
                lblTitel.Text = _addUserFormTitle;
            }
            else
            {
                lblTitel.Text = _updateUserFormTitle;
                lblUserID.Text = User.ID.ToString();

                ucPersonInfoWithFilter1.gpbFilter.Enabled = false;
                ucPersonInfoWithFilter1.mtxtFilter.Enabled = false;

                tabControl1.SelectedIndex = 0; //tabControl1.SelectedTab = tabPageLoginInfo;

                await ucPersonInfoWithFilter1.ucPersonCard1.LoadPersonInfo(person: Person);
                this.LoadUserInfo(user: User);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (!_isAddingMode) {
                tabControl1.SelectedIndex = 1; // tabControl1.SelectedTab = tabPageLoginInfo;
                return;
            }

            if (_isPersonIsEmpty()) {
                DialogResult.ShowMessageBoxErrorDial("There is no person selected.", "Error");
                return;
            }

            if (await clsUser.IsExsitsAsync(PersonID: ucPersonInfoWithFilter1.ucPersonCard1.Person.ID)) {
                DialogResult.ShowMessageBoxErrorDial("Selected person already has a user, choose another one.", "Select Another Person");
                return;
            }

            tabControl1.SelectedIndex = 1; // tabControl1.SelectedTab = tabPageLoginInfo;
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_isPersonIsEmpty()) {
                DialogResult.ShowMessageBoxErrorDial("Person is not found!", "Faild To Find Person");
                tabControl1.SelectedIndex = 0; // tabControl1.SelectedTab = tabPagePersonInfo;
                return;
            }

            if (_isNotAllUserInfoValid()) { DialogResult.ShowMessageBoxInfoDial("Not all user info is valid!"); return; }

            this.Person = ucPersonInfoWithFilter1.ucPersonCard1.Person;

            User = _isAddingMode ? await _saveAsAddingMode() : await _saveAsEditingMode();

            if (IsAnyChangedHappened) frmAddEditUser_Load(this, EventArgs.Empty);
        }
        

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            string userName = txtUserName.Text.RemoveWhiteSpaces();
            
            string message = null;
            bool isFocusNeeded;
            const int minimumUserNameLength = 5;

            if (isFocusNeeded = userName.IsNullOrEmpty())
                message = "User Name field should have a value!";
            else if (isFocusNeeded = userName.Length < minimumUserNameLength)
                message = "User Name field should be at lest 5 characters!";
            else if (isFocusNeeded = _isAddingMode ? clsUser.IsExsits(userName) 
                                                   : User.UserName.NotEquals(userName) && clsUser.IsExsits(userName))
                message = "User Name is used form another person!";

            //if (e.Cancel = isFocusNeeded) txtUserName.Focus();
            erprovIsValidInput.SetError(control: txtUserName, value: message);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            string password = txtPassword.Text.RemoveWhiteSpaces();

            string message = null;
            bool isFocusNeeded;
            const int minimumPasswordLength = 5;

            if (isFocusNeeded = password.IsNullOrEmpty())
                message = "Password field should have a value!";
            else if (isFocusNeeded = password.Length < minimumPasswordLength)
                message = "Password field should be at lest 5 characters!";

            //if (e.Cancel = isFocusNeeded) txtPassword.Focus();
            erprovIsValidInput.SetError(control: txtPassword, value: message);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            string confirmPassword = txtConfirmPassword.Text.RemoveWhiteSpaces();

            string message = null;
            bool isFocusNeeded;
            const int minimumConfirmPasswordLength = 5;

            if (isFocusNeeded = confirmPassword.IsNullOrEmpty())
                message = "Confirm Password field should have a value!";
            else if (isFocusNeeded = confirmPassword.Length < minimumConfirmPasswordLength)
                message = "Confirm Password field should be at lest 5 characters!";
            else if (confirmPassword.NotEquals(txtPassword.Text.RemoveWhiteSpaces()))
                message = "Confirm Password field should be equal to Password field!";

            //if (e.Cancel = isFocusNeeded) txtConfirmPassword.Focus();
            erprovIsValidInput.SetError(control: txtConfirmPassword, value: message);
        }
        #endregion
    }
}
