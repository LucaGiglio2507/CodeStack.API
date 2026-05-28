using System;
using System.Collections.Generic;
using System.Text;

namespace CodeStack.Core.Interfaces.Services.Tools
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string storedPassword);
    }
}
