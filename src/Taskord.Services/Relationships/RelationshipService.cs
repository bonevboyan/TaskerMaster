namespace Taskord.Services.Relationships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats;
    using Taskord.Services.Relationships.Models;
    using Taskord.Services.Users.Models;

    using static Taskord.Common.ErrorMessages.Relationship;

    public class RelationshipService : IRelationshipService
    {
        private readonly TaskordDbContext data;
        private readonly IChatService chatService;

        public RelationshipService(TaskordDbContext data, IChatService chatService)
        {
            this.data = data;
            this.chatService = chatService;
        }

        public IEnumerable<UserListServiceModel> GetUserReceivedFriendRequests(string userId)
        {
            var pendingRequests = this.data.Relationships
                .Where(x => x.ReceiverId == userId && x.State == RelationshipState.Pending)
                .Select(x => new UserListServiceModel
                {
                    ImagePath = x.Sender.ImagePath,
                    Name = x.Sender.UserName,
                    Id = x.Sender.Id
                })
                .ToList();

            return pendingRequests;
        }

        public IEnumerable<UserListServiceModel> GetUserSentFriendRequests(string userId)
        {
            var pendingRequests = this.data.Relationships
                .Include(x => x.Receiver)
                .Where(x => x.SenderId == userId && x.State == RelationshipState.Pending)
                .Select(x => new UserListServiceModel
                {
                    ImagePath = x.Receiver.ImagePath,
                    Name = x.Receiver.UserName,
                    Id = x.Receiver.Id
                })
                .ToList();

            return pendingRequests;
        }

        public string SendFriendRequest(string senderId, string receiverId)
        {
            if (senderId == receiverId)
            {
                throw new ArgumentException(InvalidFriendRequestParameters);
            }

            var relationship = this.data.Relationships
                .FirstOrDefault(x => x.SenderId == senderId && x.ReceiverId == receiverId
                || x.ReceiverId == senderId && x.SenderId == receiverId);

            if (relationship?.State == RelationshipState.Accepted)
            {
                throw new ArgumentException(FriendshipAlreadyExists);
            }

            if (relationship == null)
            {

                relationship = new Relationship
                {
                    SenderId = senderId,
                    ReceiverId = receiverId
                };

                this.data.Relationships.Add(relationship);
            }
            else
            {
                relationship.State = RelationshipState.Pending;
            }

            this.data.SaveChanges();

            return relationship.Id;
        }

        public void ChangeRelationshipState(string senderId, string receiverId, RelationshipState state)
        {
            var request = this.data.Relationships
                .FirstOrDefault(x => x.SenderId == senderId && x.ReceiverId == receiverId);

            if (request is null)
            {
                throw new ArgumentException(InvalidFriendRequest);
            }


            request.State = state;

            if (state == RelationshipState.Accepted)
            {
                var chatId = this.chatService.CreatePersonalChat(senderId, receiverId);

                request.ChatId = chatId;
            }

            this.data.SaveChanges();
        }

        public RelationshipServiceModel GetRelationship(string userId, string secondUserId)
        {
            var relationship = this.data.Relationships
                .FirstOrDefault(x => (x.SenderId == userId && x.ReceiverId == secondUserId)
                    || (x.ReceiverId == userId && x.SenderId == secondUserId));

            return relationship is null ? null : new RelationshipServiceModel
            {
                ReceiverId = relationship.ReceiverId,
                SenderId = relationship.SenderId,
                State = relationship.State
            };
        }
    }
}
