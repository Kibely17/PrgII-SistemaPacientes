using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IMedicamentoService
    {
        ApiResponse<IEnumerable<MedicamentoDTO>> GetAllMedicamentos();

        ApiResponse<MedicamentoDTO> GetMedicamentoById(int id);

        ApiResponse<MedicamentoDTO> CreateMedicamento(CreateMedicamentoDTO request);

        ApiResponse<bool> UpdateMedicamento(int id, UpdateMedicamentoDTO request);

        ApiResponse<bool> DeleteMedicamento(int id);
    }
}
