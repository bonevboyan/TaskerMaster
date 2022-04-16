namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats;
    using Taskord.Services.Posts;
    using Taskord.Services.Relationships;
    using Taskord.Services.Teams;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IChatService chatService;
        private readonly IRelationshipService relationshipService;
        private readonly ITeamService teamService;
        private readonly IPostService postService;
        private readonly UserManager<User> userManager;

        public UserController(IUserService userService, 
            UserManager<User> userManager, 
            IChatService chatService,
            ITeamService teamService, 
            IRelationshipService relationshipService,
            IPostService postService)
        {
            this.teamService = teamService;
            this.chatService = chatService;
            this.relationshipService = relationshipService;
            this.userService = userService;
            this.userManager = userManager;
            this.postService = postService;
        }

        public IActionResult SendRequest(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                this.relationshipService.SendFriendRequest(myUserId, userId);
                return this.Redirect("/chats/me");
            }
            catch (ArgumentException ex)
            {
                return this.View("Error", ex.Message);
            }

        }

        public IActionResult AcceptRequest(string userId)
        {
            return this.ChangeRelationshipState(userId, RelationshipState.Accepted);
        }

        public IActionResult DeclineRequest(string userId)
        {
            return this.ChangeRelationshipState(userId, RelationshipState.Declined);
        }

        public IActionResult WithdrawRequest(string userId)
        {
            return this.ChangeRelationshipState(userId, RelationshipState.Withdrawn);
        }

        public IActionResult Chats(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);
            try
            {
                var chat = this.chatService.GetPersonalChat(myUserId, userId);
                return this.View(chat);
            }
            catch (ArgumentException ex)
            {
                return this.View("Error", ex.Message);
            }


        }

        public IActionResult Search([FromQuery] UserQueryModel query)
        {
            var userId = this.userManager.GetUserId(this.User);

            var queryResult = this.userService.GetQueryUsers(
                userId,
                query.SearchTerm,
                query.CurrentPage,
                UserQueryModel.UsersPerPage);

            query.TotalUsers = queryResult.TotalUsers;
            query.Users = queryResult.Users;

            return this.View(query);
        }

        public IActionResult Requests()
        {
            var userId = this.userManager.GetUserId(this.User);

            var requests = new RequestsViewModel
            {
                ReceivedRequests = this.relationshipService.GetUserReceivedFriendRequests(userId),
                SentRequests = this.relationshipService.GetUserSentFriendRequests(userId),
                TeamInvites = this.teamService.GetTeamInvites(userId)
            };

            return this.View(requests);
        }

        public IActionResult Profile(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            var profile = new UserProfileViewModel
            {
                IsOwn = false
            };

            if (userId == "me" || userId == myUserId)
            {
                userId = myUserId;
                profile.IsOwn = true;
            }

            try
            {

                profile.Post = this.postService.GetLatest(userId);
                profile.User = this.userService.GetUserProfile(userId);
                profile.Relationship = this.relationshipService.GetRelationship(myUserId, userId);

                return this.View(profile);
            }
            catch(ArgumentException ex)
            {
                return this.View("Error", ex.Message);
            }
        }

        private IActionResult ChangeRelationshipState(string userId, RelationshipState state)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                if (state == RelationshipState.Withdrawn)
                {
                    this.relationshipService.ChangeRelationshipState(myUserId, userId, state);
                }
                else
                {
                    this.relationshipService.ChangeRelationshipState(userId, myUserId, state);
                }

                return this.Redirect("/chats/me");
            }
            catch (ArgumentException ex)
            {
                return this.View("Error", ex.Message);
            }
        }
    }
}
