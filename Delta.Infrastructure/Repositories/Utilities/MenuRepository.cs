using Delta.Application.Interfaces.Utilities;
using Delta.Domain.Entities.Utilities;
using Delta.Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Infrastructure.Repositories.Utilities
{

    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            return await _context.Set<Menu>()
                .Where(x => x.DelStatus == 0 || x.DelStatus == null)
                .OrderBy(x => x.MenuOrder)
                .AsNoTracking()
                .ToListAsync();
        }


    }
}
