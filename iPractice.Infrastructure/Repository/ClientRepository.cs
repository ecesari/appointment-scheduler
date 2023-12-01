using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using iPractice.Infrastructure.Data;
using iPractice.Infrastructure.Repository.Base;

namespace iPractice.Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
