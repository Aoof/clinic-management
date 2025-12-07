using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    /// <summary>
    /// Provides services for managing doctors in the clinic.
    /// </summary>
    public class DoctorService
    {
        private readonly ClinicDbContext clinicDb;

        /// <summary>
        /// Initializes a new instance of the DoctorService class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public DoctorService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        /// <summary>
        /// Creates a new doctor.
        /// </summary>
        /// <param name="doctorDto">The doctor to create.</param>
        /// <returns>The created doctor.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public DoctorDTO CreateDoctor(DoctorDTO doctorDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can update doctors.");

            clinicDb.Doctors.Add(doctorDto);
            clinicDb.SaveChanges();
            return doctorDto;
        }

        /// <summary>
        /// Updates an existing doctor.
        /// </summary>
        /// <param name="doctorDto">The doctor to update.</param>
        /// <returns>The updated doctor.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public DoctorDTO UpdateDoctor(DoctorDTO doctorDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can update doctors.");

            clinicDb.SaveChanges();
            return doctorDto;
        }

        /// <summary>
        /// Deletes a doctor by ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to delete.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        /// <exception cref="ArgumentException">Thrown if the doctor is not found.</exception>
        public void DeleteDoctor(int id)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can delete doctors.");

            var doctor = clinicDb.Doctors
                .Where(d => d.Id == id)
                .FirstOrDefault()
                ?? throw new ArgumentException("Doctor not found");

            clinicDb.Doctors.Remove(doctor);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Deletes a doctor.
        /// </summary>
        /// <param name="doctorDto">The doctor to delete.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public void DeleteDoctor(DoctorDTO doctorDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
            {
                throw new UnauthorizedAccessException("Only Admin users can delete doctors.");
            }
            clinicDb.Doctors.Remove(doctorDto);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Gets all doctors.
        /// </summary>
        /// <returns>A list of all doctors.</returns>
        public List<DoctorDTO> GetAllDoctors()
        {
            return clinicDb.Doctors
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

        /// <summary>
        /// Gets all specialties.
        /// </summary>
        /// <returns>A list of all specialties.</returns>
        public List<SpecialtyDTO> GetAllSpecialties()
        {
            return clinicDb.Specialties.Include(s => s.Doctors).ToList();
        }

        /// <summary>
        /// Searches doctors by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of doctors matching the name.</returns>
        public List<DoctorDTO> Search(string name)
        {
            return clinicDb.Doctors
                .Where(d => d.FirstName.Contains(name) || d.LastName.Contains(name))
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

        /// <summary>
        /// Searches doctors by ID.
        /// </summary>
        /// <param name="id">The ID to search for.</param>
        /// <returns>A list of doctors matching the ID.</returns>
        public List<DoctorDTO> Search(int id)
        {
            return clinicDb.Doctors
                .Where(d => d.Id.ToString().Contains(id.ToString()))
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

        /// <summary>
        /// Searches doctors by license number.
        /// </summary>
        /// <param name="license">The license number to search for.</param>
        /// <returns>A list of doctors matching the license.</returns>
        public List<DoctorDTO> SearchByLicense(string license)
        {
            return clinicDb.Doctors
                .Where(d => d.LicenseNumber.Contains(license))
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }
    }
}