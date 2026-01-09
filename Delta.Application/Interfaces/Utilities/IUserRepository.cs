using Delta.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Interfaces.Utilities
{

    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<bool> Delete(int id);

        Task<User> GetByUserNameAsync(string userName);

    }

}
