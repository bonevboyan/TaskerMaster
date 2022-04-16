namespace Taskord.Services.Posts
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Posts.Models;
    using Taskord.Services.Users;
    using Taskord.Services.Relationships;
    using Taskord.Services.Users.Models;

    using static Taskord.Common.ErrorMessages.Post;

    public class PostService : IPostService
    {
        private readonly TaskordDbContext data;
        private readonly IRelationshipService relationshipService;

        public PostService(TaskordDbContext data, IRelationshipService relationshipService)
        {
            this.data = data;
            this.relationshipService = relationshipService;
        }

        public IEnumerable<PostServiceModel> All(string userId, string viewerId)
        {
            if(userId != viewerId && this.relationshipService.GetRelationship(userId, viewerId)?.State != RelationshipState.Accepted)
            {
                throw new ArgumentException(UserNotPermittedToSeePosts);
            }

            var posts = this.GetAll(userId);

            return posts;
        }

        public IEnumerable<PostServiceModel> AllFriendsPosts(string userId)
        {
            var friendsPosts = this.data.Posts
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new PostServiceModel
                {
                    DateTime = x.CreatedOn.ToString("MM/dd HH:mm"),
                    Content = x.Content,
                    User = new UserListServiceModel
                    {
                        Id = x.User.Id,
                        ImagePath = x.User.ImagePath,
                        Name = x.User.UserName,
                    }
                });

            return friendsPosts;
        }

        public PostServiceModel GetLatest(string userId)
        {
            var post = this.GetAll(userId)?.FirstOrDefault();

            return post;
        }

        public string Post(string userId, string content)
        {
            var post = new Post
            {
                UserId = userId,
                Content = content
            };

            this.data.Posts.Add(post);
            this.data.SaveChanges();

            return post.Id;
        }

        private IEnumerable<PostServiceModel> GetAll(string userId)
        {
            var posts = this.data.Posts
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new PostServiceModel
                {
                    DateTime = x.CreatedOn.ToString("MM/dd HH:mm"),
                    Content = x.Content,
                    User = new UserListServiceModel
                    {
                        Id = x.User.Id,
                        ImagePath = x.User.ImagePath,
                        Name = x.User.UserName,
                    }
                });

            return posts;
        }
    }
}
