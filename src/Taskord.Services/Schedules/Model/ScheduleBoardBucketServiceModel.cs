namespace Taskord.Services.Schedules.Model
{
    public class ScheduleBoardBucketServiceModel
    {
        public string Name { get; set; }

        public IEnumerable<ScheduleCardServiceModel> Cards { get; set; }
    }
}
