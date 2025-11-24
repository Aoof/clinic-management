using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement_proj.BLL.DTO
{
    public class DoctorDTO
    {
        public static int FIRSTNAME_MAX_LENGTH = 64;
        public static int LASTNAME_MAX_LENGTH = 64;
        public int LICENSE_MAX_LENGTH = 24; 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<AppointmentDTO> Appointments { get; set; }
        public List<ScheduleConstraintDTO> ScheduleConstraints { get; set; }
        public List<SpecialtyDTO> Specialties { get; set; }
    }
}
