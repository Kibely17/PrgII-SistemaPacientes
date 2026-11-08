using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Contracts
{
    public interface ICitaService
    {
        ApiResponse<IEnumerable<CitaDTO>> GetAllCitas();

        ApiResponse<CitaDTO> GetCitaById(int id);

        ApiResponse<CitaDTO> CreateCita(CreateCitaDTO request);

        ApiResponse<bool> UpdateCita(int id, UpdateCitaDTO request);

        ApiResponse<bool> DeleteCita(int id);
    }
}
