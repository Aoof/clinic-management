using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    public class AppointmentService
    {
        private readonly ClinicDbContext clinicDb;

        public AppointmentService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        public AppointmentDTO CreateAppointment(AppointmentDTO appointment)
        {
            clinicDb.Appointments.Add(appointment);
            clinicDb.SaveChanges();
            return appointment;
        }

        public AppointmentDTO UpdateAppointment(AppointmentDTO appointment)
        {
            clinicDb.SaveChanges();
            return appointment;
        }

        public void DeleteAppointment(AppointmentDTO appointment)
        {
            clinicDb.Appointments.Remove(appointment);
            clinicDb.SaveChanges();
        }

        public List<AppointmentDTO> GetAllAppointments()
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .ToList();
        }

        public List<AppointmentDTO> Search(int id)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.Id.ToString().Contains(id.ToString()))
                .ToList();
        }

        public List<AppointmentDTO> Search(DateTime date)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => DateTime.Compare(a.Date, date.Date) == 0)
                .ToList();
        }

        public List<AppointmentDTO> Search(DoctorDTO doctor)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.DoctorId == doctor.Id)
                .ToList();
        }

        public List<AppointmentDTO> Search(PatientDTO patient)
        {
            return clinicDb.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.PatientId == patient.Id)
                .ToList();
        }

        public TimeSlotDTO GetTimeSlotById(int id)
        {
            return clinicDb.TimeSlots.Find(id);
        }

        public List<TimeSlotDTO> GetAvailableTimeSlots(int doctorId, DateTime date)
        {
            var results = clinicDb.Database.SqlQuery<sp_GetAvailableSlots_Result>("EXEC sp_GetAvailableSlots @p0, @p1", doctorId, date).ToList();
            return results.Select(r => GetTimeSlotById(r.TimeSlotId)).ToList();
        }
    }
}
