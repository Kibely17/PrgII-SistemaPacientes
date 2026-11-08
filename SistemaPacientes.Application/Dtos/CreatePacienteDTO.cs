namespace SistemaPacientes.Application.Dtos
{
    public class CreatePacienteDTO
    {
        public string Cedula { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Direccion { get; set; }

        public int? SeguroId { get; set; }
    }
}
