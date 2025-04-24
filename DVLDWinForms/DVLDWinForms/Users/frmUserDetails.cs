using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DVLD_BLL.Users;

namespace DVLDWinForms.Users
{
    public partial class frmUserDetails : Form
    {
        #region Constructors
        private frmUserDetails() => InitializeComponent();

        public static frmUserDetails Create(clsUser User)
        {
            frmUserDetails frmUserDetails = new frmUserDetails();
            frmUserDetails.User = User;
            return frmUserDetails;
        }

        public static async Task<frmUserDetails> Create(UserID UserID)
        {
            frmUserDetails frmUserDetails = new frmUserDetails();
            frmUserDetails.User = await clsUser.FindAsync(UserID);
            return frmUserDetails;
        }
        #endregion

        #region Properties
        public clsUser User { get; private set; }
        #endregion


        #region Main UI Methods
        private async void frmUserDetails_Load(object sender, EventArgs e)
        {
            await this.ucUserCard1.LoadUserInfo(User: User);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        #endregion
    }
}
