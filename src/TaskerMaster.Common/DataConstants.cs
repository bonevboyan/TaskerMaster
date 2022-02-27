namespace TaskerMaster.Common
{
    public class DataConstants
    {
        public class User
        {
            public const int FirstNameMaxLength = 30;

            public const int LastNameMaxLength = 40;

            public const int JobTitleMaxLength = 50;

            public const int JobDescriptionMaxLength = 300;
        }

        public class Bucket
        {
            public const int NameMaxLength = 30;
        }

        public class Company
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 300;
        }

        public class Discussion
        {
            public const int NameMaxLength = 30;
        }

        public class Message
        {
            public const int ContentMaxLength = 1000;
        }

        public class Tag
        {
            public const int NameMaxLength = 20;
        }

        public class Task
        {
            public const int NameMaxLength = 40;
            public const int DescriptionMaxLength = 300;
        }

        public class Team
        {
            public const int NameMaxLength = 40;
            public const int DescriptionMaxLength = 300;
        }

        public class Workspace
        {
        }
    }
}
