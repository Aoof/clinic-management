using System;
using System.Collections.Generic;

namespace ClinicManagement_proj.BLL.DTO
{
    public class RoleDTO
    {
        public static int ROLENAME_MAXLENGTH = 64;

        private string _roleName;

        public int Id { get; set; }
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = ValidateRoleName(value); }
        }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<UserDTO> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the RoleDTO class.
        /// </summary>
        public RoleDTO()
        {
            Users = new List<UserDTO>();
        }

        /// <summary>
        /// Validates the role name string.
        /// </summary>
        /// <param name="roleName">The role name to validate.</param>
        /// <returns>The validated role name.</returns>
        /// <exception cref="ArgumentException">Thrown if role name is null, empty, or exceeds max length.</exception>
        public static string ValidateRoleName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("RoleName cannot be null or empty.");
            if (roleName.Length > ROLENAME_MAXLENGTH)
                throw new ArgumentException($"RoleName must be at most {ROLENAME_MAXLENGTH} characters.");
            return roleName;
        }

        /// <summary>
        /// Initializes a new instance of the RoleDTO class with specified parameters.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="createdAt">The creation date.</param>
        /// <param name="modifiedAt">The modification date.</param>
        public RoleDTO(string roleName, DateTime createdAt, DateTime modifiedAt)
        {
            RoleName = roleName;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            Users = new List<UserDTO>();
        }

        /// <summary>
        /// Initializes a new instance of the RoleDTO class with specified parameters including ID and users.
        /// </summary>
        /// <param name="id">The ID of the role.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="createdAt">The creation date.</param>
        /// <param name="modifiedAt">The modification date.</param>
        /// <param name="users">The collection of users.</param>
        public RoleDTO(int id, string roleName, DateTime createdAt, DateTime modifiedAt, ICollection<UserDTO> users = null)
        {
            Id = id;
            RoleName = roleName;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            Users = users ?? new List<UserDTO>();
        }

        /// <summary>
        /// Returns a string representation of the role.
        /// </summary>
        /// <returns>The role name.</returns>
        public override string ToString()
        {
            return RoleName;
        }
    }
}
