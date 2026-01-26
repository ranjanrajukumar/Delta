using Delta.Application.Interfaces.Utilities;
using Delta.Domain.Entities.Utilities;
using Delta.Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delta.Infrastructure.Repositories.Utilities
{
    public class MenuFormRightsRepository : IMenuFormRightsRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuFormRightsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuFormRight>> GetByMenuIdAsync(int menuId)
        {
            return await _context.MenuFormRights
                .Where(r => r.MenuId == menuId && r.DelStatus == 0)
                .OrderBy(r => r.Tab)
                .ThenBy(r => r.ButtonText)
                .ToListAsync();
        }
    }
}
