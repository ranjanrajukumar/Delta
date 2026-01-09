using Microsoft.AspNetCore.Mvc;
using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;
using Delta.Application.Services.Utilities;

namespace Delta.API.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(new { message = "User not found" });

            return Ok(result);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.Create(dto);
            return Ok(result);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.Update(id, dto);
            if (result == null)
                return NotFound(new { message = "User not found for update" });

            return Ok(result);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.Delete(id);
            if (!success)
                return NotFound(new { message = "User not found to delete" });

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
