using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Product;
using StorageProject.Application.Validators;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StorageProject.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ProductValidator _productValidator = new ProductValidator();

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Get

        [SwaggerResponse((int)HttpStatusCode.OK, "Return all Products")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Products Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageQuantity = 20)
        {
            try
            {
                var result = await _productService.GetAllAsync(page, pageQuantity);

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
        [SwaggerResponse((int)HttpStatusCode.OK, "Return Product")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);
                if (result.IsNotFound())
                {
                    return NotFound(result.Errors);
                }
                return Ok(result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }
        #endregion

        #region Create
        [SwaggerResponse((int)HttpStatusCode.Created, "Product Created")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Product already exist")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for create Product")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO createProductDTO)
        {
            try
            {
                var result = await _productService.CreateAsync(createProductDTO);

                if (result.IsConflict())
                    return Conflict(result);
                if (result.IsInvalid())
                    return BadRequest(result.Errors);

                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }
        #endregion

        #region Update
        [SwaggerResponse((int)HttpStatusCode.OK, "Product Updated")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Product already exist")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product Not Found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for update Product")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO updateProductDTO)
        {
            try
            {
                var result = await _productService.UpdateAsync(updateProductDTO);

                if (result.IsConflict())
                    return Conflict(result);
                if (result.IsInvalid())
                    return BadRequest(result.Errors);
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

        #region UpdatePatch

        [SwaggerResponse((int)HttpStatusCode.OK, "Quantity changed sucessfully")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "This field is require for only number")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPatch]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateProductQuantityDTO quantityDTO)
        {
            try
            {
                var result = await _productService.UpdateQuantityAsync(quantityDTO);

                if (result.IsInvalid())
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }

        #endregion

        #region Delete
        [SwaggerResponse((int)HttpStatusCode.OK, "Product Deleted")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _productService.RemoveAsync(id);
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
