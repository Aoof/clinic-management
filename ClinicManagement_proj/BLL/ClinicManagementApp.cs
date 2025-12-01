using System;
using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.DAL;
using System.Linq;

namespace ClinicManagement_proj.BLL
{
    public static class ClinicManagementApp
    {
        public static ClinicDbContext DbContext { get; } = new ClinicDbContext();
        public static UserDTO CurrentUser { get; set; }

        public static UserService UserService { get; } = new UserService(DbContext);
        public static LoginService LoginService { get; } = new LoginService(UserService);
        public static AppointmentService AppointmentService { get; } = new AppointmentService(DbContext);
        public static DoctorService DoctorService { get; } = new DoctorService(DbContext);
        public static PatientService PatientService { get; } = new PatientService(DbContext);
        public static DoctorScheduleService DoctorScheduleService { get; } = new DoctorScheduleService(DbContext);

        public static bool CurrentUserHasRole(UserService.UserRoles role)
        {
            if (CurrentUser == null) return false;
            return CurrentUser.Roles.Any(r => r.RoleName == role.ToString());
        }

        public static bool CurrentUserHasRole(params UserService.UserRoles[] roles)
        {
            if (ClinicManagementApp.CurrentUser == null) return false;
            var roleNames = roles.Select(r => r.ToString()).ToList();
            return ClinicManagementApp.CurrentUser.Roles.Any(r => roleNames.Contains(r.RoleName));
        }
    }
}