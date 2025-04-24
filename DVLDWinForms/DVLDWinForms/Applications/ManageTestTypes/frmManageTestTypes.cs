using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BLL.Applications.ManageTestTypes;

namespace DVLDWinForms.Applications.ManageTestTypes
{
    public partial class frmManageTestTypes: Form
    {
        #region Constructors
        public frmManageTestTypes() => InitializeComponent();
        #endregion

        #region Private Fields
        DataTable _dtTestTypes { get; set; }
        #endregion

        
        #region Private Helper UI Methods
        private void _refreshLableNumberOfRecords() => lblNumberOfRecords.Text = lblNumberOfRecords.Tag.ToString() + _dtTestTypes.DefaultView.Count;

        private void _setTestTypesDGVColumnsSize()
        {
            try { 
                dgvTestTypes.Columns["Title"].Width = 150; 
                dgvTestTypes.Columns["Description"].Width = 400; 
            }
            catch { }
        }

        private (int ID, string Title, string Description, float Fees) _getTestTypeInfo()
        {
            int selectedRowIndex = dgvTestTypes.SelectedRows[0].Index;
            const int idColumnIndex = 0, titleColumnIndex = 1, descriptionColumnIndex = 2, feesColumnIndex = 3;

            int id = 0;
            if (int.TryParse(dgvTestTypes[idColumnIndex, selectedRowIndex].Value.ToString(), out id)) { }

            string title = dgvTestTypes[titleColumnIndex, selectedRowIndex].Value.ToString();
            
            string description = dgvTestTypes[descriptionColumnIndex, selectedRowIndex].Value.ToString();

            float fees = 0f;
            if (float.TryParse(dgvTestTypes[feesColumnIndex, selectedRowIndex].Value.ToString(), out fees)) { }

            return (ID: id, Title: title, Description: description, Fees: fees);
        }

        private async void _reloadTestTypes()
        {
            int previousSelectedRowIndex = dgvTestTypes.SelectedRows[0].Index;

            dgvTestTypes.DataSource = _dtTestTypes = await clsTestType.GetTestTypesAsync();

            dgvTestTypes.Rows[previousSelectedRowIndex].Selected = true;
        }
        #endregion


        #region Main UI Methods
        private async void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypes.DataSource = _dtTestTypes = await clsTestType.GetTestTypesAsync();

            _setTestTypesDGVColumnsSize();
            _refreshLableNumberOfRecords();
        }

        private void EditTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (int id, string title, string description, float fees) = _getTestTypeInfo();

            frmUpdateTestType frmUpdateTestType = new frmUpdateTestType(ID: id, Title: title, Description: description, Fees: fees);
            frmUpdateTestType.ShowDialog();

            if (frmUpdateTestType.IsAnyChangedHappened) _reloadTestTypes();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
        #endregion

    }
}
