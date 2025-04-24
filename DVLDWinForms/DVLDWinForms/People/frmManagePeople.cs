using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Collections.Generic;
using HelperClasses.Extensions;
using DVLDWinForms.Users;
using DVLD_BLL.People;

namespace DVLDWinForms.People
{
    public sealed partial class frmManagePeople : Form
    {
        #region Constructors
        public frmManagePeople() => InitializeComponent();
        #endregion


        #region Private Fields
        DataTable _dtPeople { get; set; }
        #endregion


        #region Private Helper Methods
        private async Task<(bool IsPersonDeleted, bool IsUserCencelOperation)> _deletePersonWillNotifyUserUsingMessageBox(int SelectedPersonID)
        {
            if (DialogResult.ShowMessageBoxQuestionDial($"Are you sure you want to delete person with ID = {SelectedPersonID} ?", "Confirme Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return (IsPersonDeleted: false, IsUserCencelOperation: true);

            PersonID selectedPersonID = PersonID.CreateNew(SelectedPersonID);

            string personImagePath = await clsPerson.GetImagePathAsync(id: selectedPersonID);

            bool isPersonDeleted = await clsPerson.DeleteAsync(id: selectedPersonID), isImageDeleted;
            if (isImageDeleted = !(personImagePath.NotIsNullOrEmptyOrWhiteSpace() && File.Exists(personImagePath)))
            {
                try { File.Delete(personImagePath); } catch { }
                isImageDeleted &= !File.Exists(personImagePath);
            }

            if (isPersonDeleted && isImageDeleted)
                DialogResult.ShowMessageBoxInfoDial($"Person with ID = {SelectedPersonID} is deleted.", "Deleting Person");
            else if (!isPersonDeleted && !isImageDeleted)
                DialogResult.ShowMessageBoxErrorDial($"Person with ID = {SelectedPersonID} is deleted and it image is not deleted.", "Deleting Person");
            else DialogResult.ShowMessageBoxErrorDial("Person was not deleted because it has data linked it.", "Error");

            return (IsPersonDeleted: isPersonDeleted, IsUserCencelOperation: false);
        }
        #endregion


        #region Private Helper Methods For UI
        private void _refreshDGV_DefaultViewWithLableNumberOfRecords()
        {
            string filterColumn = cmbFilterBy.Text.RemoveWhiteSpaces();
            string filterText = mtxtFilter.Text.RemoveWhiteSpaces();

            _setDGVDefaultViewRowFilter(dataTable: _dtPeople, selectedFilterColumn: filterColumn,
                filterText: filterText, isNumber: filterColumn == "PersonID");

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

        private void _refreshLableNumberOfRecords() => lblNumberOfRecords.Text = lblNumberOfRecords.Tag.ToString() + _dtPeople.DefaultView.Count;


        private string _getMaskBasedOnSelectedFilterColumn(string FillterByColumn)
        {
            switch (FillterByColumn)
            {
                case "Person ID": return "000000000";

                case "National No": return "N000000000";

                case "First Name":
                case "Second Name":
                case "Third Name":
                case "Nationality":
                    return ">?|LLLLLLLLLLLLLLLL";

                case "Last Name": return ">?|&&&&&&&&&&&&&&&&";

                case "Email": return "&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&";

                case "Gender": return ">?";

                case "Phone": return "0000 000 0000 000 0000";

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
            dgvPeople.DataSource = _dtPeople = await clsPerson.GetPeopleAsync();

            _refreshDGV_DefaultViewWithLableNumberOfRecords();
        }

        private void _reselectRows(DataGridView dataGridView, int[] selectedRowsIndeces)
        {
            dataGridView.SelectedRows[0].Selected = false; // Remove the first row from selected rows.

            int count = selectedRowsIndeces.Length, invalidIndex = -1;
            while (--count > invalidIndex) dataGridView.Rows[selectedRowsIndeces[count]].Selected = true;
        }

        private async Task _refresh_dgvPeopleWithReselectSelectedRows(DataGridView dataGridView)
        {
            int[] selectedRowsIndeces = _getSelectedRowsIndeces(dataGridView);

            await _refrashDGVItemsAsync();

            _reselectRows(dataGridView: dataGridView, selectedRowsIndeces: selectedRowsIndeces);
        }


        private async void _loadEditPersonForm()
        {
            bool isPersonIDValid = int.TryParse(dgvPeople[0, dgvPeople.SelectedRows[0].Index].Value.ToString(), out int _selectedPersonID);

            if (!isPersonIDValid && _selectedPersonID < 1)
            {
                DialogResult.ShowMessageBoxErrorDial("Person ID is not valid!", "An Error Occurded");
                return;
            }

            PersonID selectedPersonID = PersonID.CreateNew(_selectedPersonID);

            frmAddEditPerson frmEditPerson = frmAddEditPerson.CreateNew(PersonID: selectedPersonID);

            frmEditPerson.ShowDialog();

            if (frmEditPerson.IsAnyChangedHappened) await _refresh_dgvPeopleWithReselectSelectedRows(dataGridView: dgvPeople);
        }
        
        private void _setPeopleDGVColumnsSize()
        {
            try
            {
                const int widthOfEmailColumn = 205, widthOfNationalityColumn = 130;
                const int widthOfDateColumn = 130, widthOfNameColumn = 110;
                const int widthOfIdColumn = 95, widthOfNationalNoColumn = 95, widthOfPhoneColumn = 95;

                dgvPeople.Columns["PersonID"]   .Width = widthOfIdColumn;
                dgvPeople.Columns["NationalNo"] .Width = widthOfNationalNoColumn;
                dgvPeople.Columns["FirstName"]  .Width = widthOfNameColumn;
                dgvPeople.Columns["SecondName"] .Width = widthOfNameColumn;
                dgvPeople.Columns["ThirdName"]  .Width = widthOfNameColumn;
                dgvPeople.Columns["LastName"]   .Width = widthOfNameColumn;
                dgvPeople.Columns["DateOfBirth"].Width = widthOfDateColumn;
                dgvPeople.Columns["Phone"]      .Width = widthOfPhoneColumn;
                dgvPeople.Columns["Email"]      .Width = widthOfEmailColumn;
                dgvPeople.Columns["Nationality"].Width = widthOfNationalityColumn;
            }
            catch { }
        }
        #endregion


        #region Main UI Methods
        private async void frmManagePeople_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            dgvPeople.DataSource = _dtPeople = await clsPerson.GetPeopleAsync();

            _setPeopleDGVColumnsSize();
            _refreshLableNumberOfRecords();
        }


        private void frmManagePeople_FormClosed(object sender, FormClosedEventArgs e) => ((frmMain)ParentForm).IsAnyFormOpen = false;

        private void btnClose_Click(object sender, EventArgs e) { ((frmMain)ParentForm).IsAnyFormOpen = false; Close(); }

        private async void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddPerson = frmAddEditPerson.CreateNew(PersonID: PersonID.Empty);
            frmAddPerson.ShowDialog();

            if (frmAddPerson.IsAnyChangedHappened) await _refrashDGVItemsAsync();
        }


        private async void tsmShowDetails_Click(object sender, EventArgs e)
        {
            const int firstRowIndex = 0, indexOfPersonIDColumn = 0;
            int selectedRowIndex = dgvPeople.SelectedRows[firstRowIndex].Index;
            if (!int.TryParse(dgvPeople[indexOfPersonIDColumn, selectedRowIndex].Value.ToString(), out int _personID)) return;

            PersonID personID = PersonID.CreateNew(_personID);
            frmPersonDetails personDetails = await frmPersonDetails.CreateNewAsync(PersonID: personID);

            personDetails.ShowDialog();
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e) => btnAddPerson_Click(sender, e);

        private void tsmEditPerson_Click(object sender, EventArgs e) => _loadEditPersonForm();

        private async void tsmDeletePerson_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count < 1) return;

            int selectedPersonID = -1;
            short numberOfDeltedPeople = 0;
            bool isPersonDeleted = false, isUserCancelOperation = false;

            int[] selectedRowsIndeces = new int[dgvPeople.SelectedRows.Count];

            for (int i = 0; i < dgvPeople.SelectedRows.Count; i++)
                selectedRowsIndeces[i] = dgvPeople.SelectedRows[i].Index;

            for (int i = 0; i < selectedRowsIndeces.Length; i++)
            {
                if (int.TryParse(dgvPeople[0, selectedRowsIndeces[i]].Value.ToString(), out selectedPersonID)) 
                {
                    (isPersonDeleted, isUserCancelOperation) = await _deletePersonWillNotifyUserUsingMessageBox(selectedPersonID);
                    if (isPersonDeleted) numberOfDeltedPeople++;
                }

                if (!isUserCancelOperation && !isPersonDeleted) break;
            }

            if (numberOfDeltedPeople > 0) await _refrashDGVItemsAsync();
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
                mtxtFilter.Visible = false;
                mtxtFilter.Text = string.Empty;
                mtxtFilter.Mask = string.Empty;
                return;
            }

            mtxtFilter.Visible = true;
            mtxtFilter.Mask = _getMaskBasedOnSelectedFilterColumn(cmbFilterBy.SelectedItem.ToString());
        }

        private void mtxtFillter_TextChanged(object sender, EventArgs e) => _refreshDGV_DefaultViewWithLableNumberOfRecords();
        #endregion
    };

}