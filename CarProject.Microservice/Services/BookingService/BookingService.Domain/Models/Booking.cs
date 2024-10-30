

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

        public static Booking Create(Guid bookingId, Guid userId, Guid serviceOrganizationId, DateTime bookingTime, string? notes = null)
        {
            var booking = new Booking()
            {
                BookingId = bookingId,
                UserId = userId,
                ServiceOrganizationId = serviceOrganizationId,
                BookingTime = bookingTime,
                Notes = notes
            };

            return booking;
        }
    }
}
