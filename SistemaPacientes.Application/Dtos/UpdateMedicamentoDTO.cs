namespace SistemaPacientes.Application.Dtos
{
    public class UpdateMedicamentoDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Presentacion { get; set; }
    }
}
