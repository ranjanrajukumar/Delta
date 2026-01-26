using Delta.Application.Interfaces.Utilities;
using Delta.Domain.Entities.Utilities;
using Delta.Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delta.Infrastructure.Repositories.Utilities
{
    public class MenuTabRepository : IMenuTabRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuTabRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<List<MenuTab>> GetAllAsync()
        //{
        //    return await _context.MenuTabs.ToListAsync();
        //}
    }
}
