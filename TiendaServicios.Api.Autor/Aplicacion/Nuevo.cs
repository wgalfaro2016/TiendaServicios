
using FluentValidation;
using MediatR; // Para IRequest, IRequestHandler y Unit
using System.Net.NetworkInformation;
using TiendaServicios.Api.Autor.Modelo; // Si es necesario
using TiendaServicios.Api.Autor.Persistencia; // Si es necesario


namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest<bool> 
        {
            public string Nombre { get; set; }

            public string Apellido { get; set; }

            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>  //Aca indicamos la clase que debemos validar, en este caso la clase ejecuta
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();

            }
        }

        public class Manejador : IRequestHandler<Ejecuta, bool>
        {

            private readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<bool> Handle (Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    Apellido = request.Apellido,
                    AutorLibroGuid = Guid.NewGuid().ToString()
                };

                _contexto.AutorLibro.Add(autorLibro);
                var valor = await _contexto.SaveChangesAsync();

                if (valor > 0) 
                {
                    return true;
                }
                throw new Exception("No se pudo insertar el autor del libro");
            }
        }

    }
}