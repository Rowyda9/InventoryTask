namespace BackCore.Utilities.Paging
{
    public class PaginatedItemsViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchBy { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }

       

        public int totalNumbers { get; set; }

        public int SortDateBy { get; set; }

        public int SortPriceBy { get; set; }


    }
}
