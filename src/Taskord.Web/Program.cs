using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskord.Data;
using Taskord.Services.Chats;
using Taskord.Services.Schedules;
using Taskord.Services.Teams;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskordDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TaskordDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IScheduleService, ScheduleService>();
builder.Services.AddTransient<IChatService, ChatService>();

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
    pattern: "chats/@me/{chatId}",
    defaults: new { controller = "Chats", action = "Personal" });

app.MapControllerRoute(
    name: "chats",
    pattern: "chats/{teamId}/{chatId}",
    defaults: new { controller = "Chats", action = "ById" });

app.MapControllerRoute(
    name: "schedule",
    pattern: "schedule/{teamId}/{action=Board}",
    defaults: new { controller = "Schedules"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
