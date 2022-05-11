

using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackCore.Utilities.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace BackCore.Base
{
    public class BaseService<TViewModel, TEntity> : IBaseService<TViewModel, TEntity>
    {

        protected readonly IRepositry<TEntity> repository;
        protected readonly DbContext context;
        protected readonly IMapper mapper;

        public BaseService(DbContext _context, IRepositry<TEntity> _repository, IMapper _mapper)
        {
            mapper = _mapper;
            context = _context;
            repository = _repository;
        }


        public async virtual Task<TViewModel> AddAsync(TViewModel viewModel)
        {
            return await Task<TViewModel>.Run(() =>
            {

                TEntity entity = mapper.Map<TViewModel, TEntity>(viewModel);

                TEntity resultentity = repository.AddAsync(entity);

                TViewModel result = mapper.Map<TEntity, TViewModel>(resultentity);
                return result;
            });
        }

        public async virtual Task<List<TViewModel>> AddAsync(List<TViewModel> ViewModelLst)
        {
            return await Task<List<TViewModel>>.Run(() =>
            {

                List<TEntity> entity = mapper.Map<List<TViewModel>, List<TEntity>>(ViewModelLst);

                List<TEntity> resultentity = repository.AddAsync(entity);

                List<TViewModel> result = mapper.Map<List<TEntity>, List<TViewModel>>(resultentity);
                return result;
            });
        }

        public async virtual Task<bool> DeleteAsync(TViewModel viewModel)
        {
            return await Task<List<TViewModel>>.Run(() =>
            {

                TEntity entity = mapper.Map<TViewModel, TEntity>(viewModel);

                bool result = repository.Delete(entity);

                return result;
            });
        }

        public async virtual Task<bool> DeleteAsync(List<TViewModel> ViewModelLst)
        {
            return await Task<List<TViewModel>>.Run(() =>
            {

                List<TEntity> entity = mapper.Map<List<TViewModel>, List<TEntity>>(ViewModelLst);

                bool result = repository.Delete(entity);

                return result;
            });
        }


        public async virtual Task<bool> SoftDeleteAsync(TViewModel viewModel)
        {
            return await Task<List<TViewModel>>.Run(() =>
            {

                TEntity entity = mapper.Map<TViewModel, TEntity>(viewModel);

                bool result = repository.SoftDelete(entity);

                return result;
            });
        }

        public async virtual Task<bool> SoftDeleteAsync(List<TViewModel> ViewModelLst)
        {
            return await Task<List<TViewModel>>.Run(() =>
            {

                List<TEntity> entity = mapper.Map<List<TViewModel>, List<TEntity>>(ViewModelLst);

                bool result = repository.SoftDelete(entity);

                return result;
            });
        }

        public async virtual Task<bool> UpdateAsync(TViewModel viewModel)
        {
            return await Task<TViewModel>.Run(() =>
            {

                TEntity entity = mapper.Map<TViewModel, TEntity>(viewModel);
                bool result = repository.UpdateAsync(entity);

                return result;
            });
        }

        public async virtual Task<List<TProjectedModel>> GetAllAsync<TProjectedModel>()
        {
            try
            {
                return await Task.Run(() => repository.GetAll().ProjectTo<TProjectedModel>().ToList());

            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }

        public async virtual Task<PagedResult<TViewModel>> GetAllByPaginationAsync(PaginatedItemsViewModel pagingparametermodel)
        {
            return await Task.Run(() =>
            {
                var pagedResult = new PagedResult<TViewModel>();

                pagingparametermodel.PageNumber = (pagingparametermodel.PageNumber == 0) ? 1 : pagingparametermodel.PageNumber;
                pagingparametermodel.PageSize = (pagingparametermodel.PageSize == 0) ? 20 : pagingparametermodel.PageSize;

                var source = repository.GetAll().ProjectTo<TViewModel>();

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


        public async virtual Task<bool> UpdateAsync<TCommand>(TCommand viewModel)
        {
            return await Task<TCommand>.Run(() =>
            {
                TEntity entity = mapper.Map<TCommand, TEntity>(viewModel);
                bool result = repository.UpdateAsync(entity);

                return result;
            });
        }

      
    }


}
