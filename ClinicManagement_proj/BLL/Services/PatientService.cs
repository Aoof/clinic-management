using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.DAL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagement_proj.BLL.Services
{
    /// <summary>
    /// Service class for managing patients.
    /// </summary>
    public class PatientService
    {
        private ClinicDbContext clinicDb;

        /// <summary>
        /// Initializes a new instance of the PatientService class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PatientService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        /// <summary>
        /// Gets all patients.
        /// </summary>
        /// <returns>A list of all patients.</returns>
        public List<PatientDTO> GetAllPatients()
        {
            return clinicDb.Patients
                .Include(pat => pat.Appointments)
                .ToList();
        }

        /// <summary>
        /// Gets a patient by ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The patient with the specified ID.</returns>
        public PatientDTO GetPatientById(int id)
        {
            return clinicDb.Patients
                .Include(pat => pat.Appointments)
                .FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Adds a new patient.
        /// </summary>
        /// <param name="dto">The patient to add.</param>
        /// <returns>The added patient.</returns>
        public PatientDTO AddPatient(PatientDTO dto)
        {
            clinicDb.Patients.Add(dto);
            clinicDb.SaveChanges();
            return dto;
        }

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        /// <param name="patientDto">The patient to update.</param>
        /// <returns>The updated patient.</returns>
        public PatientDTO UpdatePatient(PatientDTO patientDto)
        {
            clinicDb.SaveChanges();
            return patientDto;
        }

        /// <summary>
        /// Searches patients by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of patients matching the name.</returns>
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



        /// <summary>
        /// Searches patients by ID.
        /// </summary>
        /// <param name="id">The ID to search for.</param>
        /// <returns>A list of patients matching the ID.</returns>
        public List<PatientDTO> Search(int id)
        {
            return clinicDb.Patients.Where(p => p.Id == id).Include(p => p.Appointments).ToList();

        }

        /// <summary>
        /// Checks if a patient exists by ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>True if the patient exists, otherwise false.</returns>
        public bool Exists(int id)
        {
            return clinicDb.Patients.Any(s => s.Id == id);
        }

    }
}
