namespace SistemaPacientes.Application.Dtos
{
    public class DiagnosticoDTO
    {
        public int Id { get; set; }

        public int ConsultaId { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string? Codigo { get; set; }
    }
}
