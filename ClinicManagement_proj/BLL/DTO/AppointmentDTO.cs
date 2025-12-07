using System;

namespace ClinicManagement_proj.BLL.DTO
{
    public class AppointmentDTO
    {
        public static int NOTES_MAX_LENGTH = 512;

        private string _notes;

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes
        {
            get { return _notes; }
            set { _notes = ValidateNotes(value); }
        }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int TimeSlotId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public PatientDTO Patient { get; set; }
        public DoctorDTO Doctor { get; set; }
        public TimeSlotDTO TimeSlot { get; set; }

        /// <summary>
        /// Initializes a new instance of the AppointmentDTO class.
        /// </summary>
        public AppointmentDTO()
        {
        }

        /// <summary>
        /// Validates the notes string to ensure it does not exceed the maximum length.
        /// </summary>
        /// <param name="notes">The notes string to validate.</param>
        /// <returns>The notes string if valid.</returns>
        /// <exception cref="ArgumentException">Thrown if the notes length exceeds NOTES_MAX_LENGTH.</exception>
        public static string ValidateNotes(string notes)
        {
            if (notes != null && notes.Length > NOTES_MAX_LENGTH)
                throw new ArgumentException($"Notes must be at most {NOTES_MAX_LENGTH} characters.");
            return notes;
        }

        /// <summary>
        /// Initializes a new instance of the AppointmentDTO class with specified parameters.
        /// </summary>
        /// <param name="date">The date of the appointment.</param>
        /// <param name="notes">The notes for the appointment.</param>
        /// <param name="doctorId">The ID of the doctor.</param>
        /// <param name="patientId">The ID of the patient.</param>
        /// <param name="timeSlotId">The ID of the time slot.</param>
        /// <param name="createdAt">The creation date.</param>
        /// <param name="modifiedAt">The modification date.</param>
        public AppointmentDTO(DateTime date, string notes, int doctorId, int patientId, int timeSlotId, DateTime createdAt, DateTime modifiedAt)
        {
            Date = date;
            Notes = notes;
            DoctorId = doctorId;
            PatientId = patientId;
            TimeSlotId = timeSlotId;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }

        /// <summary>
        /// Returns a string representation of the appointment.
        /// </summary>
        /// <returns>A string describing the appointment details.</returns>
        public override string ToString()
        {
            return $"Appointment on {Date.ToShortDateString()} at {TimeSlot?.ToString() ?? "N/A"} with {Doctor?.ToString() ?? "Unknown Doctor"} for {Patient?.ToString() ?? "Unknown Patient"}";
        }
    }
}
