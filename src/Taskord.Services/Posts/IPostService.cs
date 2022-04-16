namespace Taskord.Services.Posts
{
    using Taskord.Services.Posts.Models;

    public interface IPostService
    {
        string Post(string userId, string content);

        IEnumerable<PostServiceModel> All(string userId, string viewerId);

        IEnumerable<PostServiceModel> AllFriendsPosts(string userId);

        PostServiceModel GetLatest(string userId);
    }
}
