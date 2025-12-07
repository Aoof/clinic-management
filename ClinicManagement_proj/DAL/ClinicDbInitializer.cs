using ClinicManagement_proj.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;

namespace ClinicManagement_proj.DAL
{
    public class ClinicDbInitializer : CreateDatabaseIfNotExists<ClinicDbContext>
    {
        protected override void Seed(ClinicDbContext context)
        {
            // Prepare roles
            var roles = new List<RoleDTO>
            {
                new RoleDTO { RoleName = "Administrator", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RoleDTO { RoleName = "Doctor", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RoleDTO { RoleName = "Receptionist", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
            };
            context.Roles.AddRange(roles);

            // Prepare specialties
            var specialties = new List<SpecialtyDTO>
            {
                new SpecialtyDTO { Name = "General Practice" },
                new SpecialtyDTO { Name = "Cardiology" },
                new SpecialtyDTO { Name = "Pediatrics" },
                new SpecialtyDTO { Name = "Dermatology" },
                new SpecialtyDTO { Name = "Neurology" },
                new SpecialtyDTO { Name = "Orthopedics" }
            };
            context.Specialties.AddRange(specialties);

            // Prepare time slots
            var timeSlots = new List<TimeSlotDTO>();
            for (int hour = 8; hour < 18; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    timeSlots.Add(new TimeSlotDTO { HourOfDay = hour, MinuteOfHour = minute });
                }
            }
            context.TimeSlots.AddRange(timeSlots);

            // Prepare admin user
            string username = ConfigurationManager.AppSettings.Get("AdminUsername");
            string password = ConfigurationManager.AppSettings.Get("AdminPassword");
            string hashedPassword = HashPassword(password);
            var adminUser = new UserDTO { Username = username, PasswordHash = hashedPassword, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now };
            context.Users.Add(adminUser);

            // Assign role to user (after adding to context)
            var adminRole = roles.First(r => r.RoleName == "Administrator");
            adminUser.Roles.Add(adminRole);

            // Create trigger (raw SQL for DDL)
            context.Database.ExecuteSqlCommand(@"
                CREATE TRIGGER trg_AuditAppointmentStatus
                ON dbo.Appointment
                AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    INSERT INTO dbo.Audit_Appointment (AppointmentId, PatientName, DoctorName, NewStatus)
                    SELECT 
                        i.Id,
                        p.FirstName + ' ' + p.LastName AS PatientName,
                        d.FirstName + ' ' + d.LastName AS DoctorName,
                        i.Status
                    FROM inserted i
                    INNER JOIN deleted d_old ON i.Id = d_old.Id
                    INNER JOIN dbo.Patient p ON i.PatientId = p.Id
                    INNER JOIN dbo.Doctor d ON i.DoctorId = d.Id
                    WHERE i.Status IN ('CONFIRMED', 'CANCELLED')
                      AND i.Status <> d_old.Status;
                END;
            ");
        }

        private static string HashPassword(string plainPassword)
        {
            UserDTO.ValidatePassword(plainPassword);
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}