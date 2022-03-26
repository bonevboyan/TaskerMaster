namespace Taskord.Common
{
    public class ErrorMessages
    {
        public class User
        {
            public const string InvalidFriendRequest = "There is no such friend request.";

            public const string InvalidFriendRequestParameters = "You can't send an friend request to yourself!"; 
        }

        public class Chat
        {
            public const string InvalidTeam = "Team not found!";

            public const string InvalidUsers = "User Ids not found!";

            public const string InvalidChatParticipants = "A chat between these two users doesn't exist.";
        }
    }
}
