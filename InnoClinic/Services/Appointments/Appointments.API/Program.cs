using Appointments.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration)
                .AddApplication()
                .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.WriteTo.File(path:"ResponseErrors.txt", outputTemplate: "{Message}{NewLine:1}{Exception:1}");
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseLoggingMiddleware();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseLoggingMiddleware();

app.Run();
