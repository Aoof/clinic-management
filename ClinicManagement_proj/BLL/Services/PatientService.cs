using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement_proj.BLL.Services
{
    public class PatientService
    {
        private readonly ClinicDbContext clinicDb = new ClinicDbContext();

        public int CreatePatient(PatientDTO patientDto)
        {
            var patient = new PatientDTO(patientDto.FirstName, patientDto.LastName, patientDto.InsuranceNumber, patientDto.DateOfBirth, patientDto.PhoneNumber, DateTime.UtcNow, DateTime.UtcNow);

            clinicDb.Patients.Add(patient);
            clinicDb.SaveChanges();

            return patient.Id;
        }

        public bool UpdatePatient(PatientDTO patientDto)
        {
            var patient = clinicDb.Patients.FirstOrDefault(p => p.Id == patientDto.Id);
            if (patient == null) return false;

            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.InsuranceNumber = patientDto.InsuranceNumber;
            patient.DateOfBirth = patientDto.DateOfBirth;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.ModifiedAt = DateTime.UtcNow;

            clinicDb.SaveChanges();
            return true;
        }

        public bool DeletePatient(int id)
        {
            var patient = clinicDb.Patients.Find(id);
            if (patient == null) return false;

            clinicDb.Patients.Remove(patient);
            clinicDb.SaveChanges();
            return true;
        }

        public PatientDTO Search(int id)
        {
            PatientDTO patientToSearch = new PatientDTO();

            //Check if the contact id exist
            patientToSearch = clinicDb.Patients.FirstOrDefault(u => u.Id == id);

            // return true if exist otherwise return false
            return patientToSearch;
        }


    }
}
