using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Cita : BaseEntity
    {
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }

        public int MedicoId { get; set; }

        public Medico? Medico { get; set; }

        public int ConsultorioId { get; set; }

        public Consultorio? Consultorio { get; set; }

        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = "Pendiente";

        public string? Motivo { get; set; }

        public Consulta? Consulta { get; set; }
    }
}
