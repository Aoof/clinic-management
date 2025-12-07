using System;
using System.Collections.Generic;

namespace ClinicManagement_proj.BLL.DTO
{
    public class TimeSlotDTO
    {
        private int _hourOfDay;
        private int _minuteOfHour;

        public int Id { get; set; }
        public int HourOfDay
        {
            get { return _hourOfDay; }
            set { _hourOfDay = ValidateHourOfDay(value); }
        }
        public int MinuteOfHour
        {
            get { return _minuteOfHour; }
            set { _minuteOfHour = ValidateMinuteOfHour(value); }
        }
        public ICollection<AppointmentDTO> Appointments { get; set; }

        /// <summary>
        /// Initializes a new instance of the TimeSlotDTO class.
        /// </summary>
        public TimeSlotDTO()
        {
            Appointments = new List<AppointmentDTO>();
        }

        /// <summary>
        /// Validates the hour of the day.
        /// </summary>
        /// <param name="hourOfDay">The hour to validate.</param>
        /// <returns>The validated hour.</returns>
        /// <exception cref="ArgumentException">Thrown if hour is not between 0 and 23.</exception>
        public static int ValidateHourOfDay(int hourOfDay)
        {
            if (hourOfDay < 0 || hourOfDay > 23)
                throw new ArgumentException("HourOfDay must be between 0 and 23.");
            return hourOfDay;
        }

        /// <summary>
        /// Validates the minute of the hour.
        /// </summary>
        /// <param name="minuteOfHour">The minute to validate.</param>
        /// <returns>The validated minute.</returns>
        /// <exception cref="ArgumentException">Thrown if minute is not between 0 and 59.</exception>
        public static int ValidateMinuteOfHour(int minuteOfHour)
        {
            if (minuteOfHour < 0 || minuteOfHour > 59)
                throw new ArgumentException("MinuteOfHour must be between 0 and 59.");
            return minuteOfHour;
        }

        /// <summary>
        /// Initializes a new instance of the TimeSlotDTO class with specified parameters.
        /// </summary>
        /// <param name="hourOfDay">The hour of the day.</param>
        /// <param name="minuteOfHour">The minute of the hour.</param>
        public TimeSlotDTO(int hourOfDay, int minuteOfHour)
        {
            HourOfDay = hourOfDay;
            MinuteOfHour = minuteOfHour;
            Appointments = new List<AppointmentDTO>();
        }

        /// <summary>
        /// Returns a string representation of the time slot.
        /// </summary>
        /// <returns>A string in HH:MM format.</returns>
        public override string ToString()
        {
            return $"{HourOfDay:D2}:{MinuteOfHour:D2}";
        }
    }
}
