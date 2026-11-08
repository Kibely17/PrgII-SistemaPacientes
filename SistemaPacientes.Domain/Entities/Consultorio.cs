using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Consultorio : BaseEntity
    {
        public string Numero { get; set; } = string.Empty;

        public string? Piso { get; set; }

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
