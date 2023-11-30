using iPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
