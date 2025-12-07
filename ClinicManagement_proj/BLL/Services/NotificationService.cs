using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Utils;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    /// <summary>
    /// Service class to manage notifications.
    /// </summary>
    public class NotificationService
    {
        private readonly ClinicDbContext clinicDb;
        private static List<Notification> notifications = new List<Notification>();

        /// <summary>
        /// Event raised when a notification is added.
        /// </summary>
        public event Action<Notification> NotificationAdded;

        /// <summary>
        /// Initializes a new instance of the NotificationService class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public NotificationService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        /// <summary>
        /// Adds a new notification.
        /// </summary>
        /// <param name="message">The notification message.</param>
        /// <param name="type">The type of notification.</param>
        public void AddNotification(string message, NotificationType type)
        {
            var notif = new Notification(message, type);
            lock (notifications)
            {
                notifications.Add(notif);
                // Clean up old ones
                for (int i = notifications.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Now.Subtract(notifications[i].Timestamp).TotalMinutes > 30)
                    {
                        notifications.RemoveAt(i);
                    }
                }
            }
            NotificationAdded?.Invoke(notif);
        }

        /// <summary>
        /// Gets the active notifications.
        /// </summary>
        /// <returns>A list of active notifications.</returns>
        public List<Notification> GetActiveNotifications()
        {
            lock (notifications)
            {
                List<Notification> active = new List<Notification>();
                foreach (var n in notifications)
                {
                    if (DateTime.Now.Subtract(n.Timestamp).TotalMinutes <= 30)
                    {
                        active.Add(n);
                    }
                }
                return active;
            }
        }

        /// <summary>
        /// Gets the audit notifications.
        /// </summary>
        /// <returns>A list of audit appointment notifications.</returns>
        public List<AuditAppointmentDTO> GetAuditNotifications()
        {
            return clinicDb.AuditAppointments
                .OrderByDescending(a => a.AuditDate)
                .ToList();
        }
    }
}