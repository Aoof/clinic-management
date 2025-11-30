using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.DAL;

namespace ClinicManagement_proj.BLL
{
    public static class ClinicManagementApp
    {
        public static ClinicDbContext DbContext { get; private set; }
        public static UserDTO CurrentUser { get; set; }

        public static UserService UserService { get; private set; }
        public static RoleService RoleService { get; private set; }
        public static LoginService LoginService { get; private set; }
        public static AppointmentService AppointmentService { get; private set; }
        public static DoctorService DoctorService { get; private set; }
        public static PatientService PatientService { get; private set; }

        static ClinicManagementApp()
        {
            DbContext = new ClinicDbContext();
            UserService = new UserService(DbContext);
            RoleService = new RoleService(DbContext);
            LoginService = new LoginService(UserService);
            AppointmentService = new AppointmentService(DbContext);
            DoctorService = new DoctorService(DbContext);
            PatientService = new PatientService(DbContext);
        }
    }
}