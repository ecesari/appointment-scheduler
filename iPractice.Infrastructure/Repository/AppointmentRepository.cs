using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using iPractice.Infrastructure.Data;
using iPractice.Infrastructure.Repository.Base;

namespace iPractice.Infrastructure.Repository
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
