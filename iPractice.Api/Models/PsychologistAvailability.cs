using System;

namespace iPractice.Api.Models
{
    public class PsychologistAvailability
    {
        public long PsychologistId { get; set; }
        public string PsychologistName { get; set; }
        public long TimeSlotId { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
    }
}
