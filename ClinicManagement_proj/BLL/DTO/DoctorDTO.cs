using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagement_proj.BLL.DTO
{
    public class DoctorDTO
    {
        public static int FIRSTNAME_MAX_LENGTH = 64;
        public static int LASTNAME_MAX_LENGTH = 64;
        public static int LICENSE_MAX_LENGTH = 24;

        private string _firstName;
        private string _lastName;
        private string _licenseNumber;

        public int Id { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = ValidateFirstName(value); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = ValidateLastName(value); }
        }
        public string LicenseNumber
        {
            get { return _licenseNumber; }
            set { _licenseNumber = ValidateLicenseNumber(value); }
        }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<AppointmentDTO> Appointments { get; set; }
        public ICollection<DoctorScheduleDTO> DoctorSchedules { get; set; }
        public ICollection<SpecialtyDTO> Specialties { get; set; }

        /// <summary>
        /// Initializes a new instance of the DoctorDTO class.
        /// </summary>
        public DoctorDTO()
        {
            Appointments = new List<AppointmentDTO>();
            DoctorSchedules = new List<DoctorScheduleDTO>();
            Specialties = new List<SpecialtyDTO>();
        }

        /// <summary>
        /// Validates the first name string.
        /// </summary>
        /// <param name="firstName">The first name to validate.</param>
        /// <returns>The validated first name.</returns>
        /// <exception cref="ArgumentException">Thrown if first name is null, empty, or exceeds max length.</exception>
        public static string ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("FirstName cannot be null or empty.");
            if (firstName.Length > FIRSTNAME_MAX_LENGTH)
                throw new ArgumentException($"FirstName must be at most {FIRSTNAME_MAX_LENGTH} characters.");
            return firstName;
        }

        /// <summary>
        /// Validates the last name string.
        /// </summary>
        /// <param name="lastName">The last name to validate.</param>
        /// <returns>The validated last name.</returns>
        /// <exception cref="ArgumentException">Thrown if last name is null, empty, or exceeds max length.</exception>
        public static string ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName cannot be null or empty.");
            if (lastName.Length > LASTNAME_MAX_LENGTH)
                throw new ArgumentException($"LastName must be at most {LASTNAME_MAX_LENGTH} characters.");
            return lastName;
        }

        /// <summary>
        /// Validates the license number string.
        /// </summary>
        /// <param name="licenseNumber">The license number to validate.</param>
        /// <returns>The validated license number.</returns>
        /// <exception cref="ArgumentException">Thrown if license number is null, empty, or exceeds max length.</exception>
        public static string ValidateLicenseNumber(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("LicenseNumber cannot be null or empty.");
            if (licenseNumber.Length > LICENSE_MAX_LENGTH)
                throw new ArgumentException($"LicenseNumber must be at most {LICENSE_MAX_LENGTH} characters.");
            return licenseNumber;
        }

        /// <summary>
        /// Initializes a new instance of the DoctorDTO class with specified parameters.
        /// </summary>
        /// <param name="firstName">The first name of the doctor.</param>
        /// <param name="lastName">The last name of the doctor.</param>
        /// <param name="licenseNumber">The license number of the doctor.</param>
        /// <param name="createdAt">The creation date.</param>
        /// <param name="modifiedAt">The modification date.</param>
        public DoctorDTO(string firstName, string lastName, string licenseNumber, DateTime createdAt, DateTime modifiedAt)
        {
            FirstName = firstName;
            LastName = lastName;
            LicenseNumber = licenseNumber;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            Appointments = new List<AppointmentDTO>();
            DoctorSchedules = new List<DoctorScheduleDTO>();
            Specialties = new List<SpecialtyDTO>();
        }

        /// <summary>
        /// Returns a string representation of the doctor.
        /// </summary>
        /// <returns>A string describing the doctor.</returns>
        public override string ToString()
        {
            string specialty = Specialties.FirstOrDefault().ToString();
            return $"{FirstName} {LastName} ({specialty}) [{Id}]";
        }
    }
}
