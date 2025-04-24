namespace DVLDWinForms.Applications.ManageApplications
{
    partial class frmLocalDrivingLicenseApplications
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvLDLApplications = new System.Windows.Forms.DataGridView();
            this.ctxtmsDGVLocalDLApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmEditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSechduleTests = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSechduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSechduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSechduleStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmIssueDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.btnAddLDLApplications = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.mtxtFilter = new System.Windows.Forms.MaskedTextBox();
            this.lblTitel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).BeginInit();
            this.ctxtmsDGVLocalDLApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLDLApplications
            // 
            this.dgvLDLApplications.AllowUserToAddRows = false;
            this.dgvLDLApplications.AllowUserToDeleteRows = false;
            this.dgvLDLApplications.AllowUserToResizeRows = false;
            this.dgvLDLApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLDLApplications.BackgroundColor = System.Drawing.Color.White;
            this.dgvLDLApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLDLApplications.ContextMenuStrip = this.ctxtmsDGVLocalDLApplications;
            this.dgvLDLApplications.Location = new System.Drawing.Point(17, 289);
            this.dgvLDLApplications.Name = "dgvLDLApplications";
            this.dgvLDLApplications.ReadOnly = true;
            this.dgvLDLApplications.RowHeadersWidth = 51;
            this.dgvLDLApplications.RowTemplate.Height = 24;
            this.dgvLDLApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLDLApplications.Size = new System.Drawing.Size(1458, 513);
            this.dgvLDLApplications.TabIndex = 2;
            this.dgvLDLApplications.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvLDLApplications_CellContextMenuStripNeeded);
            // 
            // ctxtmsDGVLocalDLApplications
            // 
            this.ctxtmsDGVLocalDLApplications.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ctxtmsDGVLocalDLApplications.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxtmsDGVLocalDLApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowDetails,
            this.toolStripMenuItem1,
            this.tsmEditApplication,
            this.tsmDeleteApplication,
            this.tsmCancelApplication,
            this.toolStripMenuItem2,
            this.tsmSechduleTests,
            this.toolStripSeparator2,
            this.tsmIssueDrivingLicense,
            this.toolStripSeparator1,
            this.tsmShowLicense,
            this.toolStripMenuItem3,
            this.tsmShowPersonLicenseHistory});
            this.ctxtmsDGVLocalDLApplications.Name = "ctxtmsDGVManagePeople";
            this.ctxtmsDGVLocalDLApplications.Size = new System.Drawing.Size(277, 226);
            // 
            // tsmShowDetails
            // 
            this.tsmShowDetails.Name = "tsmShowDetails";
            this.tsmShowDetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmShowDetails.Size = new System.Drawing.Size(276, 24);
            this.tsmShowDetails.Text = "&Show Details";
            this.tsmShowDetails.Click += new System.EventHandler(this.tsmShowDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(273, 6);
            // 
            // tsmEditApplication
            // 
            this.tsmEditApplication.Name = "tsmEditApplication";
            this.tsmEditApplication.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmEditApplication.Size = new System.Drawing.Size(276, 24);
            this.tsmEditApplication.Text = "&Edit Application";
            this.tsmEditApplication.Click += new System.EventHandler(this.tsmEditApplication_Click);
            // 
            // tsmDeleteApplication
            // 
            this.tsmDeleteApplication.Name = "tsmDeleteApplication";
            this.tsmDeleteApplication.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.tsmDeleteApplication.Size = new System.Drawing.Size(276, 24);
            this.tsmDeleteApplication.Text = "&Delete Application";
            this.tsmDeleteApplication.Click += new System.EventHandler(this.tsmDeleteLDLApplications_Click);
            // 
            // tsmCancelApplication
            // 
            this.tsmCancelApplication.Name = "tsmCancelApplication";
            this.tsmCancelApplication.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmCancelApplication.Size = new System.Drawing.Size(276, 24);
            this.tsmCancelApplication.Text = "&Cancel Application";
            this.tsmCancelApplication.Click += new System.EventHandler(this.tsmCancelApplication_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(273, 6);
            // 
            // tsmSechduleTests
            // 
            this.tsmSechduleTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSechduleVisionTest,
            this.tsmSechduleWrittenTest,
            this.tsmSechduleStreetTest});
            this.tsmSechduleTests.Name = "tsmSechduleTests";
            this.tsmSechduleTests.Size = new System.Drawing.Size(276, 24);
            this.tsmSechduleTests.Text = "Sechdule &Tests";
            // 
            // tsmSechduleVisionTest
            // 
            this.tsmSechduleVisionTest.Name = "tsmSechduleVisionTest";
            this.tsmSechduleVisionTest.Size = new System.Drawing.Size(210, 24);
            this.tsmSechduleVisionTest.Text = "Sechdule &Vision Test";
            this.tsmSechduleVisionTest.Click += new System.EventHandler(this.tsmSechduleVisionTest_Click);
            // 
            // tsmSechduleWrittenTest
            // 
            this.tsmSechduleWrittenTest.Enabled = false;
            this.tsmSechduleWrittenTest.Name = "tsmSechduleWrittenTest";
            this.tsmSechduleWrittenTest.Size = new System.Drawing.Size(210, 24);
            this.tsmSechduleWrittenTest.Text = "Sechdule &Written Test";
            this.tsmSechduleWrittenTest.Click += new System.EventHandler(this.tsmSechduleWrittenTest_Click);
            // 
            // tsmSechduleStreetTest
            // 
            this.tsmSechduleStreetTest.Enabled = false;
            this.tsmSechduleStreetTest.Name = "tsmSechduleStreetTest";
            this.tsmSechduleStreetTest.Size = new System.Drawing.Size(210, 24);
            this.tsmSechduleStreetTest.Text = "Sechdule &Street Test";
            this.tsmSechduleStreetTest.Click += new System.EventHandler(this.tsmSechduleStreetTest_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(273, 6);
            // 
            // tsmIssueDrivingLicense
            // 
            this.tsmIssueDrivingLicense.Enabled = false;
            this.tsmIssueDrivingLicense.Name = "tsmIssueDrivingLicense";
            this.tsmIssueDrivingLicense.Size = new System.Drawing.Size(276, 24);
            this.tsmIssueDrivingLicense.Text = "&Issue Driving License (First Time)";
            this.tsmIssueDrivingLicense.Click += new System.EventHandler(this.tsmIssueDrivingLicense_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(273, 6);
            // 
            // tsmShowLicense
            // 
            this.tsmShowLicense.Enabled = false;
            this.tsmShowLicense.Name = "tsmShowLicense";
            this.tsmShowLicense.Size = new System.Drawing.Size(276, 24);
            this.tsmShowLicense.Text = "Show &License";
            this.tsmShowLicense.Click += new System.EventHandler(this.tsmShowLicense_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(273, 6);
            // 
            // tsmShowPersonLicenseHistory
            // 
            this.tsmShowPersonLicenseHistory.Name = "tsmShowPersonLicenseHistory";
            this.tsmShowPersonLicenseHistory.Size = new System.Drawing.Size(276, 24);
            this.tsmShowPersonLicenseHistory.Text = "Show Person License &History";
            this.tsmShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmShowPersonLicenseHistory_Click);
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfRecords.Location = new System.Drawing.Point(8, 833);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(101, 20);
            this.lblNumberOfRecords.TabIndex = 0;
            this.lblNumberOfRecords.Tag = "# Records: ";
            this.lblNumberOfRecords.Text = "# Records: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By:";
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No.",
            "Full Name",
            "Status"});
            this.cmbFilterBy.Location = new System.Drawing.Point(124, 224);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(210, 28);
            this.cmbFilterBy.TabIndex = 0;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // btnAddLDLApplications
            // 
            this.btnAddLDLApplications.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddLDLApplications.Image = global::DVLDWinForms.Properties.Resources.ambassador_30x30_75A2D8;
            this.btnAddLDLApplications.Location = new System.Drawing.Point(1368, 213);
            this.btnAddLDLApplications.Name = "btnAddLDLApplications";
            this.btnAddLDLApplications.Size = new System.Drawing.Size(107, 58);
            this.btnAddLDLApplications.TabIndex = 3;
            this.btnAddLDLApplications.UseVisualStyleBackColor = true;
            this.btnAddLDLApplications.Click += new System.EventHandler(this.btnAddLDLApplication_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1289, 817);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(186, 56);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // mtxtFilter
            // 
            this.mtxtFilter.AsciiOnly = true;
            this.mtxtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.mtxtFilter.HidePromptOnLeave = true;
            this.mtxtFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mtxtFilter.Location = new System.Drawing.Point(351, 224);
            this.mtxtFilter.Name = "mtxtFilter";
            this.mtxtFilter.Size = new System.Drawing.Size(260, 26);
            this.mtxtFilter.TabIndex = 1;
            this.mtxtFilter.TextChanged += new System.EventHandler(this.mtxtFillter_TextChanged);
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTitel.ForeColor = System.Drawing.Color.Red;
            this.lblTitel.Location = new System.Drawing.Point(532, 165);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(426, 31);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Local Driving License Applications";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDWinForms.Properties.Resources.users___People;
            this.pictureBox1.Location = new System.Drawing.Point(653, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // frmLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1492, 884);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.mtxtFilter);
            this.Controls.Add(this.btnAddLDLApplications);
            this.Controls.Add(this.cmbFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvLDLApplications);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDrivingLicenseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage People";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLocalDrivingLicenseApplications_FormClosed);
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).EndInit();
            this.ctxtmsDGVLocalDLApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLDLApplications;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Button btnAddLDLApplications;
        private System.Windows.Forms.ContextMenuStrip ctxtmsDGVLocalDLApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmShowDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmEditApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmCancelApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmSechduleTests;
        private System.Windows.Forms.ToolStripMenuItem tsmIssueDrivingLicense;
        private System.Windows.Forms.MaskedTextBox mtxtFilter;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem tsmSechduleVisionTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmSechduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem tsmSechduleStreetTest;
    }
}