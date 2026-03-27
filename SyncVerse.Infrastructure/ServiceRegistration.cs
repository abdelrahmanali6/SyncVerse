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
            services.AddScoped<SyncVerse.Infrastructure.Repositories.IMessageRepository, SyncVerse.Infrastructure.Repositories.MessageRepository>();
            services.AddScoped<SyncVerse.Application.Services.IChannelService, SyncVerse.Infrastructure.Services.ChannelService>();
            services.AddScoped<SyncVerse.Application.Services.IMessageService, SyncVerse.Infrastructure.Services.MessageService>();
            return services;
        }
    }
}
