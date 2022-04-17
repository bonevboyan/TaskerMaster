namespace Taskord.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Services.Posts;
    using Taskord.Services.Relationships;
    using Taskord.Services.Statistics;
    using Taskord.Services.Teams;
    using Taskord.Services.Users;
    using Taskord.Web.Extensions;
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TaskordDbContext>(options =>
            options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IRelationshipService, RelationshipService>();
            services.AddTransient<IStatisticsService, StatisticsService>();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TaskordDbContext>();

            services.AddMemoryCache();

            services.AddMvc(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "personal",
                   pattern: "me/{action}/{userId?}",
                   defaults: new { controller = "User" });

                endpoints.MapControllerRoute(
                    name: "posts",
                    pattern: "posts/{action=Personal}/{userId?}",
                    defaults: new { controller = "Posts" });

                endpoints.MapControllerRoute(
                    name: "chats",
                    pattern: "chats/{teamId}/{chatId?}",
                    defaults: new { controller = "Chats", action = "Get" });

                endpoints.MapControllerRoute(
                    name: "teams",
                    pattern: "teams/{teamId}/{action}/{chatId?}",
                    defaults: new { controller = "Teams" });

                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

        }

    }
}
