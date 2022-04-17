namespace Taskord.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Posts;
    using Taskord.Services.Relationships;
    using Taskord.Services.Teams;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRelationshipService relationshipService;
        private readonly ITeamService teamService;
        private readonly IPostService postService;
        private readonly UserManager<User> userManager;

        public UserController(IUserService userService, 
            UserManager<User> userManager,
            ITeamService teamService, 
            IRelationshipService relationshipService,
            IPostService postService)
        {
            this.teamService = teamService;
            this.relationshipService = relationshipService;
            this.userService = userService;
            this.userManager = userManager;
            this.postService = postService;
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
    }
}
