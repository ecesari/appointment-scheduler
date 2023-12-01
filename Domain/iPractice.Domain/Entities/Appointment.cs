using System.ComponentModel.DataAnnotations;

namespace iPractice.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Psychologist Psychologist { get; set; }
        public Client Client { get; set; }
        public TimeSlot TimeSlot { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
    }
}
