using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Diagnostico : BaseEntity
    {
        public int ConsultaId { get; set; }

        public Consulta? Consulta { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string? Codigo { get; set; }
    }
}
