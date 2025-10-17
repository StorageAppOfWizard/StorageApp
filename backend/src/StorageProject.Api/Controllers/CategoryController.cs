using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Extensions;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Category;
using StorageProject.Application.Validators;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StorageProject.Api.Controllers
{

    [ApiController]
    [Route("/category")]
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
        [Authorize]
        public async Task<IActionResult> Get()
        {

            var result = await _categoryService.GetAllAsync();
            return result.ToActionResult();
        }
        #endregion


        #region GetByID
        [SwaggerResponse((int)HttpStatusCode.OK, "Return Category")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet("{id:Guid}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var result = await _categoryService.GetByIdAsync(id);
            return result.ToActionResult();
        }
        #endregion


        #region Create
        [SwaggerResponse((int)HttpStatusCode.OK, "Category Created")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Category already exist")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for create Category")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPost]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {

            var result = await _categoryService.CreateAsync(dto);
            return result.ToActionResult();

        }
        #endregion


        #region Update
        [SwaggerResponse((int)HttpStatusCode.OK, "Category Updated")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Category already exist")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category Not Found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for update Category")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPut]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO dto)
        {

            var result = await _categoryService.UpdateAsync(dto);
            return result.ToActionResult();

        }
        #endregion


        #region Delete
        [SwaggerResponse((int)HttpStatusCode.OK, "Category Deleted")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _categoryService.RemoveAsync(id);
            return result.ToActionResult();
        }
        #endregion
    }
}
