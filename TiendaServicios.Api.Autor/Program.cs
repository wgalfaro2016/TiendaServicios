
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using TiendaServicios.Api.Autor.Persistencia;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static TiendaServicios.Api.Autor.Aplicacion.Nuevo;
using FluentValidation.AspNetCore;
using TiendaServicios.Api.Autor.Aplicacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
builder.Services.AddDbContext<ContextoAutor>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDatabase"));
}, ServiceLifetime.Scoped);
builder.Services.AddAutoMapper(typeof(Consulta.Manejador));

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Ejecuta).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
