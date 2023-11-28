namespace iPractice.Domain.Entities
{
    public class TimeSlot : BaseEntity
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
    }
}
