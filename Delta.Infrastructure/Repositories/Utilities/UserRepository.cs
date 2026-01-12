using Microsoft.EntityFrameworkCore;
using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;
using Delta.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Delta.Infrastructure.Persistence.EF;

namespace Delta.Infrastructure.Repositories.Utilities
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .Where(x => x.DelStatus == 0)
                .ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
                .Where(x => x.UserId == id && x.DelStatus == 0)
                .FirstOrDefaultAsync();
        }

        public async Task<User> Add(User user)
        {
            user.AddOnDt = DateTime.Now;
            user.DelStatus = 0;  // not deleted

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            user.EditOnDt = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.DelStatus = 1;  // mark as deleted
            user.DelOnDt = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

    }
}
