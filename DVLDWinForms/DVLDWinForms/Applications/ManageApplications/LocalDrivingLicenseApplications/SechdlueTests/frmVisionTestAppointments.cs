using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Helper.Extensions;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications.SechduleTests;

namespace DVLDWinForms.Applications.ManageApplications.LocalDrivingLicenseApplications.SechdlueTests
{
    public sealed partial class frmVisionTestAppointments: Form
    {
        #region Constructors
        private frmVisionTestAppointments(clsLocalDrivingLicenseApplication localDLApplication)
        {
            InitializeComponent();
            _LDLApplicaion = localDLApplication;
        }

        public static frmVisionTestAppointments CreateNew(clsLocalDrivingLicenseApplication localDLApplication) => 
            new frmVisionTestAppointments(localDLApplication);

        public static async Task<frmVisionTestAppointments> CreateNew(LocalDrivingLicenseApplicationID localDLAppID)
        {
            clsLocalDrivingLicenseApplication lDLApp = await clsLocalDrivingLicenseApplication.FindAsync(localDLAppID);
            return new frmVisionTestAppointments(lDLApp);
        }
        #endregion

        #region Private Fields
        private DataTable _dtVisionTestAppointments;
        private clsLocalDrivingLicenseApplication _LDLApplicaion { get; set; }

        private static readonly ValueTuple<string, int>[] _columnsResizeWidthInfo = new (string, int)[] {
            ("Appointment ID", 130), ("Appointment Date", 180), ("Paid Fees", 130), ("Is Locked", 130)
        };

        private static readonly ValueTuple<string, ListSortDirection>[] _columnsSortingInfo = new(string, ListSortDirection)[] { 
            ("Appointment ID", ListSortDirection.Descending)
        };
        #endregion


        #region Private Helper UI Methods
        private void _update_lblNumberOfRecordsOfDataGridView(DataTable dataSource)
        {
            lblNumberOfRecords.Text = lblNumberOfRecords.Tag.ToString() +
                (dataSource is null || dataSource.Rows is null || dataSource.Rows.Count < 1 ? 0: dataSource.DefaultView.Count);
        }

        private async Task _loadAppointmentsVisionTestToDataGridViewAndDataTable(DataGridView dataGridView, DataTable dataTable)
        {
            dataTable = await clsSechduleTest.GetAllTestAppointmentsAsync(_LDLApplicaion.ID, clsSechduleTest.enTestAppointmentType.Vision);
            dataGridView.DataSource = dataTable;

            _update_lblNumberOfRecordsOfDataGridView(dataSource: dataTable);
        }

        private void _resizeWidthAndSortingDataGridViewCloumns(DataGridView dataGridView, 
            (string colName, int widthSize)[] columnsResizeWidthInfo, 
            (string colName, ListSortDirection listSortDirection)[] columnsSortingInfo)
        {
            if (dataGridView.RowCount > 0)
            {
                dataGridView.SortingColumns(columnsSortingInfo);
                dataGridView.ResizeCloumnsWight(columnsResizeWidthInfo);
            }
        }
        #endregion


        #region Main UI Methods
        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            this.ucLocalDLAppInfoCard1.LoadLDLApplicationInfo(_LDLApplicaion).SafeFireAndForget();

            _loadAppointmentsVisionTestToDataGridViewAndDataTable(dataGridView: dgvVisionTestAppointments, dataTable: _dtVisionTestAppointments).SafeFireAndForget();
            
            _resizeWidthAndSortingDataGridViewCloumns(dataGridView: dgvVisionTestAppointments, _columnsResizeWidthInfo, _columnsSortingInfo);
        }

        private void btnAddNewVisionTest_Click(object sender, EventArgs e)
        {
            if (dgvVisionTestAppointments.RowCount > 0)
            {
                bool isValidIsLockedValue = bool.TryParse(value: 
                    dgvVisionTestAppointments.Rows[0].Cells["Is Locked"].Value.ToString(), out bool result);

                if (!isValidIsLockedValue && result == true)
                    DialogResult.ShowMessageBoxErrorDial("Person already have an active appointment for this test, you cannot add new appointment.", "Not Allowed");
            }

            frmSechduleVisionTest frmSechduleVisionTest = new frmSechduleVisionTest(_LDLApplicaion);
            frmSechduleVisionTest.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
        #endregion
    }
}
