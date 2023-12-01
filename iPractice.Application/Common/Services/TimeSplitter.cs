using iPractice.Domain.Entities;

namespace iPractice.Application.Common.Services
{
    public class TimeSplitter : ITimeSplitter
    {
        public List<TimeSlot> Split(DateTime start, DateTime end, int duration)
        {
            var list = new List<TimeSlot>();
            while (start.AddMinutes(duration) <= end)
            {
                var timeSlot = new TimeSlot
                {
                    TimeFrom = start,
                    TimeTo = start.AddMinutes(duration),
                };
                list.Add(timeSlot);
                start = start.AddMinutes(duration);
            }
            return list;
        }
    }
}
