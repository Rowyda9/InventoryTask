using BackCore.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackCore.Data
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
