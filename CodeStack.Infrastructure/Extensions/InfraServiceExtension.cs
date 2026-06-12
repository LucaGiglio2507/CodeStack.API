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
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CodeStackDBContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IKanbanRepository, KanbanRepository>();
        services.AddScoped<IKanbanColumnRepository, KanbanColumnRepository>();
        services.AddScoped<IKanbanTaskRepository, KanbanTaskRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IFolderRepository, FolderRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ISynthesisRepository, SynthesisRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IKanbanService, KanbanService>();
        services.AddScoped<IKanbanColumnService, KanbanColumnService>();
        services.AddScoped<IKanbanTaskService, KanbanTaskService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IFolderService, FolderService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ISynthesisService, SynthesisService>();

        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
    }
}
