namespace SistemaPacientes.Application.Dtos
{
    public class CreateMedicamentoDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Presentacion { get; set; }
    }
}
