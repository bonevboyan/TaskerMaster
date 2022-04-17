namespace Taskord.Test.Pipeline
{
    using MyTested.AspNetCore.Mvc;
    using Taskord.Services.Statistics.Models;
    using Taskord.Web.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .View(view => view.WithModelOfType<StatisticsServiceModel>());

        [Fact]
        public void IndexLoggedInRouteShouldRedirect()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/")
                    .WithUser())
                .To<HomeController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Redirect("/chats/me");

        [Fact]
        public void AboutShouldReturnViewWithCorrectModelAndData()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/About")
                .To<HomeController>(c => c.About())
                .Which()
                .ShouldReturn()
                .View();

        [Fact]
        public void AboutLoggedInRouteShouldRedirect()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Home/About")
                    .WithUser())
                .To<HomeController>(c => c.About())
                .Which()
                .ShouldReturn()
                .Redirect("/chats/me");

    }
}
