using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement_proj.BLL.DTO
{
    public class DoctorScheduleDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DaysOfWeekEnum DayOfWeek { get; set; }
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
            DayOfWeek = dayOfWeek;
            WorkStartTime = workStartTime;
            WorkEndTime = workEndTime;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }
    }
}
