namespace Taskord.Services.Relationships.Models
{
    using Taskord.Data.Models.Enums;

    public class RelationshipServiceModel
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public RelationshipState State { get; set; }
    }
}
