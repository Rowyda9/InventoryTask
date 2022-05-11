
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BackCore.BLL.Constants;
using BackCore.BLL;
using BackCore.BLL.ViewModels;
using BackCore.Utilities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BackCore.Validtor;

namespace BackCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = StaticRoleNames.Admins)]
    public class StatusController : ControllerBase
    {
        
        public readonly IMapper _mapper;
        public readonly IStatusService _StatusService;


        public StatusController(IMapper mapper, IStatusService AdStatusService)
        {
            _mapper = mapper;
            _StatusService = AdStatusService;
        }


        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _StatusService.GetAllAsync<StatusViewModel>());
        }

        [AllowAnonymous]
        [Route("GetAllByPagination")]
        [HttpPost]
        public async Task<ActionResult> GetAllByPaginationAsync(PaginatedItemsViewModel pagingparametermodel)
        {
            return Ok(await _StatusService.GetAllByPaginationAsync(pagingparametermodel));
        }

        [Route("Details/{id}")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            return Ok(await _StatusService.Get(id));
        }

       
        [HttpPost]
        [Route("Create")]
        [Route("Update")]
        public async Task<ActionResult> CreateAsync([FromBody]StatusViewModel AdStatusViewModel)
        {
            try{
                var validator = new StatusViewModelValidator();
                var validationResult = await validator.ValidateAsync(AdStatusViewModel);
                if (validationResult.IsValid == false)
                        return BadRequest(validationResult.Errors);
              
                    return Ok(await _StatusService.AddAsync(AdStatusViewModel));
              
            }
            catch (Exception e)
            {
                throw e;
            }
        }


     
        [HttpPut]
        public async Task<ActionResult> Update(StatusViewModel AdStatusViewModel)
        {
            try
            {
                var validator = new StatusViewModelValidator();
                var validationResult = await validator.ValidateAsync(AdStatusViewModel);

                if (validationResult.IsValid == false)
                    return BadRequest(validationResult.Errors);
                
                    return Ok(await _StatusService.UpdateAsync(AdStatusViewModel));
                
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
                var AdStatusToDelete = await _StatusService.Get(id);
                var result = await _StatusService.SoftDeleteAsync(AdStatusToDelete);
          
                if (result)  return Ok(result);
              
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
                var result = await _StatusService.DeleteAsync(await _StatusService.Get(id));

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