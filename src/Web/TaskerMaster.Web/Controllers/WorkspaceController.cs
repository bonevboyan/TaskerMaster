namespace TaskerMaster.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskerMaster.Web.ViewModels.Companies;

    public class WorkspaceController : Controller
    {
        //private readonly EfDeletableEntityRepository<Team> data;

        public WorkspaceController()
        {
            //data = new EfDeletableEntityRepository<Team>();
        }

        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Login");
            }

            return this.View();
        }

        public IActionResult Create()
        {
            return this.View(new WorkspaceFormModel());
        }
    }
}
