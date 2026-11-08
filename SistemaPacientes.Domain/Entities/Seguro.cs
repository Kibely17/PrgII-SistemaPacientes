using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Seguro : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Cobertura { get; set; }

        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
