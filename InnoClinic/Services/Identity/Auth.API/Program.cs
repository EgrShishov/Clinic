using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddPresentation();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
await RoleSeeder.SeedRolesAsync(roleManager);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
