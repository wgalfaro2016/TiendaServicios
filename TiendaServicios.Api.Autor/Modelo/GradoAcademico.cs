namespace TiendaServicios.Api.Autor.Modelo
{
    public class GradoAcademico
    {
        public int GradoAcademicoId { get; set; }

        public int Nombre { get; set; }

        public string CentroAcademico { get; set; }

        public DateTime FechaGrado { get; set; }

        public int AutorLibroId { get; set; }

        public int AutorLibro { get; set; }

        public string GradoAcademicoGuid { get; set; }
    }
}
