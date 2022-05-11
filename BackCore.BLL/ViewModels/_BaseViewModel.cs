using System;

namespace BackCore.BLL.ViewModel
{
    public class _BaseViewModel
    {
        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string DeletedBy{ get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; }

        public DateTime DeletedDate { get; set; }

        
    }
}