using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Consulta : BaseEntity
    {
        public int CitaId { get; set; }

        public Cita? Cita { get; set; }

        public DateTime Fecha { get; set; }

        public string? Sintomas { get; set; }

        public string? Observaciones { get; set; }

        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();

        public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
    }
}
