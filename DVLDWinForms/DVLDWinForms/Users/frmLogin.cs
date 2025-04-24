using Microsoft.Win32;
using System;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using DVLD_BLL.Users;
using HelperClasses.Extensions;
using HelperClasses.Helper_Classes;

namespace DVLDWinForms
{
    public sealed partial class frmLogin : Form
    {
        #region Constructors
        public frmLogin() 
        { 
            InitializeComponent();

            if (_isRememberingUserInfoNeeded = _getValueOfIsRemberingUserInfoNeeded()) _loadUserInfoFromWinRegistry();
        }
        #endregion

        #region Private Fields
        //private string _filePath = "HKEY_CURRENT_USER\\Software\\DVLD";
        private string _filePath = "Software\\DVLD";
        private string _userName_Name = "UserName";
        private string _password_Name = "Password";
        private string _isRememberingUserInfoNeeded_Name = "IsRememberingUserInfoNeeded";
        private bool _isRememberingUserInfoNeeded;
        #endregion


        #region Private Helper Methods
        private bool _getValueOfIsRemberingUserInfoNeeded(Action<string> logError = null) => 
            WinRegistry.GetValueAsBoolean(keyPath: _filePath, valueName: _isRememberingUserInfoNeeded_Name, null, logError: logError)
            .Reduce(false);
        #endregion

        #region Private Helper UI Methods
        private void _loadUserInfoFromWinRegistry() => (txtUserName.Text, txtPassword.Text, chkRememberMe.Checked) = _readUserInfoFromWinRegistry();

        private void _saveUserInfoInWinRegisrty(string userName, string password, bool isRememberingUserInfoNeeded)
        {
            bool isUserNameSaved = WinRegistry.SetValue(keyPath: _filePath, valueName: _userName_Name, valueData: userName, RegistryValueKind.String);
            bool isPasswordSaved = WinRegistry.SetValue(keyPath: _filePath, valueName: _password_Name, valueData: password, RegistryValueKind.String);

            if (isUserNameSaved && isPasswordSaved)
                WinRegistry.SetValue(keyPath: _filePath, valueName: _isRememberingUserInfoNeeded_Name, valueData: isRememberingUserInfoNeeded, RegistryValueKind.DWord);
        }
        
        private (string UserName, string Password, bool IsRememberingUserInfoNeeded) _readUserInfoFromWinRegistry()
        {
            if (_isRememberingUserInfoNeeded) 
                return (UserName: WinRegistry.GetValueAsString(keyPath: _filePath, valueName: _userName_Name, null), 
                    Password: WinRegistry.GetValueAsString(keyPath: _filePath, valueName: _password_Name, null), 
                    IsRememberingUserInfoNeeded: _isRememberingUserInfoNeeded);

            return (UserName: null, Password: null, IsRememberingUserInfoNeeded: false);
        }

        private (string UserName, string Password) _getUserNameAndPasswordFromUI() =>
            (UserName: txtUserName.Text.RemoveWhiteSpaces(), Password: txtPassword.Text.RemoveWhiteSpaces());
        #endregion

        #region Main UI Mathods
        private void chkRememberMe_CheckedChanged(object sender, EventArgs e) => _isRememberingUserInfoNeeded = chkRememberMe.Checked;

        private async void btnClose_Click(object sender, EventArgs e) 
        {
            if (_isRememberingUserInfoNeeded)
            {
                (string userName, string password) = _getUserNameAndPasswordFromUI();

                if (await clsUser.IsExsitsAsync(userName, password)) _saveUserInfoInWinRegisrty(userName, password, true);
            }
            else _saveUserInfoInWinRegisrty(string.Empty, string.Empty, false);

            Application.Exit();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            (string userName, string password) = _getUserNameAndPasswordFromUI();

            bool isNotValidUserName = userName.IsNullOrEmpty(), isNotValidPassword = password.IsNullOrEmpty();

            if (isNotValidUserName || isNotValidPassword)
            {
                StringBuilder stringBuilder = new StringBuilder(capacity: 22);

                if (isNotValidUserName && isNotValidPassword)
                {
                    stringBuilder.Append("Username");
                    stringBuilder.Append(' '); stringBuilder.Append(','); stringBuilder.Append(' ');
                    stringBuilder.Append("Password");
                    stringBuilder.Append("are");
                }
                else if (isNotValidUserName) stringBuilder.Append("Username is");
                else                         stringBuilder.Append("Password is");

                DialogResult.ShowMessageBoxErrorDial($"{stringBuilder} empty!", "Invalid Input");
                return;
            }
            
            (bool isExsits, bool isActive) = clsUser.IsExsitsAndActive(userName, password);

            if (!isExsits)
            {
                DialogResult.ShowMessageBoxErrorDial("Invalid Username/Password.", "Wrong Credintials");
            }
            else if (!isActive)
            {
                DialogResult.ShowMessageBoxErrorDial("Tell your admin to activate your account.", "Log In Faild");
            }
            else
            {
                Users.frmMain frmMain = await Users.frmMain.CreateAsync(UserName: userName, Password: password);
                frmMain.ShowDialog();
            }
        }
        #endregion
    }
}
