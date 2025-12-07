using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement_proj.BLL.DTO
{
    public class DoctorScheduleDTO
    {
        public static int DAYOFWEEK_MAX_LENGTH = 10;

        private string _dayOfWeek;

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DayOfWeek
        {
            get { return _dayOfWeek; }
            set { _dayOfWeek = ValidateDayOfWeek(value); }
        }
        [NotMapped]
        public DaysOfWeekEnum DayOfWeekEnum
        {
            get { return (DaysOfWeekEnum)Enum.Parse(typeof(DaysOfWeekEnum), DayOfWeek); }
            set { DayOfWeek = value.ToString(); }
        }
        public TimeSpan WorkStartTime { get; set; }
        public TimeSpan WorkEndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DoctorDTO Doctor { get; set; }

        /// <summary>
        /// Initializes a new instance of the DoctorScheduleDTO class.
        /// </summary>
        public DoctorScheduleDTO()
        {
        }

        /// <summary>
        /// Validates the day of the week string.
        /// </summary>
        /// <param name="dayOfWeek">The day of the week to validate.</param>
        /// <returns>The validated day of the week.</returns>
        /// <exception cref="ArgumentException">Thrown if day of week is null, empty, or exceeds max length.</exception>
        public static string ValidateDayOfWeek(string dayOfWeek)
        {
            if (string.IsNullOrWhiteSpace(dayOfWeek))
                throw new ArgumentException("DayOfWeek cannot be null or empty.");
            if (dayOfWeek.Length > DAYOFWEEK_MAX_LENGTH)
                throw new ArgumentException($"DayOfWeek must be at most {DAYOFWEEK_MAX_LENGTH} characters.");
            return dayOfWeek;
        }

        /// <summary>
        /// Initializes a new instance of the DoctorScheduleDTO class with specified parameters.
        /// </summary>
        /// <param name="doctorId">The ID of the doctor.</param>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <param name="workStartTime">The work start time.</param>
        /// <param name="workEndTime">The work end time.</param>
        /// <param name="createdAt">The creation date.</param>
        /// <param name="modifiedAt">The modification date.</param>
        public DoctorScheduleDTO(int doctorId, DaysOfWeekEnum dayOfWeek, TimeSpan workStartTime, TimeSpan workEndTime, DateTime createdAt, DateTime modifiedAt)
        {
            DoctorId = doctorId;
            DayOfWeekEnum = dayOfWeek;
            WorkStartTime = workStartTime;
            WorkEndTime = workEndTime;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }
    }
}
