namespace iPractice.Application.Clients.Queries.GetPsychologistAvailability
{
    public class PsychologistAvailabilityResponse
    {
        public long PsychologistId { get; set; }
        public string PsychologistName { get; set; }
        public long TimeSlotId { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
    }
}
