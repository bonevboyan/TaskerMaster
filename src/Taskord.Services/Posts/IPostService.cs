namespace Taskord.Services.Posts
{
    using Taskord.Services.Posts.Models;

    public interface IPostService
    {
        string Post(string userId, string content);

        IEnumerable<PostServiceModel> Own(string userId, string viewerId);

        IEnumerable<PostServiceModel> All();

        PostServiceModel GetLatest(string userId);

        void Delete(string postId);
    }
}
