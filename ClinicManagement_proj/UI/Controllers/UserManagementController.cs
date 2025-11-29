using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the User Management panel
    /// </summary>
    public class UserManagementController : IPanelController
    {
        private readonly Panel panel;
        private readonly UserService userService;
        private readonly RoleService roleService;
        private int selectedUserId;

        private DataGridView dgvUsers => (DataGridView)(panel.Controls["dgvUsers"] 
                ?? throw new Exception("No control named [dgvUsers] found in panel controls collection."));
        private GroupBox grpAdminForm => (GroupBox)(panel.Controls["grpAdminForm"] 
                ?? throw new Exception("No control named [grpAdminForm] found in panel controls collection."));
        private Panel pnlButtons => (Panel)(grpAdminForm.Controls["pnlButtons"] 
                ?? throw new Exception("No control named [pnlButtons] found in grpAdminForm controls collection."));
        private Button btnUsrCreate => (Button)(pnlButtons.Controls["btnUsrCreate"] 
                ?? throw new Exception("No control named [btnUsrCreate] found in pnlButtons controls collection."));
        private Button btnUsrUpdate => (Button)(pnlButtons.Controls["btnUsrUpdate"] 
                ?? throw new Exception("No control named [btnUsrUpdate] found in pnlButtons controls collection."));
        private Button btnUsrCancel => (Button)(pnlButtons.Controls["btnUsrCancel"] 
                ?? throw new Exception("No control named [btnUsrCancel] found in pnlButtons controls collection."));
        private Button btnUsrDelete => (Button)(pnlButtons.Controls["btnUsrDelete"] 
                ?? throw new Exception("No control named [btnUsrDelete] found in pnlButtons controls collection."));
        private Button btnUsrDisplay => (Button)(pnlButtons.Controls["btnUsrDisplay"] 
                ?? throw new Exception("No control named [btnUsrDisplay] found in pnlButtons controls collection."));
        private Button btnUsrSearch => (Button)(pnlButtons.Controls["btnUsrSearch"] 
                ?? throw new Exception("No control named [btnUsrSearch] found in pnlButtons controls collection."));
        private Panel pnlPassword => (Panel)(grpAdminForm.Controls["pnlPassword"] 
                ?? throw new Exception("No control named [pnlPassword] found in grpAdminForm controls collection."));
        private Button btnTogglePassword => (Button)(pnlPassword.Controls["btnTogglePassword"] 
                ?? throw new Exception("No control named [btnTogglePassword] found in pnlPassword controls collection."));
        private Button btnGenPassword => (Button)(pnlButtons.Controls["btnGenPassword"] 
                ?? throw new Exception("No control named [btnGenPassword] found in pnlButtons controls collection."));
        private TextBox txtUsrId => (TextBox)(grpAdminForm.Controls["txtUsrId"] 
                ?? throw new Exception("No control named [txtUsrId] found in grpAdminForm controls collection."));
        private TextBox txtUsrUsername => (TextBox)(grpAdminForm.Controls["txtUsrUsername"] 
                ?? throw new Exception("No control named [txtUsrUsername] found in grpAdminForm controls collection."));
        private TextBox txtUsrPassword => (TextBox)(pnlPassword.Controls["txtUsrPassword"] 
                ?? throw new Exception("No control named [txtUsrPassword] found in pnlPassword controls collection."));
        private ComboBox comboRoles => (ComboBox)(grpAdminForm.Controls["comboRoles"] 
                ?? throw new Exception("No control named [comboRoles] found in grpAdminForm controls collection."));

        private bool isEditMode = false;
        private bool isPasswordChanged = false;

        public Panel Panel => panel;

        public UserManagementController(Panel panel)
        {
            this.panel = panel;
            this.userService = new UserService();
            this.roleService = new RoleService();
        }

        public void Initialize()
        {
            btnUsrCreate.Click += new EventHandler(btnUsrCreate_Click);
            btnUsrUpdate.Click += new EventHandler(btnUsrUpdate_Click);
            btnUsrCancel.Click += new EventHandler(btnUsrCancel_Click);
            btnUsrDelete.Click += new EventHandler(btnUsrDelete_Click);
            btnUsrDisplay.Click += new EventHandler(btnUsrDisplay_Click);
            btnUsrSearch.Click += new EventHandler(btnUsrSearch_Click);
            btnTogglePassword.Click += new EventHandler(btnTogglePassword_Click);
            btnGenPassword.Click += new EventHandler(btnGenPassword_Click);
            txtUsrPassword.TextChanged += new EventHandler(txtUsrPassword_TextChanged);
            dgvUsers.Click += new EventHandler(dgvUsers_Click);
        }

        public void OnShow()
        {
            LoadUsers();
            ResetUsrForm();
            comboRoles.DataSource = roleService.GetAllRoles();
            comboRoles.DisplayMember = "RoleName";
            comboRoles.ValueMember = "Id";
        }

        private void LoadUsers()
        {
            var users = userService.GetAllUsers();
            dgvUsers.DataSource = users;
        }

        /// <summary>
        /// Reset the user management form to initial state
        /// </summary>
        private void ResetUsrForm()
        {
            txtUsrPassword.Text = string.Empty;
            comboRoles.SelectedIndex = -1;
            isEditMode = false;
            isPasswordChanged = false;
            btnUsrCancel.Visible = false;
            btnUsrDelete.Visible = false;
            dgvUsers.ClearSelection();
            grpAdminForm.Text = "Create User";
            txtUsrPassword.UseSystemPasswordChar = true;
            btnTogglePassword.Text = "Show";
            txtUsrPassword.BackColor = SystemColors.Window;
            selectedUserId = 0;
        }

        /// <summary>
        /// Enter edit mode for user management
        /// </summary>
        private void EnterUsrEditMode()
        {
            isEditMode = true;
            btnUsrCancel.Visible = true;
            btnUsrDelete.Visible = true;
            txtUsrPassword.Text = string.Empty;
            txtUsrPassword.BackColor = SystemColors.ControlDark;
            isPasswordChanged = false;
            grpAdminForm.Text = "Edit User";
        }

        /// <summary>
        /// Handle password text change to track modifications
        /// </summary>
        private void txtUsrPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtUsrPassword.Text.Length == 0)
            {
                txtUsrPassword.BackColor = SystemColors.ControlDark;
            }
            else
            {
                txtUsrPassword.BackColor = SystemColors.Window;
                if (!isPasswordChanged)
                    isPasswordChanged = true;
            }
        }

        /// <summary>
        /// Handle create user button click
        /// </summary>
        private void btnUsrCreate_Click(object sender, EventArgs e)
        {
            string username = txtUsrUsername.Text.Trim();
            string password = txtUsrPassword.Text.Trim();
            var selectedRole = comboRoles.SelectedItem as RoleDTO;

            if (string.IsNullOrEmpty(username) || selectedRole == null || string.IsNullOrEmpty(password))
            {
                NotificationManager.AddNotification("All fields are required!", NotificationType.Error);
                return;
            }

            try
            {
                userService.CreateUser(username, password, new List<RoleDTO> { selectedRole });
                NotificationManager.AddNotification("User created successfully!", NotificationType.Info);
                LoadUsers();
                ResetUsrForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotificationManager.AddNotification($"Error: {ex.Message}", NotificationType.Error);
            }
        }

        /// <summary>
        /// Handle update user button click
        /// </summary>
        private void btnUsrUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                NotificationManager.AddNotification("Please select a user to update!", NotificationType.Error);
                return;
            }

            string username = txtUsrUsername.Text.Trim();
            var selectedRole = comboRoles.SelectedItem as RoleDTO;

            if (string.IsNullOrEmpty(username) || selectedRole == null)
            {
                NotificationManager.AddNotification("Username and Role are required!", NotificationType.Error);
                return;
            }

            try
            {
                userService.UpdateUser(selectedUserId, username, isPasswordChanged ? txtUsrPassword.Text.Trim() : null, new List<RoleDTO> { selectedRole });
                NotificationManager.AddNotification("User updated successfully!", NotificationType.Info);
                LoadUsers();
                ResetUsrForm();
            }
            catch (Exception ex)
            {
                NotificationManager.AddNotification($"Error: {ex.Message}", NotificationType.Error);
            }
        }

        /// <summary>
        /// Handle display users button click
        /// </summary>
        private void btnUsrDisplay_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        /// <summary>
        /// Handle search user button click
        /// </summary>
        private void btnUsrSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtUsrId.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                NotificationManager.AddNotification("Enter a user id to search!", NotificationType.Error);
                return;
            }

            try
            {
                var user = userService.GetUserById(Convert.ToInt32(searchTerm));
                if (user != null)
                {
                    selectedUserId = user.Id;
                    txtUsrId.Text = selectedUserId.ToString();
                    txtUsrUsername.Text = user.Username;
                    if (user.Roles.Any())
                    {
                        comboRoles.SelectedValue = user.Roles.First().Id;
                    }
                    EnterUsrEditMode();
                    dgvUsers.DataSource = new List<UserDTO> { user };
                }
                else
                {
                    NotificationManager.AddNotification("User not found!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                NotificationManager.AddNotification($"Error: {ex.Message}", NotificationType.Error);
            }
        }

        /// <summary>
        /// Handle user selection from grid
        /// </summary>
        private void dgvUsers_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                selectedUserId = (int)dgvUsers.CurrentRow.Cells["Id"].Value;
                txtUsrId.Text = selectedUserId.ToString();
                var user = userService.GetUserById(selectedUserId);
                txtUsrUsername.Text = user.Username;
                if (user.Roles.Any())
                {
                    comboRoles.SelectedValue = user.Roles.First().Id;
                }
                EnterUsrEditMode();
            }
        }

        /// <summary>
        /// Cancel user form operation
        /// </summary>
        private void btnUsrCancel_Click(object sender, EventArgs e)
        {
            ResetUsrForm();
        }

        /// <summary>
        /// Toggle password visibility
        /// </summary>
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            txtUsrPassword.UseSystemPasswordChar = !txtUsrPassword.UseSystemPasswordChar;
            if (txtUsrPassword.UseSystemPasswordChar)
            {
                btnTogglePassword.Text = "Show";
            }
            else
            {
                btnTogglePassword.Text = "Hide";
            }
        }

        /// <summary>
        /// Delete selected user
        /// </summary>
        private void btnUsrDelete_Click(object sender, EventArgs e)
        {
            try
            {
                userService.DeleteUser(selectedUserId);
                NotificationManager.AddNotification("User deleted successfully!", NotificationType.Info);
                LoadUsers();
                ResetUsrForm();
            }
            catch (Exception ex)
            {
                NotificationManager.AddNotification($"Error: {ex.Message}", NotificationType.Error);
            }
        }

        /// <summary>
        /// Generate a random password
        /// </summary>
        private void btnGenPassword_Click(object sender, EventArgs e)
        {
            string generatedPassword = GeneratePassword();
            txtUsrPassword.Text = generatedPassword;
        }

        public void OnHide()
        {
            // Cleanup when leaving panel
        }

        public void Cleanup()
        {
            // Dispose resources if needed
        }

        private string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
