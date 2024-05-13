using MediatR;
using UserManagementService.Application.Behavior;
using UserManagementService.Application.Extensions;
using UserManagementService.Security.Extensions;
using UserManagementService.Persistence.EF.Extensions;
using System.Reflection;
using UserManagementService.Api.Mappings;
using MassTransit;
using UserManagementService.Infrastructure.Extensions;
using static MassTransit.Logging.OperationName;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services
    .AddApplication()
    .AddPersistenceEF(builder.Configuration)
    .AddAuth(builder.Configuration)
    .AddRedis(builder.Configuration);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });

        configurator.ConfigureEndpoints(context);
        configurator.AutoStart = true;

    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwagger",
        builder =>
        {
            // Allow requests from the Ocelot API Gateway
            builder.WithOrigins("http://localhost:5000", "https://localhost:5001")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors("AllowSwagger");
//app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
