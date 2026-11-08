namespace SistemaPacientes.Application.Dtos
{
    public class CreateRecetaDTO
    {
        public int ConsultaId { get; set; }

        public int MedicamentoId { get; set; }

        public string Dosis { get; set; } = string.Empty;

        public string? Indicaciones { get; set; }

        public int DiasTratamiento { get; set; }
    }
}
