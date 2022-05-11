
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackCore.BLL.ViewModels;
using BackCore.Data;
using BackCore.Base;
using BackCore.Utilities.Paging;

namespace BackCore.BLL
{
 
    public class StatusService : BaseService<StatusViewModel, Status>, IStatusService
    {
        private readonly IMapper _mapper;

        public StatusService(DbContext _context, IRepositry<Status> _repository, 
            IMapper mapper) : base(_context, _repository, mapper)
        {
            _mapper = mapper;
        }

        public async Task<StatusViewModel> Get(int id)
        {
            return await repository.GetAll().Where(x => x.Id == id)
                .ProjectTo<StatusViewModel>().FirstOrDefaultAsync();
        }


        public override  async Task<List<AdStatusViewModel>> GetAllAsync<AdStatusViewModel>()
        {
            try
            {
                 return  await Task.Run(() =>repository.GetAll()
                                          .ProjectTo<AdStatusViewModel>()
                                         .ToList());
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public override async Task<PagedResult<StatusViewModel>> GetAllByPaginationAsync(PaginatedItemsViewModel pagingparametermodel)
        {
            return await Task.Run(() =>
            {
                var pagedResult = new PagedResult<StatusViewModel>();

                pagingparametermodel.PageNumber = (pagingparametermodel.PageNumber == 0) ? 1 : pagingparametermodel.PageNumber;
                pagingparametermodel.PageSize = (pagingparametermodel.PageSize == 0) ? 20 : pagingparametermodel.PageSize;

                var source = repository.GetAll()
                            .ProjectTo<StatusViewModel>();

                if (!String.IsNullOrWhiteSpace(pagingparametermodel.SearchBy))
                {
                    source = source.Where(a => a.Name.ToLower().Contains(pagingparametermodel.SearchBy)
                              || a.Description.ToLower().Contains(pagingparametermodel.SearchBy));
                }

                // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
                int CurrentPage = pagingparametermodel.PageNumber;

                // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
                int PageSize = pagingparametermodel.PageSize;
                pagedResult.TotalCount = source.Count();//
                pagedResult.Result = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                return pagedResult;

            }
         );
        }

        public override async Task<StatusViewModel> AddAsync(StatusViewModel viewModel)
        {
            return await base.AddAsync(viewModel) ;
        }

        public override async Task<bool> UpdateAsync(StatusViewModel viewModel)
        {
            return await base.UpdateAsync(viewModel);
        }

    }
}
