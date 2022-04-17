namespace Taskord.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Taskord.Web.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void AboutRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/About")
                .To<HomeController>(c => c.About());

    }
}
