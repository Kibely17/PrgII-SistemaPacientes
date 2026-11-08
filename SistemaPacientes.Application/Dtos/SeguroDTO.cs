namespace SistemaPacientes.Application.Dtos
{
    public class SeguroDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Cobertura { get; set; }
    }
}
