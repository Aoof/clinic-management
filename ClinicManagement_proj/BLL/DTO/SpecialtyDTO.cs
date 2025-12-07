using System;
using System.Collections.Generic;

namespace ClinicManagement_proj.BLL.DTO
{
    /// <summary>
    /// Represents a specialty in the clinic.
    /// </summary>
    public class SpecialtyDTO
    {
        public static int NAME_MAX_LENGTH = 64;

        private string _name;

        /// <summary>
        /// Gets or sets the Id of the specialty.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the specialty.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = ValidateName(value); }
        }

        /// <summary>
        /// Gets the collection of doctors associated with the specialty.
        /// </summary>
        public ICollection<DoctorDTO> Doctors { get; set; }

        /// <summary>
        /// Initializes a new instance of the SpecialtyDTO class.
        /// </summary>
        public SpecialtyDTO()
        {
            Doctors = new List<DoctorDTO>();
        }

        /// <summary>
        /// Validates the specialty name.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>The validated name.</returns>
        /// <exception cref="ArgumentException">Thrown if name is null, empty, or exceeds max length.</exception>
        public static string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.");
            if (name.Length > NAME_MAX_LENGTH)
                throw new ArgumentException($"Name must be at most {NAME_MAX_LENGTH} characters.");
            return name;
        }

        /// <summary>
        /// Initializes a new instance of the SpecialtyDTO class with specified name.
        /// </summary>
        /// <param name="name">The name of the specialty.</param>
        public SpecialtyDTO(string name)
        {
            Name = name;
            Doctors = new List<DoctorDTO>();
        }

        /// <summary>
        /// Returns a string representation of the specialty.
        /// </summary>
        /// <returns>The name of the specialty.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
