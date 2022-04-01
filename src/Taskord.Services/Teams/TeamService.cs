namespace Taskord.Services.Teams
{
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Teams.Models;

    public class TeamService : ITeamService
    {
        private readonly TaskordDbContext data;

        public TeamService(TaskordDbContext data)
        {
            this.data = data;
        }

        public string Create(string name, string description, string imageUrl, string userId)
        {
            var schedule = new Schedule();

            schedule.Buckets.Add(new Bucket
            {
                Name = "To Do",
            });

            schedule.Buckets.Add(new Bucket
            {
                Name = "Done",
            });

            var team = new Team
            {
                Name = name,
                Description = description,
                ImagePath = imageUrl,
                ScheduleId = schedule.Id,
            };

            var chat = new Chat
            {
                Name = "General",
                ChatType = ChatType.Team
            };

            var userTeam = new UserTeam
            {
                Role = TeamRole.Admin,
                UserId = userId,
                TeamId = team.Id,
            };

            var chatUser = new ChatUser
            {
                LastReadMessageId = null,
                ChatId = chat.Id,
                UserId = userId
            };

            chat.Users.Add(this.data.Users.FirstOrDefault(x => x.Id == userId));
            team.Chats.Add(chat);
            team.UserTeams.Add(userTeam);
            this.data.Schedules.Add(schedule);
            this.data.ChatUsers.Add(chatUser);
            this.data.Teams.Add(team);
            this.data.SaveChanges();

            return team.Id;
        }

        public IEnumerable<TeamListServiceModel> GetTeamList(string userId)
        {
            var teamList = this.data.Users
                .FirstOrDefault(x => x.Id == userId)
                .UserTeams
                .Select(t => new TeamListServiceModel
                {
                    ImagePath = t.Team.ImagePath,
                    Name = t.Team.Name,
                    Id = t.Team.Id,
                })
                .ToList();

            return teamList;
        }

        public TeamListServiceModel GetTeam(string teamId)
        {
            var team = this.data.Teams
                .FirstOrDefault(x => x.Id == teamId);

            return new TeamListServiceModel
            {
                Id = team.Id,
                Name = team.Name,
                ImagePath = team.ImagePath,
                Description = team.Description
            };
        }

        public bool IsAdmin(string userId, string teamId)
        {
            var userTeam = this.data.UserTeams
                .FirstOrDefault(x => x.TeamId == teamId && x.UserId == userId);

            return userTeam?.Role == TeamRole.Admin;
        }

        public string SendTeamInvite(string teamId, string senderId, string receiverId)
        {
            var invite = new TeamInvite
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                TeamId = teamId
            };

            this.data.TeamInvites.Add(invite);
            this.data.SaveChanges();

            return invite.Id;
        }
    }
}
