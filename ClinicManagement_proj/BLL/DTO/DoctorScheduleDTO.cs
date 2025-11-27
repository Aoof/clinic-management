using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement_proj.BLL.DTO
{
    public class DoctorScheduleDTO
    {
        public static int DAYOFWEEK_MAX_LENGTH = 10;

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DayOfWeek { get; set; }
        [NotMapped]
        public DaysOfWeekEnum DayOfWeekEnum
        {
            get { return (DaysOfWeekEnum)Enum.Parse(typeof(DaysOfWeekEnum), DayOfWeek); }
            set { DayOfWeek = value.ToString(); }
        }
        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DoctorDTO Doctor { get; set; }

        public DoctorScheduleDTO()
        {
        }

        public DoctorScheduleDTO(int doctorId, DaysOfWeekEnum dayOfWeek, DateTime workStartTime, DateTime workEndTime, DateTime createdAt, DateTime modifiedAt)
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
