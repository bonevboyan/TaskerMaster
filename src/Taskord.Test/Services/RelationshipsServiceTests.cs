namespace Taskord.Test.Services
{
    using System;
    using System.Linq;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Xunit;

    public class RelationshipsServiceTests : BaseServiceTests
    {
        private readonly string user1 = "user1";
        private readonly string user2 = "user2";
        private readonly string user3 = "user3";

        public RelationshipsServiceTests()
            : base()
        {
            this.data.Users.AddRange(new User
            {
                Id = user1
            },
            new User
            {
                Id = user2
            },
            new User
            {
                Id = user3
            });

            this.data.Relationships.AddRange(new Relationship
            {
                SenderId = user1,
                ReceiverId = user2,
                State = RelationshipState.Pending
            },
            new Relationship
            {
                SenderId = user2,
                ReceiverId = user3,
                State = RelationshipState.Pending
            });

            this.data.SaveChanges();
        }

        [Fact]
        public void ReceivedRequestsShouldReturnCorrectData()
        {
            Assert.Equal(1, this.relationshipService.GetUserReceivedFriendRequests(user2).Count());
        }

        [Fact]
        public void SentRequestsShouldReturnCorrectData()
        {
            Assert.Equal(1, this.relationshipService.GetUserSentFriendRequests(user1).Count());
        }

        [Fact]
        public void SendFriendRequestShouldThrowWhenIdsMatch()
        {
            Assert.Throws<ArgumentException>(() => this.relationshipService.SendFriendRequest(user1, user1));
        }

        [Fact]
        public void SendFriendRequestShouldPostCorrectData()
        {
            this.relationshipService.SendFriendRequest(user1, user3);

            Assert.Equal(3, this.data.Relationships.Count());
            Assert.True(this.data.Relationships.All(x => x.State == RelationshipState.Pending));
        }

        [Fact]
        public void SendFriendRequestShouldThrowWhenUsersAreFriends()
        {
            this.data.Relationships
                .FirstOrDefault(x => x.SenderId == user1 && x.ReceiverId == user2)
                .State = RelationshipState.Accepted;

            this.data.SaveChanges();

            Assert.Throws<ArgumentException>(() => this.relationshipService.SendFriendRequest(user1, user2));
        }

        [Fact]
        public void SendFriendRequestShouldModifyStateToPending()
        {
            this.data.Relationships
                .FirstOrDefault(x => x.SenderId == user1 && x.ReceiverId == user2)
                .State = RelationshipState.Withdrawn;

            this.data.SaveChanges();

            this.relationshipService.SendFriendRequest(user1, user2);

            Assert.Equal(RelationshipState.Pending, 
                this.data.Relationships.FirstOrDefault(x => x.SenderId == user1 && x.ReceiverId == user2).State);
        }

        [Fact]
        public void ChangeStateShouldModifyStateCorrectly()
        {
            this.relationshipService.ChangeRelationshipState(user1, user2, RelationshipState.Declined);

            Assert.Equal(RelationshipState.Declined,
                this.data.Relationships.FirstOrDefault(x => x.SenderId == user1 && x.ReceiverId == user2).State);
        }

        [Fact]
        public void ChangeStateShouldThrowIfInvalidRelationship()
        {
            Assert.Throws<ArgumentException>(() => 
                this.relationshipService.ChangeRelationshipState("invalidId", "invalidId2", RelationshipState.Declined));
        }

        [Fact]
        public void ChangeStateShouldCreateChatWhenAccepted()
        {
            this.relationshipService.ChangeRelationshipState(user1, user2, RelationshipState.Accepted);

            Assert.Equal(1,
                this.data.Chats.Count());
        }
    }
}
