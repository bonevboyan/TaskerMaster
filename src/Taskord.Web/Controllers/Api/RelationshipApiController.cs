namespace Taskord.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Relationships;
    using Taskord.Services.Users;
    using Taskord.Web.Models.Api;

    [ApiController]
    [IgnoreAntiforgeryToken]
    public class RelationshipApiController : ControllerBase
    {
        private readonly IRelationshipService relationshipService;
        private readonly UserManager<User> userManager;

        public RelationshipApiController(UserManager<User> userManager, IRelationshipService relationshipService)
        {
            this.userManager = userManager;
            this.relationshipService = relationshipService;
        }

        [Authorize]
        [HttpPost]
        [Route("api/me/sendFriendRequest")]
        public IActionResult SendFriendRequest(FriendRequestPostModel request)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                this.relationshipService.SendFriendRequest(myUserId, request.UserId);
                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/me/respondToFriendRequest")]
        public IActionResult RespondToFriendRequest(RespondToFriendRequestPostModel response)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                this.relationshipService.ChangeRelationshipState(response.UserId, myUserId,
                    response.IsAccepted
                        ? RelationshipState.Accepted
                        : RelationshipState.Declined);

                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }
        }


        [Authorize]
        [HttpPost]
        [Route("api/me/withdrawFriendRequest")]
        public IActionResult WithdrawFriendRequest(WithdrawFriendRequestPostModel withdraw)
        {
            var userId = this.userManager.GetUserId(this.User);

            try
            {
                this.relationshipService.ChangeRelationshipState(userId, withdraw.UserId, RelationshipState.Withdrawn);

                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/me/receivedRequestsCount")]
        public string GetReceivedRequestsCount()
        {
            var userId = this.userManager.GetUserId(this.User);

            var count = this.relationshipService.GetUserReceivedFriendRequests(userId).Count();

            return count.ToString();
        }

        [Authorize]
        [HttpGet]
        [Route("api/me/sentRequestsCount")]
        public string GetSentRequestsCount()
        {
            var userId = this.userManager.GetUserId(this.User);

            var count = this.relationshipService.GetUserSentFriendRequests(userId).Count();

            return count.ToString();
        }
    }
}
