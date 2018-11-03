namespace AccountingSwift
{
    partial class Reset_Password
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
            this.userID = new System.Windows.Forms.TextBox();
            this.newPass = new System.Windows.Forms.TextBox();
            this.confPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.newPassBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userID
            // 
            this.userID.Location = new System.Drawing.Point(135, 29);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(127, 20);
            this.userID.TabIndex = 0;
            this.userID.TextChanged += new System.EventHandler(this.userID_TextChanged);
            // 
            // newPass
            // 
            this.newPass.Location = new System.Drawing.Point(135, 101);
            this.newPass.Name = "newPass";
            this.newPass.PasswordChar = '*';
            this.newPass.Size = new System.Drawing.Size(127, 20);
            this.newPass.TabIndex = 1;
            this.newPass.TextChanged += new System.EventHandler(this.newPass_TextChanged);
            // 
            // confPass
            // 
            this.confPass.Location = new System.Drawing.Point(135, 179);
            this.confPass.Name = "confPass";
            this.confPass.PasswordChar = '*';
            this.confPass.Size = new System.Drawing.Size(127, 20);
            this.confPass.TabIndex = 2;
            this.confPass.TextChanged += new System.EventHandler(this.confPass_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "User ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "New Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirm Password:";
            // 
            // newPassBtn
            // 
            this.newPassBtn.Location = new System.Drawing.Point(187, 219);
            this.newPassBtn.Name = "newPassBtn";
            this.newPassBtn.Size = new System.Drawing.Size(75, 23);
            this.newPassBtn.TabIndex = 6;
            this.newPassBtn.Text = "Submit";
            this.newPassBtn.UseVisualStyleBackColor = true;
            this.newPassBtn.Click += new System.EventHandler(this.newPassBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(43, 219);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // Reset_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 254);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.newPassBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.confPass);
            this.Controls.Add(this.newPass);
            this.Controls.Add(this.userID);
            this.Name = "Reset_Password";
            this.Text = "Reset_Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userID;
        private System.Windows.Forms.TextBox newPass;
        private System.Windows.Forms.TextBox confPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button newPassBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}