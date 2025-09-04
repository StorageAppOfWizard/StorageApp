using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.User;

namespace StorageProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _userService.GetAllAsync();

                if (result.IsNotFound())
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _userService.GetByIdAsync(id);

                if (result.IsNotFound())
                    return NotFound(result);
                return Ok(result);
            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred. ", message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            try
            {
                var result = await _userService.CreateAsync(dto);

                if (result.IsInvalid())
                    return BadRequest(result);
                if (result.IsConflict())
                    return BadRequest(result);

                return CreatedAtAction(nameof(Create), result);


            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO dto)
        {
            try
            {
                var result = await _userService.UpdateAsync(dto);

                if (result.IsInvalid())
                    return BadRequest(result);
                if (result.IsConflict())
                    return Conflict(result);
                if (result.IsNotFound())
                    return NotFound(result);
                return Ok(result);

            }
            catch (Exception message)
            {
                return StatusCode(500, new { Message = message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
             
        }
    }
}