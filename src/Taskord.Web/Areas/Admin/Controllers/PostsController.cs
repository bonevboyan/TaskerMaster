namespace Taskord.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Services.Posts;

    public class PostsController : AdminController
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult All()
        {
            var posts = this.postService.All();
            return this.View(posts);
        }

        public IActionResult Delete(string id)
        {
            try
            {
                this.postService.Delete(id);

                return this.Redirect("/Admin/Posts/All");
            }
            catch(ArgumentException ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}
