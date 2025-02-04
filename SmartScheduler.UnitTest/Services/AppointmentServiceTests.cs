using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using SmartScheduler.Application.Services;
using SmartScheduler.Domain.Entities;
using SmartScheduler.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.UnitTest.Services
{
    public class AppointmentServiceTests
    {
        private readonly AppointmentService _appointmentService;
        private readonly ApplicationDbContext _dbContext;

        public AppointmentServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AppointmentServiceTests")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _appointmentService = new AppointmentService(_dbContext);
        }

        [Fact]
        public async Task GetAllAppointmentAsync_ShouldReturnAllAppointments()
        {
            // Arrange
            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    Title = "Appointment 1",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    Location = "Location 1",
                    IsConfirmed = true
                },
                new Appointment
                {
                    Title = "Appointment 2",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    Location = "Location 2",
                    IsConfirmed = true
                },
                new Appointment
                {
                    Title = "Appointment 3",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    Location = "Location 3",
                    IsConfirmed = true
                },
                new Appointment
                {
                    Title = "Appointment 4",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    Location = "Location 4",
                    IsConfirmed = true
                }
            };
            _dbContext.Appointments.AddRange(appointments);
            await _dbContext.SaveChangesAsync();
            // Act
            var result = await _appointmentService.GetAllAppointmentsAsync();

            // Assert
            result.Should().HaveCount(4);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // //TODO: Need to fix
        [Fact]
        public async Task GetAppointmentByIdAsync_ShouldReturnCorrectAppointment()
        {
            // Arrange
            var appointment = new Appointment {  Title = "Test Meeting" };
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _appointmentService.GetAppointmentByIdAsync(appointment.Id);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Test Meeting");
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldAddAppointmentToDatabase()
        {
            // Arrange
            var appointment = new Appointment { Title = "New Meeting" };

            // Act
            await _appointmentService.CreateAppointmentAsync(appointment);
            var result = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == appointment.Id);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("New Meeting");
        }

        [Fact]
        public async Task UpdateAppointmentAsync_ShouldModifyExistingAppointment()
        {
            // Arrange
            var appointment = new Appointment { Title = "Old Title" };
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();

            // Act
            appointment.Title = "Updated Title";
            await _appointmentService.UpdateAppointmentAsync(appointment, appointment.Id);

            // Assert
            var updatedAppointment = await _dbContext.Appointments.FindAsync(appointment.Id);
            updatedAppointment.Title.Should().Be("Updated Title");
        }

        [Fact]
        public async Task DeleteAppointmentAsync_ShouldRemoveAppointmentFromDatabase()
        {
            // Arrange
            var appointment = new Appointment {  Title = "To Be Deleted" };
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();

            // Act
            await _appointmentService.DeleteAppointmentAsync(appointment.Id);

            // Assert
            var result = await _dbContext.Appointments.FindAsync(appointment.Id);
            result.Should().BeNull();
        }
    }
}
