using System;
using System.Windows.Forms;
using Helper.Extensions;
using DVLD_BLL;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BLL.People;

namespace DVLDWinForms.People.UserControls
{
    public sealed partial class ucPersonInfoCardWithFilter : UserControl
    {
        #region Constructors
        public ucPersonInfoCardWithFilter() => InitializeComponent();
        #endregion


        #region Private Helper UI Methods 
        private async void _reloadPersonInfo(clsPerson Person) => await this.ucPersonCard1.LoadPersonInfo(Person);

        private async Task<clsPerson> _getPersonInfoPyFilterText(string filterColumn, string filterText)
        {
            switch (filterColumn)
            {
                case "Person ID":
                    if (int.TryParse(s: filterText, out int id)) return await clsPerson.FindAsync(PersonID.CreateNew(id));
                    MessageBox.Show("Invalid person id!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;

                case "National No": return clsPerson.FindByNationalNo(nationalNo: filterText);
                default: return null;
            }
        }

        private string _getMaskBasedOnSelectedFilterColumn(string FillterByColumn)
        {
            switch (FillterByColumn)
            {
                case "Person ID": return "000000000";
                case "National No": return "N000000000";
                default: return string.Empty;
            }
        }
        #endregion

        #region Public UI Helper Methods
        public async Task<clsPerson> LoadPersonInfo(clsPerson person)
        {
            if (clsPerson.IsNotEmpty(person)) this.mtxtFilter.Text = person.NationalNo;
            return await this.ucPersonCard1.LoadPersonInfo(person);
        }
        #endregion

        #region Main UI Methods
        private void ucPersonInfoCardWithFilter_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedItem = "National No";
        }


        private async void btnFilter_Click(object sender, EventArgs e)
        {
            string filterText = mtxtFilter.Text.RemoveWhiteSpaces();
            if (filterText.IsNullOrEmpty()) return;

            string filterColumn = (string)cmbFilterBy.SelectedItem;
            clsPerson person = await _getPersonInfoPyFilterText(filterColumn: filterColumn, filterText: filterText);

            if (clsPerson.IsEmpty(person)) MessageBox.Show($"No person with {filterColumn} = {filterText}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            await ucPersonCard1.LoadPersonInfo(person);
        }


        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddPerson = frmAddEditPerson.CreateNew(PersonID: PersonID.Empty);

            frmAddPerson.ucAddEditPerson1.OnPersonInfoSaved += _reloadPersonInfo;

            frmAddPerson.ShowDialog();
        }


        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtxtFilter.Text = null;
            mtxtFilter.Mask = _getMaskBasedOnSelectedFilterColumn(cmbFilterBy.SelectedItem.ToString());
        }
        #endregion
    }
}
