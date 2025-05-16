using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Helper.Extensions;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.People;
using DVLD_BLL.Users;

namespace DVLDWinForms.Applications.UserControls
{
    public sealed partial class ucLocalDLAppInfoCard: UserControl
    {
        #region Constructors
        public ucLocalDLAppInfoCard() => InitializeComponent();
        #endregion


        #region Private Fields
        private readonly string _unknownLableValue = "[?????]";
        private clsPerson _person { get; set; }
        private clsUser _user { get; set; }
        #endregion

        #region Public Properties
        public clsLocalDrivingLicenseApplication LDLApplication { get; set; }
        #endregion

        #region Public UI Helper Methods
        public async Task LoadLDLApplicationInfo(clsLocalDrivingLicenseApplication localDLApplication)
        {
            if (localDLApplication is null) return;

            (string drivingClass, string nationalNo, string fullName, DateTime applicationDate, int passedTests, string status) localDLApp = 
                await clsLocalDrivingLicenseApplication.GetSingleLcoalDLApplications_ViewAsync(localDLApplication.ID);

            _person = localDLApplication.Application.ApplicantPerson;
            _user = await clsUser.FindAsync(localDLApplication.Application.CreatedByUserID);

            lblDLAppID.Text = localDLApplication.ID.Value > 1 ? localDLApplication.ID.ToString() : _unknownLableValue;
            lblAppliedForLicense.Text = localDLApp.drivingClass ?? _unknownLableValue;
            lblPassedTests.Text = localDLApp.passedTests.ToString();

            this.ucApplicationBasicInfoCard1.LoadApplicationInfo(localDLApplication.Application, _person, _user)
                .SafeFireAndForget();
        }
        #endregion

        #region Main UI Methods
        private void linklblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LDLApplication is null) return;


        }
        #endregion
    }
}
