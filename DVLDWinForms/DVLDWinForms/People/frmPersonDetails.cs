using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using DVLD_BLL;
using DVLD_BLL.People;

namespace DVLDWinForms.People
{
    public sealed partial class frmPersonDetails : Form
    {
        #region Constructors
        private frmPersonDetails() => InitializeComponent();

        public static async Task<frmPersonDetails> CreateNewAsync(PersonID PersonID)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails();
            await frmPersonDetails.ucPersonCard1.LoadPersonInfo(await clsPerson.FindAsync(PersonID));
            return frmPersonDetails;
        }

        public static async Task<frmPersonDetails> CreateNewAsync(clsPerson person)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails();
            await frmPersonDetails.ucPersonCard1.LoadPersonInfo(person);
            return frmPersonDetails;
        }
        #endregion


        #region Main UI Methods
        private void btnClose_Click(object sender, EventArgs e) => Close();
        #endregion
    }
}
