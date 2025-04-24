namespace DVLDWinForms.Applications.ManageApplications.LocalDrivingLicenseApplications.SechdlueTests
{
    partial class frmVisionTestAppointments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>7
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewVisionTest = new System.Windows.Forms.Button();
            this.dgvVisionTestAppointments = new System.Windows.Forms.DataGridView();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ucLocalDLAppInfoCard1 = new DVLDWinForms.Applications.UserControls.ucLocalDLAppInfoCard();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisionTestAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Image = global::DVLDWinForms.Properties.Resources.account_settings_72x72;
            this.pictureBox1.Location = new System.Drawing.Point(436, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitel
            // 
            this.lblTitel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lblTitel.ForeColor = System.Drawing.Color.Red;
            this.lblTitel.Location = new System.Drawing.Point(334, 131);
            this.lblTitel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(366, 37);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Vision Test Appointment";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 540);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Appointments:";
            // 
            // btnAddNewVisionTest
            // 
            this.btnAddNewVisionTest.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddNewVisionTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewVisionTest.Image = global::DVLDWinForms.Properties.Resources.ambassador_30x30_75A2D8;
            this.btnAddNewVisionTest.Location = new System.Drawing.Point(926, 531);
            this.btnAddNewVisionTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddNewVisionTest.Name = "btnAddNewVisionTest";
            this.btnAddNewVisionTest.Size = new System.Drawing.Size(62, 45);
            this.btnAddNewVisionTest.TabIndex = 1;
            this.btnAddNewVisionTest.UseVisualStyleBackColor = true;
            this.btnAddNewVisionTest.Click += new System.EventHandler(this.btnAddNewVisionTest_Click);
            // 
            // dgvVisionTestAppointments
            // 
            this.dgvVisionTestAppointments.AllowUserToAddRows = false;
            this.dgvVisionTestAppointments.AllowUserToDeleteRows = false;
            this.dgvVisionTestAppointments.AllowUserToResizeRows = false;
            this.dgvVisionTestAppointments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVisionTestAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvVisionTestAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisionTestAppointments.Location = new System.Drawing.Point(14, 583);
            this.dgvVisionTestAppointments.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVisionTestAppointments.Name = "dgvVisionTestAppointments";
            this.dgvVisionTestAppointments.ReadOnly = true;
            this.dgvVisionTestAppointments.RowHeadersWidth = 51;
            this.dgvVisionTestAppointments.RowTemplate.Height = 24;
            this.dgvVisionTestAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisionTestAppointments.Size = new System.Drawing.Size(974, 219);
            this.dgvVisionTestAppointments.TabIndex = 4;
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(10, 825);
            this.lblNumberOfRecords.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(101, 20);
            this.lblNumberOfRecords.TabIndex = 0;
            this.lblNumberOfRecords.Tag = "# Records: ";
            this.lblNumberOfRecords.Text = "# Records: ";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(866, 811);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 47);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucLocalDLAppInfoCard1
            // 
            this.ucLocalDLAppInfoCard1.LDLApplication = null;
            this.ucLocalDLAppInfoCard1.Location = new System.Drawing.Point(28, 169);
            this.ucLocalDLAppInfoCard1.Margin = new System.Windows.Forms.Padding(2);
            this.ucLocalDLAppInfoCard1.Name = "ucLocalDLAppInfoCard1";
            this.ucLocalDLAppInfoCard1.Size = new System.Drawing.Size(946, 362);
            this.ucLocalDLAppInfoCard1.TabIndex = 5;
            // 
            // frmSechduleVisionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(997, 862);
            this.Controls.Add(this.ucLocalDLAppInfoCard1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.dgvVisionTestAppointments);
            this.Controls.Add(this.btnAddNewVisionTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(1013, 901);
            this.MinimumSize = new System.Drawing.Size(1013, 901);
            this.Name = "frmSechduleVisionTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sechdule Vision Test";
            this.Load += new System.EventHandler(this.frmVisionTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisionTestAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewVisionTest;
        private System.Windows.Forms.DataGridView dgvVisionTestAppointments;
        private System.Windows.Forms.Button btnClose;
        private UserControls.ucLocalDLAppInfoCard ucLocalDLAppInfoCard1;
    }
}