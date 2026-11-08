using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface IDiagnosticoService
    {
        ApiResponse<IEnumerable<DiagnosticoDTO>> GetAllDiagnosticos();

        ApiResponse<DiagnosticoDTO> GetDiagnosticoById(int id);

        ApiResponse<DiagnosticoDTO> CreateDiagnostico(CreateDiagnosticoDTO request);

        ApiResponse<bool> UpdateDiagnostico(int id, UpdateDiagnosticoDTO request);

        ApiResponse<bool> DeleteDiagnostico(int id);
    }
}
