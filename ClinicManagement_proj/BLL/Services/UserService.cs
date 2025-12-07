using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;

namespace ClinicManagement_proj.BLL.Services
{
    public class UserService
    {
        private ClinicDbContext clinicDb;

        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UserService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        public enum UserRoles
        {
            Administrator,
            Doctor,
            Receptionist
        }

        /// <summary>
        /// Authenticates a user with username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The authenticated user or null.</returns>
        public UserDTO Authenticate(string username, string password)
        {
            try
            {
                var user = GetUserByUsername(username);
                if (user != null && ComparePassword(password, user.PasswordHash))
                {
                    return user;
                }
            } catch (Exception) { }
            return null;
        }

        private static string HashPassword(string plainPassword)
        {
            UserDTO.ValidatePassword(plainPassword);
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Compares a plain password with a stored hash.
        /// </summary>
        /// <param name="plainPassword">The plain password.</param>
        /// <param name="storedHash">The stored hash.</param>
        /// <returns>True if passwords match, otherwise false.</returns>
        public static bool ComparePassword(string plainPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public List<UserDTO> GetAllUsers()
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can access the list of all users.");

            return clinicDb.Users
                .Include(u => u.Roles)
                .ToList();
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>A list of all roles.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public List<RoleDTO> GetAllRoles()
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can access roles.");

            return clinicDb.Roles
                .Include(r => r.Users)
                .ToList();
        }

        /// <summary>
        /// Gets a user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user with the specified username.</returns>
        /// <exception cref="ArgumentException">Thrown if the user is not found.</exception>
        public UserDTO GetUserByUsername(string username)
        {
            var user = clinicDb.Users
                .Where(u => u.Username == username)
                .Include(u => u.Roles)
                .SingleOrDefault()
                ?? throw new ArgumentException("User not found");

            return user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        /// <exception cref="ArgumentException">Thrown if the username already exists.</exception>
        public void CreateUser(UserDTO user, string password)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can create new users.");

            if (clinicDb.Users.Any(u => u.Username.ToLower() == user.Username.ToLower()))
                throw new ArgumentException("Username already exists");

            user.PasswordHash = HashPassword(password);
            clinicDb.Users.Add(user);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="password">The new password, optional.</param>
        /// <returns>The updated user.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public UserDTO UpdateUser(UserDTO user, string password = null)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can update users.");

            if (!string.IsNullOrEmpty(password))
                user.PasswordHash = HashPassword(password);

            clinicDb.SaveChanges();
            return user;
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        /// <exception cref="ArgumentException">Thrown if the user is not found.</exception>
        public void DeleteUser(int id)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can delete users.");

            var user = clinicDb.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefault()
                    ?? throw new ArgumentException("User not found");

            clinicDb.Users.Remove(user);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public void DeleteUser(UserDTO user)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can delete users.");

            clinicDb.Users.Remove(user);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Searches users by ID.
        /// </summary>
        /// <param name="id">The ID to search for.</param>
        /// <returns>A list of users matching the ID.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public List<UserDTO> Search(int id)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can search users.");

            return clinicDb.Users
                .Where(u => u.Id.ToString().Contains(id.ToString()))
                .Include(u => u.Roles)
                .ToList();
        }

        /// <summary>
        /// Searches users by username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>A list of users matching the username.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public List<UserDTO> Search(string username)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can search users.");

            return clinicDb.Users
                .Where(u => u.Username.Contains(username))
                .Include(u => u.Roles)
                .ToList();
        }

    }
}
