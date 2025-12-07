using ClinicManagement_proj.BLL;
using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Notifications panel
    /// </summary>
    public class NotificationsController : IPanelController
    {
        private readonly Panel panel;
        private FlowLayoutPanel flowPanel => (FlowLayoutPanel)panel.Controls.Find("flowLayoutPanelNotifications", true).FirstOrDefault()
            ?? throw new Exception("No control named [flowLayoutPanelNotifications] found.");
        private Button btnNotifications => (Button)panel.Parent.Controls.Find("btnNotifications", true).FirstOrDefault()
            ?? throw new Exception("No control named [btnNotifications] found.");
        private Label lblToast => (Label)panel.Parent.Controls.Find("lblToast", true).FirstOrDefault()
            ?? throw new Exception("No control named [lblToast] found.");
        private Timer timerToast;

        public Panel Panel => panel;

        public NotificationsController(Panel panel, Timer timerToast)
        {
            this.panel = panel;
            this.timerToast = timerToast;
        }

        /// <summary>
        /// Setup notification system
        /// </summary>
        private void SetupNotifications()
        {
            ClinicManagementApp.NotificationService.NotificationAdded += OnNotificationAdded;

            // Configure FlowLayoutPanel for scrolling
            flowPanel.AutoScroll = true;
            flowPanel.WrapContents = false;
            flowPanel.FlowDirection = FlowDirection.TopDown;
        }

        /// <summary>
        /// Handle new notification added event
        /// </summary>
        private void OnNotificationAdded(Notification notif)
        {
            ShowToast(notif);

            // Update list if notifications panel is visible
            if (panel.Visible)
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
            LoadAuditNotifications();
        }

        /// <summary>
        /// Toggle notifications panel visibility
        /// </summary>
        private void btnNotifications_Click(object sender, EventArgs e)
        {
            panel.Visible = !panel.Visible;
            if (panel.Visible)
            {
                RefreshNotificationsList();
            }
        }

        public void Initialize()
        {
            flowPanel.Controls.Clear();
            SetupNotifications();
            btnNotifications.Click += btnNotifications_Click;
            timerToast.Tick += timerToast_Tick;
        }

        public void OnShow()
        {
            LoadAuditNotifications();
        }

        private void LoadAuditNotifications()
        {
            flowPanel.Controls.Clear();

            var audits = ClinicManagementApp.NotificationService.GetAuditNotifications();

            if (audits.Count == 0)
            {
                Label lblNoNotifications = new Label
                {
                    Text = "No audit notifications available.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Margin = new Padding(20)
                };
                flowPanel.Controls.Add(lblNoNotifications);
                return;
            }

            foreach (var audit in audits)
            {
                Panel card = CreateNotificationCard(audit);
                flowPanel.Controls.Add(card);
            }
        }

        /// <summary>
        /// Create a styled notification card
        /// </summary>
        private Panel CreateNotificationCard(AuditAppointmentDTO audit)
        {
            Panel card = new Panel
            {
                Width = flowPanel.Width - 30,
                Height = 135,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10, 10, 10, 10),
                Padding = new Padding(15),
                BackColor = Color.White
            };

            // Add shadow effect with a darker border
            card.Paint += (sender, e) =>
                   {
                       using (Pen shadowPen = new Pen(Color.FromArgb(220, 220, 220), 1))
                       {
                           e.Graphics.DrawRectangle(shadowPen, 0, 0, card.Width - 1, card.Height - 1);
                       }
                   };

            // Status indicator bar on the left
            Panel statusBar = new Panel
            {
                Width = 5,
                Height = card.Height,
                Location = new Point(0, 0),
                BackColor = GetStatusColor(audit.NewStatus)
            };
            card.Controls.Add(statusBar);

            // Status badge in top-right corner
            Label statusBadge = new Label
            {
                Text = audit.NewStatus ?? "PENDING",
                AutoSize = false,
                Size = new Size(90, 24),
                Location = new Point(card.Width - 105, 12),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = GetStatusColor(audit.NewStatus),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Padding = new Padding(5, 3, 5, 3)
            };
            card.Controls.Add(statusBadge);

            // Appointment ID with icon-like prefix
            Label lblAppointmentId = new Label
            {
                Text = $"📋 Appointment #{audit.AppointmentId}",
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            card.Controls.Add(lblAppointmentId);

            // Doctor Name with icon
            Label lblDoctor = new Label
            {
                Text = $"👨‍⚕️ Doctor: {audit.DoctorName}",
                Location = new Point(20, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80)
            };
            card.Controls.Add(lblDoctor);

            // Patient Name with icon
            Label lblPatient = new Label
            {
                Text = $"👤 Patient: {audit.PatientName}",
                Location = new Point(20, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80)
            };
            card.Controls.Add(lblPatient);

            // Audit Date
            Label lblDate = new Label
            {
                Text = $"🕒 {audit.AuditDate:MMM dd, yyyy hh:mm tt}",
                Location = new Point(20, 95),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 120, 120)
            };
            card.Controls.Add(lblDate);

            return card;
        }

        /// <summary>
        /// Get color based on appointment status
        /// </summary>
        private Color GetStatusColor(string status)
        {
            if (string.IsNullOrEmpty(status))
                return Color.FromArgb(158, 158, 158); // Gray

            switch (status.ToUpper())
            {
                case "CONFIRMED":
                    return Color.FromArgb(76, 175, 80); // Material Green
                case "CANCELLED":
                    return Color.FromArgb(244, 67, 54); // Material Red
                case "PENDING":
                    return Color.FromArgb(255, 152, 0); // Material Orange
                case "COMPLETED":
                    return Color.FromArgb(33, 150, 243); // Material Blue
                default:
                    return Color.FromArgb(158, 158, 158); // Gray
            }
        }

        public void Cleanup()
        {
            flowPanel.Controls.Clear();
            ClinicManagementApp.NotificationService.NotificationAdded -= OnNotificationAdded;
            btnNotifications.Click -= btnNotifications_Click;
            timerToast.Tick -= timerToast_Tick;
        }

        public void OnHide()
        {
        }
    }
}