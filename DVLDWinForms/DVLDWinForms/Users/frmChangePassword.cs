using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
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
    public partial class frmChangePassword : Form
    {
        #region Constructors
        private frmChangePassword() => InitializeComponent();

        public static frmChangePassword Create(clsUser User)
        {
            frmChangePassword frmChangePassword = new frmChangePassword();
            frmChangePassword.User = User;
            return frmChangePassword;
        }

        public static async Task<frmChangePassword> Create(UserID UserID)
        {
            frmChangePassword frmChangePassword = new frmChangePassword();
            frmChangePassword.User = await clsUser.FindAsync(UserID: UserID);
            return frmChangePassword;
        }
        #endregion


        #region Public Properties
        public bool IsAnyChangedHappened { get; set; }
        public clsUser User { get; private set; }
        #endregion


        #region Private Helper UI Methods
        private async Task _loadWithUserInfo(clsUser User) => await this.ucUserCard1.LoadUserInfo(User: User);

        private bool _isNotAllUserInfoValid() =>
            erprovIsValidInput.GetError(txtCurrentPassword).NotIsNullOrEmpty() || erprovIsValidInput.GetError(txtNewPassword).NotIsNullOrEmpty() || erprovIsValidInput.GetError(txtConfirmPassword).NotIsNullOrEmpty();

        private (string CurrentPassword, string NewPassword, string ConfirmPassword) _getUserInfo() =>
            (CurrentPassword: txtCurrentPassword.Text.RemoveWhiteSpaces(), NewPassword: txtNewPassword.Text.RemoveWhiteSpaces(), ConfirmPassword: txtConfirmPassword.Text.RemoveWhiteSpaces());
        #endregion

        #region Public Helper UI Methods
        public async void LoadWithUserInfo(clsUser user) => await _loadWithUserInfo(user);
        #endregion


        #region Main UI Methods
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            LoadWithUserInfo(User);
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isNotAllUserInfoValid())
            {
                DialogResult.ShowMessageBoxErrorDial("Not all user info is valid!", "Ereor");
                return;
            }

            if (clsPerson.IsEmpty(User.Person)) { 
                DialogResult.ShowMessageBoxErrorDial("Person is not found!", "Error");
                return;
            }

            (string currentPassword, string newPassword, string confirmPassword) = _getUserInfo();

            User.Password = newPassword;
            IsAnyChangedHappened = User.Save();

            DialogResult.ShowMessageBoxInfoDial("Password changed successfully.", "Saved");
        }


        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text.RemoveWhiteSpaces();

            string message = null;
            bool isFocusNeeded;
            const int minimumPasswordLength = 5;

            if (isFocusNeeded = currentPassword.IsNullOrEmpty())
                message = "Password field should have a value!";
            else if (isFocusNeeded = currentPassword.Length < minimumPasswordLength)
                message = "Password field should be at lest 5 characters!";
            else if (isFocusNeeded = currentPassword.NotEquals(User.Password))
                message = "Password field should be at lest 5 characters!";

            if (e.Cancel = isFocusNeeded) txtCurrentPassword.Focus();
            erprovIsValidInput.SetError(control: txtCurrentPassword, value: message);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            string password = txtNewPassword.Text.RemoveWhiteSpaces();

            string message = null;
            bool isFocusNeeded;
            const int minimumPasswordLength = 5;

            if (isFocusNeeded = password.IsNullOrEmpty())
                message = "Password field should have a value!";
            else if (isFocusNeeded = password.Length < minimumPasswordLength)
                message = "Password field should be at lest 5 characters!";

            if (e.Cancel = isFocusNeeded) txtNewPassword.Focus();
            erprovIsValidInput.SetError(control: txtNewPassword, value: message);
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
            else if (confirmPassword.NotEquals(txtNewPassword.Text.RemoveWhiteSpaces()))
                message = "Confirm Password field should be equal to Password field!";

            if (e.Cancel = isFocusNeeded) txtConfirmPassword.Focus();
            erprovIsValidInput.SetError(control: txtConfirmPassword, value: message);
        }
        #endregion
    }
}
