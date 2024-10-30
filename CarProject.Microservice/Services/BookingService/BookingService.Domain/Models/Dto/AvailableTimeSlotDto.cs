
namespace BookingService.Domain.Models.Dto
{
    public class AvailableTimeSlotDto
    {
        public TimeSpan StartTime { get; set; } // Начало времени
        public TimeSpan EndTime { get; set; } // Конец времени
        public bool IsBooked { get; set; } // Занято/незанято
    }
}
