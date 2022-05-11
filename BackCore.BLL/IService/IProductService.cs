using BackCore.Base;
using BackCore.BLL.ViewModels;
using BackCore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BackCore.BLL
{
    
    public interface IProductService : IBaseService<ProductViewModel, Product>
         
    {
       Task<ProductViewModel> Get(int id);
        Task<ProductReportViewModel> GetProductReportAsync();

       

    }
}
