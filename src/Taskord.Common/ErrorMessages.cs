﻿namespace Taskord.Common
{
    public class ErrorMessages
    {
        public class User
        {
            public const string InvalidFriendRequest = "There is no such friend request.";

            public const string InvalidFriendRequestParameters = "You can't send an friend request to yourself!"; 
        }

        public class Team
        {
            public const string InvalidTeamInvite = "There is no such invite.";

            public const string InviteExists = "This team invite is already sent!";

            public const string InvalidInviteStateChange = "An invite's new state must be different and cannot be 'Pending'";
        }

        public class Chat
        {
            public const string InvalidTeam = "Team not found!";

            public const string InvalidChat = "Chat not found!";

            public const string InvalidUsers = "User Ids not found!";

            public const string InvalidChatParticipants = "A chat between these two users doesn't exist.";

            public const string UserNotInChat = "The user does not participate in this chat.";
        }
    }
}
