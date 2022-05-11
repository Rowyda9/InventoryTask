
using System;
using System.Threading.Tasks;
using AutoMapper;
using BackCore.BLL.Constants;
using BackCore.BLL;
using BackCore.BLL.ViewModels;
using BackCore.Utilities.Paging;
using BackCore.Validtor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackCore.BLL.Enums;

namespace BackCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]

    public class ProductController : ControllerBase
    {
        
        public readonly IMapper _mapper;
        public readonly IProductService _ProductService;

        public ProductController(IMapper mapper, IProductService ProductService)
        {
            _mapper = mapper;
            _ProductService = ProductService;
        }

        [Route("GetProductReport")]
        public async Task<ActionResult> GetProductReportAsync()
        {
            return Ok(await _ProductService.GetProductReportAsync());
        }

        [Route("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        { 
            return Ok(await _ProductService.GetAllAsync<ProductViewModel>());
        }

        
        [Route("GetAllByPagination")]
        [HttpPost]
        public async Task<ActionResult> GetAllByPaginationAsync(PaginatedItemsViewModel pagingparametermodel)
        {
            return Ok(await _ProductService.GetAllByPaginationAsync(pagingparametermodel));
        }

        [Route("GetAllByProductInStock")]
        [HttpPost]
        public async Task<ActionResult> GetAllByProductInStockAsync(PaginatedItemsViewModel pagingparametermodel)
        {
            pagingparametermodel.StatusId = (byte)StatusEnum.InStock;
            return Ok(await _ProductService.GetAllByPaginationAsync(pagingparametermodel));
        }

        [Route("Details/{id}")]
        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            return Ok(await _ProductService.Get(id));
        }


        [HttpPost]
        [Route("Create")]

        [Authorize(Roles = StaticRoleNames.Admins)]
        public async Task<ActionResult> CreateAsync([FromBody] ProductFormViewModel ProductVM)
        {
            try

            {
                var validator = new ProductViewModelValidator();
                var validationResult = await validator.ValidateAsync(ProductVM);
                if (validationResult.IsValid == false)
                             return BadRequest(validationResult.Errors);
                ProductViewModel productToCreate = _mapper.Map<ProductViewModel>(ProductVM);
                productToCreate.StatusId =(byte)StatusEnum.InStock;
                    return Ok(await _ProductService.AddAsync(productToCreate));
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize(Roles = StaticRoleNames.Admins)]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(ProductFormViewModel ProductVM)
        {
            try
            {
                var validator = new ProductViewModelValidator();
                var validationResult = await validator.ValidateAsync(ProductVM);
                if (validationResult.IsValid == false)
                         return BadRequest(validationResult.Errors);
                ProductViewModel productToUpdate = _mapper.Map<ProductViewModel>(ProductVM);
                return Ok(await _ProductService.UpdateAsync(productToUpdate));
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize(Roles = StaticRoleNames.Admins)]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var ProductToDelete = await _ProductService.Get(id);
                var result = await _ProductService.SoftDeleteAsync(ProductToDelete);
                if (result)
                        return Ok(result);
               
                    return BadRequest();
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("ChangeStatus")]
        [HttpGet]
        public async Task<ActionResult> ChangeStatus(int ProductID, int StatusID)
        {
            var product = await _ProductService.Get(ProductID);
            if (product == null) return NotFound();
            var productToUpdate = _mapper.Map<ProductViewModel>(product);
            productToUpdate.StatusId = StatusID;
            await _ProductService.UpdateAsync(productToUpdate);
            return Ok();
        }

        [Route("SellProduct")]
        [HttpGet]
        public async Task<ActionResult> SellProduct(int ProductID)
        {
            var product = await _ProductService.Get(ProductID);
            if (product == null) return NotFound();
            var productToUpdate = _mapper.Map<ProductViewModel>(product);
            switch (productToUpdate.StatusId)
            {
                case (byte)StatusEnum.Sold:
                    return BadRequest("Product Out of Stock");
                  
                case (byte)StatusEnum.Damaged:
                    return BadRequest("Can not Sell Damaged Product");
                   
            }
            if (productToUpdate.StatusId == (byte)StatusEnum.InStock)
            {
                productToUpdate.StatusId = (byte)StatusEnum.Sold;
                return Ok(await _ProductService.UpdateAsync(productToUpdate));
            }
            return BadRequest("Can not Sell Product");
        }


        [Authorize(Roles = StaticRoleNames.Admins)]
        [HttpDelete("AdminDelete/{id}")]
        public async Task<ActionResult> AdminDeleteAsync(int id)
        {
            try
            {
                var ProductToDelete = await _ProductService.Get(id);
                var result = await _ProductService.DeleteAsync(ProductToDelete);

                if (result) 
                    return Ok(result);

                return BadRequest();

            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}