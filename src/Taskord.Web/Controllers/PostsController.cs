namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Posts;
    using Taskord.Services.Users;

    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<User> userManager;

        public PostsController(IPostService postService, UserManager<User> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        public IActionResult All(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                var posts = this.postService.All(userId, myUserId);

                return this.View();
            }
            catch(ArgumentException ex)
            {
                return this.StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
