using System;

namespace BackCore.Base
{
    public class BaseEntity : IBaseEntity
    {
        public virtual string CreatedBy { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual string DeletedBy { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        public virtual DateTime DeletedDate { get; set; }
    }
}
