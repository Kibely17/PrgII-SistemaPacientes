using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Contracts
{
    public interface IMedicoRepository : IGenericRepository<Medico>
    {
        IEnumerable<Medico> GetAllMedicosWithEspecialidad();

        Medico? GetMedicoWithEspecialidad(int id);

        bool TieneCitas(int medicoId);
    }
}
