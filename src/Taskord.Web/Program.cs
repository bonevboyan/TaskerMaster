using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskord.Data;
using Taskord.Data.Models;
using Taskord.Services.Chats;
using Taskord.Services.Schedules;
using Taskord.Services.Teams;
using Taskord.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskordDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TaskordDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IScheduleService, ScheduleService>();
builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "personalChat",
    pattern: "me/{action=All}/{userId?}",
    defaults: new { controller = "Personal" });

app.MapControllerRoute(
    name: "chats",
    pattern: "chats/{chatId}",
    defaults: new { controller = "Chats", action = "Chats" });

app.MapControllerRoute(
    name: "chats",
    pattern: "chats/{teamId}/{action}",
    defaults: new { controller = "Chats"});

app.MapControllerRoute(
    name: "schedule",
    pattern: "schedule/{teamId}/{action=Board}",
    defaults: new { controller = "Schedules"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
