using BackCore.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackCore.Base
{
    public interface IBaseService<TViewModel, TEntity>
    {
        Task<List<TProjectedModel>> GetAllAsync<TProjectedModel>();
        Task<TViewModel> AddAsync(TViewModel entity);
        Task<List<TViewModel>> AddAsync(List<TViewModel> entityLst);
        Task<bool> UpdateAsync(TViewModel entity);
        Task<bool> DeleteAsync(TViewModel entity);
        Task<bool> DeleteAsync(List<TViewModel> entitylst);
        Task<bool> SoftDeleteAsync(TViewModel entity);
        Task<bool> SoftDeleteAsync(List<TViewModel> ViewModelLst);
      
        Task<PagedResult<TViewModel>> GetAllByPaginationAsync(PaginatedItemsViewModel pagingparametermodel);


    }
}