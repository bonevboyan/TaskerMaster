namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Chats")]
    public class ChatsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
