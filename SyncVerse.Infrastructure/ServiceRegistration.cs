using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SyncVerse.Infrastructure;

namespace SyncVerse.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<SyncVerse.Infrastructure.Repositories.IInviteRepository, SyncVerse.Infrastructure.Repositories.InviteRepository>();
            services.AddScoped<SyncVerse.Application.Services.IInviteService, SyncVerse.Infrastructure.Services.InviteService>();
            services.AddScoped<SyncVerse.Application.Services.IUserService, SyncVerse.Infrastructure.Services.UserService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IServerRepository, SyncVerse.Infrastructure.Repositories.ServerRepository>();
            services.AddScoped<SyncVerse.Infrastructure.Repositories.IChannelRepository, SyncVerse.Infrastructure.Repositories.ChannelRepository>();
            services.AddScoped<SyncVerse.Application.Interfaces.IMessageRepository, SyncVerse.Infrastructure.Repositories.MessageRepository>();
            services.AddScoped<SyncVerse.Application.Services.IChannelService, SyncVerse.Infrastructure.Services.ChannelService>();
            services.AddScoped<SyncVerse.Application.Services.IMessageService, SyncVerse.Infrastructure.Services.MessageService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IDirectMessageRepository, SyncVerse.Infrastructure.Repositories.DirectMessageRepository>();
            services.AddScoped<SyncVerse.Application.Services.IDirectMessageService, SyncVerse.Infrastructure.Services.DirectMessageService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IMessageReactionRepository, SyncVerse.Infrastructure.Repositories.MessageReactionRepository>();
            services.AddScoped<SyncVerse.Application.Services.IMessageReactionService, SyncVerse.Infrastructure.Services.MessageReactionService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IAppFileRepository, SyncVerse.Infrastructure.Repositories.AppFileRepository>();
            services.AddScoped<SyncVerse.Application.Services.IAppFileService, SyncVerse.Infrastructure.Services.AppFileService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IFriendshipRepository, SyncVerse.Infrastructure.Repositories.FriendshipRepository>();
            services.AddScoped<SyncVerse.Application.Services.IFriendshipService, SyncVerse.Infrastructure.Services.FriendshipService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IUserRoleRepository, SyncVerse.Infrastructure.Repositories.UserRoleRepository>();
            services.AddScoped<SyncVerse.Application.Interfaces.IAuditLogRepository, SyncVerse.Infrastructure.Repositories.AuditLogRepository>();
            services.AddScoped<SyncVerse.Application.Interfaces.IServerBanRepository, SyncVerse.Infrastructure.Repositories.ServerBanRepository>();
            services.AddScoped<SyncVerse.Application.Services.IModerationService, SyncVerse.Infrastructure.Services.ModerationService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IPinnedMessageRepository, SyncVerse.Infrastructure.Repositories.PinnedMessageRepository>();
            services.AddScoped<SyncVerse.Application.Services.IPinnedMessageService, SyncVerse.Infrastructure.Services.PinnedMessageService>();
            services.AddScoped<SyncVerse.Application.Services.IMessageSearchService, SyncVerse.Infrastructure.Services.MessageSearchService>();
            return services;
        }
    }
}
