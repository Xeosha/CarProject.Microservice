

namespace BookingService.Domain.Models.Dto
{
    public class WorkingHoursDto
    {
        public int OrganizationServiceId { get; set; } // Идентификатор связки "Организация-Услуга"
        public List<DailyWorkingHours> DailyHours { get; set; } = new();
    }

    public class DailyWorkingHours
    {
        public DayOfWeek DayOfWeek { get; set; } // День недели, например, понедельник, вторник и т.д.
        public TimeSpan StartTime { get; set; }  // Время начала работы
        public TimeSpan EndTime { get; set; }    // Время окончания работы
        public TimeSpan? BreakStartTime { get; set; } // Время начала перерыва (если есть)
        public TimeSpan? BreakEndTime { get; set; }   // Время окончания перерыва (если есть)
    }
}
