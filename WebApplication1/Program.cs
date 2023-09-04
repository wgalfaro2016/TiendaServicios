using System.Reflection;
using MediatR;
using WebApplication1.Persistencia;
using Microsoft.EntityFrameworkCore;
using static TiendaServicios.Api.Autor.Aplicacion.Nuevo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextoAutor>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDatabase"));
});

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Ejecuta).Assembly));

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
