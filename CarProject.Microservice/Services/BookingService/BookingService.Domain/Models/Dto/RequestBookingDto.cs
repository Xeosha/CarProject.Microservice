

namespace BookingService.Domain.Models.Dto
{
    public class RequestBookingDto
    {
        public int OrganizationId { get; set; }
        public string? Service { get; set; }
    }
}
