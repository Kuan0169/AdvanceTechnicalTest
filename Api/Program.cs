using Microsoft.EntityFrameworkCore; // Ensure this is included
using MyCompany.Test.Core;
using MyCompany.Test.Infrastructure.Services;
using Scalar.AspNetCore;
// Add the following using directive to resolve the 'UseSqlServer' method
using Microsoft.EntityFrameworkCore.SqlServer;
using ApiMyCompany.Test.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
// Register the ProductService and its interface
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
