using Appointments.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration)
                .AddApplication()
                .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.WriteTo.File(path:"ResponseErrors.txt", outputTemplate: "{Message}{NewLine:1}{Exception:1}");
    configuration.WriteTo.Console(theme: new CustomConsoleTheme());
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
