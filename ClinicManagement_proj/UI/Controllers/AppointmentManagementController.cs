using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Doctor Management panel
    /// </summary>
    public class AppointmentManagementController : IPanelController
    {
        private readonly Panel panel;

        public Panel Panel => panel;

        public AppointmentManagementController(Panel panel)
        {
            this.panel = panel;
        }

        public void Initialize()
        {
            // Setup initial state
        }

        public void OnShow()
        {
            // Refresh doctor list when panel becomes visible
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
