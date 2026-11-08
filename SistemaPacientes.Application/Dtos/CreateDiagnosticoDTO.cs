namespace SistemaPacientes.Application.Dtos
{
    public class CreateDiagnosticoDTO
    {
        public int ConsultaId { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string? Codigo { get; set; }
    }
}
