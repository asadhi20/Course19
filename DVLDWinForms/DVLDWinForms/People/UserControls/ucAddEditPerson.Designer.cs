namespace DVLDWinForms.People.UserControls
{
    partial class ucAddEditPerson
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.erprovIsValidInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.ofdSetImagePath = new System.Windows.Forms.OpenFileDialog();
            this.mtxtNationalNo = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.txtSecondName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.txtThirdName = new System.Windows.Forms.TextBox();
            this.dtpDataOfBirth = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.linklblSetImage = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.linklblRemove = new System.Windows.Forms.LinkLabel();
            this.mtxtPhone = new System.Windows.Forms.MaskedTextBox();
            this.gpbPerson = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.erprovIsValidInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.gpbPerson.SuspendLayout();
            this.SuspendLayout();
            // 
            // erprovIsValidInput
            // 
            this.erprovIsValidInput.ContainerControl = this;
            // 
            // mtxtNationalNo
            // 
            this.mtxtNationalNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.mtxtNationalNo.Location = new System.Drawing.Point(193, 112);
            this.mtxtNationalNo.Mask = "N000000000";
            this.mtxtNationalNo.Name = "mtxtNationalNo";
            this.mtxtNationalNo.Size = new System.Drawing.Size(210, 30);
            this.mtxtNationalNo.TabIndex = 4;
            this.mtxtNationalNo.TextChanged += new System.EventHandler(this.mtxtNationalNo_TextChanged);
            this.mtxtNationalNo.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtNationalNo_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDWinForms.Properties.Resources.user;
            this.pictureBox1.Location = new System.Drawing.Point(147, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(20, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "National No:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLDWinForms.Properties.Resources.field_numeric;
            this.pictureBox2.Location = new System.Drawing.Point(147, 107);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtFirstName.Location = new System.Drawing.Point(193, 52);
            this.txtFirstName.Multiline = true;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(210, 30);
            this.txtFirstName.TabIndex = 0;
            this.txtFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirstName_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(20, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Gender:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLDWinForms.Properties.Resources.administrator;
            this.pictureBox3.Location = new System.Drawing.Point(147, 167);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(20, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Email:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLDWinForms.Properties.Resources.address;
            this.pictureBox4.Location = new System.Drawing.Point(147, 227);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtEmail.Location = new System.Drawing.Point(193, 232);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(210, 30);
            this.txtEmail.TabIndex = 9;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(20, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Address:";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLDWinForms.Properties.Resources.homework;
            this.pictureBox5.Location = new System.Drawing.Point(147, 287);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(35, 35);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 13;
            this.pictureBox5.TabStop = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtAddress.Location = new System.Drawing.Point(193, 287);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(703, 147);
            this.txtAddress.TabIndex = 11;
            this.txtAddress.Validating += new System.ComponentModel.CancelEventHandler(this.txtAddress_Validating);
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.rbMale.Location = new System.Drawing.Point(193, 170);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(70, 24);
            this.rbMale.TabIndex = 6;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            this.rbMale.CheckedChanged += new System.EventHandler(this.rbMale_CheckedChanged);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::DVLDWinForms.Properties.Resources.user_female;
            this.pictureBox6.Location = new System.Drawing.Point(281, 167);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(35, 35);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 16;
            this.pictureBox6.TabStop = false;
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.rbFemale.Location = new System.Drawing.Point(337, 170);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(91, 24);
            this.rbFemale.TabIndex = 7;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            this.rbFemale.CheckedChanged += new System.EventHandler(this.rbFemale_CheckedChanged);
            // 
            // txtSecondName
            // 
            this.txtSecondName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtSecondName.Location = new System.Drawing.Point(461, 52);
            this.txtSecondName.Multiline = true;
            this.txtSecondName.Name = "txtSecondName";
            this.txtSecondName.Size = new System.Drawing.Size(210, 30);
            this.txtSecondName.TabIndex = 1;
            this.txtSecondName.Validating += new System.ComponentModel.CancelEventHandler(this.txtSecondName_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(490, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Data Of Birth:";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::DVLDWinForms.Properties.Resources.calendar_week;
            this.pictureBox7.Location = new System.Drawing.Point(633, 104);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(35, 35);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 20;
            this.pictureBox7.TabStop = false;
            // 
            // txtThirdName
            // 
            this.txtThirdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtThirdName.Location = new System.Drawing.Point(729, 52);
            this.txtThirdName.Multiline = true;
            this.txtThirdName.Name = "txtThirdName";
            this.txtThirdName.Size = new System.Drawing.Size(210, 30);
            this.txtThirdName.TabIndex = 2;
            // 
            // dtpDataOfBirth
            // 
            this.dtpDataOfBirth.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtpDataOfBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.dtpDataOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataOfBirth.Location = new System.Drawing.Point(686, 110);
            this.dtpDataOfBirth.Name = "dtpDataOfBirth";
            this.dtpDataOfBirth.Size = new System.Drawing.Size(210, 26);
            this.dtpDataOfBirth.TabIndex = 5;
            this.dtpDataOfBirth.Value = new System.DateTime(2007, 1, 15, 20, 26, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(551, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "Phone:";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLDWinForms.Properties.Resources.phone_vintage;
            this.pictureBox8.Location = new System.Drawing.Point(633, 167);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(35, 35);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 24;
            this.pictureBox8.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(538, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 26;
            this.label8.Text = "Country:";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::DVLDWinForms.Properties.Resources.world_north_america;
            this.pictureBox9.Location = new System.Drawing.Point(633, 227);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(35, 35);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 27;
            this.pictureBox9.TabStop = false;
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtLastName.Location = new System.Drawing.Point(997, 52);
            this.txtLastName.Multiline = true;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(210, 30);
            this.txtLastName.TabIndex = 3;
            this.txtLastName.Validating += new System.ComponentModel.CancelEventHandler(this.txtLastName_Validating);
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.Image = global::DVLDWinForms.Properties.Resources.person_man;
            this.pbPersonImage.Location = new System.Drawing.Point(927, 107);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(280, 270);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 30;
            this.pbPersonImage.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(274, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 20);
            this.label9.TabIndex = 31;
            this.label9.Text = "First";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(531, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 20);
            this.label10.TabIndex = 32;
            this.label10.Text = "Second";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(808, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 20);
            this.label11.TabIndex = 33;
            this.label11.Text = "Third";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(1079, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "Last";
            // 
            // linklblSetImage
            // 
            this.linklblSetImage.AutoSize = true;
            this.linklblSetImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linklblSetImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.linklblSetImage.Location = new System.Drawing.Point(1012, 392);
            this.linklblSetImage.Name = "linklblSetImage";
            this.linklblSetImage.Size = new System.Drawing.Size(110, 25);
            this.linklblSetImage.TabIndex = 12;
            this.linklblSetImage.TabStop = true;
            this.linklblSetImage.Text = "Set Image";
            this.linklblSetImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblSetImage_LinkClicked);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Image = global::DVLDWinForms.Properties.Resources.diskette_32x32_C7DBEB;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(753, 449);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(143, 51);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbCountry
            // 
            this.cmbCountry.BackColor = System.Drawing.Color.Gray;
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(686, 231);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(210, 33);
            this.cmbCountry.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(590, 449);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(143, 51);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // linklblRemove
            // 
            this.linklblRemove.AutoSize = true;
            this.linklblRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linklblRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.linklblRemove.Location = new System.Drawing.Point(1022, 429);
            this.linklblRemove.Name = "linklblRemove";
            this.linklblRemove.Size = new System.Drawing.Size(90, 25);
            this.linklblRemove.TabIndex = 35;
            this.linklblRemove.TabStop = true;
            this.linklblRemove.Text = "Remove";
            this.linklblRemove.Visible = false;
            this.linklblRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblRemove_LinkClicked);
            // 
            // mtxtPhone
            // 
            this.mtxtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.mtxtPhone.Location = new System.Drawing.Point(686, 167);
            this.mtxtPhone.Mask = "0000-000-0000";
            this.mtxtPhone.Name = "mtxtPhone";
            this.mtxtPhone.Size = new System.Drawing.Size(210, 30);
            this.mtxtPhone.TabIndex = 8;
            this.mtxtPhone.TextChanged += new System.EventHandler(this.mtxtPhone_TextChanged);
            this.mtxtPhone.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtPhone_Validating);
            // 
            // gpbPerson
            // 
            this.gpbPerson.Controls.Add(this.mtxtPhone);
            this.gpbPerson.Controls.Add(this.linklblRemove);
            this.gpbPerson.Controls.Add(this.btnClose);
            this.gpbPerson.Controls.Add(this.cmbCountry);
            this.gpbPerson.Controls.Add(this.btnSave);
            this.gpbPerson.Controls.Add(this.linklblSetImage);
            this.gpbPerson.Controls.Add(this.label12);
            this.gpbPerson.Controls.Add(this.label11);
            this.gpbPerson.Controls.Add(this.label10);
            this.gpbPerson.Controls.Add(this.label9);
            this.gpbPerson.Controls.Add(this.pbPersonImage);
            this.gpbPerson.Controls.Add(this.txtLastName);
            this.gpbPerson.Controls.Add(this.pictureBox9);
            this.gpbPerson.Controls.Add(this.label8);
            this.gpbPerson.Controls.Add(this.pictureBox8);
            this.gpbPerson.Controls.Add(this.label7);
            this.gpbPerson.Controls.Add(this.dtpDataOfBirth);
            this.gpbPerson.Controls.Add(this.txtThirdName);
            this.gpbPerson.Controls.Add(this.pictureBox7);
            this.gpbPerson.Controls.Add(this.label6);
            this.gpbPerson.Controls.Add(this.txtSecondName);
            this.gpbPerson.Controls.Add(this.rbFemale);
            this.gpbPerson.Controls.Add(this.pictureBox6);
            this.gpbPerson.Controls.Add(this.rbMale);
            this.gpbPerson.Controls.Add(this.txtAddress);
            this.gpbPerson.Controls.Add(this.pictureBox5);
            this.gpbPerson.Controls.Add(this.label5);
            this.gpbPerson.Controls.Add(this.txtEmail);
            this.gpbPerson.Controls.Add(this.pictureBox4);
            this.gpbPerson.Controls.Add(this.label4);
            this.gpbPerson.Controls.Add(this.pictureBox3);
            this.gpbPerson.Controls.Add(this.label3);
            this.gpbPerson.Controls.Add(this.txtFirstName);
            this.gpbPerson.Controls.Add(this.pictureBox2);
            this.gpbPerson.Controls.Add(this.label2);
            this.gpbPerson.Controls.Add(this.pictureBox1);
            this.gpbPerson.Controls.Add(this.label1);
            this.gpbPerson.Controls.Add(this.mtxtNationalNo);
            this.gpbPerson.Location = new System.Drawing.Point(3, 3);
            this.gpbPerson.Name = "gpbPerson";
            this.gpbPerson.Size = new System.Drawing.Size(1229, 510);
            this.gpbPerson.TabIndex = 0;
            this.gpbPerson.TabStop = false;
            // 
            // ucAddEditPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbPerson);
            this.Name = "ucAddEditPerson";
            this.Size = new System.Drawing.Size(1240, 520);
            this.Load += new System.EventHandler(this.ucPerson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erprovIsValidInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.gpbPerson.ResumeLayout(false);
            this.gpbPerson.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider erprovIsValidInput;
        private System.Windows.Forms.OpenFileDialog ofdSetImagePath;
        private System.Windows.Forms.GroupBox gpbPerson;
        private System.Windows.Forms.MaskedTextBox mtxtPhone;
        private System.Windows.Forms.LinkLabel linklblRemove;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.LinkLabel linklblSetImage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDataOfBirth;
        private System.Windows.Forms.TextBox txtThirdName;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSecondName;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtxtNationalNo;
    }
}
