using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IConsultorioService
    {
        ApiResponse<IEnumerable<ConsultorioDTO>> GetAllConsultorios();

        ApiResponse<ConsultorioDTO> GetConsultorioById(int id);

        ApiResponse<ConsultorioDTO> CreateConsultorio(CreateConsultorioDTO request);

        ApiResponse<bool> UpdateConsultorio(int id, UpdateConsultorioDTO request);

        ApiResponse<bool> DeleteConsultorio(int id);
    }
}
