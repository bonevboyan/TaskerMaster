namespace Taskord.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Taskord.Web.Controllers;
    using Xunit;

    public class ChatsControllerTest
    {
        [Theory]
        [InlineData("team1", "chat1")]
        [InlineData("me", "user1")]
        public void GetRouteShouldBeMapped(string teamId, string chatId)
            => MyRouting
                .Configuration()
                .ShouldMap($"chats/{teamId}/{chatId}")
                .To<ChatsController>(c => c.Get(teamId, chatId));
    }
}
