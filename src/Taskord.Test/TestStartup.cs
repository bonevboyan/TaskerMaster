namespace Taskord.Test
{
    using Microsoft.Extensions.Configuration;
    using Taskord.Web;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
