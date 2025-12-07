using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagement_proj.BLL
{
    /// <summary>
    /// Provides services for managing doctor schedules.
    /// </summary>
    public class DoctorScheduleService
    {
        private readonly ClinicDbContext clinicDb;

        /// <summary>
        /// Initializes a new instance of the DoctorScheduleService class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public DoctorScheduleService(ClinicDbContext dbContext)
        {
            clinicDb = dbContext;
        }

        /// <summary>
        /// Creates a new doctor schedule.
        /// </summary>
        /// <param name="scheduleDto">The schedule to create.</param>
        /// <returns>The created schedule.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public DoctorScheduleDTO CreateSchedule(DoctorScheduleDTO scheduleDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Access denied. Administrator role required.");

            clinicDb.DoctorSchedules.Add(scheduleDto);
            clinicDb.SaveChanges();
            return scheduleDto;
        }

        /// <summary>
        /// Updates an existing doctor schedule.
        /// </summary>
        /// <param name="scheduleDto">The schedule to update.</param>
        /// <returns>The updated schedule.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public DoctorScheduleDTO UpdateSchedule(DoctorScheduleDTO scheduleDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Access denied. Administrator role required.");

            scheduleDto.ModifiedAt = DateTime.Now;
            clinicDb.SaveChanges();
            return scheduleDto;
        }

        /// <summary>
        /// Deletes a doctor schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to delete.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        /// <exception cref="ArgumentException">Thrown if the schedule is not found.</exception>
        public void DeleteSchedule(int id)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Access denied. Administrator role required.");
            var schedule = clinicDb.DoctorSchedules
                .Where(s => s.Id == id)
                .FirstOrDefault()
                ?? throw new ArgumentException("Schedule not found");

            clinicDb.DoctorSchedules.Remove(schedule);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Deletes a doctor schedule.
        /// </summary>
        /// <param name="scheduleDto">The schedule to delete.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not an administrator.</exception>
        public void DeleteSchedule(DoctorScheduleDTO scheduleDto)
        {
            if (!ClinicManagementApp.CurrentUserHasRole(UserService.UserRoles.Administrator))
                throw new UnauthorizedAccessException("Access denied. Administrator role required.");

            clinicDb.DoctorSchedules.Remove(scheduleDto);
            clinicDb.SaveChanges();
        }

        /// <summary>
        /// Gets all doctor schedules.
        /// </summary>
        /// <returns>A list of all schedules.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user does not have access.</exception>
        public List<DoctorScheduleDTO> GetAllSchedules()
        {
            if (!ClinicManagementApp.CurrentUserHasRole
                (
                    UserService.UserRoles.Administrator,
                    UserService.UserRoles.Doctor,
                    UserService.UserRoles.Receptionist
                )
            )
                throw new UnauthorizedAccessException("You don't have access to read all schedules");
            return clinicDb.DoctorSchedules.ToList();
        }

        /// <summary>
        /// Searches for a doctor schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the schedule.</param>
        /// <returns>The schedule with the specified ID.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user does not have access.</exception>
        /// <exception cref="ArgumentException">Thrown if the schedule is not found.</exception>
        public DoctorScheduleDTO Search(int id)
        {
            if (!ClinicManagementApp.CurrentUserHasRole
                (
                    UserService.UserRoles.Administrator,
                    UserService.UserRoles.Doctor,
                    UserService.UserRoles.Receptionist
                )
            )
                throw new UnauthorizedAccessException("You don't have access to search schedules");

            var schedule = clinicDb.DoctorSchedules
                .Where(s => s.Id == id)
                .FirstOrDefault()
                ?? throw new ArgumentException("Schedule not found");
            return schedule;
        }

        /// <summary>
        /// Searches for a doctor schedule by day of the week.
        /// </summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns>The schedule for the specified day.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user does not have access.</exception>
        /// <exception cref="ArgumentException">Thrown if the schedule is not found.</exception>
        public DoctorScheduleDTO Search(string dayOfWeek)
        {
            if (!ClinicManagementApp.CurrentUserHasRole
                (
                    UserService.UserRoles.Administrator,
                    UserService.UserRoles.Doctor,
                    UserService.UserRoles.Receptionist
                )
            )
                throw new UnauthorizedAccessException("You don't have access to search schedules");

            var schedule = clinicDb.DoctorSchedules
                .Where(s => s.DayOfWeek == dayOfWeek)
                .FirstOrDefault()
                ?? throw new ArgumentException("Schedule not found");
            return schedule;
        }
    }
}
