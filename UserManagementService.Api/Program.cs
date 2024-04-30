using MediatR;
using UserManagementService.Application.Behavior;
using UserManagementService.Application.Extensions;
using UserManagementService.Security.Extensions;
using UserManagementService.Persistence.EF.Extensions;
using System.Reflection;
using UserManagementService.Api.Mappings;

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
app.UseCors("AllowSwagger");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();