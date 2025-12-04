using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement_proj.BLL.DTO
{
    public class AuditAppointmentDTO
    {
        public int AuditId { get; set; }
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string NewStatus { get; set; }
        public DateTime AuditDate { get; set; }
    }
}
