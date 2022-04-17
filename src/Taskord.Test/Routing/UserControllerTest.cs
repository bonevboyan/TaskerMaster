namespace Taskord.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Taskord.Web.Controllers;
    using Taskord.Web.Models;
    using Xunit;

    public class UserControllerTest
    {
        [Fact]
        public void RequestsRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("me/requests")
                .To<UserController>(c => c.Requests());

        [Theory]
        [InlineData(1, null)]
        [InlineData(4, "abc")]
        public void SearchRouteShouldBeMapped(int currentPage, string searchTerm)
            => MyRouting
                .Configuration()
                .ShouldMap($"/me/search?{"SearchTerm=" + searchTerm}&{"currentPage=" + currentPage}")
                .To<UserController>(c => c.Search(new UserQueryModel
                {
                    CurrentPage = currentPage,
                    SearchTerm = searchTerm,
                    TotalUsers = 0,
                    Users = null
                }));

        [Theory]
        [InlineData("userId1")]
        [InlineData("me")]
        public void ProfileRouteShouldBeMapped(string userId)
            => MyRouting
                .Configuration()
                .ShouldMap($"/me/profile/{userId}")
                .To<UserController>(c => c.Profile(userId));

    }
}
