using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Medicamento : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Presentacion { get; set; }

        public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
    }
}
