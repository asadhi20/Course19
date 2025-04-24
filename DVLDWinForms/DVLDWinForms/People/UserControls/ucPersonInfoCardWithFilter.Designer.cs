namespace DVLDWinForms.People.UserControls
{
    partial class ucPersonInfoCardWithFilter
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
            this.gpbFilter = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.ucPersonCard1 = new DVLDWinForms.People.UserControls.ucPersonCard();
            this.mtxtFilter = new System.Windows.Forms.MaskedTextBox();
            this.gpbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbFilter
            // 
            this.gpbFilter.Controls.Add(this.btnAddNewPerson);
            this.gpbFilter.Controls.Add(this.btnFilter);
            this.gpbFilter.Controls.Add(this.label1);
            this.gpbFilter.Controls.Add(this.cmbFilterBy);
            this.gpbFilter.Location = new System.Drawing.Point(4, 4);
            this.gpbFilter.Name = "gpbFilter";
            this.gpbFilter.Size = new System.Drawing.Size(1230, 76);
            this.gpbFilter.TabIndex = 0;
            this.gpbFilter.TabStop = false;
            this.gpbFilter.Text = "Filter";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.Image = global::DVLDWinForms.Properties.Resources.ambassador_30x30_75A2D8;
            this.btnAddNewPerson.Location = new System.Drawing.Point(775, 22);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(75, 42);
            this.btnAddNewPerson.TabIndex = 3;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Image = global::DVLDWinForms.Properties.Resources.administrator_30x30_75A2D8;
            this.btnFilter.Location = new System.Drawing.Point(694, 22);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 42);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By:";
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "Person ID",
            "National No"});
            this.cmbFilterBy.Location = new System.Drawing.Point(120, 29);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(210, 28);
            this.cmbFilterBy.TabIndex = 2;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilterBy_SelectedIndexChanged);
            // 
            // ucPersonCard1
            // 
            this.ucPersonCard1.Location = new System.Drawing.Point(2, 86);
            this.ucPersonCard1.Name = "ucPersonCard1";
            this.ucPersonCard1.Size = new System.Drawing.Size(1240, 410);
            this.ucPersonCard1.TabIndex = 3;
            // 
            // mtxtFilter
            // 
            this.mtxtFilter.AsciiOnly = true;
            this.mtxtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.mtxtFilter.HidePromptOnLeave = true;
            this.mtxtFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mtxtFilter.Location = new System.Drawing.Point(350, 31);
            this.mtxtFilter.Name = "mtxtFilter";
            this.mtxtFilter.Size = new System.Drawing.Size(327, 30);
            this.mtxtFilter.TabIndex = 0;
            // 
            // ucPersonInfoCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mtxtFilter);
            this.Controls.Add(this.ucPersonCard1);
            this.Controls.Add(this.gpbFilter);
            this.Name = "ucPersonInfoCardWithFilter";
            this.Size = new System.Drawing.Size(1242, 495);
            this.Load += new System.EventHandler(this.ucPersonInfoCardWithFilter_Load);
            this.gpbFilter.ResumeLayout(false);
            this.gpbFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        public ucPersonCard ucPersonCard1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnAddNewPerson;
        internal System.Windows.Forms.GroupBox gpbFilter;
        internal System.Windows.Forms.MaskedTextBox mtxtFilter;
    }
}
