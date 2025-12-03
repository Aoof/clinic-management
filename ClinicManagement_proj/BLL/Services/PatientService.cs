using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    public class PatientService
    {
        private ClinicDbContext clinicDb;

        public PatientService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        public List<PatientDTO> GetAllPatients()
        {
            return clinicDb.Patients
                .Include(pat => pat.Appointments)
                .ToList();
        }

        public PatientDTO GetPatientById(int id)
        {
            return clinicDb.Patients
                .Include(pat => pat.Appointments)
                .FirstOrDefault(s => s.Id == id);
        }
        public PatientDTO AddPatient(PatientDTO dto)
        {
            clinicDb.Patients.Add(dto);
            clinicDb.SaveChanges();
            return dto;
        }

        public PatientDTO UpdatePatient(PatientDTO patientDto)
        {
            clinicDb.SaveChanges();
            return patientDto;
        }

        public List<PatientDTO> Search(string name)
        {
            return clinicDb.Patients
                .Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name))
                .Include(p => p.Appointments)
                .ToList();
        }

        //public void DeletePatient(int id)
        //{
        //    var patient = clinicDb.Patients.Find(id);
        //    if (patient != null)
        //    {
        //        clinicDb.Patients.Remove(patient);
        //        clinicDb.SaveChanges();

        //    }
        //}



        public List<PatientDTO> Search(int id)
        {
            return clinicDb.Patients.Where(p => p.Id == id).Include(p => p.Appointments).ToList();

        }

        public bool Exists(int id)
        {
            return clinicDb.Patients.Any(s => s.Id == id);
        }

    }
}
