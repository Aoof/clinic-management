using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Doctor Scheduling panel
    /// </summary>
    public class SchedulingController : IPanelController
    {
        private readonly Panel panel;

        public Panel Panel => panel;

        public SchedulingController(Panel panel)
        {
            this.panel = panel;
        }

        public void Initialize()
        {
            // Setup scheduling views
        }

        public void OnShow()
        {
            // Refresh scheduling data when panel becomes visible
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
