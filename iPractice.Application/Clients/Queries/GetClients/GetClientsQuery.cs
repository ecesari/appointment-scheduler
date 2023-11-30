using AutoMapper;
using iPractice.Application.Common.Models;
using iPractice.Domain.Repository;
using MediatR;

namespace iPractice.Application.Clients.Commands.CreateAvailability
{
    public class GetClientsQuery : IRequest<List<ClientResponse>>
    {
    }

    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, List<ClientResponse>>
    {
        private readonly IClientRepository repository;
        private readonly IMapper mapper;
        public GetClientsQueryHandler(IClientRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ClientResponse>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {            
            var clients = await repository.GetAllAsync();
            var returnModel = mapper.Map<List<ClientResponse>>(clients);
            return returnModel;
        }
    }
}
