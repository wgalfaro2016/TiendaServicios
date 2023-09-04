using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Modelo;

namespace WebApplication1.Persistencia
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options)
        {
        }

        public DbSet<AutorLibro> AutorLibro { get; set; }

        public DbSet<GradoAcademico> GradoAcademico { get; set; }

    }

}
