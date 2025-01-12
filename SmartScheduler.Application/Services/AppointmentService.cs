using Microsoft.EntityFrameworkCore;
using SmartScheduler.Application.Interfaces;
using SmartScheduler.Domain.Entities;
using SmartScheduler.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateAppointmentAsync(Appointment appointment)
        {
            appointment.SetId();
            _context.Appointments.Add(appointment);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if(appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all appointments
        /// Add Filters ---
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
            => await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

        public async Task UpdateAppointmentAsync(Appointment appointment, Guid id)
        {
            appointment.SetId(id);
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
