using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.DTOs
{
    public class CreateServiceDto
    {
        public Guid OrgId { get; set; }
        public Guid ServiceId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
