namespace Taskord.Services.Users.Models
{
    using Taskord.Data.Models.Enums;

    public class UserListServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public RelationshipState? RelationshipState { get; set; }
    }
}
