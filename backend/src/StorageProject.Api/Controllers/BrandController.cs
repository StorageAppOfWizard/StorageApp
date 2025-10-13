using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Extensions;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Application.Validators;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StorageProject.Api.Controllers
{
    [ApiController]
    [Route("/brand")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;


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

            var result = await _brandService.GetAllAsync();
            return result.ToActionResult();
        }
        #endregion

        #region GetByID
        [SwaggerResponse((int)HttpStatusCode.OK, "Return all Brands")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Brand Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var result = await _brandService.GetByIdAsync(id);
            return result.ToActionResult();
        }
        #endregion

        #region Create   
        [SwaggerResponse((int)HttpStatusCode.OK, "Brand Created")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Brand already exist")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for create Brand")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrandDTO dto)
        {

            var result = await _brandService.CreateAsync(dto);
            return result.ToActionResult();
        }
        #endregion

        #region Update
        [SwaggerResponse((int)HttpStatusCode.OK, "Brand Updated")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Brand already exist")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Brand Not Found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Error for update Brand")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBrandDTO dto)
        {

            var result = await _brandService.UpdateAsync(dto);
            return result.ToActionResult();

        }
        #endregion

        #region Delete
        [SwaggerResponse((int)HttpStatusCode.OK, "Brand Deleted")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Brand Not Found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unexpected Error")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _brandService.RemoveAsync(id);
            return result.ToActionResult();


            #endregion
        }
    }
}


