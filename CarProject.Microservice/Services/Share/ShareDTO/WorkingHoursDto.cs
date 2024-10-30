

namespace ShareDTO
{
    public class WorkingHoursDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<TimeSlotDto> TimeSlots { get; set; }
    }
}
