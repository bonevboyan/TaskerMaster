namespace Taskord.Data.Common
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
            CreatedOn = DateTime.Now;
            IsDeleted = false;
        }

        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
