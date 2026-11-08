namespace SistemaPacientes.Application.Dtos
{
    public class UpdateCitaDTO
    {
        public int PacienteId { get; set; }

        public int MedicoId { get; set; }

        public int ConsultorioId { get; set; }

        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = string.Empty;

        public string? Motivo { get; set; }
    }
}
