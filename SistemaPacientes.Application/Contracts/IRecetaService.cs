using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IRecetaService
    {
        ApiResponse<IEnumerable<RecetaDTO>> GetAllRecetas();

        ApiResponse<RecetaDTO> GetRecetaById(int id);

        ApiResponse<RecetaDTO> CreateReceta(CreateRecetaDTO request);

        ApiResponse<bool> UpdateReceta(int id, UpdateRecetaDTO request);

        ApiResponse<bool> DeleteReceta(int id);
    }
}
