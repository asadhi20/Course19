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

namespace DVLDWinForms.Users.UserControles
{
    public partial class ucUserCard : UserControl
    {
        #region Contructors
        public ucUserCard() => InitializeComponent();
        #endregion


        #region Public Methods
        public async Task LoadUserInfo(clsUser User)
        {
            if (clsUser.IsEmpty(User)) return;

            await this.ucPersonCard1.LoadPersonInfo(person: User.Person);
            this.lblUserID.Text = User.ID.ToString();
            this.lblUserName.Text = User.UserName;
            this.lblIsActive.Text = User.IsActive ? "Yes" : "No";
        }
        #endregion
    }
}
