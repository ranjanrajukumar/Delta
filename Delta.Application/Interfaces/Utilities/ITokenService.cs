using Delta.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Interfaces.Utilities
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string email, string role);
    }

}
