namespace SistemaPacientes.Application.Dtos
{
    public class MedicoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Exequatur { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public int EspecialidadId { get; set; }

        public string? EspecialidadNombre { get; set; }
    }
}
