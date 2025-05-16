using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Extensions;
using DVLD_BLL.Applications.ManageApplicationTypes;

namespace DVLDWinForms.Applications.ManageApplicationTypes
{
    public sealed partial class frmUpdateApplicationType: Form
    {
        #region Constructors
        public frmUpdateApplicationType(ApplicationTypeID ID, string Title, float Fees) 
        { 
            InitializeComponent();
            (_id, _title, _fees) = (ID, Title, Fees);
        }
        #endregion

        #region Private Fields
        ApplicationTypeID _id { get; set; }
        float _fees { get; set; }
        string _title { get; set; }
        #endregion

        #region Public Properties
        public bool IsAnyChangedHappened { get; set; }
        public Action<(ApplicationTypeID ID, string Title, float Fees)> OnApplicationTypeUpdated;
        #endregion


        #region Private Helper Methods
        private (string Title, float Fees) _getNewApplicationTypeINfo()
        {
            string title = mtxtTitle.Text.Trim();
            bool isValidFees = float.TryParse(s: mtxtFees.Text.RemoveWhiteSpaces(), out float fees);
            return (Title: title, Fees: isValidFees ? fees : 0f);
        }
        #endregion


        #region Main Ui Methods
        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            lblID.Text = _id.ToString();
            mtxtTitle.Text = _title;
            mtxtFees.Text = _fees.ToString();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            (string title, float fees) = _getNewApplicationTypeINfo();

            if (_title.Equals(title) && _fees.Equals(fees))
                DialogResult.ShowMessageBoxWarningDial("You have not enter a new data!", "Application Type Update Faild");
            else if (await clsApplicationType.UpdateAsync(ID: _id, Title: title, Fees: fees))
            {
                DialogResult.ShowMessageBoxInfoDial("Application type updated successfully.", "Saved");
                OnApplicationTypeUpdated?.Invoke((ID: _id, Title: title, Fees: fees));
                IsAnyChangedHappened = true;
            }
            else DialogResult.ShowMessageBoxErrorDial("Application type updated faild!", "Error");
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
        #endregion

    }
}
