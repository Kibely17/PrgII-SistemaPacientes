using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Receta : BaseEntity
    {
        public int ConsultaId { get; set; }

        public Consulta? Consulta { get; set; }

        public int MedicamentoId { get; set; }

        public Medicamento? Medicamento { get; set; }

        public string Dosis { get; set; } = string.Empty;

        public string? Indicaciones { get; set; }

        public int DiasTratamiento { get; set; }
    }
}
