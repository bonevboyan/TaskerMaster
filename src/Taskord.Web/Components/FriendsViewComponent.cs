namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Services.Chats;
    using Taskord.Services.Teams;
    using Taskord.Web.Models;

    [ViewComponent(Name = "Friends")]
    public class FriendsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
