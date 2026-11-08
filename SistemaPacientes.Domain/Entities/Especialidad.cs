using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Especialidad : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();
    }
}
