namespace DVLDWinForms.People
{
    partial class frmManagePeople
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
            this.dgvPeople = new System.Windows.Forms.DataGridView();
            this.ctxtmsDGVManagePeople = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeletePerson = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.mtxtFilter = new System.Windows.Forms.MaskedTextBox();
            this.lblTitel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.ctxtmsDGVManagePeople.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPeople
            // 
            this.dgvPeople.AllowUserToAddRows = false;
            this.dgvPeople.AllowUserToDeleteRows = false;
            this.dgvPeople.AllowUserToResizeRows = false;
            this.dgvPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPeople.BackgroundColor = System.Drawing.Color.White;
            this.dgvPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeople.ContextMenuStrip = this.ctxtmsDGVManagePeople;
            this.dgvPeople.Location = new System.Drawing.Point(17, 297);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.ReadOnly = true;
            this.dgvPeople.RowHeadersWidth = 51;
            this.dgvPeople.RowTemplate.Height = 24;
            this.dgvPeople.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPeople.Size = new System.Drawing.Size(1458, 499);
            this.dgvPeople.TabIndex = 2;
            // 
            // ctxtmsDGVManagePeople
            // 
            this.ctxtmsDGVManagePeople.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctxtmsDGVManagePeople.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxtmsDGVManagePeople.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowDetails,
            this.toolStripMenuItem1,
            this.tsmAddNewPerson,
            this.tsmEditPerson,
            this.tsmDeletePerson,
            this.toolStripMenuItem2,
            this.tsmSendEmail,
            this.tsmPhoneCall});
            this.ctxtmsDGVManagePeople.Name = "ctxtmsDGVManagePeople";
            this.ctxtmsDGVManagePeople.Size = new System.Drawing.Size(278, 184);
            // 
            // tsmShowDetails
            // 
            this.tsmShowDetails.Name = "tsmShowDetails";
            this.tsmShowDetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmShowDetails.Size = new System.Drawing.Size(277, 28);
            this.tsmShowDetails.Text = "Show Details";
            this.tsmShowDetails.Click += new System.EventHandler(this.tsmShowDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(274, 6);
            // 
            // tsmAddNewPerson
            // 
            this.tsmAddNewPerson.Name = "tsmAddNewPerson";
            this.tsmAddNewPerson.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmAddNewPerson.Size = new System.Drawing.Size(277, 28);
            this.tsmAddNewPerson.Text = "Add New Person";
            this.tsmAddNewPerson.Click += new System.EventHandler(this.tsmAddNewPerson_Click);
            // 
            // tsmEditPerson
            // 
            this.tsmEditPerson.Name = "tsmEditPerson";
            this.tsmEditPerson.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmEditPerson.Size = new System.Drawing.Size(277, 28);
            this.tsmEditPerson.Text = "Edit Person";
            this.tsmEditPerson.Click += new System.EventHandler(this.tsmEditPerson_Click);
            // 
            // tsmDeletePerson
            // 
            this.tsmDeletePerson.Name = "tsmDeletePerson";
            this.tsmDeletePerson.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.tsmDeletePerson.Size = new System.Drawing.Size(277, 28);
            this.tsmDeletePerson.Text = "Delete Person";
            this.tsmDeletePerson.Click += new System.EventHandler(this.tsmDeletePerson_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(274, 6);
            // 
            // tsmSendEmail
            // 
            this.tsmSendEmail.Name = "tsmSendEmail";
            this.tsmSendEmail.Size = new System.Drawing.Size(277, 28);
            this.tsmSendEmail.Text = "Send Email";
            this.tsmSendEmail.Click += new System.EventHandler(this.tsmSendEmail_Click);
            // 
            // tsmPhoneCall
            // 
            this.tsmPhoneCall.Name = "tsmPhoneCall";
            this.tsmPhoneCall.Size = new System.Drawing.Size(277, 28);
            this.tsmPhoneCall.Text = "Phone Call";
            this.tsmPhoneCall.Click += new System.EventHandler(this.tsmPhoneCall_Click);
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfRecords.Location = new System.Drawing.Point(15, 821);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(122, 25);
            this.lblNumberOfRecords.TabIndex = 0;
            this.lblNumberOfRecords.Tag = "# Records: ";
            this.lblNumberOfRecords.Text = "# Records: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
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
            "Person ID",
            "National No",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gender",
            "Phone",
            "Email"});
            this.cmbFilterBy.Location = new System.Drawing.Point(122, 237);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(210, 33);
            this.cmbFilterBy.TabIndex = 0;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddPerson.Image = global::DVLDWinForms.Properties.Resources.ambassador_30x30_75A2D8;
            this.btnAddPerson.Location = new System.Drawing.Point(1355, 225);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(120, 60);
            this.btnAddPerson.TabIndex = 3;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1310, 806);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(165, 54);
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
            this.mtxtFilter.Location = new System.Drawing.Point(349, 237);
            this.mtxtFilter.Name = "mtxtFilter";
            this.mtxtFilter.Size = new System.Drawing.Size(312, 30);
            this.mtxtFilter.TabIndex = 1;
            this.mtxtFilter.TextChanged += new System.EventHandler(this.mtxtFillter_TextChanged);
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitel.ForeColor = System.Drawing.Color.Red;
            this.lblTitel.Location = new System.Drawing.Point(653, 174);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(312, 46);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Manage People";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDWinForms.Properties.Resources.users___People;
            this.pictureBox1.Location = new System.Drawing.Point(672, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // frmManagePeople
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1498, 884);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.mtxtFilter);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.cmbFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvPeople);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmManagePeople";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage People";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManagePeople_FormClosed);
            this.Load += new System.EventHandler(this.frmManagePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.ctxtmsDGVManagePeople.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPeople;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.ContextMenuStrip ctxtmsDGVManagePeople;
        private System.Windows.Forms.ToolStripMenuItem tsmShowDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmDeletePerson;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmEditPerson;
        private System.Windows.Forms.ToolStripMenuItem tsmAddNewPerson;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmSendEmail;
        private System.Windows.Forms.ToolStripMenuItem tsmPhoneCall;
        private System.Windows.Forms.MaskedTextBox mtxtFilter;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}