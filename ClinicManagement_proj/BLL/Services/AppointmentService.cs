using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    /// <summary>
    /// Provides services for managing appointments.
    /// </summary>
    public class AppointmentService
    {
        private readonly ClinicDbContext clinicDb;

        /// <summary>
        /// Initializes a new instance of the AppointmentService class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AppointmentService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">The appointment to create.</param>
        /// <returns>The created appointment.</returns>
        public AppointmentDTO CreateAppointment(AppointmentDTO appointment)
        {
            clinicDb.Appointments.Add(appointment);
            clinicDb.SaveChanges();
            return appointment;
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointment">The appointment to update.</param>
        /// <returns>The updated appointment.</returns>
        public AppointmentDTO UpdateAppointment(AppointmentDTO appointment)
        {
            clinicDb.SaveChanges();
            return appointment;
        }

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="appointment">The appointment to delete.</param>
        public void DeleteAppointment(AppointmentDTO appointment)
        {
            clinicDb.Appointments.Remove(appointment);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Gets all appointments.
        /// </summary>
        /// <returns>A list of all appointments.</returns>
        public List<AppointmentDTO> GetAllAppointments()
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .ToList();
        }

        /// <summary>
        /// Searches appointments by ID.
        /// </summary>
        /// <param name="id">The ID to search for.</param>
        /// <returns>A list of appointments matching the ID.</returns>
        public List<AppointmentDTO> Search(int id)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.Id.ToString().Contains(id.ToString()))
                .ToList();
        }

        /// <summary>
        /// Searches appointments by date.
        /// </summary>
        /// <param name="date">The date to search for.</param>
        /// <returns>A list of appointments on the specified date.</returns>
        public List<AppointmentDTO> Search(DateTime date)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => DateTime.Compare(a.Date, date.Date) == 0)
                .ToList();
        }

        /// <summary>
        /// Searches appointments by doctor.
        /// </summary>
        /// <param name="doctor">The doctor to search for.</param>
        /// <returns>A list of appointments with the specified doctor.</returns>
        public List<AppointmentDTO> Search(DoctorDTO doctor)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.DoctorId == doctor.Id)
                .ToList();
        }

        /// <summary>
        /// Searches appointments by patient.
        /// </summary>
        /// <param name="patient">The patient to search for.</param>
        /// <returns>A list of appointments with the specified patient.</returns>
        public List<AppointmentDTO> Search(PatientDTO patient)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.PatientId == patient.Id)
                .ToList();
        }

        /// <summary>
        /// Gets a time slot by ID.
        /// </summary>
        /// <param name="id">The ID of the time slot.</param>
        /// <returns>The time slot with the specified ID.</returns>
        public TimeSlotDTO GetTimeSlotById(int id)
        {
            return clinicDb.TimeSlots.Find(id);
        }

        /// <summary>
        /// Gets available time slots for a doctor on a specific date.
        /// </summary>
        /// <param name="doctorId">The ID of the doctor.</param>
        /// <param name="date">The date.</param>
        /// <returns>A list of available time slots.</returns>
        public List<TimeSlotDTO> GetAvailableTimeSlots(int doctorId, DateTime date)
        {
            var results = clinicDb.Database.SqlQuery<sp_GetAvailableSlots_Result>("EXEC sp_GetAvailableSlots @p0, @p1", doctorId, date).ToList();
            return results.Select(r => GetTimeSlotById(r.TimeSlotId)).ToList();
        }
    }
}
