using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Contracts
{
    public interface IPacienteRepository : IGenericRepository<Paciente>
    {
        IEnumerable<Paciente> GetAllPacientesWithSeguro();

        Paciente? GetPacienteWithSeguro(int id);

        Paciente? GetPacienteByCedula(string cedula);

        bool TieneCitas(int pacienteId);
    }
}
