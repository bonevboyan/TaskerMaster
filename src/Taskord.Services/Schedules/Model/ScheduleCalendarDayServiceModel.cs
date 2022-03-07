namespace Taskord.Services.Schedules.Model
{
    public class ScheduleCalendarDayServiceModel
    {
        public DateTime Date { get; set; }

        public IEnumerable<ScheduleCalendarCardServiceModel> Cards { get; set; }
    }
}
