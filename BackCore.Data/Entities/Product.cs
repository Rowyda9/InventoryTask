using BackCore.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackCore.Data
{
    [Table("Product")]
    public class Product : BaseEntity
    {
   
        public int Id { get; set; }

        public string Name { set; get; }

        public string Description { set; get; }

        public string Barcode { set; get; }

        public float Wieght { get; set; }
        public byte WieghtUnit{ get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


    }
}
