using BackCore.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackCore.Data
{
    [Table("Status")]
    public class Status : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
