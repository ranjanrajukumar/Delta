using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Interfaces.Utilities
{
    public interface IUserContext
    {
        string UserId { get; }
        string Username { get; }
        string Token { get; }
    }
}
