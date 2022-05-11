
using System;
using System.Threading.Tasks;
using AutoMapper;
using BackCore.BLL;
using BackCore.BLL.Constants;
using BackCore.BLL.ViewModels;
using BackCore.Utilities.Paging;
using BackCore.Validtor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = StaticRoleNames.Admins)]
   
    public class CategoryController : ControllerBase
    {
        
        public readonly IMapper _mapper;
        public readonly ICategoryService _categoryService;
        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        { 
            return Ok(await _categoryService.GetAllAsync<CategoryViewModel>());
        }

        [AllowAnonymous]
        [Route("GetAllByPagination")]
        [HttpPost]
        public async Task<ActionResult> GetAllByPaginationAsync(PaginatedItemsViewModel pagingparametermodel)
        {
            return Ok(await _categoryService.GetAllByPaginationAsync(pagingparametermodel));
        }

        [Route("Details/{id}")]
        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            return Ok(await _categoryService.Get(id));
        }


        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateAsync([FromBody]CategoryViewModel categoryVM)
        {
            try
            {
                var validator = new CategoryViewModelValidator();
                var validationResult = await validator.ValidateAsync(categoryVM);
                if (validationResult.IsValid == false)
                             return BadRequest(validationResult.Errors);

                return Ok(await _categoryService.AddAsync(categoryVM));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(CategoryViewModel categoryVM)
        {
            try
            {
                var validator = new CategoryViewModelValidator();
                var validationResult = await validator.ValidateAsync(categoryVM);
                if (validationResult.IsValid == false)
                         return BadRequest(validationResult.Errors);
             
                return Ok(await _categoryService.UpdateAsync(categoryVM));
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var categoryToDelete = await _categoryService.Get(id);
                var result = await _categoryService.SoftDeleteAsync(categoryToDelete);
                if (result)
                        return Ok(result);
               
                    return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #region adminActions
        [HttpDelete("AdminDelete/{id}")]
        public async Task<ActionResult> AdminDeleteAsync(int id)
        {
            try
            {
                var categoryToDelete = await _categoryService.Get(id);
                var result = await _categoryService.DeleteAsync(categoryToDelete);

                if (result) 
                    return Ok(result);

                return BadRequest();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

    }
}