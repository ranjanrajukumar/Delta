using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Delta.API.Controllers.Utilities
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        // GET: api/city
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _cityRepository.GetAllAsync();
            return Ok(cities);
        }

        // GET: api/city/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);

            if (city == null)
                return NotFound(new { message = "City not found" });

            return Ok(city);
        }

        // POST: api/city
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cityId = await _cityRepository.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = cityId },
                new { CityId = cityId, message = "City created successfully" }
            );
        }

        // PUT: api/city
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _cityRepository.UpdateAsync(dto);

            if (!updated)
                return NotFound(new { message = "City not found" });

            return Ok(new { message = "City updated successfully" });
        }

        // DELETE: api/city/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _cityRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "City not found" });

            return Ok(new { message = "City deleted successfully" });
        }
    }
}
