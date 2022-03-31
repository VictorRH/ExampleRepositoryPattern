using ExampleRepositoryPattern.BusinessLogic.Data;
using ExampleRepositoryPattern.BusinessLogic.Logic;
using ExampleRepositoryPattern.Core.Interfaz;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddDbContext<RepositoryPatternDbContext>(options =>
{
    options.EnableSensitiveDataLogging().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Example Repository Pattern",
        Version = "v1"
    });
    x.CustomSchemaIds(x => x.FullName);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("v1/swagger.json", "Example Repository Patter");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
