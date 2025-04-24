namespace DVLDWinForms.Applications.ManageApplications.LocalDrivingLicenseApplications.SechdlueTests
{
    partial class frmSechduleVisionTest
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ucSechdule_ResechduleTest1 = new DVLDWinForms.Applications.UserControls.ucSechdule_ResechduleTest();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLDWinForms.Properties.Resources.diskette_32x32_C7DBEB;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(249, 680);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 50);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucSechdule_ResechduleTest1
            // 
            this.ucSechdule_ResechduleTest1.Location = new System.Drawing.Point(18, 13);
            this.ucSechdule_ResechduleTest1.Name = "ucSechdule_ResechduleTest1";
            this.ucSechdule_ResechduleTest1.Size = new System.Drawing.Size(628, 668);
            this.ucSechdule_ResechduleTest1.TabIndex = 2;
            // 
            // frmSechduleVisionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(664, 741);
            this.Controls.Add(this.ucSechdule_ResechduleTest1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSechduleVisionTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sechdule Test";
            this.Load += new System.EventHandler(this.frmSechduleVisionTest_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private UserControls.ucSechdule_ResechduleTest ucSechdule_ResechduleTest1;
    }
}