namespace SistemaPacientes.Application.Dtos
{
    public class CreateMedicoDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Exequatur { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public int EspecialidadId { get; set; }
    }
}
