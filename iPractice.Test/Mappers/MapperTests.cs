using AutoMapper;
using iPractice.Application.Clients.Queries.GetClients;
using iPractice.Application.Common.Configurations;
using iPractice.Domain.Entities;

namespace iPractice.Test
{
    public class MapperTests
    {
        private readonly IMapper mapper;
        private readonly MapperConfiguration config;


        public MapperTests()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            }); mapper = config.CreateMapper();
        }

        [Fact]
        public void ConfigurationTest()
        {
            config.AssertConfigurationIsValid();

            Assert.NotNull(mapper);
        }

        [Theory]
        [InlineData(1, "Foo")]
        public void Given_Client_ShouldReturn_ClientResponse(long clientId, string clientName)
        {
            var client = new Client { Id = clientId, Name = clientName };
            var response = mapper.Map<ClientResponse>(client);

            Assert.NotNull(response);
            Assert.Equal(client.Id, response.Id);
            Assert.Equal(client.Name, response.Name);
        }
    }
}