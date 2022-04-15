namespace Taskord.Services.Schedules.Model
{
    using Taskord.Data.Models.Enums;

    public class ScheduleCardServiceModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string State { get; set; }

        public IEnumerable<ScheduleTagServiceModel> Tags { get; set; }
    }
}
