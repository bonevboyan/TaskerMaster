namespace Taskord.Test.Services
{
    using System;
    using System.Linq;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Xunit;

    public class TeamServiceTest : BaseServiceTests
    {
        private readonly string user1 = "user1";
        private readonly string user2 = "user2";
        private readonly string senderId = "senderId";
        private readonly string receiverId = "receiverId";
        private readonly string teamId = "teamId";
        private readonly string name = "name";
        private readonly string description = "description";
        private readonly string image = "image";

        public TeamServiceTest()
            : base()
        {
            this.data.Users.Add(new User
            {
                Id = senderId
            });

            this.data.Users.Add(new User
            {
                Id = receiverId
            });

            var newUser1 = new User
            {
                Id = user1
            };

            var newUser2 = new User
            {
                Id = user2
            };

            this.data.Users.AddRange(newUser1, newUser2);

            var team = new Team
            {
                Id = teamId,
                Name = name,
                Description = description,
                ImagePath = image
            };

            team.UserTeams.Add(new UserTeam
            {
                UserId = user1,
                State = RelationshipState.Accepted,
                Role = TeamRole.Admin
            });

            team.UserTeams.Add(new UserTeam
            {
                UserId = user2,
                State = RelationshipState.Accepted,
                Role = TeamRole.Member
            });

            team.Chats.Add(new Chat
            {
                ChatType = ChatType.General
            });

            this.data.Teams.Add(team);

            this.data.SaveChanges();
        }

        [Fact]
        public void CreateTeamShouldCreateTeamWithCorrectData()
        {
            var team = this.data.Teams
                .FirstOrDefault(x => x.Id == this.teamService.Create(name, description, image, user1));

            Assert.Equal(2, this.data.Teams.Count());
            Assert.Equal(name, team.Name);
            Assert.Equal(description, team.Description);
            Assert.Equal(image, team.ImagePath);
        }

        [Fact]
        public void GetTeamListShouldReturnCorrectData()
        {
            Assert.Equal(1, this.teamService.GetTeamList(user1).Count());
        }

        [Fact]
        public void GetTeamShouldReturnCorrectData()
        {
            var team = this.teamService.GetTeam(teamId);

            Assert.Equal(teamId, team.Id);
            Assert.Equal(name, team.Name);
            Assert.Equal(description, team.Description);
            Assert.Equal(image, team.ImagePath);
        }

        [Fact]
        public void IsAdminShouldReturnCorrectData()
        {
            Assert.True(this.teamService.IsAdmin(user1, teamId));
            Assert.False(this.teamService.IsAdmin(user2, teamId));
        }

        [Fact]
        public void SendTeamInviteShouldPostCorrectData()
        {
            var inviteId = this.teamService.SendTeamInvite(teamId, senderId, receiverId);

            Assert.Equal(this.data.UserTeams.FirstOrDefault(x => x.State == RelationshipState.Pending).Id, inviteId);
        }

        [Fact]
        public void SendTeamInviteShouldPostCorrectDataWhenRequestIsWithdrawn()
        {
            this.data.UserTeams.Add(new UserTeam
            {
                TeamId = teamId,
                InviterId = senderId,
                UserId = receiverId,
                State = RelationshipState.Withdrawn
            });

            this.data.SaveChanges();

            var inviteId = this.teamService.SendTeamInvite(teamId, senderId, receiverId);

            Assert.Equal(this.data.UserTeams.FirstOrDefault(x => x.State == RelationshipState.Pending).Id, inviteId);
        }

        [Fact]
        public void SendTeamInviteShouldThrowWhenInviteExists()
        {
            this.data.UserTeams.Add(new UserTeam
            {
                TeamId = teamId,
                InviterId = senderId,
                UserId = receiverId,
                State = RelationshipState.Accepted
            });

            this.data.SaveChanges();

            Assert.Throws<ArgumentException>(() => this.teamService.SendTeamInvite(teamId, senderId, receiverId));
        }

        [Fact]
        public void GetTeamInvitesShouldReturnCorrectData()
        {
            this.data.UserTeams.Add(new UserTeam
            {
                TeamId = teamId,
                InviterId = senderId,
                UserId = receiverId,
                State = RelationshipState.Pending
            });

            this.data.SaveChanges();

            var invites = this.teamService.GetTeamInvites(receiverId);

            Assert.Equal(1, invites.Count());
        }

        [Fact]
        public void RespondToTeamInviteShouldPostCorrectData()
        {
            var invite = new UserTeam
            {
                TeamId = teamId,
                InviterId = senderId,
                UserId = receiverId,
                State = RelationshipState.Pending
            };

            this.data.UserTeams.Add(invite);

            this.data.SaveChanges();

            var inviteId = this.teamService.RespondToTeamInvite(invite.Id, true);

            Assert.Equal(invite.Id, inviteId);
            Assert.Equal(3, this.data.UserTeams.Where(x => x.State == RelationshipState.Accepted).Count());
        }

        [Fact]
        public void RespondToTeamInviteShouldThrowIfUserTeamDoesntExist()
        {
            Assert.Throws<ArgumentException>(() => this.teamService.RespondToTeamInvite("invalidId", true));
        }

        [Fact]
        public void WithdrawInviteShouldPostCorrectData()
        {
            var invite = new UserTeam
            {
                TeamId = teamId,
                InviterId = senderId,
                UserId = receiverId,
                State = RelationshipState.Pending
            };

            this.data.UserTeams.Add(invite);

            this.data.SaveChanges();

            var inviteId = this.teamService.WithdrawTeamInvite(teamId, receiverId);

            Assert.Equal(invite.Id, inviteId);
            Assert.Equal(0, this.data.UserTeams.Where(x => x.State == RelationshipState.Pending).Count());
        }

        [Fact]
        public void WithdrawTeamInviteShouldThrowIfUserTeamDoesntExist()
        {
            Assert.Throws<ArgumentException>(() => this.teamService.WithdrawTeamInvite("invalidId", "invalidId"));
        }

        [Fact]
        public void IsUserInvitedShoudlReturnCorrectData()
        {
            var invite = new UserTeam
            {
                TeamId = teamId,
                InviterId = senderId,
                UserId = receiverId,
                State = RelationshipState.Pending
            };

            this.data.UserTeams.Add(invite);

            this.data.SaveChanges();

            var state = this.teamService.IsUserInvited(receiverId, teamId);

            Assert.Equal(RelationshipState.Pending, state);
        }

        [Fact]
        public void ManageMemberRolesShouldPostCorrectData()
        {
            var invite = new UserTeam
            {
                TeamId = teamId,
                InviterId = senderId,
                UserId = receiverId,
                State = RelationshipState.Accepted,
                Role = TeamRole.Member,
            };

            this.data.UserTeams.Add(invite);

            this.data.SaveChanges();

            this.teamService.ManageMemberRole(receiverId, teamId, TeamRole.Admin);

            Assert.Equal(TeamRole.Admin, this.data.UserTeams.FirstOrDefault(x => x.Id == invite.Id).Role);
        }

        [Fact]
        public void ManageMemberRolesShoudlThrowWhenUserTeamDoesntExist()
        {
            Assert.Throws<ArgumentException>(() => this.teamService.ManageMemberRole("invalidId", teamId, TeamRole.Admin));
        }
    }

}
