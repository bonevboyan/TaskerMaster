namespace Taskord.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Taskord.Web.Controllers;
    using Xunit;

    public class PostsControllerTest
    {
        [Fact]
        public void AllPostsShouldReturnViewWithCorrectModelAndData()
            => MyRouting
                .Configuration()
                .ShouldMap("/posts/all")
                .To<PostsController>(c => c.All());

        [Theory]
        [InlineData("me")]
        [InlineData("123")]
        public void PersonalPostsShouldReturnViewWithCorrectModelAndData(string userId)
            => MyRouting
                .Configuration()
                .ShouldMap("/posts/personal/" + userId)
                .To<PostsController>(c => c.Personal(userId));
    }
}
