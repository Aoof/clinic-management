using System.Windows.Forms;
using ClinicManagement_proj.BLL.Services;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the User Management panel
    /// </summary>
    public class UserManagementController : IPanelController
    {
        private readonly Panel panel;
        private readonly UserService userService;
        private DataGridView dgvUsers => (DataGridView)panel.Controls["dgvUsers"];

        public Panel Panel => panel;

        public UserManagementController(Panel panel)
        {
            this.panel = panel;
            this.userService = new UserService();
        }

        public void Initialize()
        {
            // Setup initial state, data bindings, etc.
            // This would be called once during dashboard initialization
        }

        public void OnShow()
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = userService.GetAllUsers();
            dgvUsers.DataSource = users;
        }

        public void OnHide()
        {
            // Cleanup when leaving panel
        }

        public void Cleanup()
        {
            // Dispose resources if needed
        }
    }
}
