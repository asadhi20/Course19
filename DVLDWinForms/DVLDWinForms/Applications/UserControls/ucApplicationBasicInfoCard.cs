using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using DVLD_BLL.Applications.ManageApplicationTypes;
using DVLD_BLL.Applications.ManageApplications;
using DVLDWinForms.People;
using DVLD_BLL.People;
using DVLD_BLL.Users;

namespace DVLDWinForms.Applications.UserControls
{
    public sealed partial class ucApplicationBasicInfoCard : UserControl
    {
        #region Constructors
        public ucApplicationBasicInfoCard() => InitializeComponent();
        #endregion

        #region Private Fields
        private readonly string _unknownLableValue = "[?????]";
        private clsPerson _person { get; set; }
        private clsUser _user { get; set; }
        #endregion

        #region Public Methods
        public clsApplication Application { get; private set; }
        #endregion

        #region Public UI Helper Methods
        public async Task LoadApplicationInfo(clsApplication application, clsPerson person, clsUser user)
        {
            if (application is null || person is null || user is null) return;

            Application = application;
            _person = person;
            _user = user;

            Task<string> taskAppType = clsApplicationType.FindAsync(application.ApplicationTypeID);

            try { lblAppType.Text = await taskAppType ?? _unknownLableValue; }
            catch { lblAppType.Text = _unknownLableValue; }

            lblAppID.Text = application.ID.ToString();
            lblAppStatus.Text = application.ApplicationStatus.ToString();
            lblAppFees.Text = application.PaidFees.ToString();
            lblApplicantName.Text = _person.FullName() ?? _unknownLableValue;
            lblAppDate.Text = application.ApplicationDate.ToString();
            lblLastStatusDate.Text = application.LastStatusDate.ToString();
            lblCreatedByUserName.Text = _user.UserName ?? _unknownLableValue;
        }
        #endregion

        #region Main UI Methods
        private async void linklblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_person is null) return;

            try
            {
                frmPersonDetails frmPersonDetails = await frmPersonDetails.CreateNewAsync(_person);
                frmPersonDetails.ShowDialog();
            }
            catch { MessageBox.Show("Error opening person details!", "An Error Occurreded", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        #endregion
    }
}
