using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IMedicoService
    {
        ApiResponse<IEnumerable<MedicoDTO>> GetAllMedicos();

        ApiResponse<MedicoDTO> GetMedicoById(int id);

        ApiResponse<MedicoDTO> CreateMedico(CreateMedicoDTO request);

        ApiResponse<bool> UpdateMedico(int id, UpdateMedicoDTO request);

        ApiResponse<bool> DeleteMedico(int id);
    }
}
