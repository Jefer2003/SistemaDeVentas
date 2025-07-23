
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using SistemaDeVentas.Data;
using SistemaDeVentas.Services;
using SistemaDeVentas.Mapping;
using SistemaDeVentas.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Entity Framework
builder.Services.AddDbContext<SistemaVentasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios de negocio
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDetalleVentaService, DetalleVentaService>();

// Registrar AutoMapper con todos los perfiles específicos
builder.Services.AddAutoMapper(typeof(CategoriaMapping), typeof(ProductoMapping), typeof(UsuarioMapping), typeof(VentaMapping), typeof(DetalleVentaMapping));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Crear la base de datos
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SistemaVentasContext>();
    context.Database.EnsureCreated();
}

app.Run();
