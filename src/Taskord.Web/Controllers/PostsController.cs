namespace Taskord.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Posts;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public PostsController(IPostService postService, UserManager<User> userManager, IUserService userService)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.userService = userService;
        }

        public IActionResult Personal(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            if (userId == "me")
            {
                userId = myUserId;
            }

            try
            {
                var posts = this.postService.Own(userId, myUserId);

                var postsModel = new PersonalPostsViewModel
                {
                    Posts = posts,
                    IsOwn = myUserId == userId,
                    User = this.userService.GetUserListModel(userId)
                };

                return this.View(postsModel);
            }
            catch (ArgumentException ex)
            {
                return this.View("Error", ex.Message);
            }
        }

        public IActionResult All()
        {
            try
            {
                var posts = this.postService.All();

                return this.View(posts);
            }
            catch (ArgumentException ex)
            {
                return this.View("Error", ex.Message);
            }
        }
    }
}
