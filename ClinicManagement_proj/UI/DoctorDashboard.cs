using ClinicManagement_proj.BLL.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    public partial class DoctorDashboard : Form
    {
        private readonly Color SIDEBAR_BG = Color.FromArgb(44, 62, 80);
        private readonly Color SIDEBAR_ACTIVE = Color.FromArgb(52, 73, 94);
        private readonly Color HEADER_BG = Color.FromArgb(41, 128, 185);

        private NavigationManager navigationManager;
        private PatientRegistrationController patientRegistrationController;
        private ApptMgmtController appointmentManagementController;
        private ReportsController reportsController;

        public DoctorDashboard()
        {
            InitializeComponent();
            InitializeManagers();
            SetupNavigation();
            SetupNotifications();
            StyleButtons();
        }

        /// <summary>
        /// Initialize all managers and controllers
        /// </summary>
        private void InitializeManagers()
        {
            navigationManager = new NavigationManager(SIDEBAR_BG, SIDEBAR_ACTIVE);

            // Initialize panel controllers
            patientRegistrationController = new PatientRegistrationController(pnlPatientRegistration);
            reportsController = new ReportsController(pnlReports);
            appointmentManagementController = new ApptMgmtController(pnlAppointmentManagement);
        }

        /// <summary>
        /// Setup navigation between panels
        /// </summary>
        private void SetupNavigation()
        {
            navigationManager.RegisterPanel(btnPatientRegistration, patientRegistrationController);
            navigationManager.RegisterPanel(btnReports, reportsController);
            navigationManager.RegisterPanel(btnAppointmentManagement, appointmentManagementController);

            navigationManager.InitializeAll();
            navigationManager.NavigateTo(btnReports);
        }

        /// <summary>
        /// Setup notification system
        /// </summary>
        private void SetupNotifications()
        {
            NotificationManager.NotificationAdded += OnNotificationAdded;
        }

        /// <summary>
        /// Apply styling to navigation buttons
        /// </summary>
        private void StyleButtons()
        {
            ImageHelper.StyleButton(btnPatientRegistration, 30, 30);
            ImageHelper.StyleButton(btnReports, 30, 30);
            ImageHelper.StyleButton(btnAppointmentManagement, 30, 30);

            btnNotifications.Image = ImageHelper.ResizeImage(btnNotifications.Image, 25, 25);
            btnNotifications.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void DoctorDashboard_Load(object sender, EventArgs e)
        {
        }

        // Navigation Methods - Now simplified using NavigationManager
        private void btnReports_Click(object sender, EventArgs e)
        {
            navigationManager.NavigateTo(btnReports);
        }

        private void btnPatientRegistration_Click(object sender, EventArgs e)
        {
            navigationManager.NavigateTo(btnPatientRegistration);
        }

        private void btnAppointmentManagement_Click(object sender, EventArgs e)
        {
            navigationManager.NavigateTo(btnAppointmentManagement);
        }

        /// <summary>
        /// Cleanup resources on form closing
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            navigationManager?.CleanupAll();
            ImageHelper.ClearCache();
            NotificationManager.NotificationAdded -= OnNotificationAdded;
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Handle new notification added event
        /// </summary>
        private void OnNotificationAdded(Notification notif)
        {
            ShowToast(notif);

            // Update list if notifications panel is visible
            if (pnlNotifications.Visible)
            {
                RefreshNotificationsList();
            }
        }

        /// <summary>
        /// Show a toast notification
        /// </summary>
        private void ShowToast(Notification notif)
        {
            timerToast.Stop();
            lblToast.Text = notif.Message;
            lblToast.BackColor = GetNotificationColor(notif.Type);
            lblToast.Visible = true;
            timerToast.Start();
        }

        /// <summary>
        /// Get color based on notification type
        /// </summary>
        private Color GetNotificationColor(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Error:
                    return Color.Red;
                case NotificationType.Warning:
                    return Color.Orange;
                default:
                    return Color.Green;
            }
        }

        /// <summary>
        /// Timer tick to hide toast notification
        /// </summary>
        private void timerToast_Tick(object sender, EventArgs e)
        {
            lblToast.Visible = false;
            timerToast.Stop();
        }

        /// <summary>
        /// Refresh the notifications list
        /// </summary>
        private void RefreshNotificationsList()
        {
            lbNotifications.Items.Clear();
            foreach (var n in NotificationManager.GetActiveNotifications().OrderByDescending(n => n.Timestamp))
            {
                lbNotifications.Items.Add($"{n.Timestamp:HH:mm:ss} - {n.Type}: {n.Message}");
            }
        }

        /// <summary>
        /// Toggle notifications panel visibility
        /// </summary>
        private void btnNotifications_Click(object sender, EventArgs e)
        {
            pnlNotifications.Visible = !pnlNotifications.Visible;
            if (pnlNotifications.Visible)
            {
                RefreshNotificationsList();
            }
        }
    }
}
