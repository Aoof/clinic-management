using ClinicManagement_proj.DAL;
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
        private FlowLayoutPanel flowPanel;

        public Panel Panel => panel;

        public NotificationsController(Panel panel)
        {
            this.panel = panel;

            // Create FlowLayoutPanel dynamically inside notifications panel
            flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Name = "flowLayoutPanelNotifications"
            };
            panel.Controls.Add(flowPanel);
        }

        public void Initialize()
        {
            flowPanel.Controls.Clear();
        }

        public void OnShow()
        {
            LoadAuditNotifications();
        }

        private void LoadAuditNotifications()
        {
            flowPanel.Controls.Clear();

            using (var context = new ClinicDbContext())
            {
                var audits = context.AuditAppointments
                                    .OrderByDescending(a => a.AuditDate)
                                    .ToList();

                foreach (var audit in audits)
                {
                    Panel card = new Panel
                    {
                        Width = 350,
                        Height = 120,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(10),
                        BackColor = audit.NewStatus == "CONFIRMED" ? Color.LightGreen :
                                    audit.NewStatus == "CANCELLED" ? Color.LightCoral : Color.WhiteSmoke
                    };

                    card.Controls.Add(new Label { Text = $"Appointment Id: {audit.AppointmentId}", Location = new Point(10, 10), AutoSize = true });
                    card.Controls.Add(new Label { Text = $"Doctor Name: {audit.DoctorName}", Location = new Point(10, 35), AutoSize = true });
                    card.Controls.Add(new Label { Text = $"Patient Name: {audit.PatientName}", Location = new Point(10, 60), AutoSize = true });
                    card.Controls.Add(new Label { Text = $"New Status: {audit.NewStatus}", Location = new Point(10, 85), AutoSize = true });

                    flowPanel.Controls.Add(card);
                }
            }
        }

        public void Cleanup()
        {
            flowPanel.Controls.Clear();
        }

        public void OnHide()
        {
            throw new NotImplementedException();
        }
    }
}