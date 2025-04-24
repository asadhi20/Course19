using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DVLDWinForms.Applications.ManageApplications;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.Users;

using frmManageApplicationTypes = DVLDWinForms.Applications.ManageApplicationTypes.frmManageApplicationTypes;
using frmManageTestTypes = DVLDWinForms.Applications.ManageTestTypes.frmManageTestTypes;

using frmNewLocalDrivingLicenseApplication = DVLDWinForms.Applications.DivingLicenseServices.NewDrivingLicense.frmNewLocalDrivingLicenseApplication;

namespace DVLDWinForms.Users
{
    public sealed partial class frmMain : Form
    {
        #region Constractors
        private frmMain() => InitializeComponent();

        public static frmMain Create(clsUser User)
        {
            frmMain frmMain = new frmMain();
            frmMain._currentUser = User;
            return frmMain;
        }

        public static async Task<frmMain> CreateAsync(string UserName, string Password)
        {
            frmMain frmMain = new frmMain();
            frmMain._currentUser = await clsUser.FindAsync(UserName, Password);
            return frmMain;
        }
        #endregion

        #region Private Fields
        private clsUser _currentUser { get; set; }

        People.frmManagePeople frmManagePeople = null;
        frmManageUsers frmManageUsers = null;
        frmChangePassword frmChangePassword = null;
        frmLocalDrivingLicenseApplications frmLocalDLApplications = null;
        #endregion

        #region Public Properties
        public bool IsAnyFormOpen;
        #endregion


        #region Main UI Methods
        ////////////////////// Account Settings \\\\\\\\\\\\\\\\\\\\\\
        private void CurrentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails frmUserDetails = frmUserDetails.Create(User: _currentUser);
            frmUserDetails.ShowDialog();
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmChangePassword is null || frmChangePassword.IsDisposed) {
                frmChangePassword = frmChangePassword.Create(User: _currentUser);
            }

            frmChangePassword.ShowDialog();
        }

        private void SignOutToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();


        //////////////////////   Menage Users   \\\\\\\\\\\\\\\\\\\\\\
        private void ManageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAnyFormOpen) return;

            if (frmManageUsers is null || frmManageUsers.IsDisposed) frmManageUsers = new frmManageUsers();
            IsAnyFormOpen = true;
            frmManageUsers.MdiParent = this;
            frmManageUsers.Show();
        }


        //////////////////////     Drivers      \\\\\\\\\\\\\\\\\\\\\\



        //////////////////////   Manage People  \\\\\\\\\\\\\\\\\\\\\\
        private void ManagePeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAnyFormOpen) return;

            if (frmManagePeople is null || frmManagePeople.IsDisposed) frmManagePeople = new People.frmManagePeople();
            IsAnyFormOpen = true;
            frmManagePeople.MdiParent = this;
            frmManagePeople.Show();
        }


        //////////////////////   Applications   \\\\\\\\\\\\\\\\\\\\\\

        //////////////////////   1- Driving License Services
        //////////////////////   1.1- New Driving License
        //////////////////////   1.1.1- Local License
        private void LocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLDLApplication = frmNewLocalDrivingLicenseApplication.CreateNew(clsLocalDrivingLicenseApplication.Empty, User: _currentUser);

            frmNewLDLApplication.ShowDialog();
        }

        //////////////////////   1.1.2- International License
        private void InternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //////////////////////   1.2- Renew Driving License



        //////////////////////   2- Manage Application
        //////////////////////   2.1- Local Driving Licesne Applications
        private void LocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAnyFormOpen) return;

            if (frmLocalDLApplications is null || frmLocalDLApplications.IsDisposed) frmLocalDLApplications = frmLocalDrivingLicenseApplications.CreateNew(_currentUser);
            IsAnyFormOpen = true;
            frmLocalDLApplications.MdiParent = this;
            frmLocalDLApplications.Show();
        }

        //////////////////////   3- 

        //////////////////////   4- Manage Application Types
        private void ManageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frmManageApplicationTypes = new frmManageApplicationTypes();
            frmManageApplicationTypes.ShowDialog();
        }

        //////////////////////   5- Manage Test Types
        private void ManageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();
        }
        #endregion
    }
}
