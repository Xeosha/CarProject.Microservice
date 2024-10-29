

namespace BookingService.Infrastracture.Entities
{
    public class BookingEntity
    {
        public long Id { get; set; }    
        public long UserId { get; set; }
        public long ServiceId {  get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
