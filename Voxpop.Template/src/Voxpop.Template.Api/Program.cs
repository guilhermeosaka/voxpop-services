using Serilog;
using Voxpop.Packages.Dispatcher.Extensions;
using Voxpop.Template.Api.Middlewares;
using Voxpop.Template.Application.Options;
using Voxpop.Template.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogger(builder.Configuration);

builder.Services.AddControllers();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDispatcher()
    .AddPersistence(builder.Configuration.GetConnectionString("TemplateDb"))
    .AddAuth(builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseSerilogRequestLogging()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();