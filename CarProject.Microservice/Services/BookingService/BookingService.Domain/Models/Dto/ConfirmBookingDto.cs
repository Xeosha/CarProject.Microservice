

namespace BookingService.Domain.Models.Dto
{
    public class ConfirmBookingDto
    {
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
