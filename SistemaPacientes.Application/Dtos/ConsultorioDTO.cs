namespace SistemaPacientes.Application.Dtos
{
    public class ConsultorioDTO
    {
        public int Id { get; set; }

        public string Numero { get; set; } = string.Empty;

        public string? Piso { get; set; }
    }
}
