using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IPacienteService
    {
        ApiResponse<IEnumerable<PacienteDTO>> GetAllPacientes();

        ApiResponse<PacienteDTO> GetPacienteById(int id);

        ApiResponse<PacienteDTO> CreatePaciente(CreatePacienteDTO request);

        ApiResponse<bool> UpdatePaciente(int id, UpdatePacienteDTO request);

        ApiResponse<bool> DeletePaciente(int id);
    }
}
