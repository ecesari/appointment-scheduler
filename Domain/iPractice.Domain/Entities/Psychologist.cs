namespace iPractice.Domain.Entities
{
    public class Psychologist : BaseEntity
    {
        public string Name { get; set; }
        public List<Client> Clients { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<TimeSlot> Availability { get; set; }
    }
}