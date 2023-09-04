using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;
using static TiendaServicios.Api.Libro.Aplicacion.Nuevo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
builder.Services.AddDbContext<ContextoLibreria>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexionDB"));
}, ServiceLifetime.Scoped);
builder.Services.AddAutoMapper(typeof(Consulta.Ejecuta));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
