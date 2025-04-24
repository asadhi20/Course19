namespace DVLDWinForms.Users
{
    partial class frmManageUsers
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mtxtFilter = new System.Windows.Forms.MaskedTextBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tsmPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmDeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddNewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxtmsDGVManageUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTitel = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.cmbIsActiveFilter = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ctxtmsDGVManageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDWinForms.Properties.Resources.activate_profile_config;
            this.pictureBox1.Location = new System.Drawing.Point(584, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // mtxtFilter
            // 
            this.mtxtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtFilter.Location = new System.Drawing.Point(379, 251);
            this.mtxtFilter.Name = "mtxtFilter";
            this.mtxtFilter.Size = new System.Drawing.Size(270, 30);
            this.mtxtFilter.TabIndex = 1;
            this.mtxtFilter.TextChanged += new System.EventHandler(this.mtxtFillter_TextChanged);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddUser.Image = global::DVLDWinForms.Properties.Resources.activate_profile_config;
            this.btnAddUser.Location = new System.Drawing.Point(1256, 211);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 100);
            this.btnAddUser.TabIndex = 3;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "None",
            "User ID",
            "User Name",
            "Person ID",
            "Full Name",
            "Is Active"});
            this.cmbFilterBy.Location = new System.Drawing.Point(152, 251);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(210, 33);
            this.cmbFilterBy.TabIndex = 0;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(23, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By:";
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfRecords.Location = new System.Drawing.Point(23, 823);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(153, 31);
            this.lblNumberOfRecords.TabIndex = 0;
            this.lblNumberOfRecords.Tag = "# Records: ";
            this.lblNumberOfRecords.Text = "# Records: ";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1199, 808);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(157, 50);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tsmPhoneCall
            // 
            this.tsmPhoneCall.Name = "tsmPhoneCall";
            this.tsmPhoneCall.Size = new System.Drawing.Size(227, 24);
            this.tsmPhoneCall.Text = "&Phone Call";
            this.tsmPhoneCall.Click += new System.EventHandler(this.tsmPhoneCall_Click);
            // 
            // tsmSendEmail
            // 
            this.tsmSendEmail.Name = "tsmSendEmail";
            this.tsmSendEmail.Size = new System.Drawing.Size(227, 24);
            this.tsmSendEmail.Text = "Send &Email";
            this.tsmSendEmail.Click += new System.EventHandler(this.tsmSendEmail_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(224, 6);
            // 
            // tsmDeleteUser
            // 
            this.tsmDeleteUser.Name = "tsmDeleteUser";
            this.tsmDeleteUser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.tsmDeleteUser.Size = new System.Drawing.Size(227, 24);
            this.tsmDeleteUser.Text = "&Delete User";
            this.tsmDeleteUser.Click += new System.EventHandler(this.tsmDeleteUser_Click);
            // 
            // tsmEditUser
            // 
            this.tsmEditUser.Name = "tsmEditUser";
            this.tsmEditUser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmEditUser.Size = new System.Drawing.Size(227, 24);
            this.tsmEditUser.Text = "&Edit User";
            this.tsmEditUser.Click += new System.EventHandler(this.tsmEditUser_Click);
            // 
            // tsmAddNewUser
            // 
            this.tsmAddNewUser.Name = "tsmAddNewUser";
            this.tsmAddNewUser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmAddNewUser.Size = new System.Drawing.Size(227, 24);
            this.tsmAddNewUser.Text = "Add &New User";
            this.tsmAddNewUser.Click += new System.EventHandler(this.tsmAddNewUser_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 6);
            // 
            // tsmShowDetails
            // 
            this.tsmShowDetails.Name = "tsmShowDetails";
            this.tsmShowDetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmShowDetails.Size = new System.Drawing.Size(227, 24);
            this.tsmShowDetails.Text = "&Show Details";
            this.tsmShowDetails.Click += new System.EventHandler(this.tsmShowDetails_Click);
            // 
            // ctxtmsDGVManageUsers
            // 
            this.ctxtmsDGVManageUsers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxtmsDGVManageUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowDetails,
            this.toolStripMenuItem1,
            this.tsmAddNewUser,
            this.tsmEditUser,
            this.tsmDeleteUser,
            this.tsmChangePassword,
            this.toolStripMenuItem2,
            this.tsmSendEmail,
            this.tsmPhoneCall});
            this.ctxtmsDGVManageUsers.Name = "ctxtmsDGVManagePeople";
            this.ctxtmsDGVManageUsers.Size = new System.Drawing.Size(228, 184);
            // 
            // tsmChangePassword
            // 
            this.tsmChangePassword.Name = "tsmChangePassword";
            this.tsmChangePassword.Size = new System.Drawing.Size(227, 24);
            this.tsmChangePassword.Text = "&Change Password";
            this.tsmChangePassword.Click += new System.EventHandler(this.tsmChangePassword_Click);
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.ForeColor = System.Drawing.Color.Red;
            this.lblTitel.Location = new System.Drawing.Point(535, 165);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(290, 46);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "Manage Users";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.ContextMenuStrip = this.ctxtmsDGVManageUsers;
            this.dgvUsers.Location = new System.Drawing.Point(24, 326);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(1332, 465);
            this.dgvUsers.TabIndex = 2;
            // 
            // cmbIsActiveFilter
            // 
            this.cmbIsActiveFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsActiveFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIsActiveFilter.FormattingEnabled = true;
            this.cmbIsActiveFilter.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cmbIsActiveFilter.Location = new System.Drawing.Point(379, 251);
            this.cmbIsActiveFilter.Name = "cmbIsActiveFilter";
            this.cmbIsActiveFilter.Size = new System.Drawing.Size(150, 33);
            this.cmbIsActiveFilter.TabIndex = 1;
            this.cmbIsActiveFilter.SelectedIndexChanged += new System.EventHandler(this.cmbIsActiveFilter_SelectedIndexChanged);
            // 
            // frmManageUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1380, 870);
            this.Controls.Add(this.cmbIsActiveFilter);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mtxtFilter);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.cmbFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.dgvUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmManageUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Users";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManageUsers_FormClosed);
            this.Load += new System.EventHandler(this.frmManageUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ctxtmsDGVManageUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MaskedTextBox mtxtFilter;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem tsmPhoneCall;
        private System.Windows.Forms.ToolStripMenuItem tsmSendEmail;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteUser;
        private System.Windows.Forms.ToolStripMenuItem tsmEditUser;
        private System.Windows.Forms.ToolStripMenuItem tsmAddNewUser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmShowDetails;
        private System.Windows.Forms.ContextMenuStrip ctxtmsDGVManageUsers;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.ComboBox cmbIsActiveFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmChangePassword;
    }
}