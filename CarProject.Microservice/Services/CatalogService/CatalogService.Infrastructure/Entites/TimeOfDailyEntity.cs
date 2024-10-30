
namespace CatalogService.Infrastructure.Entites
{
    public class TimeOfDailyEntity
    {
        public Guid Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
