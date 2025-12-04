using ClinicManagement_proj.BLL;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.BLL.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            NotificationManager.NotificationAdded += OnNotificationAdded;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;

            cmbRole.Items.AddRange(Enum.GetNames(typeof(UserService.UserRoles)));
            cmbRole.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                checkRememberPassword.Checked = true;
                if (!ClinicManagementApp.HasLoggedInBefore)
                    btnLogin.PerformClick();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblToast.Visible = false;
            timerToast.Stop();

            var user = ClinicManagementApp.UserService.Authenticate(txtUsername.Text, txtPassword.Text);

            if (user != null)
            {
                ClinicManagementApp.CurrentUser = user;

                var selectedRole = (UserService.UserRoles)Enum.Parse(typeof(UserService.UserRoles), cmbRole.Text);
                if (!ClinicManagementApp.CurrentUserHasRole(selectedRole))
                {
                    NotificationManager.AddNotification("User does not have the selected role.", NotificationType.Error);
                    ClinicManagementApp.CurrentUser = null;
                    return;
                }

                if (checkRememberPassword.Checked)
                {
                    Properties.Settings.Default.Username = txtUsername.Text;
                    Properties.Settings.Default.Password = txtPassword.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Username = "";
                    Properties.Settings.Default.Password = "";
                    Properties.Settings.Default.Save();
                }

                Form dashboard = null;

                if (ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                {
                    dashboard = new AdminDashboard();
                }
                else if (ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Doctor))
                {
                    dashboard = new DoctorDashboard();
                }
                else if (ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Receptionist))
                {
                    dashboard = new ReceptionistDashboard();
                }
                else
                {
                    NotificationManager.AddNotification("User has no valid roles assigned.", NotificationType.Error);
                    return;
                }

                this.Hide();
                Label lblLogout = dashboard.Controls.Find("lblLogout", true)[0] as Label;
                lblLogout.Text = $"Welcome {ClinicManagementApp.CurrentUser.Username}";
                NotificationManager.AddNotification("Login successful.", NotificationType.Info);
                dashboard.ShowDialog();
                Close();
            }
            else
            {
                NotificationManager.AddNotification("Invalid username or password.", NotificationType.Error);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnLogin.PerformClick(); }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnLogin.PerformClick(); }
        }

        private void OnNotificationAdded(Notification notif)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Notification>(OnNotificationAdded), notif);
                return;
            }

            // Only show error notifications as toast
            if (notif.Type == NotificationType.Error)
            {
                ShowErrorToast(notif.Message);
            }
        }

        private void ShowErrorToast(string message)
        {
            timerToast.Stop();
            lblToast.Text = message;
            lblToast.BackColor = Color.FromArgb(231, 76, 60);
            lblToast.Visible = true;
            timerToast.Start();
        }

        private void timerToast_Tick(object sender, EventArgs e)
        {
            lblToast.Visible = false;
            timerToast.Stop();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            NotificationManager.NotificationAdded -= OnNotificationAdded;
            base.OnFormClosing(e);
        }
    }
}
