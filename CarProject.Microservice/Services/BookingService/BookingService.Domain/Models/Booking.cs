

namespace BookingService.Domain.Models
{
    public class Booking 
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceOrganizationId { get; set; }
        public DateTime BookingTime { get; set; } = DateTime.UtcNow;    
        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;
        public string? Notes { get; set; } // Дополнительные комментарии
    }
}
