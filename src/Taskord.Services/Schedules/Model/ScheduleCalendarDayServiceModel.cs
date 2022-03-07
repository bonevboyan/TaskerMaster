namespace Taskord.Services.Schedules.Model
{
    public class ScheduleCalendarDayServiceModel
    {
        public DateTime Date { get; set; }

        public IEnumerable<ScheduleCardServiceModel> Cards { get; set; }
    }
}
