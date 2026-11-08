using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IConsultaService
    {
        ApiResponse<IEnumerable<ConsultaDTO>> GetAllConsultas();

        ApiResponse<ConsultaDTO> GetConsultaById(int id);

        ApiResponse<ConsultaDTO> CreateConsulta(CreateConsultaDTO request);

        ApiResponse<bool> UpdateConsulta(int id, UpdateConsultaDTO request);

        ApiResponse<bool> DeleteConsulta(int id);
    }
}
