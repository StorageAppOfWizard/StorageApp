using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Category;
using StorageProject.Application.Validators;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StorageProject.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        #region Get
        [SwaggerResponse((int)HttpStatusCode.OK, "Return all Categories")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Categories Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _categoryService.GetAllAsync();

                if (result.IsNotFound())
                    return NotFound(result);

                return Ok(result);

            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }
        #endregion


        #region GetByID
        [SwaggerResponse((int)HttpStatusCode.OK, "Return Category")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);

                if (result.IsNotFound())
                    return NotFound(result.Errors);
                
                return Ok(result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }
        #endregion


        #region Create
        [SwaggerResponse((int)HttpStatusCode.OK, "Category Created")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Category already exist")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for create Category")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            try
            {
               var result = await _categoryService.CreateAsync(dto);

                if (result.IsConflict())
                    return Conflict(result);
                if (result.IsInvalid())
                    return BadRequest(result);

                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }
        #endregion


        #region Update
        [SwaggerResponse((int)HttpStatusCode.OK, "Category Updated")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Category already exist")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category Not Found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for update Category")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO dto)
        {
            try
            {
                var result = await _categoryService.UpdateAsync(dto);

                if (result.IsConflict())
                    return Conflict(result);
                if (result.IsInvalid())
                    return BadRequest(result);
                if (result.IsNotFound())
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }

        }
        #endregion


        #region Delete
        [SwaggerResponse((int)HttpStatusCode.OK, "Category Deleted")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _categoryService.RemoveAsync(id);
                if (result.IsNotFound())
                    return NotFound(result.Errors);
                
                return Ok(result);

            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }
        #endregion
    }
}
