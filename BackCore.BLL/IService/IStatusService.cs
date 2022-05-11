using BackCore.Base;
using BackCore.BLL.ViewModels;
using BackCore.Data;
using System.Threading.Tasks;


namespace BackCore.BLL
{
    
    public interface IStatusService : IBaseService<StatusViewModel, Status>
         
    {
        Task<StatusViewModel> Get(int id);
    }
}
