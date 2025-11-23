using System;
using System.Data.Entity;

namespace ClinicManagement_proj.DAL
{
    internal class ClinicDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }

        public ClinicDbContext() : base("data source=.;initial catalog=HealthCareClinicDB;integrated security=True;encrypt=False;MultipleActiveResultSets=True;App=EntityFramework")
        {
            Database.SetInitializer(new ClinicDbContextInitializer());
        } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>().HasKey(a => new { a.Id });
            modelBuilder.Entity<Doctor>().HasKey(d => new { d.Id });
            modelBuilder.Entity<Patient>().HasKey(p => new { p.Id });
            modelBuilder.Entity<Schedule>().HasKey(s => new { s.Id });
            modelBuilder.Entity<User>().HasKey(u => new { u.Id });
        }
    }


    internal class ClinicDbContextInitializer : DropCreateDatabaseIfModelChanges<ClinicDbContext>
    {
        protected override void Seed(ClinicDbContext context)
        {
            // This is sample data for testing purposes. In production, data will come from real users.

            // Users
            context.Users.Add(new User { Id = 1, Username = "admin", PasswordHash = "dHsKcdUmGyLbbM2LlD8u5L7RmODA7o4S9Aab9y4tcTCj21Ut", Role = "Administrator" });
            context.Users.Add(new User { Id = 2, Username = "dr_who", PasswordHash = "dHsKcdUmGyLbbM2LlD8u5L7RmODA7o4S9Aab9y4tcTCj21Ut", Role = "Doctor" });
            context.Users.Add(new User { Id = 3, Username = "dr_smith", PasswordHash = "dHsKcdUmGyLbbM2LlD8u5L7RmODA7o4S9Aab9y4tcTCj21Ut", Role = "Doctor" });
            context.Users.Add(new User { Id = 4, Username = "dr_jones", PasswordHash = "dHsKcdUmGyLbbM2LlD8u5L7RmODA7o4S9Aab9y4tcTCj21Ut", Role = "Doctor" });
            context.Users.Add(new User { Id = 5, Username = "receptionist1", PasswordHash = "dHsKcdUmGyLbbM2LlD8u5L7RmODA7o4S9Aab9y4tcTCj21Ut", Role = "Receptionist" });
            context.Users.Add(new User { Id = 6, Username = "receptionist2", PasswordHash = "dHsKcdUmGyLbbM2LlD8u5L7RmODA7o4S9Aab9y4tcTCj21Ut", Role = "Receptionist" });

            // Doctors
            context.Doctors.Add(new Doctor { Id = 2, UserId = 2, FirstName = "John", LastName = "Who", Specialization = "General Practice", Availability = "Monday-Friday 9AM-5PM" });
            context.Doctors.Add(new Doctor { Id = 3, UserId = 3, FirstName = "Sarah", LastName = "Smith", Specialization = "Cardiology", Availability = "Monday-Thursday 8AM-4PM" });
            context.Doctors.Add(new Doctor { Id = 4, UserId = 4, FirstName = "Michael", LastName = "Jones", Specialization = "Pediatrics", Availability = "Tuesday-Saturday 10AM-6PM" });

            // Patients
            context.Patients.Add(new Patient { Id = 1, FirstName = "Emma", LastName = "Wilson", DateOfBirth = new DateTime(1985, 3, 15), MedicalNumber = "WILE85031500 01", PhoneNumber = "555-0101", Email = "emma.wilson@email.com" });
            context.Patients.Add(new Patient { Id = 2, FirstName = "James", LastName = "Brown", DateOfBirth = new DateTime(1990, 7, 22), MedicalNumber = "BROJ90072200 02", PhoneNumber = "555-0102", Email = "james.brown@email.com" });
            context.Patients.Add(new Patient { Id = 3, FirstName = "Olivia", LastName = "Davis", DateOfBirth = new DateTime(1978, 11, 30), MedicalNumber = "DAVO78113000 03", PhoneNumber = "555-0103", Email = "olivia.davis@email.com" });
            context.Patients.Add(new Patient { Id = 4, FirstName = "William", LastName = "Miller", DateOfBirth = new DateTime(2015, 5, 10), MedicalNumber = "MILW15051000 04", PhoneNumber = "555-0104", Email = "parent.miller@email.com" });
            context.Patients.Add(new Patient { Id = 5, FirstName = "Sophia", LastName = "Garcia", DateOfBirth = new DateTime(1995, 9, 18), MedicalNumber = "GARS95091800 05", PhoneNumber = "555-0105", Email = "sophia.garcia@email.com" });

            // Schedules
            context.Schedules.Add(new Schedule { Id = 1, DoctorId = 2, Schedule1 = "Mon: 9AM-5PM, Tue: 9AM-5PM, Wed: 9AM-5PM, Thu: 9AM-5PM, Fri: 9AM-5PM", EffectiveDate = new DateTime(2025, 1, 1) });
            context.Schedules.Add(new Schedule { Id = 2, DoctorId = 3, Schedule1 = "Mon: 8AM-4PM, Tue: 8AM-4PM, Wed: 8AM-4PM, Thu: 8AM-4PM", EffectiveDate = new DateTime(2025, 1, 1) });
            context.Schedules.Add(new Schedule { Id = 3, DoctorId = 4, Schedule1 = "Tue: 10AM-6PM, Wed: 10AM-6PM, Thu: 10AM-6PM, Fri: 10AM-6PM, Sat: 10AM-6PM", EffectiveDate = new DateTime(2025, 1, 1) });

            // Appointments
            context.Appointments.Add(new Appointment { Id = 1, PatientId = 1, DoctorId = 2, CreatedBy = 5, Description = "Annual checkup", AppointmentDate = new DateTime(2025, 11, 25, 10, 0, 0), Status = "Confirmed", Duration = 30, ModifiedDate = DateTime.Now });
            context.Appointments.Add(new Appointment { Id = 2, PatientId = 2, DoctorId = 3, CreatedBy = 5, Description = "Heart consultation", AppointmentDate = new DateTime(2025, 11, 26, 14, 0, 0), Status = "Confirmed", Duration = 45, ModifiedDate = DateTime.Now });
            context.Appointments.Add(new Appointment { Id = 3, PatientId = 3, DoctorId = 2, CreatedBy = 6, Description = "Follow-up visit", AppointmentDate = new DateTime(2025, 11, 22, 11, 0, 0), Status = "Completed", Duration = 20, ModifiedDate = DateTime.Now });
            context.Appointments.Add(new Appointment { Id = 4, PatientId = 4, DoctorId = 4, CreatedBy = 5, Description = "Child vaccination", AppointmentDate = new DateTime(2025, 11, 27, 15, 0, 0), Status = "Pending", Duration = 30, ModifiedDate = DateTime.Now });
            context.Appointments.Add(new Appointment { Id = 5, PatientId = 5, DoctorId = 3, CreatedBy = 6, Description = "Cardiac screening", AppointmentDate = new DateTime(2025, 11, 23, 9, 0, 0), Status = "Cancelled", Duration = 60, ModifiedDate = DateTime.Now });
            context.Appointments.Add(new Appointment { Id = 6, PatientId = 1, DoctorId = 3, CreatedBy = 5, Description = "Blood pressure check", AppointmentDate = new DateTime(2025, 12, 1, 10, 30, 0), Status = "Confirmed", Duration = 30, ModifiedDate = DateTime.Now });
        }
    }
}
