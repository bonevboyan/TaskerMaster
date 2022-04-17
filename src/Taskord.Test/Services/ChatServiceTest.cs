namespace Taskord.Test.Services
{
    using System;
    using System.Linq;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Xunit;

    public class ChatServiceTest : BaseServiceTests
    {
        private readonly string user1 = "user1";
        private readonly string user2 = "user2";
        private readonly string teamId = "teamId";
        private readonly string chatId;
        private readonly string message = "message";

        public ChatServiceTest()
            :base()
        {
            var newUser1 = new User
            {
                Id = user1
            };

            var newUser2 = new User
            {
                Id = user2
            };

            this.data.Users.AddRange(newUser1, newUser2);

            this.relationshipService.SendFriendRequest(user1, user2);
            this.relationshipService.ChangeRelationshipState(user1, user2, RelationshipState.Accepted);

            var chat = this.data.Chats.FirstOrDefault();
            chatId = chat.Id;

            this.data.Teams.Add(new Team
            {
                Id = teamId
            });

            this.data.SaveChanges();
        }

        [Fact]
        public void GetPersonalChatShouldReturnCorrectData()
        {
            var chat = this.chatService.GetPersonalChat(user1, user2);

            Assert.Equal(chatId, chat.Id);
        }

        [Fact]
        public void GetPersonalChatShouldThrowIfChatDoesntExist()
        {
            Assert.Throws<ArgumentException>(() => this.chatService.GetPersonalChat(user1, "invalidUser"));
        }

        [Fact]
        public void GetPersonalChatShouldReturnCorrectDataWhenSecondUserIdIsNull()
        {
            var chat = this.chatService.GetPersonalChat(user1, null);

            Assert.Equal(chatId, chat.Id);
        }

        [Fact]
        public void GetPersonalChatShouldReturnNullWhenUserIsInNoChats()
        {
            this.data.Chats.Remove(this.data.Chats.FirstOrDefault());
            this.data.SaveChanges();

            var chat = this.chatService.GetPersonalChat(user1, null);

            Assert.Null(chat);
        }

        [Fact]
        public void SendMessageShouldSendCorrectInformation()
        {
            var messageId = this.chatService.SendMessage(chatId, user1, message);

            var sentMessage = this.data.Messages.FirstOrDefault();

            Assert.Equal(messageId, sentMessage.Id);
            Assert.Equal(message, sentMessage.Content);
        }

        [Fact]
        public void SendMessageShouldThrowIfChatDoesntExist()
        {
            Assert.Throws<ArgumentException>(() => this.chatService.SendMessage("invalidId", user1, message));
        }

        [Fact]
        public void SendMessageShouldThrowIfUserIsNotParticipant()
        {
            Assert.Throws<ArgumentException>(() => this.chatService.SendMessage(chatId, "invalidId", message));
        }

        [Fact]
        public void CreateTeamChatShouldCreateTeamWithCorrectData()
        {
            this.chatService.CreateTeamChat(teamId, "name");

            Assert.Equal(2, this.data.Chats.Count());
        }

        [Fact]
        public void CreateTeamChatShouldThrowWhenInvalidTeamId()
        {
            Assert.Throws<ArgumentException>(() => this.chatService.CreateTeamChat("invalidId", "name"));
        }

        //[Fact]
        //public void GetTeamChatShouldReturnCorrectData()
        //{
        //    var team = this.data.Teams.FirstOrDefault(x => x.Id == teamId);

        //    this.chatService.CreateTeamChat(teamId, "name");

        //    var chat = this.data.Chats
        //        .Include(x => x.ChatUsers)
        //        .FirstOrDefault(x => x.TeamId == teamId);

        //    chat.Users.Add(this.data.Users.FirstOrDefault(x => x.Id == user1));
        //    chat.Users.Add(this.data.Users.FirstOrDefault(x => x.Id == user2));
        //    chat.ChatUsers.Add(new ChatUser
        //    {
        //        ChatId = chatId,
        //        UserId = user1
        //    });
        //    chat.ChatUsers.Add(new ChatUser
        //    {
        //        ChatId = chatId,
        //        UserId = user2
        //    });

        //    this.data.SaveChanges();

        //    var teamChat = this.chatService.GetTeamChat(user1, teamId, chat.Id);

        //    Assert.Equal(chat.Id, teamChat.Id);
        //}

        [Fact]
        public void GetTeamChatShouldThrowIfChatDoesntExist()
        {
            Assert.Throws<ArgumentException>(() => this.chatService.GetTeamChat(user1, teamId, "invalidId"));
        }

        [Fact]
        public void GetPersonalChatShouldReturnNullWhenTeamHasNoChats()
        {
            var chat = this.chatService.GetTeamChat(user1, teamId, null);

            Assert.Null(chat);
        }
    }
}
