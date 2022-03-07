namespace Taskord.Services.Schedules.Model
{
    public class ScheduleBoardServiceModel
    {
        public string Name { get; set; }

        public IEnumerable<ScheduleBoardBucketServiceModel> Buckets { get; set; }
    }
}
