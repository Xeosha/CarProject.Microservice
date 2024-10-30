using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Entites
{
    public class DailyWorkingHoursEntity
    {
        public Guid Id { get; set; }
        public Guid IdTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; } // День недели, например, понедельник, вторник и т.д. // можно было вынести в отдельную табл.
       
    }
}
