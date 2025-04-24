namespace DVLDWinForms.Users
{
    partial class frmAddEditUser
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPagePersonInfo = new System.Windows.Forms.TabPage();
            this.ucPersonInfoWithFilter1 = new DVLDWinForms.People.UserControls.ucPersonInfoCardWithFilter();
            this.btnNext = new System.Windows.Forms.Button();
            this.tabPageLoginInfo = new System.Windows.Forms.TabPage();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitel = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.erprovIsValidInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPagePersonInfo.SuspendLayout();
            this.tabPageLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erprovIsValidInput)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPagePersonInfo);
            this.tabControl1.Controls.Add(this.tabPageLoginInfo);
            this.tabControl1.Location = new System.Drawing.Point(0, 68);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1264, 591);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPagePersonInfo
            // 
            this.tabPagePersonInfo.Controls.Add(this.ucPersonInfoWithFilter1);
            this.tabPagePersonInfo.Controls.Add(this.btnNext);
            this.tabPagePersonInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPagePersonInfo.Name = "tabPagePersonInfo";
            this.tabPagePersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePersonInfo.Size = new System.Drawing.Size(1256, 562);
            this.tabPagePersonInfo.TabIndex = 0;
            this.tabPagePersonInfo.Text = "Person Info";
            this.tabPagePersonInfo.UseVisualStyleBackColor = true;
            // 
            // ucPersonInfoWithFilter1
            // 
            this.ucPersonInfoWithFilter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPersonInfoWithFilter1.Location = new System.Drawing.Point(3, 3);
            this.ucPersonInfoWithFilter1.Name = "ucPersonInfoWithFilter1";
            this.ucPersonInfoWithFilter1.Size = new System.Drawing.Size(1250, 495);
            this.ucPersonInfoWithFilter1.TabIndex = 0;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnNext.Image = global::DVLDWinForms.Properties.Resources.arrow_right_32x32_C7DBEB;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(1101, 506);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(140, 50);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tabPageLoginInfo
            // 
            this.tabPageLoginInfo.Controls.Add(this.txtPassword);
            this.tabPageLoginInfo.Controls.Add(this.txtConfirmPassword);
            this.tabPageLoginInfo.Controls.Add(this.txtUserName);
            this.tabPageLoginInfo.Controls.Add(this.chkIsActive);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox4);
            this.tabPageLoginInfo.Controls.Add(this.label8);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox3);
            this.tabPageLoginInfo.Controls.Add(this.label6);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox2);
            this.tabPageLoginInfo.Controls.Add(this.label4);
            this.tabPageLoginInfo.Controls.Add(this.lblUserID);
            this.tabPageLoginInfo.Controls.Add(this.pictureBox1);
            this.tabPageLoginInfo.Controls.Add(this.label2);
            this.tabPageLoginInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPageLoginInfo.Name = "tabPageLoginInfo";
            this.tabPageLoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLoginInfo.Size = new System.Drawing.Size(1256, 562);
            this.tabPageLoginInfo.TabIndex = 1;
            this.tabPageLoginInfo.Text = "Login Info";
            this.tabPageLoginInfo.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtPassword.Location = new System.Drawing.Point(349, 170);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(240, 30);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtConfirmPassword.Location = new System.Drawing.Point(349, 230);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(240, 30);
            this.txtConfirmPassword.TabIndex = 2;
            this.txtConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtConfirmPassword_Validating);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtUserName.Location = new System.Drawing.Point(349, 111);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(240, 30);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.chkIsActive.Location = new System.Drawing.Point(348, 296);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(110, 28);
            this.chkIsActive.TabIndex = 3;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLDWinForms.Properties.Resources.field_numeric;
            this.pictureBox4.Location = new System.Drawing.Point(290, 228);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 15;
            this.pictureBox4.TabStop = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(65, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "Confirm Password:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLDWinForms.Properties.Resources.field_numeric;
            this.pictureBox3.Location = new System.Drawing.Point(290, 168);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(65, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "Password:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLDWinForms.Properties.Resources.user;
            this.pictureBox2.Location = new System.Drawing.Point(290, 108);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(65, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "UserName:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserID.Location = new System.Drawing.Point(344, 53);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(86, 25);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.Text = "[?????]";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDWinForms.Properties.Resources.field_numeric;
            this.pictureBox1.Location = new System.Drawing.Point(290, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(65, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "User ID:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTitel.ForeColor = System.Drawing.Color.Red;
            this.lblTitel.Location = new System.Drawing.Point(540, 25);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(237, 39);
            this.lblTitel.TabIndex = 7;
            this.lblTitel.Text = "Add New User";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Image = global::DVLDWinForms.Properties.Resources.diskette_32x32_C7DBEB;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1105, 666);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 50);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(950, 666);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 50);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // erprovIsValidInput
            // 
            this.erprovIsValidInput.ContainerControl = this;
            // 
            // frmAddEditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1264, 723);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddEditUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddNewUser";
            this.Load += new System.EventHandler(this.frmAddEditUser_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPagePersonInfo.ResumeLayout(false);
            this.tabPageLoginInfo.ResumeLayout(false);
            this.tabPageLoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erprovIsValidInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPagePersonInfo;
        private System.Windows.Forms.TabPage tabPageLoginInfo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNext;
        private DVLDWinForms.People.UserControls.ucPersonInfoCardWithFilter ucPersonInfoWithFilter1;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.ErrorProvider erprovIsValidInput;
    }
}