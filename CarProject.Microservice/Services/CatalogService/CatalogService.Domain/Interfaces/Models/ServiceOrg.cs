﻿
namespace CatalogService.Domain.Interfaces.Models
{
    public class ServiceOrg
    {
        public long Id { get; set; }
        public long IdOrganization { get; set; }
        public long IdService { get; set; }
        public int Price { get; set; } = 0;
        public DateTime dateTime { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
    }
}