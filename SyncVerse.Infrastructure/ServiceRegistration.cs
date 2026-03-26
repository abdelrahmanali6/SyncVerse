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
            services.AddScoped<SyncVerse.Application.Services.IUserService, SyncVerse.Infrastructure.Services.UserService>();
            services.AddScoped<SyncVerse.Application.Interfaces.IServerRepository, SyncVerse.Infrastructure.Repositories.ServerRepository>();
            services.AddScoped<SyncVerse.Application.Interfaces.IChannelRepository, SyncVerse.Infrastructure.Repositories.ChannelRepository>();
            services.AddScoped<SyncVerse.Application.Interfaces.IMessageRepository, SyncVerse.Infrastructure.Repositories.MessageRepository>();
            return services;
        }
    }
}
