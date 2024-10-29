﻿

namespace CatalogService.Domain.Interfaces.Models
{
    public class ServiceModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
