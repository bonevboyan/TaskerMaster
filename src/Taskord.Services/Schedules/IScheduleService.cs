namespace Taskord.Services.Schedules
{
    using Taskord.Services.Schedules.Model;

    public interface IScheduleService
    {
        ScheduleBoardServiceModel GetBoard(string teamId);

        ScheduleCalendarServiceModel GetCalendar(string teamId);
    }
}
