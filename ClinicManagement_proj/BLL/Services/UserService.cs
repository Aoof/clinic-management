using System;
using System.Collections.Generic;
using System.Linq;
using ClinicManagement_proj.DAL;

namespace ClinicManagement_proj.BLL.Services
{
    internal class UserService
    {
        private ClinicDbContext clinicDb = new ClinicDbContext();

        public List<User> GetAllUsers()
        { 
            return clinicDb.Users.ToList();
        }
    }
}
