using CodeStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeStack.Core.Interfaces.Services.Tools
{
    public interface IJwtService
    {
        string GenerateToken(User user); //à la place de Task<LoginRequestDto>, la méthode renvoie un string
    }
}
