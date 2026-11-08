using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Contracts
{
    public interface ICitaRepository : IGenericRepository<Cita>
    {
        IEnumerable<Cita> GetAllCitasWithDetalle();

        Cita? GetCitaWithDetalle(int id);

        bool ConsultorioOcupado(int consultorioId, DateTime fecha, int citaIdExcluida);

        bool MedicoOcupado(int medicoId, DateTime fecha, int citaIdExcluida);
    }
}
