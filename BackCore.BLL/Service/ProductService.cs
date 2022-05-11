
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
using BackCore.BLL.Enums;

namespace BackCore.BLL
{
 
    public class ProductService : BaseService<ProductViewModel, Product>, IProductService
    {
        private readonly IMapper _mapper;

        public ProductService(DbContext _context, IRepositry<Product> _repository, 
            IMapper mapper) : base(_context, _repository, mapper)
        {
            _mapper = mapper;
        }


        public async Task<ProductViewModel> Get(int id)
        {
            return await repository.GetAll().Where(x => x.Id == id)
                             .ProjectTo<ProductViewModel>().FirstOrDefaultAsync();
        }

        public override async Task<List<ProductViewModel>> GetAllAsync<ProductViewModel>()
        {
            try
            {
                return await Task.Run(() =>
                                          repository.GetAll()
                                          .ProjectTo<ProductViewModel>()
                                          .ToList());
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public async Task<ProductReportViewModel> GetProductReportAsync()
        {
            var products = await Task.Run(() => repository.GetAll().ProjectTo<ProductViewModel>().ToList());
            ProductReportViewModel reportViewModel = new ProductReportViewModel()
            {
                InStockCount = products.Count(a=>a.StatusId == (byte)StatusEnum.InStock),
                DamagedCount = products.Count(a => a.StatusId == (byte)StatusEnum.Damaged),
                SoldCount = products.Count(a => a.StatusId == (byte)StatusEnum.Sold)
            };

            return reportViewModel;
        }

        public override async Task<PagedResult<ProductViewModel>> GetAllByPaginationAsync(PaginatedItemsViewModel pagingparametermodel)
        {
            return await Task.Run(() =>
            {
                var pagedResult = new PagedResult<ProductViewModel>();

                pagingparametermodel.PageNumber = (pagingparametermodel.PageNumber == 0) ? 1 : pagingparametermodel.PageNumber;
                pagingparametermodel.PageSize = (pagingparametermodel.PageSize == 0) ? 20 : pagingparametermodel.PageSize;

                var source = repository.GetAll()
                            .ProjectTo<ProductViewModel>();
                if (!String.IsNullOrWhiteSpace(pagingparametermodel.SearchBy))
                {
                    source = source.Where(a => a.Name.ToLower().Contains(pagingparametermodel.SearchBy)
                              || a.Barcode.ToLower().Contains(pagingparametermodel.SearchBy));
                }

                if (pagingparametermodel.StatusId > 0)
                {
                    source = source.Where(a => a.StatusId == pagingparametermodel.StatusId);
                }

                if (pagingparametermodel.CategoryId > 0)
                {
                    source = source.Where(a => a.CategoryId == pagingparametermodel.StatusId);
                } 
                int CurrentPage = pagingparametermodel.PageNumber; 
                int PageSize = pagingparametermodel.PageSize;
                pagedResult.TotalCount = source.Count();
                pagedResult.Result = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                return pagedResult;

            }
         );
        }


        public override async Task<ProductViewModel> AddAsync(ProductViewModel viewModel)
        {
            return await base.AddAsync(viewModel); 
        }

        public override async Task<bool> UpdateAsync(ProductViewModel viewModel)
        {
            return await base.UpdateAsync(viewModel);
        }

      
    }
}
