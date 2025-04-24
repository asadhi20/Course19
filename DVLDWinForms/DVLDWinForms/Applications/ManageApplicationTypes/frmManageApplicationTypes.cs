using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BLL.Applications.ManageApplicationTypes;

namespace DVLDWinForms.Applications.ManageApplicationTypes
{
    public sealed partial class frmManageApplicationTypes: Form
    {
        #region Constructors
        public frmManageApplicationTypes() => InitializeComponent();
        #endregion


        #region Private Fields
        private DataTable _dtApplicationtypes;
        #endregion

        #region Private Helper UI Methods
        private void _refreshLableNumberOfRecords() => lblNumberOfRecords.Text = lblNumberOfRecords.Tag.ToString() + _dtApplicationtypes.DefaultView.Count;

        private void _setApplicationTypesDGVColumnsSize()
        {
            try { dgvApplicationTypes.Columns["Title"].Width = dgvApplicationTypes.Width - 260; }
            catch { }
        }

        private (int ID, string Title, float Fees) _getApplicationTypeInfo()
        {
            int selectedRowIndex = dgvApplicationTypes.SelectedRows[0].Index;
            const int idColumnIndex = 0, titleColumnIndex = 1, feesColumnIndex = 2;

            int id = 0;
            if (int.TryParse(dgvApplicationTypes[idColumnIndex, selectedRowIndex].Value.ToString(), out id)) { }
            
            string title = dgvApplicationTypes[titleColumnIndex, selectedRowIndex].Value.ToString();

            float fees = 0f;
            if (float.TryParse(dgvApplicationTypes[feesColumnIndex, selectedRowIndex].Value.ToString(), out fees)) { }

            return (ID: id, Title: title, Fees: fees);
        }

        private async void _reloadApplicationTypes()
        {
            int previousSelectedRowIndex = dgvApplicationTypes.SelectedRows[0].Index;

            dgvApplicationTypes.DataSource = _dtApplicationtypes = await clsApplicationType.GetApplicationTypesAsync();

            dgvApplicationTypes.Rows[previousSelectedRowIndex].Selected = true;
        }
        #endregion

        #region Main UI Methods
        private async void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dgvApplicationTypes.DataSource = _dtApplicationtypes = await clsApplicationType.GetApplicationTypesAsync();

            _setApplicationTypesDGVColumnsSize();
            _refreshLableNumberOfRecords();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void EditApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (int id, string title, float fees) = _getApplicationTypeInfo();

            frmUpdateApplicationType frmUpdateApplicationType = new frmUpdateApplicationType(ID: ApplicationTypeID.CreateNew(id), Title: title, Fees: fees);
            frmUpdateApplicationType.ShowDialog();

            if (frmUpdateApplicationType.IsAnyChangedHappened) _reloadApplicationTypes();
        }
        #endregion
    }
}
