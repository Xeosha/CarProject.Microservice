

namespace BookingService.Domain.Models
{
    public class Booking 
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public string Service {  get; set; }    = string.Empty;
        public DateTime BookingTime { get; set; } = DateTime.UtcNow;    
        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;
        public string? Notes { get; set; } // Дополнительные комментарии
    }
}
