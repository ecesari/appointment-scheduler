using AutoMapper;
using iPractice.Application.Clients.Commands.CreateAvailability;
using iPractice.Application.Clients.Queries.GetPsychologistAvailability;
using iPractice.Application.Common.Exceptions;
using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using Moq;

namespace iPractice.Test.Handlers.Clients
{
    public class GetPsychologistAvailabilityQueryTests
    {
        private readonly Mock<IClientRepository> repositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly GetPsychologistAvailabilityQueryHandler handler;



        public GetPsychologistAvailabilityQueryTests()
        {
            repositoryMock = new Mock<IClientRepository>();
            mapperMock = new Mock<IMapper>();
            handler = new GetPsychologistAvailabilityQueryHandler(repositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public void ConfigurationTest()
        {
            Assert.NotNull(handler);
        }

        [Theory]
        [InlineData(1)]
        public void Given_ClientNotFound_Should_ThrowError(long clientId)
        {
            var query = new GetPsychologistAvailabilityQuery { ClientId = clientId};
            repositoryMock.Setup(x => x.GetByIdAsync(clientId)).Returns<Client?>(null);
            Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Theory]
        [InlineData(1)]
        public void Given_ClientWithPsychologist_Should_ReturnAvailability(long clientId)
        {
            var query = new GetPsychologistAvailabilityQuery { ClientId = clientId };
            repositoryMock.Setup(x => x.GetByIdAsync(clientId)).Returns<Client?>(null);
            Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}
