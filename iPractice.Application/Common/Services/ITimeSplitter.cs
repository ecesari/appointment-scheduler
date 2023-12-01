using iPractice.Domain.Entities;

namespace iPractice.Application.Common.Services
{
    public interface ITimeSplitter
    {
        List<TimeSlot> Split(DateTime start, DateTime end, int duration);
    }
}
