using ClinicManagement_proj.BLL;
using System;
using System.Drawing;
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
        private NotificationsController notificationController;

        public DoctorDashboard()
        {
            InitializeComponent();
            InitializeManagers();
            SetupNavigation();
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
            notificationController = new NotificationsController(pnlNotifications, timerToast);

            notificationController.Initialize();
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
        /// Apply styling to navigation buttons
        /// </summary>
        private void StyleButtons()
        {
            ImageHelper.StyleButton(btnPatientRegistration, 30, 30);
            ImageHelper.StyleButton(btnReports, 30, 30);
            ImageHelper.StyleButton(btnAppointmentManagement, 30, 30);

            btnNotifications.Image = ImageHelper.ResizeImage(btnNotifications.Image, 25, 25);
            btnNotifications.ImageAlign = ContentAlignment.MiddleCenter;

            btnLogout.Image = ImageHelper.ResizeImage(btnLogout.Image, 25, 25);
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
            notificationController?.Cleanup();
            base.OnFormClosing(e);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Hide();
            ClinicManagementApp.HasLoggedInBefore = true;
            ClinicManagementApp.CurrentUser = null;
            Form loginForm = new LoginForm();
            loginForm.ShowDialog();
            Close();
        }
    }
}
