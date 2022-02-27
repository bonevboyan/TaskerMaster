namespace TaskerMaster.Data.Common.Models
{
    using System;

    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        public BaseDeletableModel()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.Now;
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
