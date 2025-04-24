using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications.SechduleTests;

namespace DVLDWinForms.Applications.ManageApplications.LocalDrivingLicenseApplications.SechdlueTests
{
    public partial class frmSechduleVisionTest: Form
    {
        #region Constructors
        public frmSechduleVisionTest(clsLocalDrivingLicenseApplication lDLApplication)
        {
            InitializeComponent();
            LDLApplication = lDLApplication;
        }
        #endregion


        #region Private Fields
        #endregion


        #region Public Properties
        public clsLocalDrivingLicenseApplication LDLApplication;
        #endregion

        #region Main UI Methods
        private void frmSechduleVisionTest_Load(object sender, EventArgs e)
        {
            this.ucSechdule_ResechduleTest1.LoadInfoToControl(clsSechduleTest.Empty, clsSechduleTest.enTestAppointmentType.None, 0);
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
        #endregion
    }
}
