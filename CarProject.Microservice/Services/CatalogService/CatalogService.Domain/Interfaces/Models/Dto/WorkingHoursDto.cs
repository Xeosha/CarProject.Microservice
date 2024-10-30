using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces.Models.Dto
{
    public class WorkingHoursDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<TimeSlotDto> TimeSlots { get; set; }
    }
}
