
namespace BackCore.BLL.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Description { set; get; }

        public string Barcode { set; get; }

        public float Wieght { get; set; }
        public byte WieghtUnit { get; set; }

        public int CategoryId { get; set; }
    }

    public class ProductViewModel 
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Description { set; get; }

        public string Barcode { set; get; }

        public float Wieght { get; set; }
        public byte WieghtUnit { get; set; }

        public int StatusId { get; set; }
        public  string StatusName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
    public class ProductReportViewModel
    {
        public int InStockCount { get; set; }
        public int DamagedCount { get; set; }
        public int SoldCount { get; set; }


    }

}