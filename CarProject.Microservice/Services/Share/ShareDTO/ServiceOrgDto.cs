using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareDTO
{
    public class ServiceOrgDto
    {
        public Guid Id { get; set; }
        public Guid IdOrganization { get; set; }
        public Guid IdService { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
