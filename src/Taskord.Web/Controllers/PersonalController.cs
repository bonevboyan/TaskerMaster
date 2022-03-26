namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    public class PersonalController : Controller
    {
        private readonly IUserService userService;
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;

        public PersonalController(IUserService userService, UserManager<User> userManager, IChatService chatService)
        {
            this.chatService = chatService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var myUserId = this.userManager.GetUserId(this.User);

            var friends = this.userService.GetUserFriendsList(myUserId);

            return this.View(friends);
        }

        [Authorize]
        public IActionResult SendRequest(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            this.userService.SendFriendRequest(myUserId, userId);

            return this.Redirect("/me/all");
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

            var chat = this.chatService.GetPersonalChat(myUserId, userId);

            return this.View(chat);
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

            var requests = new FriendRequestsViewModel
            {
                ReceivedRequests = this.userService.GetUserReceivedFriendRequests(userId),
                SentRequests = this.userService.GetUserSentFriendRequests(userId)
            };

            return this.View(requests);
        }

        private IActionResult ChangeRelationshipState(string userId, RelationshipState state)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            this.userService.ChangeRelationshipState(myUserId, userId, state);

            return this.Redirect("/me/all");
        }
    }
}
