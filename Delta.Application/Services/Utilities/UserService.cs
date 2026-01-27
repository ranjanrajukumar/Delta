using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;
using Delta.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delta.Application.Services.Utilities
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly ITokenService _tokenService;

        public UserService(
            IUserRepository repo,
            ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<User?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<User> Create(UserDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Email = dto.Email,
                FullName = dto.FullName,
                MobileNo = dto.MobileNo,
                UserType = dto.UserType,
                Category = dto.Category,

                Status = "Active",
                AuthAdd = "",
                AuthDel = "",
                AuthLstEdt = ""
            };

            return await _repo.Add(user);
        }

        public async Task<User?> Update(int id, UserDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null)
                return null;

            existing.UserName = dto.UserName;
            existing.Email = dto.Email;
            existing.FullName = dto.FullName;
            existing.MobileNo = dto.MobileNo;

            return await _repo.Update(existing);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        // ✅ FIXED LOGIN METHOD
        public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto loginDto)
        {
            var user = await _repo.GetByUserNameAsync(loginDto.UserName);

            if (user == null ||
                string.IsNullOrEmpty(user.Password) ||
                !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }

            var token = _tokenService.GenerateToken(
                user.UserId,                 // int
                user.UserName,               // username
                user.Email ?? string.Empty,  // email
                user.UserType ?? "User",     // role
                user.Category ?? string.Empty
            );

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
