using System;
using System.Collections.Generic;
using System.Text;

namespace BackCore.Base
{
    public interface IBaseEntity
    {
        string CreatedBy { get; set; }

        string UpdatedBy { get; set; }

        string DeletedBy { get; set; }

        bool IsDeleted { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        DateTime DeletedDate { get; set; }
    }
}
