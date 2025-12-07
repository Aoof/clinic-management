using ClinicManagement_proj.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    /// <summary>
    /// Provides methods to retrieve various views related to patients and doctors.
    /// </summary>
    public class ViewsService
    {
        private readonly ClinicDbContext _context;

        /// <summary>
        /// Initializes a new instance of the ViewsService class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ViewsService(ClinicDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the patient records summary.
        /// </summary>
        /// <param name="patientId">Optional patient ID to filter.</param>
        /// <returns>A list of patient records summaries.</returns>
        public List<vw_PatientRecordsSummary> GetPatientRecordsSummary(int? patientId = null)
        {
            var query = _context.vw_PatientRecordsSummary.AsQueryable();
            if (patientId.HasValue)
            {
                query = query.Where(v => v.PatientId == patientId.Value);
            }
            // Only return recent visits (top N per patient)
            return query.OrderBy(v => v.PatientId)
                .ThenBy(v => v.VisitNumber)
                .ToList();
        }

        /// <summary>
        /// Gets the upcoming appointments.
        /// </summary>
        /// <param name="doctorId">Optional doctor ID to filter.</param>
        /// <returns>A list of upcoming appointments.</returns>
        public List<vw_UpcomingAppointments> GetUpcomingAppointments(int? doctorId = null)
        {
            var query = _context.vw_UpcomingAppointments.AsQueryable();
            if (doctorId.HasValue)
            {
                query = query.Where(v => v.DoctorId == doctorId.Value);
            }
            return query.OrderBy(v => v.AppointmentDate)
                .ThenBy(v => v.HourOfDay)
                .ThenBy(v => v.MinuteOfHour)
                .ToList();
        }

        /// <summary>
        /// Gets the doctor's schedule for today.
        /// </summary>
        /// <param name="doctorId">Optional doctor ID to filter.</param>
        /// <returns>A list of today's schedules.</returns>
        public List<vw_DoctorTodaySchedule> GetDoctorTodaySchedule(int? doctorId = null)
        {
            var query = _context.vw_DoctorTodaySchedule.AsQueryable();
            if (doctorId.HasValue)
            {
                query = query.Where(v => v.DoctorId == doctorId.Value);
            }
            return query.OrderBy(v => v.DoctorId)
                .ThenBy(v => v.HourOfDay)
                .ThenBy(v => v.MinuteOfHour)
                .ToList();
        }

        /// <summary>
        /// Gets the patient clinical summary.
        /// </summary>
        /// <param name="patientId">Optional patient ID to filter.</param>
        /// <returns>A list of patient clinical summaries.</returns>
        public List<vw_PatientClinicalSummary> GetPatientClinicalSummary(int? patientId = null)
        {
            var query = _context.vw_PatientClinicalSummary.AsQueryable();
            if (patientId.HasValue)
            {
                query = query.Where(v => v.PatientId == patientId.Value);
            }
            return query.OrderBy(v => v.PatientName).ToList();
        }
    }
}
