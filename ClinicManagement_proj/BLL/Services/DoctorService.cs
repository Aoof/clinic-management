using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    public class DoctorService
    {
        private readonly ClinicDbContext clinicDb;

        public DoctorService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        public DoctorDTO CreateDoctor(DoctorDTO doctorDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can update doctors.");

            clinicDb.Doctors.Add(doctorDto);
            clinicDb.SaveChanges();
            return doctorDto;
        }

        public DoctorDTO UpdateDoctor(DoctorDTO doctorDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Only Admin users can update doctors.");

            clinicDb.SaveChanges();
            return doctorDto;
        }

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

        public void DeleteDoctor(DoctorDTO doctorDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
            {
                throw new UnauthorizedAccessException("Only Admin users can delete doctors.");
            }
            clinicDb.Doctors.Remove(doctorDto);
            clinicDb.SaveChanges();
        }

        public List<DoctorDTO> GetAllDoctors()
        {
            return clinicDb.Doctors
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

        public List<SpecialtyDTO> GetAllSpecialties()
        {
            return clinicDb.Specialties.Include(s => s.Doctors).ToList();
        }

        public List<DoctorDTO> Search(string name)
        {
            return clinicDb.Doctors
                .Where(d => d.FirstName.Contains(name) || d.LastName.Contains(name))
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

        public List<DoctorDTO> Search(int id)
        {
            return clinicDb.Doctors
                .Where(d => d.Id.ToString().Contains(id.ToString()))
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

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