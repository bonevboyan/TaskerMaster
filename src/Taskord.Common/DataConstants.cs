namespace Taskord.Common
{
    public class DataConstants
    {
        public class User
        {
            public const int FirstNameMinLength = 2;

            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 2;

            public const int LastNameMaxLength = 40;

            public const int UsernameMinLength = 3;

            public const int UsernameMaxLength = 100;

            public const int StatusMaxLength = 150;
        }

        public class Chat
        {
            public const int NameMinLength = 1;

            public const int NameMaxLength = 30;
        }

        public class Message
        {
            public const int ContentMinLength = 1;

            public const int ContentMaxLength = 1000;
        }

        public class Team
        {
            public const int NameMinLength = 1;

            public const int NameMaxLength = 40;

            public const int DescriptionMaxLength = 300;
        }

        public class Post
        {
            public const int ContentMinLength = 5;

            public const int ContentMaxLength = 300;
        }
    }
}