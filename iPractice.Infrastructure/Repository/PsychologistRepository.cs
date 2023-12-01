using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using iPractice.Infrastructure.Data;
using iPractice.Infrastructure.Repository.Base;

namespace iPractice.Infrastructure.Repository
{
    public class PsychologistRepository : BaseRepository<Psychologist>, IPsychologistRepository
    {
        public PsychologistRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
