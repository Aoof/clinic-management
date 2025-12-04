namespace ClinicManagement_proj.UI
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblToast = new System.Windows.Forms.Label();
            this.pnlNotifications = new System.Windows.Forms.Panel();
            this.lblNotificationsTitle = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpForm = new System.Windows.Forms.GroupBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.checkRememberPassword = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.timerToast = new System.Windows.Forms.Timer(this.components);
            this.pbThumbnail = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlNotifications.SuspendLayout();
            this.grpForm.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.scMain.Panel1.Controls.Add(this.pbThumbnail);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.BackColor = System.Drawing.Color.White;
            this.scMain.Panel2.Controls.Add(this.pnlRight);
            this.scMain.Size = new System.Drawing.Size(1482, 783);
            this.scMain.SplitterDistance = 857;
            this.scMain.SplitterWidth = 1;
            this.scMain.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpForm);
            this.pnlRight.Controls.Add(this.lblHeader);
            this.pnlRight.Controls.Add(this.pnlNotifications);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(20);
            this.pnlRight.Size = new System.Drawing.Size(624, 783);
            this.pnlRight.TabIndex = 0;
            // 
            // lblToast
            // 
            this.lblToast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblToast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToast.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold);
            this.lblToast.ForeColor = System.Drawing.Color.White;
            this.lblToast.Location = new System.Drawing.Point(10, 40);
            this.lblToast.Name = "lblToast";
            this.lblToast.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.lblToast.Size = new System.Drawing.Size(564, 70);
            this.lblToast.TabIndex = 0;
            this.lblToast.Text = "Error message will appear here";
            this.lblToast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblToast.Visible = false;
            // 
            // pnlNotifications
            // 
            this.pnlNotifications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlNotifications.Controls.Add(this.lblToast);
            this.pnlNotifications.Controls.Add(this.lblNotificationsTitle);
            this.pnlNotifications.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNotifications.Location = new System.Drawing.Point(20, 20);
            this.pnlNotifications.Name = "pnlNotifications";
            this.pnlNotifications.Padding = new System.Windows.Forms.Padding(10);
            this.pnlNotifications.Size = new System.Drawing.Size(584, 120);
            this.pnlNotifications.TabIndex = 0;
            // 
            // lblNotificationsTitle
            // 
            this.lblNotificationsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNotificationsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotificationsTitle.Font = new System.Drawing.Font("Lucida Sans", 11F, System.Drawing.FontStyle.Bold);
            this.lblNotificationsTitle.ForeColor = System.Drawing.Color.White;
            this.lblNotificationsTitle.Location = new System.Drawing.Point(10, 10);
            this.lblNotificationsTitle.Name = "lblNotificationsTitle";
            this.lblNotificationsTitle.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblNotificationsTitle.Size = new System.Drawing.Size(564, 30);
            this.lblNotificationsTitle.TabIndex = 1;
            this.lblNotificationsTitle.Text = "📢 System Notifications";
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Lucida Sans", 28F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblHeader.Location = new System.Drawing.Point(20, 140);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.lblHeader.Size = new System.Drawing.Size(584, 100);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Welcome Back";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpForm
            // 
            this.grpForm.Controls.Add(this.lblRole);
            this.grpForm.Controls.Add(this.cmbRole);
            this.grpForm.Controls.Add(this.checkRememberPassword);
            this.grpForm.Controls.Add(this.pnlButtons);
            this.grpForm.Controls.Add(this.txtPassword);
            this.grpForm.Controls.Add(this.lblPassword);
            this.grpForm.Controls.Add(this.txtUsername);
            this.grpForm.Controls.Add(this.lblUsername);
            this.grpForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpForm.Font = new System.Drawing.Font("Lucida Sans", 10.8F);
            this.grpForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.grpForm.Location = new System.Drawing.Point(20, 240);
            this.grpForm.Name = "grpForm";
            this.grpForm.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);
            this.grpForm.Size = new System.Drawing.Size(584, 523);
            this.grpForm.TabIndex = 0;
            this.grpForm.TabStop = false;
            this.grpForm.Text = "Sign In";
            // 
            // lblRole
            // 
            this.lblRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRole.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold);
            this.lblRole.Location = new System.Drawing.Point(40, 35);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(504, 30);
            this.lblRole.TabIndex = 8;
            this.lblRole.Text = "Role";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbRole
            // 
            this.cmbRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Lucida Sans", 14F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(43, 68);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(498, 34);
            this.cmbRole.TabIndex = 0;
            // 
            // checkRememberPassword
            // 
            this.checkRememberPassword.AutoSize = true;
            this.checkRememberPassword.Font = new System.Drawing.Font("Lucida Sans", 10F);
            this.checkRememberPassword.Location = new System.Drawing.Point(43, 275);
            this.checkRememberPassword.Name = "checkRememberPassword";
            this.checkRememberPassword.Size = new System.Drawing.Size(204, 23);
            this.checkRememberPassword.TabIndex = 3;
            this.checkRememberPassword.Text = "Remember Password";
            this.checkRememberPassword.UseVisualStyleBackColor = true;
            // 
            // pnlButtons
            // 
            this.pnlButtons.ColumnCount = 1;
            this.pnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButtons.Controls.Add(this.btnLogin, 0, 0);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(40, 429);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.RowCount = 1;
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButtons.Size = new System.Drawing.Size(504, 64);
            this.pnlButtons.TabIndex = 4;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Lucida Sans", 14F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(171, 8);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(162, 48);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Lucida Sans", 14F);
            this.txtPassword.Location = new System.Drawing.Point(43, 227);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(498, 35);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold);
            this.lblPassword.Location = new System.Drawing.Point(40, 194);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(504, 30);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Lucida Sans", 14F);
            this.txtUsername.Location = new System.Drawing.Point(43, 147);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(498, 35);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsername.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsername.Location = new System.Drawing.Point(40, 114);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(504, 30);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerToast
            // 
            this.timerToast.Interval = 4000;
            this.timerToast.Tick += new System.EventHandler(this.timerToast_Tick);
            // 
            // pbThumbnail
            // 
            this.pbThumbnail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pbThumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbThumbnail.Image = global::ClinicManagement_proj.Properties.Resources.clinic_mgmt_thumbnail;
            this.pbThumbnail.ImageLocation = "";
            this.pbThumbnail.InitialImage = null;
            this.pbThumbnail.Location = new System.Drawing.Point(0, 0);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Padding = new System.Windows.Forms.Padding(40);
            this.pbThumbnail.Size = new System.Drawing.Size(857, 783);
            this.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbThumbnail.TabIndex = 0;
            this.pbThumbnail.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1482, 783);
            this.Controls.Add(this.scMain);
            this.Font = new System.Drawing.Font("Lucida Sans", 13.8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clinic Management - Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlNotifications.ResumeLayout(false);
            this.grpForm.ResumeLayout(false);
            this.grpForm.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).EndInit();
            this.ResumeLayout(false);

        }

    #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.PictureBox pbThumbnail;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblToast;
        private System.Windows.Forms.Panel pnlNotifications;
        private System.Windows.Forms.Label lblNotificationsTitle;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.GroupBox grpForm;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
   private System.Windows.Forms.Label lblPassword;
   private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TableLayoutPanel pnlButtons;
      private System.Windows.Forms.TextBox txtUsername;
      private System.Windows.Forms.CheckBox checkRememberPassword;
     private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Timer timerToast;
    }
}

