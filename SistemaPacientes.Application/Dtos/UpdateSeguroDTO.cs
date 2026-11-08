namespace SistemaPacientes.Application.Dtos
{
    public class UpdateSeguroDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Cobertura { get; set; }
    }
}
