namespace Taskord.Web.Controllers.Api
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Posts;
    using Taskord.Web.Models.Api;

    [Authorize]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class PostsApiController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly UserManager<User> userManager;

        public PostsApiController(IPostService postService, UserManager<User> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("api/posts/create")]
        public IActionResult Post(CreatePostApiModel post)
        {
            var userId = this.userManager.GetUserId(this.User);

            try
            {
                this.postService.Post(userId, post.Content);

                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}
