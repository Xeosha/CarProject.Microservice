

namespace BookingService.Infrastracture.Entities
{
    public class BookingEntity
    {
        public Guid Id { get; set; }    
        public Guid UserId { get; set; }
        public Guid ServiceOrganizationId {  get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
