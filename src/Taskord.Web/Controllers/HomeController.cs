namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System.Diagnostics;
    using Taskord.Services.Statistics;
    using Taskord.Services.Statistics.Models;
    using Taskord.Web.Models;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statisticsService;
        private readonly IMemoryCache cache;

        public HomeController(IStatisticsService statisticsService, IMemoryCache cache)
        {
            this.statisticsService = statisticsService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/chats/me");
            }

            var stats = this.cache.Get<StatisticsServiceModel>(AppUsageStatistics);

            if(stats is null)
            {
                stats = this.statisticsService.Total();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                this.cache.Set(AppUsageStatistics, stats, cacheOptions);
            }


            return this.View(stats);
        }
    }
}