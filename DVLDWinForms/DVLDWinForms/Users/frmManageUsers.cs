using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using Helper.Extensions;
using DVLD_BLL.People;
using DVLD_BLL.Users;

namespace DVLDWinForms.Users
{
    public sealed partial class frmManageUsers : Form
    {
        #region Constructors
        public frmManageUsers() => InitializeComponent();
        #endregion

        #region Private Fields
        DataTable _dtUsers { get; set; }
        #endregion


        #region Private Helper Methods
        private async Task<(bool IsUserDeleted, bool IsUserCencelOperation)> _deleteUserWillNotifyUserUsingMessageBox(int SelectedUserID)
        {
            if (DialogResult.ShowMessageBoxQuestionDial($"Are you sure you want to delete user with ID = {SelectedUserID} ?",
                    "Confirme Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return (IsUserDeleted: false, IsUserCencelOperation: true);

            bool isUserDeleted = await clsUser.DeleteAsync(UserID.CreateNew(SelectedUserID));

            if (isUserDeleted) DialogResult.ShowMessageBoxInfoDial($"User with ID = {SelectedUserID} is deleted.", "Deleting User");
            else DialogResult.ShowMessageBoxErrorDial("User was not deleted duo to data connected to it.", "Faild");

            return (IsUserDeleted: isUserDeleted, IsUserCencelOperation: false);
        }
        #endregion

        #region Private Helper Methods For UI
        private void _refreshDGV_DefaultViewWithLableNumberOfRecords()
        {
            string filterColumn = cmbFilterBy.Text.RemoveWhiteSpaces();
            string filterText = mtxtFilter.Text.RemoveWhiteSpaces();

            _resetDGVDefaultView(dataTable: _dtUsers, selectedFilterColumn: filterColumn, filterText: filterText);

            _refreshLableNumberOfRecords();
        }

        private void _resetDGVDefaultView(DataTable dataTable, string selectedFilterColumn, string filterText) =>
            _setDGVDefaultViewRowFilter(dataTable: dataTable, selectedFilterColumn: selectedFilterColumn,
                filterText: filterText, isNumberOrBoolean: selectedFilterColumn == "UserID" 
                    || selectedFilterColumn == "PersonID" || selectedFilterColumn == "IsActive");

        private void _setDGVDefaultViewRowFilter(DataTable dataTable, string selectedFilterColumn, string filterText, bool isNumberOrBoolean)
        {
            dataTable.DefaultView.RowFilter = filterText.IsNullOrEmptyOrWhiteSpace() || selectedFilterColumn.IsNullOrEmpty() || selectedFilterColumn == "None"
                                            ? string.Empty //Reset the filters in case nothing selected or filter value conains nothing.
                                            : isNumberOrBoolean //in this case we deal with integer not string.
                                            ? string.Format("[{0}] = {1}", selectedFilterColumn, filterText)
                                            : string.Format("[{0}] LIKE '{1}%'", selectedFilterColumn, filterText);
        }

        private void _refreshLableNumberOfRecords() => lblNumberOfRecords.Text = lblNumberOfRecords.Tag.ToString() + _dtUsers.DefaultView.Count;


        private string _getMaskBasedOnSelectedFilterColumn(string FillterByColumn)
        {
            switch (FillterByColumn)
            {
                case "User ID":
                case "Person ID": return "000000000";
                case "Full Name": return ">?|&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&";
                case "User Name": return ">?|LLLLLLLLLLLLLLLL";
                default: return string.Empty;
            }
        }


        private int[] _getSelectedRowsIndeces(DataGridView dataGridView)
        {
            int[] selectedRowsIndeces = new int[dataGridView.SelectedRows.Count];

            int count = selectedRowsIndeces.Length;
            const int invalidIndex = -1;
            while (--count > invalidIndex) selectedRowsIndeces[count] = dataGridView.SelectedRows[count].Index;

            return selectedRowsIndeces;
        }

        private async Task _refrashDGVItemsAsync()
        {
            dgvUsers.DataSource = _dtUsers = await clsUser.GetUsersAsync();
            _refreshDGV_DefaultViewWithLableNumberOfRecords();
        }

        private void _reselectRows(DataGridView dataGridView, int[] selectedRowsIndeces)
        {
            dataGridView.SelectedRows[0].Selected = false; // Remove the first row from selected rows.

            int count = selectedRowsIndeces.Length; const int invalidIndex = -1;
            while (--count > invalidIndex) dataGridView.Rows[selectedRowsIndeces[count]].Selected = true;
        }

        private async Task _refresh_dgvUsersWithReselectSelectedRows(DataGridView dataGridView)
        {
            int[] selectedRowsIndeces = _getSelectedRowsIndeces(dataGridView);

            await _refrashDGVItemsAsync();

            _reselectRows(dataGridView: dataGridView, selectedRowsIndeces: selectedRowsIndeces);
        }

        private (int SelectedUserID, bool IsValidUserID) _getUserIDFromUsersDGV(DataGridView dataGridViews)
        {
            const int userIDColumnIndex = 0, firstRowSelectedIndex = 0;
            int _firstRowSelectedIndex = dataGridViews.SelectedRows[firstRowSelectedIndex].Index;

            bool isUserIDValid = int.TryParse(dataGridViews[userIDColumnIndex, _firstRowSelectedIndex].Value.ToString(), out int selectedUserID);

            if (!isUserIDValid && selectedUserID < 1)
            {
                DialogResult.ShowMessageBoxErrorDial("User ID is not valid!", "An Error Occurded");
                return (SelectedUserID: -1, IsValidUserID: false);
            }
            return (SelectedUserID: selectedUserID, IsValidUserID: isUserIDValid);
        }

        private async void _loadEditUserForm()
        {
            (int selectedUserID, bool isValidUserID) = _getUserIDFromUsersDGV(dataGridViews: dgvUsers);
            
            if (!isValidUserID) return;

            clsUser user = await clsUser.FindAsync(UserID: UserID.CreateNew(selectedUserID));

            frmAddEditUser frmEditUser = frmAddEditUser.CreateAsEditingMode(user: user);
            frmEditUser.ShowDialog();

            if (frmEditUser.IsAnyChangedHappened) await _refresh_dgvUsersWithReselectSelectedRows(dataGridView: dgvUsers);
        }

        void _resetRowFilterToDefaultValue(DataTable dataTable)
        {
            if (!(dataTable is null)) dataTable.DefaultView.RowFilter = string.Empty;
        }
        #endregion


        #region Main UI Methods
        private async void frmManageUsers_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            dgvUsers.DataSource = _dtUsers = await clsUser.GetUsersAsync();

            try { 
                dgvUsers.Columns["FullName"].Width = 300;
                dgvUsers.Columns["UserName"].Width = 130;
            }
            catch { }

            _refreshLableNumberOfRecords();
        }


        private void frmManageUsers_FormClosed(object sender, FormClosedEventArgs e) => ((frmMain)ParentForm).IsAnyFormOpen = false;

        private void btnClose_Click(object sender, EventArgs e) { ((frmMain)ParentForm).IsAnyFormOpen = false; Close(); }

        private async void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frmAddNewUser = await frmAddEditUser.CreateAsAddingMode(personID: PersonID.Empty);
            frmAddNewUser.ShowDialog();
            if (frmAddNewUser.IsAnyChangedHappened) await _refrashDGVItemsAsync();
        }

        private async void tsmShowDetails_Click(object sender, EventArgs e)
        {
            (int selectedUserID, bool isValidUserID) = _getUserIDFromUsersDGV(dataGridViews: dgvUsers);

            if (!isValidUserID) return;

            UserID userID = UserID.CreateNew(selectedUserID);

            frmUserDetails frmUserDetails = await frmUserDetails.Create(UserID: userID);
            frmUserDetails.ShowDialog();
        }

        private void tsmAddNewUser_Click(object sender, EventArgs e) => btnAddNewUser_Click(sender, e);

        private void tsmEditUser_Click(object sender, EventArgs e) => _loadEditUserForm();

        private async void tsmDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count < 1) return;

            int selectedUserID = -1;
            byte numberOfDeltedUser = 0;
            bool isUserDeleted = false, isUserCancelOperation = false;

            List<int> SelectedRowsIndeces = new List<int>(dgvUsers.SelectedRows.Count);

            for (int i = 0; i < dgvUsers.SelectedRows.Count; i++)
                SelectedRowsIndeces.Add(dgvUsers.SelectedRows[i].Index);

            for (int i = 0; i < SelectedRowsIndeces.Count; i++)
            {
                if (int.TryParse(dgvUsers[0, SelectedRowsIndeces[i]].Value.ToString(), out selectedUserID))
                {
                    (isUserDeleted, isUserCancelOperation) = await _deleteUserWillNotifyUserUsingMessageBox(selectedUserID);
                    if (isUserDeleted) numberOfDeltedUser++;
                }

                if (!isUserCancelOperation && !isUserDeleted) break;
            }

            if (numberOfDeltedUser > 0) await _refrashDGVItemsAsync();
        }

        private async void tsmChangePassword_Click(object sender, EventArgs e)
        {
            (int selectedUserID, bool isValidUserID) = _getUserIDFromUsersDGV(dataGridViews: dgvUsers);

            if (!isValidUserID) return;

            clsUser user = await clsUser.FindAsync(UserID: UserID.CreateNew(selectedUserID));

            frmChangePassword frmChangePassword = frmChangePassword.Create(User: user);
            frmChangePassword.ShowDialog();

            if (frmChangePassword.IsAnyChangedHappened) await _refrashDGVItemsAsync();
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Send Email");
        }
        
        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            DialogResult.ShowMessageBoxWarningDial("Not implmented yet.", "Phone Call");
        }


        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.SelectedIndex == 0)
            {
                cmbIsActiveFilter.Visible = false;

                mtxtFilter.Visible = false;
                mtxtFilter.Text = string.Empty;
                mtxtFilter.Mask = string.Empty;

                _resetRowFilterToDefaultValue(dataTable: _dtUsers);
            }
            else if ((string)cmbFilterBy.SelectedItem == "Is Active")
            {
                mtxtFilter.Visible = false;

                cmbIsActiveFilter.Visible = true;
                cmbIsActiveFilter.SelectedIndex = 0;
            }
            else
            {
                _resetRowFilterToDefaultValue(dataTable: _dtUsers);
                cmbIsActiveFilter.Visible = false;

                mtxtFilter.Visible = true;
                mtxtFilter.Mask = _getMaskBasedOnSelectedFilterColumn(cmbFilterBy.SelectedItem.ToString());
            }

        }

        private void cmbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _resetDGVDefaultView(dataTable: _dtUsers, selectedFilterColumn: "IsActive", filterText: cmbIsActiveFilter.SelectedIndex == 0 ? null : cmbIsActiveFilter.SelectedIndex == 1 ? "True" : "False");
            _refreshLableNumberOfRecords();
        }


        private void mtxtFillter_TextChanged(object sender, EventArgs e) => _refreshDGV_DefaultViewWithLableNumberOfRecords();
        #endregion
    }
}
