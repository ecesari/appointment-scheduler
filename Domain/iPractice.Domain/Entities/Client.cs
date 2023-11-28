namespace iPractice.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public List<Psychologist> Psychologists { get; set; }
        public List<Appointment> Appointments { get; set; }

    }
}