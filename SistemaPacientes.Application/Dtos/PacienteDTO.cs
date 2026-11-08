namespace SistemaPacientes.Application.Dtos
{
    public class PacienteDTO
    {
        public int Id { get; set; }

        public string Cedula { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Direccion { get; set; }

        public int? SeguroId { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public int Edad { get; set; }

        public string? SeguroNombre { get; set; }
    }
}
