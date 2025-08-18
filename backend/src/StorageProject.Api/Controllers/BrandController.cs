using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Application.Validators;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StorageProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly BrandValidator _brandValidator = new BrandValidator();


        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        #region Get
        [SwaggerResponse((int)HttpStatusCode.OK, "Return all Brands")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Brands Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _brandService.GetAllAsync();

                if (result.IsNotFound())
                    return NotFound(result);

                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
            #endregion

        }
        #region GetByID
        [SwaggerResponse((int)HttpStatusCode.OK, "Return all Brands")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Brand Not Found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Brand ID Error")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _brandService.GetByIdAsync(id);
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
        [SwaggerResponse((int)HttpStatusCode.OK, "Brand Created")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Brand already exist")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for create Brand")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrandDTO createBrandDTO)
        {
            try
            {
                var brandValidator = await _brandValidator.ValidateAsync(createBrandDTO);

                if (!brandValidator.IsValid)
                    return BadRequest(brandValidator.ToDictionary());

                var result = await _brandService.CreateAsync(createBrandDTO);

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
        [SwaggerResponse((int)HttpStatusCode.OK, "Brand Updated")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Brand already exist")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Brand Not Found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for update Brand")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBrandDTO updateBrandDTO)
        {
            try
            {
                var brandValidator = await _brandValidator.ValidateAsync(updateBrandDTO);

                if (!brandValidator.IsValid)
                    return BadRequest(brandValidator.ToDictionary());

                var result = await _brandService.UpdateAsync(updateBrandDTO);

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
        [SwaggerResponse((int)HttpStatusCode.OK, "Brand Deleted")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Brand Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _brandService.RemoveAsync(id);
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

            #endregion
        }
    }
}

