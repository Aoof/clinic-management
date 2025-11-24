using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement_proj.BLL.DTO
{
    public class RoleDTO
    {
        public static int ROLENAME_MAXLENGTH = 64;
        public int Id { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<UserDTO> Users { get; set; }

    }
}
