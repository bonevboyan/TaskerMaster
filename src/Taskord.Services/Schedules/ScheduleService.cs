namespace Taskord.Services.Schedules
{
    using System.Linq;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Services.Schedules.Model;

    public class ScheduleService : IScheduleService
    {

        private readonly TaskordDbContext data;

        public ScheduleService(TaskordDbContext data)
        {
            this.data = data;
        }

        public ScheduleBoardServiceModel GetBoard(string teamId)
        {
            var team = GetTeamFromId(teamId);
            var buckets = data.Schedules.Find(team.ScheduleId).Buckets;

            ScheduleBoardServiceModel boardCardServiceModel = new ScheduleBoardServiceModel
            {
                Name = team.Name + " Schedule Board",
                Buckets = buckets.Select(b => new ScheduleBoardBucketServiceModel
                {
                    Name = b.Name,
                    Cards = b.Cards.Select(c => new ScheduleCardServiceModel
                    {
                        Description = c.Description,
                        Name = c.Name,
                        State = c.State.ToString(),
                        Tags = c.Tags.Select(t => new ScheduleTagServiceModel
                        {
                            Color = t.Color,
                            Name = t.Name
                        }),
                    })
                }),
            };

            return boardCardServiceModel;            
        }

        public ScheduleCalendarServiceModel GetCalendar(string teamId)
        {
            var team = GetTeamFromId(teamId);

            var cards = data.Schedules.Find(team.ScheduleId).Buckets.SelectMany(x => x.Cards);


            //TODO
            //ScheduleCalendarServiceModel calendarServiceModel = new ScheduleCalendarServiceModel
            //{
            //    Name = team.Name + " Schedule Calendar",
            //    Days = cards
            //        .GroupBy(c => c.DueDate)
            //        .Select(c => c.)
            //};

            throw new NotImplementedException();
        }

        private Team GetTeamFromId(string teamId)
        {
            return data.Teams.Find(teamId);
        }
    }
}
