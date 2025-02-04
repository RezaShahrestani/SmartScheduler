using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
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


        [Fact]
        public async Task GetAppointmentByIdAsync_ShouldReturnCorrectAppointment()
        {
            // Arrange
            var appointment = new Appointment
            {
                Title = "Doctor Appointment",
                Start = new DateTime(2025, 2, 10, 14, 0, 0),
                End = new DateTime(2025, 2, 10, 15, 0, 0),
                Location = "Clinic Room 5",
                IsConfirmed = true
            };
            appointment.SetId();
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();


            // Act
            var result = await _appointmentService.GetAppointmentByIdAsync(appointment.Id);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Doctor Appointment");
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldAddAppointmentToDatabase()
        {
            // Arrange
            var appointment = new Appointment
            {
                Title = "Team Meeting",
                Start = new DateTime(2025, 3, 5, 9, 30, 0),
                End = new DateTime(2025, 3, 5, 10, 30, 0),
                Location = "Conference Room A",
                IsConfirmed = false
            };


            // Act
            await _appointmentService.CreateAppointmentAsync(appointment);
            var result = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == appointment.Id);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Team Meeting");
        }

        [Fact]
        public async Task UpdateAppointmentAsync_ShouldModifyExistingAppointment()
        {
            // Arrange
            var appointment = new Appointment
            {
                Title = "New Title",
                Start = new DateTime(2025, 4, 1, 11, 0, 0),
                End = new DateTime(2025, 4, 1, 12, 0, 0),
                Location = "Online (Zoom)",
                IsConfirmed = false
            };
            appointment.SetId();
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
            var appointment = new Appointment
            {
                Title = "Lunch with Client",
                Start = new DateTime(2025, 5, 15, 13, 0, 0),
                End = new DateTime(2025, 5, 15, 14, 0, 0),
                Location = "Downtown Café",
                IsConfirmed = true
            };
            appointment.SetId();
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
