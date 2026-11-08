using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Medico : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Exequatur { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public int EspecialidadId { get; set; }

        public Especialidad? Especialidad { get; set; }

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
