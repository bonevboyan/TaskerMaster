namespace Taskord.Services.Schedules.Model
{
    public class ScheduleCalendarServiceModel
    {
        public string Name { get; set; }

        public IEnumerable<ScheduleCalendarDayServiceModel> Days { get; set; }
    }
}
