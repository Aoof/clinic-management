using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

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

            doctorDto.ModifiedAt = DateTime.Now;
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
            if (!ClinicManagementApp.CurrentUserHasRole
                (
                    UserService.UserRoles.Administrator,
                    UserService.UserRoles.Doctor,
                    UserService.UserRoles.Receptionist
                )
            )
                throw new UnauthorizedAccessException("You don't have access to read all doctors.");

            return clinicDb.Doctors
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

        public List<SpecialtyDTO> GetAllSpecialties()
        {
            if (!ClinicManagementApp.CurrentUserHasRole
                (
                    UserService.UserRoles.Administrator,
                    UserService.UserRoles.Doctor,
                    UserService.UserRoles.Receptionist
                )
            )
                throw new UnauthorizedAccessException("You don't have access to read specialties.");

            return clinicDb.Specialties.Include(s => s.Doctors).ToList();
        }

        public List<DoctorDTO> Search(string name)
        {
            if (!ClinicManagementApp.CurrentUserHasRole
                (
                    UserService.UserRoles.Administrator,
                    UserService.UserRoles.Doctor,
                    UserService.UserRoles.Receptionist
                )
            )
                throw new UnauthorizedAccessException("You don't have access to search doctors.");

            return clinicDb.Doctors
                .Where(d => d.FirstName.Contains(name) || d.LastName.Contains(name))
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }

        public List<DoctorDTO> Search(int id)
        {
            if (!ClinicManagementApp.CurrentUserHasRole
                (
                    UserService.UserRoles.Administrator,
                    UserService.UserRoles.Doctor,
                    UserService.UserRoles.Receptionist
                )
            )
                throw new UnauthorizedAccessException("You don't have access to search doctors.");

            return clinicDb.Doctors
                .Where(d => d.Id.ToString().Contains(id.ToString()))
                .Include(d => d.Specialties)
                .Include(d => d.DoctorSchedules)
                .ToList();
        }
    }
}
