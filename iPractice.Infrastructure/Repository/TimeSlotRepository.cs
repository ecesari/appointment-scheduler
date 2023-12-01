using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using iPractice.Infrastructure.Data;
using iPractice.Infrastructure.Repository.Base;

namespace iPractice.Infrastructure.Repository
{
    public class TimeSlotRepository : BaseRepository<TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
