namespace DVLDWinForms.Users.UserControles
{
    partial class ucUserCard
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
            this.gpbLoginInfo = new System.Windows.Forms.GroupBox();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucPersonCard1 = new DVLDWinForms.People.UserControls.ucPersonCard();
            this.gpbLoginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbLoginInfo
            // 
            this.gpbLoginInfo.Controls.Add(this.lblIsActive);
            this.gpbLoginInfo.Controls.Add(this.label3);
            this.gpbLoginInfo.Controls.Add(this.lblUserName);
            this.gpbLoginInfo.Controls.Add(this.label2);
            this.gpbLoginInfo.Controls.Add(this.lblUserID);
            this.gpbLoginInfo.Controls.Add(this.label1);
            this.gpbLoginInfo.Location = new System.Drawing.Point(70, 410);
            this.gpbLoginInfo.Name = "gpbLoginInfo";
            this.gpbLoginInfo.Size = new System.Drawing.Size(1100, 90);
            this.gpbLoginInfo.TabIndex = 0;
            this.gpbLoginInfo.TabStop = false;
            this.gpbLoginInfo.Text = "Login Information";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblIsActive.Location = new System.Drawing.Point(912, 35);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(71, 20);
            this.lblIsActive.TabIndex = 0;
            this.lblIsActive.Text = "[?????]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(812, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Is Active:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.Location = new System.Drawing.Point(527, 35);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(71, 20);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "[?????]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(415, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "UserName:";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserID.Location = new System.Drawing.Point(217, 35);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(71, 20);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.Text = "[?????]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(122, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID:";
            // 
            // ucPersonCard1
            // 
            this.ucPersonCard1.Location = new System.Drawing.Point(0, 0);
            this.ucPersonCard1.Name = "ucPersonCard1";
            this.ucPersonCard1.Size = new System.Drawing.Size(1240, 410);
            this.ucPersonCard1.TabIndex = 0;
            // 
            // ucUserCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbLoginInfo);
            this.Controls.Add(this.ucPersonCard1);
            this.Name = "ucUserCard";
            this.Size = new System.Drawing.Size(1240, 505);
            this.gpbLoginInfo.ResumeLayout(false);
            this.gpbLoginInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private People.UserControls.ucPersonCard ucPersonCard1;
        private System.Windows.Forms.GroupBox gpbLoginInfo;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label1;
    }
}
