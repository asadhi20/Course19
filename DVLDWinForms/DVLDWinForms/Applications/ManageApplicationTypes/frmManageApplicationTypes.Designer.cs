namespace DVLDWinForms.Applications.ManageApplicationTypes
{
    partial class frmManageApplicationTypes
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
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.EditApplicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxtmsDGVApplicationTypes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblTitel = new System.Windows.Forms.Label();
            this.dgvApplicationTypes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ctxtmsDGVApplicationTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(400, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfRecords.Location = new System.Drawing.Point(50, 709);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(107, 20);
            this.lblNumberOfRecords.TabIndex = 23;
            this.lblNumberOfRecords.Tag = "# Records: ";
            this.lblNumberOfRecords.Text = "# Records: ";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.close_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(865, 699);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 48);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // EditApplicationTypesToolStripMenuItem
            // 
            this.EditApplicationTypesToolStripMenuItem.Name = "EditApplicationTypesToolStripMenuItem";
            this.EditApplicationTypesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.EditApplicationTypesToolStripMenuItem.Size = new System.Drawing.Size(276, 24);
            this.EditApplicationTypesToolStripMenuItem.Text = "Edit Application Types";
            this.EditApplicationTypesToolStripMenuItem.Click += new System.EventHandler(this.EditApplicationTypesToolStripMenuItem_Click);
            // 
            // ctxtmsDGVApplicationTypes
            // 
            this.ctxtmsDGVApplicationTypes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxtmsDGVApplicationTypes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditApplicationTypesToolStripMenuItem});
            this.ctxtmsDGVApplicationTypes.Name = "ctxtmsDGVManagePeople";
            this.ctxtmsDGVApplicationTypes.Size = new System.Drawing.Size(277, 28);
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTitel.ForeColor = System.Drawing.Color.Red;
            this.lblTitel.Location = new System.Drawing.Point(300, 196);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(419, 39);
            this.lblTitel.TabIndex = 24;
            this.lblTitel.Text = "Manage Application Types";
            // 
            // dgvApplicationTypes
            // 
            this.dgvApplicationTypes.AllowUserToAddRows = false;
            this.dgvApplicationTypes.AllowUserToDeleteRows = false;
            this.dgvApplicationTypes.AllowUserToResizeRows = false;
            this.dgvApplicationTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvApplicationTypes.BackgroundColor = System.Drawing.Color.White;
            this.dgvApplicationTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApplicationTypes.ContextMenuStrip = this.ctxtmsDGVApplicationTypes;
            this.dgvApplicationTypes.Location = new System.Drawing.Point(25, 250);
            this.dgvApplicationTypes.MultiSelect = false;
            this.dgvApplicationTypes.Name = "dgvApplicationTypes";
            this.dgvApplicationTypes.ReadOnly = true;
            this.dgvApplicationTypes.RowHeadersWidth = 50;
            this.dgvApplicationTypes.RowTemplate.Height = 24;
            this.dgvApplicationTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvApplicationTypes.Size = new System.Drawing.Size(980, 435);
            this.dgvApplicationTypes.TabIndex = 26;
            // 
            // frmManageApplicationTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1032, 768);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.dgvApplicationTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmManageApplicationTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmManageApplicationTypes";
            this.Load += new System.EventHandler(this.frmManageApplicationTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ctxtmsDGVApplicationTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationTypes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem EditApplicationTypesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxtmsDGVApplicationTypes;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.DataGridView dgvApplicationTypes;
    }
}