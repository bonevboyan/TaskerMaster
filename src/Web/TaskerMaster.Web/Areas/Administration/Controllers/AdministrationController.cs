namespace TaskerMaster.Web.Areas.Administration.Controllers
{
    using TaskerMaster.Common;
    using TaskerMaster.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
