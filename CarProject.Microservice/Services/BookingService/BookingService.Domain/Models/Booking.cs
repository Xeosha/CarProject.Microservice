

namespace BookingService.Domain.Models
{
    public class Booking 
    {
        public Guid BookingId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = Guid.NewGuid();
        public Guid OrganizationId { get; set; } = Guid.NewGuid();
        public string Service {  get; set; }    = string.Empty;
        public DateTime BookingTime { get; set; } = DateTime.UtcNow;    
        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;
        public string? Notes { get; set; } // Дополнительные комментарии
    }
}
