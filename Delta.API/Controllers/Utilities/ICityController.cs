using Delta.Domain.Entities.Utilities;
using Delta.Infrastructure.Persistence.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Delta.API.Controllers.Utilities
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ICity>>> GetCities()
        {
            var cities = await _context.Cities.ToListAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ICity>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
                return NotFound();

            return Ok(city);
        }
    }
}
