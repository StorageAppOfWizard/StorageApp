using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Extensions;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Product;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StorageProject.Api.Controllers
{

    [Route("/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Get

        [SwaggerResponse((int)HttpStatusCode.OK, "Return all Products")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Products Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageQuantity = 20)
        {
            var result = await _productService.GetAllAsync(page, pageQuantity);
            return result.ToActionResult();
        }
        #endregion

        #region GetByID
        [SwaggerResponse((int)HttpStatusCode.OK, "Return Product")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet("{id:Guid}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            return result.ToActionResult();
        }
        #endregion

        #region Create
        [SwaggerResponse((int)HttpStatusCode.Created, "Product Created")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Product already exist")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for create Product")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPost]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO dto)
        {
            var result = await _productService.CreateAsync(dto);
            return result.ToActionResult();
        }
        #endregion

        #region Update
        [SwaggerResponse((int)HttpStatusCode.OK, "Product Updated")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Product already exist")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product Not Found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for update Product")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPut]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO dto)
        {

            var result = await _productService.UpdateAsync(dto);
            return result.ToActionResult();
        }
        #endregion

        #region UpdatePatch

        [SwaggerResponse((int)HttpStatusCode.OK, "Quantity changed sucessfully")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "This field is require for only number")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPatch("editQuantity")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateProductQuantityDTO dto)
        {

            var result = await _productService.UpdateQuantityAsync(dto);
            return result.ToActionResult();
        }

        #endregion

        #region Delete
        [SwaggerResponse((int)HttpStatusCode.OK, "Product Deleted")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _productService.RemoveAsync(id);
            return result.ToActionResult();
        }
        #endregion
    }
}
