using iPractice.Domain.Entities;

namespace iPractice.Application.Common.Interfaces
{
    public interface ITimeSplitter
    {
        List<TimeSlot> Split(DateTime start, DateTime end, int duration);
    }
}
