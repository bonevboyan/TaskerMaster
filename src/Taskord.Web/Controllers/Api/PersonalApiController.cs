namespace Taskord.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Users;
    using Taskord.Web.Models.Api;

    //[Route("api/me")]
    [ApiController]
    public class PersonalApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public PersonalApiController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [Route("api/me/sendFriendRequest")]
        public IActionResult SendFriendRequest(FriendRequestPostModel request)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            try
            {
                this.userService.SendFriendRequest(myUserId, request.UserId);
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
                this.userService.ChangeRelationshipState(response.UserId, myUserId,
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
                this.userService.ChangeRelationshipState(userId, withdraw.UserId, RelationshipState.Withdrawn);

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

            var count = this.userService.GetUserReceivedFriendRequests(userId).Count();

            return count.ToString();
        }

        [Authorize]
        [HttpGet]
        [Route("api/me/sentRequestsCount")]
        public string GetSentRequestsCount()
        {
            var userId = this.userManager.GetUserId(this.User);

            var count = this.userService.GetUserSentFriendRequests(userId).Count();

            return count.ToString();
        }
    }
}
