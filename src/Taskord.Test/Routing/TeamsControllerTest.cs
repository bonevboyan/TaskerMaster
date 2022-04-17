namespace Taskord.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Taskord.Web.Controllers;
    using Xunit;

    public class TeamsControllerTest
    {
        [Fact]
        public void CreateRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("teams/Create")
                .To<TeamsController>(c => c.Create());

        [Theory]
        [InlineData("teamId1")]
        public void InviteRouteShouldBeMapped(string teamId)
            => MyRouting
                .Configuration()
                .ShouldMap($"/teams/{teamId}/inviteMembers")
                .To<TeamsController>(c => c.InviteMembers(teamId));

        [Theory]
        [InlineData("teamId1", "chatId1")]
        public void ManageChatRouteShouldBeMapped(string teamId, string chatId)
            => MyRouting
                .Configuration()
                .ShouldMap($"/teams/{teamId}/manageChatMembers/{chatId}")
                .To<TeamsController>(c => c.ManageChatMembers(teamId, chatId));

        [Theory]
        [InlineData("teamId1")]
        public void ManageMembersRouteShouldBeMapped(string teamId)
            => MyRouting
                .Configuration()
                .ShouldMap($"/teams/{teamId}/manageMemberRoles")
                .To<TeamsController>(c => c.ManageMemberRoles(teamId));
    }
}
