namespace SistemaPacientes.Application.Dtos
{
    public class CreateSeguroDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Cobertura { get; set; }
    }
}
