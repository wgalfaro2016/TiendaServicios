using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteService;
using static TiendaServicios.Api.CarritoCompra.Aplicacion.Nuevo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ILibroService, LibrosService>();
builder.Services.AddControllers();
//builder.Services.AddDbContext<CarritoContexto>(options =>
//{
//    options.UseMySQL(builder.Configuration.GetConnectionString("ConexionDatabase"));
//}, ServiceLifetime.Scoped);

builder.Services.AddDbContext<CarritoContexto>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDatabase"));
}, ServiceLifetime.Scoped);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Ejecuta).Assembly));

builder.Services.AddHttpClient("Libros", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
