namespace Taskord.Tests.Pipeline
{
    using System.Collections.Generic;
    using Taskord.Web.Controllers;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using Taskord.Services.Statistics.Models;

    public class HomeControllerTest
    {

        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/"))
                .To<HomeController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<StatisticsServiceModel>());
    }
}