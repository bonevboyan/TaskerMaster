namespace Taskord.Test.Pipeline
{
    using MyTested.AspNetCore.Mvc;
    using Taskord.Web.Controllers;
    using Xunit;

    public class PostsControllerTest
    {
        [Fact]
        public void AllLoggedInRouteShouldShowView()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/posts/all")
                    .WithUser())
                .To<PostsController>(c => c.All())
                .Which()
                .ShouldReturn()
                .View();

    }
}
