using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperClasses.Extensions;
using DVLD_BLL.Applications.ManageTestTypes;

namespace DVLDWinForms.Applications.ManageTestTypes
{
    public sealed partial class frmUpdateTestType: Form
    {
        #region Constructors
        public frmUpdateTestType(int ID, string Title, string Description, float Fees) 
        { 
            InitializeComponent();
            (_id, _title, _description, _fees) = (ID, Title, Description, Fees);
        }
        #endregion

        #region Private Fields
        int _id { get; set; }
        float _fees { get; set; }
        string _title { get; set; }
        string _description { get; set; }
        #endregion

        #region Public Properties
        public bool IsAnyChangedHappened { get; set; }
        public Action<(int ID, string Title, string Description, float Fees)> OnTestTypeUpdated;
        #endregion


        #region Private Helper Methods
        private (string Title, string Description, float Fees) _getNewTestTypeINfo()
        {
            string title = mtxtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            bool isValidFees = float.TryParse(s: mtxtFees.Text.RemoveWhiteSpaces(), out float fees);
            return (Title: title, Description: description, Fees: isValidFees ? fees : 0f);
        }
        #endregion


        #region Main Ui Methods
        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            lblID.Text = _id.ToString();
            mtxtTitle.Text = _title;
            txtDescription.Text = _description;
            mtxtFees.Text = _fees.ToString();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            (string title, string description, float fees) = _getNewTestTypeINfo();

            if (_title.Equals(title) && _description.Equals(description) && _fees.Equals(fees))
                DialogResult.ShowMessageBoxWarningDial("You have not enter a new data!", "Test Type Update Faild");
            else if (await clsTestType.UpdateAsync(ID: _id, Title: title, Description: description, Fees: fees))
            {
                DialogResult.ShowMessageBoxInfoDial("Test type updated successfully.", "Saved");
                OnTestTypeUpdated?.Invoke((ID: _id, Title: title, Description: "", Fees: fees));
                IsAnyChangedHappened = true;
            }
            else DialogResult.ShowMessageBoxErrorDial("Test type update faild!", "Error");
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
        #endregion
    }
}
