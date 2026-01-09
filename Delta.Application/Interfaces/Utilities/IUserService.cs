using Delta.Application.DTOs.Utilities;
using Delta.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Interfaces.Utilities
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User> Create(UserDto dto);
        Task<User?> Update(int id, UserDto dto);
        Task<bool> Delete(int id);
        Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginDto);
    }

}
