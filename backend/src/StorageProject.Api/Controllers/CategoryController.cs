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
        private readonly CategoryValidator _categoryValidator = new CategoryValidator();

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
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
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
                if (!result.IsSuccess)
                {
                    return NotFound(result.Errors);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }
        #endregion


        #region Create
        [SwaggerResponse((int)HttpStatusCode.OK, "Category Created")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Category already exist")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for create Category")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            try
            {
                var categoryValidator = await _categoryValidator.ValidateAsync(createCategoryDTO);

                if (!categoryValidator.IsValid)
                    return BadRequest(categoryValidator.ToDictionary());

                var result = await _categoryService.CreateAsync(createCategoryDTO);

                if (result.IsConflict())
                    return Conflict(result);
                if (!result.IsSuccess)
                    return BadRequest(result);

                return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
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
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            try
            {
                var categoryValidator = await _categoryValidator.ValidateAsync(updateCategoryDTO);

                if (!categoryValidator.IsValid)
                    return BadRequest(categoryValidator.ToDictionary());

                var result = await _categoryService.UpdateAsync(updateCategoryDTO);

                if (result.IsConflict())
                    return Conflict(result);
                if (result.IsNotFound())
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
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
                if (!result.IsSuccess)
                {
                    return NotFound(result.Errors);
                }
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An Unexpected Error" });
            }
        }
        #endregion
    }
}
