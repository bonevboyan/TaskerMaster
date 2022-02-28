namespace TaskerMaster.Common
{
    public class DataConstants
    {
        public class User
        {
            public const int FirstNameMinLength = 2;

            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 2;

            public const int LastNameMaxLength = 40;

            public const int JobTitleMinLength = 5;

            public const int JobTitleMaxLength = 50;

            public const int JobDescriptionMinLength = 10;

            public const int JobDescriptionMaxLength = 300;
        }

        public class Bucket
        {
            public const int NameMinLength = 1;

            public const int NameMaxLength = 30;
        }

        public class Company
        {
            public const int NameMinLength = 3;

            public const int NameMaxLength = 50;

            public const int DescriptionMinLength = 10;

            public const int DescriptionMaxLength = 300;
        }

        public class Discussion
        {
            public const int NameMinLength = 1;

            public const int NameMaxLength = 30;
        }

        public class Message
        {
            public const int ContentMinLength = 1;

            public const int ContentMaxLength = 1000;
        }

        public class Tag
        {
            public const int NameMinLength = 1;

            public const int NameMaxLength = 20;
        }

        public class Task
        {
            public const int NameMaxLength = 1;

            public const int NameMinLength = 40;

            public const int DescriptionMaxLength = 300;

            public const int DescriptionMinLength = 300;
        }

        public class Team
        {
            public const int NameMaxLength = 40;

            public const int NameMaxLength = 40;

            public const int DescriptionMaxLength = 300;
        }

        public class Workspace
        {
        }
    }
}
