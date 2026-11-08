using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface ISeguroService
    {
        ApiResponse<IEnumerable<SeguroDTO>> GetAllSeguros();

        ApiResponse<SeguroDTO> GetSeguroById(int id);

        ApiResponse<SeguroDTO> CreateSeguro(CreateSeguroDTO request);

        ApiResponse<bool> UpdateSeguro(int id, UpdateSeguroDTO request);

        ApiResponse<bool> DeleteSeguro(int id);
    }
}
