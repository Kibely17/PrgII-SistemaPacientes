namespace SistemaPacientes.Application.Dtos
{
    public class CreateConsultaDTO
    {
        public int CitaId { get; set; }

        public DateTime Fecha { get; set; }

        public string? Sintomas { get; set; }

        public string? Observaciones { get; set; }
    }
}
