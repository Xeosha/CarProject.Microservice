

namespace BookingService.Domain.Models.Dto
{
    public class AvailableDayDto
    {
        public DayOfWeek DayOfWeek { get; set; } // День недели (1-7)
        public DateTime Date { get; set; } // Дата
        public List<AvailableTimeSlotDto> TimeSlots { get; set; } = new();
    }
}
