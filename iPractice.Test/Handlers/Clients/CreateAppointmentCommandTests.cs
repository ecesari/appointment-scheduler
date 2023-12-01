using AutoMapper;
using iPractice.Application.Clients.Commands.CreateAppointment;
using iPractice.Application.Common.Exceptions;
using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPractice.Test.Handlers.Clients
{
    public class CreateAppointmentCommandTests
    {
        private readonly Mock<IClientRepository> repositoryMock;

        private readonly Mock<ITimeSlotRepository> timeSlotRepositoryMock;
        private readonly Mock<IPsychologistRepository> psychologistRepositoryMock;
        private readonly Mock<IAppointmentRepository> appointmentRepositoryMock;
        private readonly CreateAppointmentCommandHandler handler;



        public CreateAppointmentCommandTests()
        {
            repositoryMock = new Mock<IClientRepository>();
            timeSlotRepositoryMock = new Mock<ITimeSlotRepository>();
            psychologistRepositoryMock = new Mock<IPsychologistRepository>();
            appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            handler = new CreateAppointmentCommandHandler(psychologistRepositoryMock.Object, timeSlotRepositoryMock.Object, repositoryMock.Object, appointmentRepositoryMock.Object);
        }

        [Fact]
        public void ConfigurationTest()
        {
            Assert.NotNull(handler);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void Given_ClientNotFound_Should_ThrowError(long clientId, long psychologistId, long timeSlotId)
        {
            var command = new CreateAppointmentCommand { ClientId = clientId, PsychologistId = psychologistId, TimeSlotId = timeSlotId };
            repositoryMock.Setup(x => x.GetByIdAsync(clientId)).Returns<Client?>(null);
            Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
        [Theory]
        [InlineData(1, 1, 1)]
        public void Given_PsychologistNotFound_Should_ThrowError(long clientId, long psychologistId, long timeSlotId)
        {
            var command = new CreateAppointmentCommand { ClientId = clientId, PsychologistId = psychologistId, TimeSlotId = timeSlotId };
            psychologistRepositoryMock.Setup(x => x.GetByIdAsync(clientId)).Returns<Psychologist?>(null);
            Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
        [Theory]
        [InlineData(1, 1, 1)]
        public void Given_TimeSlotNotFound_Should_ThrowError(long clientId, long psychologistId, long timeSlotId)
        {
            var command = new CreateAppointmentCommand { ClientId = clientId, PsychologistId = psychologistId, TimeSlotId = timeSlotId };
            timeSlotRepositoryMock.Setup(x => x.GetByIdAsync(clientId)).Returns<TimeSlot?>(null);
            Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
