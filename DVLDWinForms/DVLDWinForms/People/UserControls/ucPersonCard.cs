using System;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using DVLD_BLL;
using DVLDWinForms.Properties;
using Helper.Extensions;
using DVLD_BLL.People;

namespace DVLDWinForms.People.UserControls
{
    public sealed partial class ucPersonCard : UserControl
    {
        #region Constructors
        public ucPersonCard() => InitializeComponent();
        #endregion


        #region Private Fields
        string _unknownLableValue => "[?????]";
        int _previosPersonID { get; set; }
        #endregion
        
        #region Public Properties
        public clsPerson Person { get; private set; }
        #endregion

        #region Private Helper Methods
        private void _loadWithDefaultValues()
        {
            lblPersonID   .Text = _unknownLableValue;
            lblName       .Text = _unknownLableValue;
            lblNationalNo .Text = _unknownLableValue;
            lblDateOfBirth.Text = _unknownLableValue;
            lblGender     .Text = _unknownLableValue;
            lblAddress    .Text = _unknownLableValue;
            lblPhone      .Text = _unknownLableValue;
            lblEmail      .Text = _unknownLableValue;
            lblCountry    .Text = _unknownLableValue;

            pbGender     .Image = Resources.administrator;
            pbPersonImage.Image = Resources.person_man;
            pbPersonImage.ImageLocation = null;

            linklblEditPersonInfo.Enabled = false;
        }

        private async Task _loadWithPersonInfo(clsPerson person)
        {
            lblCountry    .Text = await clsCountry.GetCountryAsync(person.NationalityCountryID);

            lblPersonID   .Text = person.ID.ToString();
            lblNationalNo .Text = person.NationalNo;
            lblName       .Text = person.FullName();
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblGender     .Text = person.Gender ? "Female" : "Male";
            lblAddress    .Text = person.Address;
            lblPhone      .Text = person.Phone;
            lblEmail      .Text = person.Email.IsNullOrEmptyOrWhiteSpace() ? _unknownLableValue : person.Email;
            pbGender     .Image = person.Gender ? Resources.user_female : Resources.administrator;

            _loadPersonImage(person: person);
            
            linklblEditPersonInfo.Enabled = true;
        }

        private void _loadPersonImage(clsPerson person)
        {
            if (person.ImagePath.IsNullOrEmptyOrWhiteSpace())
            {
                pbPersonImage.Image = person.Gender ? Resources.person_woman : Resources.person_man;
                pbPersonImage.ImageLocation = null;
            }
            else
            {
                string previosImagePath = null;
                if (_previosPersonID == person.ID.Value && pbPersonImage.ImageLocation != person.ImagePath && File.Exists(pbPersonImage.ImageLocation))
                    previosImagePath = pbPersonImage.ImageLocation;

                pbPersonImage.LoadAsync(person.ImagePath);

                if (!(previosImagePath is null))
                    try { if (File.Exists(previosImagePath)) File.Delete(previosImagePath); }
                    catch { }
            }

            _previosPersonID = person.ID.Value;
        }

        private async void _reloadPersonInfo(clsPerson Person) => await this.LoadPersonInfo(person: Person);
        #endregion

        #region Public Helpers Methods
        public async Task<clsPerson> LoadPersonInfo(clsPerson person)
        {
            if (clsPerson.IsEmpty(person)) _loadWithDefaultValues();
            else await _loadWithPersonInfo(person);

            return this.Person = person;
        }
        #endregion

        #region Main UI Methods
        private void linklblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsPerson.IsEmpty(this.Person)) return;

            frmAddEditPerson frmEditPerson = frmAddEditPerson.CreateNew(Person: Person);

            frmEditPerson.ucAddEditPerson1.OnPersonInfoSaved += _reloadPersonInfo;

            frmEditPerson.ShowDialog();
        }
        #endregion
    };

}
