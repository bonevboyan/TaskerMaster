namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Member")]
    public class MembersViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
