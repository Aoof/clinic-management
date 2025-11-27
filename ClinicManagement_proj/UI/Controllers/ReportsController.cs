using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Reports panel
    /// </summary>
    public class ReportsController : IPanelController
    {
        private readonly Panel panel;

        public Panel Panel => panel;

        public ReportsController(Panel panel)
        {
            this.panel = panel;
        }

        public void Initialize()
        {
            // Setup initial state
        }

        public void OnShow()
        {
            // Refresh reports when panel becomes visible
            // Currently placeholder - to be implemented
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
