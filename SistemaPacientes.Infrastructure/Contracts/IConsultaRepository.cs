using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Contracts
{
    public interface IConsultaRepository : IGenericRepository<Consulta>
    {
        IEnumerable<Consulta> GetAllConsultasWithDetalle();

        bool CitaYaTieneConsulta(int citaId);
    }
}
