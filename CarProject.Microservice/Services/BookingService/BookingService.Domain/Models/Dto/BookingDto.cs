using ShareDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Models.Dto
{
    public class BookingDto
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string Notes { get; set; } = string.Empty;
        public ServiceOrgDto ServiceOrg { get; set; }  // Вложенный объект
    }
}
