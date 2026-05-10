using Microsoft.EntityFrameworkCore;
using MiniMercadoSaas.Application.ServiceInterfaces;
using MiniMercadoSaas.Application.Services;
using MiniMercadoSaas.Domain;
using MiniMercadoSaas.Infrastructure.Context;
using MiniMercadoSaas.Infrastructure.Repositorys;
using FluentValidation;
using MiniMercadoSaas.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = ServerVersion.AutoDetect(connectionString);



builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductService, ProdutoService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.MapControllers();




app.Run();

