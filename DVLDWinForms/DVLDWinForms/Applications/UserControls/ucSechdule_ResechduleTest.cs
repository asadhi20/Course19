using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications.SechduleTests;
using DVLD_BLL.Applications.DrivingLicenseServices;
using DVLD_BLL;
using HelperClasses.Extensions;

namespace DVLDWinForms.Applications.UserControls
{
    public partial class ucSechdule_ResechduleTest: UserControl
    {
        #region Constructors
        public ucSechdule_ResechduleTest() 
        {
            InitializeComponent();
        }
        #endregion

        #region Private Fields
        private clsSechduleTest _sechduleTest;
        private int _numberOfTrials = int.MaxValue;
        private bool _isAddingMode = false;

        private static IEnumerable<clsLicenseClass> _drivingLicense; 
        #endregion

        #region Public Properties
        #endregion


        #region Private Methods
        private async Task _loadDrivingLicense()
        {
            _drivingLicense = await clsLazySingleton.Instance.GetClassLicensesAsync();
        }
        #endregion

        #region Private UI Helper Methods
        private void _loadGeneralInfo()
        {
            this.lblDLAppID.Text = _sechduleTest.LDLApplication.ID.ToString();
            this.lblDrivingClass.Text = _drivingLicense?
                .First(licClass => licClass.LicenseID.Equals(_sechduleTest.LDLApplication.LicenseID))
                .ClassName ?? "[?????]";

            this.lblPersonName.Text = _sechduleTest.LDLApplication.Application.ApplicantPerson.FullName();
            this.lblTitel.Text = _numberOfTrials.ToString();
            this.dtpData.Text = _sechduleTest.AppointmentDate.ToShortDateString();
            this.lblFees.Text = _sechduleTest.PaidFees.ToString();
        }

        private void _loadInVisionTestMode()
        {
            _loadGeneralInfo();
            this.gpbTestType.Text = "Vision Test";
        }

        private void _loadInWrittenTestMode()
        {
            _loadGeneralInfo();
            this.gpbTestType.Text = "Written Test";
        }

        private void _loadInStreetTestMode()
        {
            _loadGeneralInfo();
            this.gpbTestType.Text = "Street Test";
        }
        #endregion

        #region Public UI Helper Methods
        public void LoadInfoToControl(clsSechduleTest SechduleTest, clsSechduleTest.enTestAppointmentType TestAppointmentType, int NumberOfTrials)
        {
            _loadDrivingLicense().SafeFireAndForget();

            _sechduleTest = SechduleTest;
            _numberOfTrials = NumberOfTrials;

            switch (TestAppointmentType)
            {
                case clsSechduleTest.enTestAppointmentType.Vision:
                    _loadInVisionTestMode();
                    break;
                case clsSechduleTest.enTestAppointmentType.Written:
                    _loadInWrittenTestMode();
                    break;
                case clsSechduleTest.enTestAppointmentType.Street:
                    _loadInStreetTestMode();
                    break;
            }

            if (_isAddingMode)
            {
                lblError.Visible = false;
            }

            if (_numberOfTrials > 0)
            {

            }
        }
        #endregion

        #region Main UI Methods
        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
