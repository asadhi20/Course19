namespace DVLDWinForms.Applications.ManageTestTypes
{
    partial class frmManageTestTypes
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
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.EditTestTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxtmsDGVApplicationTypes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblTitel = new System.Windows.Forms.Label();
            this.dgvTestTypes = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctxtmsDGVApplicationTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfRecords.Location = new System.Drawing.Point(60, 710);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(107, 20);
            this.lblNumberOfRecords.TabIndex = 30;
            this.lblNumberOfRecords.Tag = "# Records: ";
            this.lblNumberOfRecords.Text = "# Records: ";
            // 
            // EditTestTypesToolStripMenuItem
            // 
            this.EditTestTypesToolStripMenuItem.Name = "EditTestTypesToolStripMenuItem";
            this.EditTestTypesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.EditTestTypesToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.EditTestTypesToolStripMenuItem.Text = "Edit Test Types";
            this.EditTestTypesToolStripMenuItem.Click += new System.EventHandler(this.EditTestTypesToolStripMenuItem_Click);
            // 
            // ctxtmsDGVApplicationTypes
            // 
            this.ctxtmsDGVApplicationTypes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxtmsDGVApplicationTypes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditTestTypesToolStripMenuItem});
            this.ctxtmsDGVApplicationTypes.Name = "ctxtmsDGVManagePeople";
            this.ctxtmsDGVApplicationTypes.Size = new System.Drawing.Size(226, 28);
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTitel.ForeColor = System.Drawing.Color.Red;
            this.lblTitel.Location = new System.Drawing.Point(390, 180);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(317, 39);
            this.lblTitel.TabIndex = 31;
            this.lblTitel.Text = "Manage Test Types";
            // 
            // dgvTestTypes
            // 
            this.dgvTestTypes.AllowUserToAddRows = false;
            this.dgvTestTypes.AllowUserToDeleteRows = false;
            this.dgvTestTypes.AllowUserToResizeRows = false;
            this.dgvTestTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTestTypes.BackgroundColor = System.Drawing.Color.White;
            this.dgvTestTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestTypes.ContextMenuStrip = this.ctxtmsDGVApplicationTypes;
            this.dgvTestTypes.Location = new System.Drawing.Point(25, 234);
            this.dgvTestTypes.MultiSelect = false;
            this.dgvTestTypes.Name = "dgvTestTypes";
            this.dgvTestTypes.ReadOnly = true;
            this.dgvTestTypes.RowHeadersWidth = 50;
            this.dgvTestTypes.RowTemplate.Height = 24;
            this.dgvTestTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTestTypes.Size = new System.Drawing.Size(1080, 445);
            this.dgvTestTypes.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(960, 695);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 50);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(451, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // frmManageTestTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1132, 759);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.dgvTestTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmManageTestTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmManageTestTypes";
            this.Load += new System.EventHandler(this.frmManageTestTypes_Load);
            this.ctxtmsDGVApplicationTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem EditTestTypesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxtmsDGVApplicationTypes;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.DataGridView dgvTestTypes;
    }
}