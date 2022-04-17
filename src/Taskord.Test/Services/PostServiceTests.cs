namespace Taskord.Test.Services
{
    using System;
    using System.Linq;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Xunit;

    public class PostServiceTests : BaseServiceTests
    {
        private readonly string content = "content";
        private readonly string user1 = "user1";
        private readonly string user2 = "user2";
        private readonly string user3 = "user3";

        public PostServiceTests()
            : base()
        {
            this.data.Posts.Add(new Post
            {
                UserId = user1,
                Content = content
            });

            this.data.Posts.Add(new Post
            {
                UserId = user1,
                Content = content
            });

            this.data.Posts.Add(new Post
            {
                UserId = user2,
                Content = content
            });

            this.data.Relationships.Add(new Relationship
            {
                SenderId = user1,
                ReceiverId = user2,
                State = RelationshipState.Accepted
            });

            this.data.SaveChanges();
        }

        [Fact]
        public void OwnShouldReturnCorrectPosts()
        {
            var posts = this.postService.Own(user1, user1);

            Assert.Equal(2, posts.Count());
            Assert.True(posts.All(x => x.Content == content));
        }

        [Fact]
        public void OwnShouldNotThrowWhenUsersAreFriends()
        {
            var posts = this.postService.Own(user1, user2);

            Assert.Equal(2, posts.Count());
            Assert.Contains(posts, x => x.Content == content);
        }

        [Fact]
        public void OwnShouldThrowWhenUsersAreNotFriends()
        {
            Assert.Throws<ArgumentException>(() => this.postService.Own(user1, user3));
        }

        [Fact]
        public void AllShouldReturnCorrectPosts()
        {
            var posts = this.postService.All();

            Assert.Equal(3, posts.Count());
            Assert.True(posts.All(x => x.Content == content));
        }

        [Fact]
        public void PostShouldAddCorrectPost()
        {
            this.postService.Post("newUser", "newContent");

            var posts = this.postService.All();

            Assert.Equal(4, posts.Count());
            Assert.Contains(posts, x => x.Content == "newContent");
        }

        [Fact]
        public void DeleteShouldDeleteCorrectPost()
        {
            var post = this.postService.GetLatest(user2);
            this.postService.Delete(post.Id);

            var posts = this.postService.All();

            Assert.Equal(2, posts.Count());
            Assert.DoesNotContain(posts, x => x.User.Id == user2);
        }

        [Fact]
        public void DeleteShouldThrowWhenInvalidPostId()
        {
            Assert.Throws<ArgumentException>(() => this.postService.Delete("invalidPost"));
        }
    }
}
