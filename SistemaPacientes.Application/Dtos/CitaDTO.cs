namespace SistemaPacientes.Application.Dtos
{
    public class CitaDTO
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }

        public int MedicoId { get; set; }

        public int ConsultorioId { get; set; }

        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = string.Empty;

        public string? Motivo { get; set; }

        public string? PacienteNombre { get; set; }

        public string? MedicoNombre { get; set; }

        public string? ConsultorioNumero { get; set; }
    }
}
