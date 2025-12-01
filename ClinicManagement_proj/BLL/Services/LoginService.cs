using ClinicManagement_proj.BLL.DTO;

namespace ClinicManagement_proj.BLL.Services
{
    public class LoginService
    {
        private readonly UserService _userService;

        public LoginService(UserService userService)
        {
            _userService = userService;
        }

        public UserDTO Authenticate(string username, string password)
        {
            var user = _userService.GetUserByUsername(username);
            if (user != null && UserService.ComparePassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }
    }
}