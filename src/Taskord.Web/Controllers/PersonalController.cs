namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats;
    using Taskord.Services.Teams;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    public class PersonalController : Controller
    {
        private readonly IUserService userService;
        private readonly IChatService chatService;
        private readonly ITeamService teamService;
        private readonly UserManager<User> userManager;

        public PersonalController(IUserService userService, UserManager<User> userManager, IChatService chatService, ITeamService teamService)
        {
            this.teamService = teamService;
            this.chatService = chatService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult SendRequest(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                this.userService.SendFriendRequest(myUserId, userId);
                return this.Redirect("/chats/me");
            }
            catch (ArgumentException ex)
            {
                return this.StatusCode((int) HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Authorize]
        public IActionResult AcceptRequest(string userId)
        {
            return this.ChangeRelationshipState(userId, RelationshipState.Accepted);
        }

        [Authorize]
        public IActionResult DeclineRequest(string userId)
        {
            return this.ChangeRelationshipState(userId, RelationshipState.Declined);
        }

        [Authorize]
        public IActionResult WithdrawRequest(string userId)
        {
            return this.ChangeRelationshipState(userId, RelationshipState.Withdrawn);
        }

        [Authorize]
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
                return this.BadRequest(ex);
            }


        }

        [Authorize]
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


        [Authorize]
        public IActionResult Requests()
        {
            var userId = this.userManager.GetUserId(this.User);

            var requests = new RequestsViewModel
            {
                ReceivedRequests = this.userService.GetUserReceivedFriendRequests(userId),
                SentRequests = this.userService.GetUserSentFriendRequests(userId),
                TeamInvites = this.teamService.GetTeamInvites(userId)
            };

            return this.View(requests);
        }

        [Authorize]
        private IActionResult ChangeRelationshipState(string userId, RelationshipState state)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                if (state == RelationshipState.Withdrawn)
                {
                    this.userService.ChangeRelationshipState(myUserId, userId, state);
                }
                else
                {
                    this.userService.ChangeRelationshipState(userId, myUserId, state);
                }

                return this.Redirect("/chats/me");
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}
