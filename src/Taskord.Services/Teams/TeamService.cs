namespace Taskord.Services.Teams
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Teams.Models;
    using Taskord.Services.Users.Models;

    using static Taskord.Common.ErrorMessages.Team;

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
                State = RelationshipState.Accepted,
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
            var userTeam = this.data.UserTeams.FirstOrDefault(x => x.TeamId == teamId && x.InviterId == senderId && x.UserId == receiverId);

            if (userTeam is null)
            {
                var invite = new UserTeam
                {
                    InviterId = senderId,
                    UserId = receiverId,
                    TeamId = teamId
                };

                this.data.UserTeams.Add(invite);
                this.data.SaveChanges();

                return invite.Id;
            }
            else if(userTeam.State == RelationshipState.Withdrawn)
            {
                userTeam.State = RelationshipState.Pending;
                this.data.SaveChanges();

                return userTeam.Id;
            }
            else
            {
                throw new ArgumentException(InviteExists);
            }
        }

        public IEnumerable<TeamInviteServiceModel> GetTeamInvites(string userId)
        {
            var invites = this.data.UserTeams
                .Include(x => x.Team)
                .Include(x => x.Inviter)
                .Where(x => x.UserId == userId && x.State == RelationshipState.Pending)
                .Select(x => new TeamInviteServiceModel
                {
                    Id = x.Id,
                    Team = new TeamListServiceModel
                    {
                        Name = x.Team.Name,
                        Id = x.Team.Id,
                        Description = x.Team.Description,
                        ImagePath = x.Team.ImagePath
                    },
                    Sender = new UserListServiceModel
                    {
                        Id = x.Inviter.Id,
                        ImagePath = x.Inviter.ImagePath,
                        Name = x.Inviter.UserName,
                    }
                })
                .ToList();

            return invites;
        }

        public string RespondToTeamInvite(string teamInviteId, bool isAccepted)
        {
            var userTeam = this.data.UserTeams.FirstOrDefault(x => x.Id == teamInviteId);

            if (userTeam == null)
            {
                throw new ArgumentException(InvalidTeamInvite);
            }

            if (isAccepted)
            {
                userTeam.Role = TeamRole.Member;
            }

            userTeam.State = isAccepted ? RelationshipState.Accepted : RelationshipState.Declined;
            this.data.SaveChanges();

            return userTeam.Id;
        }

        public string WithdrawTeamInvite(string teamId, string userId)
        {
            var userTeam = this.data.UserTeams.FirstOrDefault(x => x.TeamId == teamId && x.UserId == userId);

            if (userTeam == null)
            {
                throw new ArgumentException(InvalidTeamInvite);
            }

            userTeam.State = RelationshipState.Withdrawn;
            this.data.SaveChanges();

            return userTeam.Id;
        }
    }
}
