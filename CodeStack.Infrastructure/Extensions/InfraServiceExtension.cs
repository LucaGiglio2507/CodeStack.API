using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services;
using CodeStack.Core.Interfaces.Services.Auth;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Core.Interfaces.Services.Tools;
using CodeStack.Core.Services;
using CodeStack.Core.Services.Auth;
using CodeStack.Core.Services.Data;
using CodeStack.Infrastructure.Database.Context;
using CodeStack.Infrastructure.Repositories;
using CodeStack.Security.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeStack.Infrastructure.Extensions;

public static class InfraServiceExtension
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CodeStackDBContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
    }
}
