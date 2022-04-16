namespace Taskord.Common
{
    public class ErrorMessages
    {
        public class User
        {

            public const string InvalidTeamId = "A team with that id doesn't exist.";

            public const string InvalidUserIdInTeam = "This user doesn't participate in this team";

            public const string InvalidUserId = "A user with this id doesn't exist.";
        }

        public class Team
        {
            public const string InvalidTeamInvite = "There is no such invite.";

            public const string InviteExists = "This team invite is already sent!";

            public const string UserNotInTeam = "This user does not participate in this team!";

            public const string InvalidInviteStateChange = "An invite's new state must be different and cannot be 'Pending'";

            public const string InvalidChat = "Chat not found!";

            public const string CantRemoveFromGeneral = "You cannot remove users from the general chat!";
        }

        public class Chat
        {
            public const string InvalidTeam = "Team not found!";

            public const string InvalidChat = "Chat not found!";

            public const string InvalidUsers = "User Ids not found!";

            public const string InvalidChatParticipants = "A chat between these two users doesn't exist.";

            public const string UserNotInChat = "The user does not participate in this chat.";
        }

        public class Post
        {
            public const string UserNotPermittedToSeePosts = "You can't see this user's posts because you two aren't friends.";

            public const string CantDeletePost = "You can't delete an inexsitent post!";
        }

        public class Relationship
        {
            public const string InvalidFriendRequest = "There is no such friend request.";

            public const string FriendshipAlreadyExists = "A relationship between these users already exists.";

            public const string InvalidFriendRequestParameters = "You can't send an friend request to yourself!";
        }
    }
}
