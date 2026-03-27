using Swashbuckle.AspNetCore.SwaggerGen;
using SyncVerse.Infrastructure;
using SyncVerse.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHub<ChatHub>("/hubs/chat");
app.Run();
