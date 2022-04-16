using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskord.Data;
using Taskord.Data.Models;
using Taskord.Services.Chats;
using Taskord.Services.Posts;
using Taskord.Services.Relationships;
using Taskord.Services.Statistics;
using Taskord.Services.Teams;
using Taskord.Services.Users;
using Taskord.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskordDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IRelationshipService, RelationshipService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();

builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services
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

var app = builder.Build();

app.PrepareDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

app.MapControllerRoute(
    name: "personal",
    pattern: "me/{action}/{userId?}",
    defaults: new { controller = "User" });

app.MapControllerRoute(
    name: "posts",
    pattern: "posts/{action=Personal}/{userId?}",
    defaults: new { controller = "Posts" });

app.MapControllerRoute(
    name: "chats",
    pattern: "chats/{teamId}/{chatId?}",
    defaults: new { controller = "Chats", action = "Get" });

app.MapControllerRoute(
    name: "teams",
    pattern: "teams/{teamId}/{action}/{id?}",
    defaults: new { controller = "Teams" });

app.MapControllerRoute(
    name: "schedule",
    pattern: "schedule/{teamId}/{action=Board}",
    defaults: new { controller = "Schedules" });

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
