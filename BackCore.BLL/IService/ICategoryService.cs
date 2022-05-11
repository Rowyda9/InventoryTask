using BackCore.Base;
using BackCore.BLL.ViewModels;
using BackCore.Data;
using System.Threading.Tasks;

namespace BackCore.BLL
{
    
    public interface ICategoryService : IBaseService<CategoryViewModel, Category>
         
    {
       Task<CategoryViewModel> Get(int id);

    }
}
