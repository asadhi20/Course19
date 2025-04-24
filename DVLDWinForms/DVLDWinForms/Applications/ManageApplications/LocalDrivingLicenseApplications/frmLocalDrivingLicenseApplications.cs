using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using HelperClasses.Extensions;
using DVLDWinForms.Applications.ManageApplications.LocalDrivingLicenseApplications.SechdlueTests;
using DVLDWinForms.Applications.DivingLicenseServices.NewDrivingLicense;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.Users;
using static DVLD_BLL.Applications.ManageApplications.clsApplication;
using System.ComponentModel;
using DVLD_BLL.Applications.ManageApplications;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications.SechduleTests;

namespace DVLDWinForms.Applications.ManageApplications
{
    public sealed partial class frmLocalDrivingLicenseApplications : Form
    {
        #region Constructors
        private frmLocalDrivingLicenseApplications() => InitializeComponent();

        public static frmLocalDrivingLicenseApplications CreateNew(clsUser User)
        {
            frmLocalDrivingLicenseApplications frmLocalDLApplications = new frmLocalDrivingLicenseApplications();
            frmLocalDLApplications.User = User;
            return frmLocalDLApplications;
        }

        public static async Task<frmLocalDrivingLicenseApplications> CreateNewAsync(string UserName, string Password)
        {
            frmLocalDrivingLicenseApplications frmLocalDLApplications = new frmLocalDrivingLicenseApplications();
            frmLocalDLApplications.User = await clsUser.FindAsync(UserName, Password);
            return frmLocalDLApplications;
        }
        #endregion


        #region Private Fields
        DataTable _dtLocalDLApplications { get; set; }
        #endregion
        
        #region Public Properties
        public clsUser User { get; set; }
        public bool IsAnyChangedHappened { get; private set; }
        #endregion


        #region Private Helper Methods
        private async Task<(bool IsApplicationDeleted, bool IsUserCencelledOperation)> _deleteApplicationWillNotifyUserUsingMessageBox(int SelectedPersonID)
        {
            if (DialogResult.ShowMessageBoxQuestionDial(
                    $"Are you sure you want to delete Local Driving License application with ID = {SelectedPersonID} ?", 
                    "Confirme Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return (IsApplicationDeleted: false, IsUserCencelledOperation: true);

            LocalDrivingLicenseApplicationID selectedLDLAppID = LocalDrivingLicenseApplicationID.CreateNew(SelectedPersonID);

            bool isLocalDLApplicationDeleted = await clsLocalDrivingLicenseApplication.DeleteAsync(ID: selectedLDLAppID);
            
            if (isLocalDLApplicationDeleted) DialogResult.ShowMessageBoxInfoDial(
                    $"Local Driving License application with ID = {SelectedPersonID} is deleted.", 
                    "Delete Local Driving License application");
            else DialogResult.ShowMessageBoxErrorDial(
                "Local Driving License application was not deleted!", "An Error Occurded");

            return (IsApplicationDeleted: isLocalDLApplicationDeleted, IsUserCencelledOperation: false);
        }
        #endregion


        #region Private Helper Methods For UI
        private void _refreshDGV_DefaultViewWithLableNumberOfRecords()
        {
            string filterColumn = cmbFilterBy.Text;
            string filterText = mtxtFilter.Text.RemoveWhiteSpaces();

            _setDGVDefaultViewRowFilter(dataTable: _dtLocalDLApplications, selectedFilterColumn: filterColumn,
                filterText: filterText, isNumber: filterColumn == "L.D.L.AppID");

            _refreshLableNumberOfRecords();
        }

        private void _setDGVDefaultViewRowFilter(DataTable dataTable, string selectedFilterColumn, string filterText, bool isNumber)
        {
            dataTable.DefaultView.RowFilter = filterText.IsNullOrEmptyOrWhiteSpace() || selectedFilterColumn.IsNullOrEmpty() || selectedFilterColumn == "None"
                                            ? string.Empty //Reset the filters in case nothing selected or filter value conains nothing.
                                            : isNumber //in this case we deal with integer not string.
                                            ? string.Format("[{0}] = {1}", selectedFilterColumn, filterText)
                                            : string.Format("[{0}] LIKE '{1}%'", selectedFilterColumn, filterText);
        }

        private void _refreshLableNumberOfRecords() => lblNumberOfRecords.Text = lblNumberOfRecords.Tag.ToString() + _dtLocalDLApplications.DefaultView.Count;


        private string _getMaskBasedOnSelectedFilterColumn(string FillterByColumn)
        {
            switch (FillterByColumn)
            {
                case "L.D.L.AppID": return "000000000";
                case "National No.": return "N000000000";
                case "Full Name": return ">?|&&&&&&&&&&&&&&&&";
                default: return string.Empty;
            }
        }


        private int[] _getSelectedRowsIndeces(DataGridView dataGridView)
        {
            int[] selectedRowsIndeces = new int[dataGridView.SelectedRows.Count];

            int count = selectedRowsIndeces.Length, invalidIndex = -1;
            while (--count > invalidIndex) selectedRowsIndeces[count] = dataGridView.SelectedRows[count].Index;

            return selectedRowsIndeces;
        }

        private async Task _refrashDGVItemsAsync()
        {
            dgvLDLApplications.DataSource = _dtLocalDLApplications = await clsLocalDrivingLicenseApplication.GetLDLApplicationsAsync();

            _refreshDGV_DefaultViewWithLableNumberOfRecords();
        }

        private void _reselectRows(DataGridView dataGridView, int[] selectedRowsIndeces)
        {
            dataGridView.SelectedRows[0].Selected = false; // Remove the first row from selected rows.

            int count = selectedRowsIndeces.Length, invalidIndex = -1;
            while (--count > invalidIndex) dataGridView.Rows[selectedRowsIndeces[count]].Selected = true;
        }

        private async Task _refresh_dgvLDLApplicationsWithReselectSelectedRows(DataGridView dataGridView)
        {
            int[] selectedRowsIndeces = _getSelectedRowsIndeces(dataGridView);

            await _refrashDGVItemsAsync();

            _reselectRows(dataGridView: dataGridView, selectedRowsIndeces: selectedRowsIndeces);
        }


        private async void _loadEditLDLApplicaionForm()
        {
            bool isLDLApplicaionIDValid = int.TryParse(dgvLDLApplications[0, dgvLDLApplications.SelectedRows[0].Index].Value.ToString(), out int _selectedLDLApplicaionID);

            if (!isLDLApplicaionIDValid && _selectedLDLApplicaionID < 1)
            {
                DialogResult.ShowMessageBoxErrorDial("Local Driving License Applicaion ID is not valid!", "An Error Occurded");
                return;
            }

            LocalDrivingLicenseApplicationID lDLApplicaionID = LocalDrivingLicenseApplicationID.CreateNew(_selectedLDLApplicaionID);

            frmNewLocalDrivingLicenseApplication frmEditLDLApplicaion = 
                await frmNewLocalDrivingLicenseApplication.CreateNewAsync(lDLApplicaionID, User: User);

            frmEditLDLApplicaion.ShowDialog();

            if (frmEditLDLApplicaion.IsAnyChangedHappened) await _refresh_dgvLDLApplicationsWithReselectSelectedRows(dataGridView: dgvLDLApplications);
        }
        
        private void _setLDLApplicationsDGVColumnsSize()
        {
            try
            {
                const int widthOfFullNameColumn = 300, widthOfDrivingClassColumn = 300, widthOfApplicationDateColumn = 150;

                dgvLDLApplications.Columns["Driving Class"].Width = widthOfDrivingClassColumn;
                dgvLDLApplications.Columns["Application Date"].Width = widthOfApplicationDateColumn;
                dgvLDLApplications.Columns["Full Name"].Width = widthOfFullNameColumn;
            }
            catch { }
        }
        #endregion


        #region Main UI Methods
        private async void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            dgvLDLApplications.DataSource = _dtLocalDLApplications = await clsLocalDrivingLicenseApplication.GetLDLApplicationsAsync();

            //dgvLDLApplications.SortingColumns(("Passed Tests", ListSortDirection.Ascending), ("National No.", ListSortDirection.Descending));
            dgvLDLApplications.SortingColumns(("Passed Tests", ListSortDirection.Ascending));

            _setLDLApplicationsDGVColumnsSize();
            _refreshLableNumberOfRecords();
        }


        private void frmLocalDrivingLicenseApplications_FormClosed(object sender, FormClosedEventArgs e) => ((Users.frmMain)ParentForm).IsAnyFormOpen = false;

        private void btnClose_Click(object sender, EventArgs e) { ((Users.frmMain)ParentForm).IsAnyFormOpen = false; Close(); }

        private async void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDLApplication = frmNewLocalDrivingLicenseApplication.CreateNew(clsLocalDrivingLicenseApplication.Empty, User: User);

            frmNewLocalDLApplication.ShowDialog();

            if (frmNewLocalDLApplication.IsAnyChangedHappened) await _refrashDGVItemsAsync();
        }


        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            if (DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Show Details") == DialogResult.OK) return;

            const int firstRowIndex = 0, indexOfLDLAppIDColumn = 0; ;
            int SelectedRowIndex = dgvLDLApplications.SelectedRows[firstRowIndex].Index;
            if (!int.TryParse(dgvLDLApplications[indexOfLDLAppIDColumn, SelectedRowIndex].Value.ToString(), out int _LDLAppID)) return;

            
        }

        private void tsmEditApplication_Click(object sender, EventArgs e) => _loadEditLDLApplicaionForm();

        private async void tsmDeleteLDLApplications_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count < 1) return;

            const int indexOfAppIDColumn = 0;
            int selectedAppID = -1;
            short numberOfDeletedApplications = 0;
            bool isAppDeleted = false, isUserCancelledOperation = false;

            int[] selectedRowsIndeces = new int[dgvLDLApplications.SelectedRows.Count];

            for (int i = 0; i < dgvLDLApplications.SelectedRows.Count; i++)
                selectedRowsIndeces[i] = dgvLDLApplications.SelectedRows[i].Index;

            for (int i = 0; i < selectedRowsIndeces.Length; i++)
            {
                if (int.TryParse(dgvLDLApplications[indexOfAppIDColumn, selectedRowsIndeces[i]].Value.ToString(), out selectedAppID)) 
                {
                    (isAppDeleted, isUserCancelledOperation) = await _deleteApplicationWillNotifyUserUsingMessageBox(selectedAppID);
                    if (isAppDeleted) numberOfDeletedApplications++;
                }

                if (!isUserCancelledOperation && !isAppDeleted) break;
            }

            if (numberOfDeletedApplications > 0) await _refrashDGVItemsAsync();
        }


        private async void tsmCancelApplication_Click(object sender, EventArgs e)
        {
            const int firstRowIndex = 0;
            int SelectedRowIndex = dgvLDLApplications.SelectedRows[firstRowIndex].Index;
            
            string selectedApplicationStatus = dgvLDLApplications["Status", SelectedRowIndex].Value.ToString();

            if (selectedApplicationStatus.NotEquals("New"))
            {
                DialogResult.ShowMessageBoxErrorDial($"You can not cancel \'{selectedApplicationStatus}\' application!", 
                    "Cancelling Application Status Faild");
                return;
            }
            
            const int indexOfLDLAppIDColumn = 0;
            if (!int.TryParse(dgvLDLApplications[indexOfLDLAppIDColumn, SelectedRowIndex].Value.ToString(), out int _LDLAppID)) return;

            LocalDrivingLicenseApplicationID LDLAppID = LocalDrivingLicenseApplicationID.CreateNew(_LDLAppID);
            bool isAppStatusChanged = await clsLocalDrivingLicenseApplication.UpdateApplicationStatusAsync(LDLAppID, enApplicationStatus.Cancelled);

            if (isAppStatusChanged)
            {
                DialogResult.ShowMessageBoxInfoDial(
                    $"Local Driving License Application with id = {_LDLAppID} is cancelled.", "Cancelation Application Status");

                await _refrashDGVItemsAsync();
            }
            else DialogResult.ShowMessageBoxInfoDial(
                $"Local Driving License Application with id = {_LDLAppID} is not cancelled.", "Cancelation Application Status");
        }


        // Sechdule Tests
        private async void tsmSechduleVisionTest_Click(object sender, EventArgs e)
        {
            const int indexOfFirstRow = 0, indexOfLDLAppCol = 0;
            int selectedRowIndex = dgvLDLApplications.SelectedRows[indexOfFirstRow].Index;

            bool isLDLAppIDValid = int.TryParse(dgvLDLApplications[indexOfLDLAppCol, selectedRowIndex].Value.ToString(),
                out int _LDLAppID);

            if (!isLDLAppIDValid || _LDLAppID < 1)
            {
                DialogResult.ShowMessageBoxErrorDial("Invalid local driving licnese application id", "An Error Occurrded");
                return;
            }

            LocalDrivingLicenseApplicationID lDLAppID = LocalDrivingLicenseApplicationID.CreateNew(_LDLAppID);

            clsLocalDrivingLicenseApplication lDLApp = await clsLocalDrivingLicenseApplication.FindAsync(lDLAppID);

            frmVisionTestAppointments frmSechduleVisionTest = frmVisionTestAppointments.CreateNew(lDLApp);
            frmSechduleVisionTest.ShowDialog();
        }

        private void tsmSechduleWrittenTest_Click(object sender, EventArgs e)
        {
            DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Sechdule Written Test");
        }

        private void tsmSechduleStreetTest_Click(object sender, EventArgs e)
        {
            DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Sechdule Street Test");
        }


        private void tsmIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Issue Driving License");
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Show License");
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Show Person License History");
        }


        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.SelectedIndex == 0)
            {
                mtxtFilter.Visible = false;
                mtxtFilter.Text = string.Empty;
                mtxtFilter.Mask = string.Empty;
                return;
            }

            mtxtFilter.Visible = true;
            mtxtFilter.Mask = _getMaskBasedOnSelectedFilterColumn(cmbFilterBy.SelectedItem.ToString());
        }

        private void mtxtFillter_TextChanged(object sender, EventArgs e) => _refreshDGV_DefaultViewWithLableNumberOfRecords();


        private void dgvLDLApplications_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            string status = dgvLDLApplications.SelectedRows[0].Cells["Status"].Value.ToString();

            bool isNotCancelledStatus = status.NotEquals(clsApplication.enApplicationStatus.Cancelled.ToString());
            bool isNotCompletedStatus = status.NotEquals(clsApplication.enApplicationStatus.Completed.ToString());

            tsmCancelApplication.Enabled = isNotCancelledStatus && isNotCompletedStatus;

            tsmEditApplication.Enabled = tsmDeleteApplication.Enabled = isNotCompletedStatus;
            tsmShowLicense.Enabled = !isNotCompletedStatus;

            bool isValidPassedTests = int.TryParse(s: dgvLDLApplications.SelectedRows[0].Cells["Passed Tests"].Value.ToString(), out int passedTests);

            if (!isNotCancelledStatus) {
                tsmSechduleTests.Enabled = false;
            }
            else if (isValidPassedTests) {
                switch (passedTests)
                {
                    case 0:
                        tsmSechduleTests.Enabled = true;
                        tsmSechduleVisionTest.Enabled = true;
                        tsmSechduleWrittenTest.Enabled = false;
                        tsmSechduleStreetTest.Enabled = false;
                        break;

                    case 1:
                        tsmSechduleTests.Enabled = true;
                        tsmSechduleVisionTest.Enabled = false;
                        tsmSechduleWrittenTest.Enabled = true;
                        tsmSechduleStreetTest.Enabled = false;
                        break;

                    case 2:
                        tsmSechduleTests.Enabled = true;
                        tsmSechduleVisionTest.Enabled = false;
                        tsmSechduleWrittenTest.Enabled = false;
                        tsmSechduleStreetTest.Enabled = true;
                        break;

                    case 3:
                        tsmSechduleTests.Enabled = false;
                        break;
                }
            }
        }
        #endregion
    };

}