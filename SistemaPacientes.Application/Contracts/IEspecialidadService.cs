using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IEspecialidadService
    {
        ApiResponse<IEnumerable<EspecialidadDTO>> GetAllEspecialidades();

        ApiResponse<EspecialidadDTO> GetEspecialidadById(int id);

        ApiResponse<EspecialidadDTO> CreateEspecialidad(CreateEspecialidadDTO request);

        ApiResponse<bool> UpdateEspecialidad(int id, UpdateEspecialidadDTO request);

        ApiResponse<bool> DeleteEspecialidad(int id);
    }
}
