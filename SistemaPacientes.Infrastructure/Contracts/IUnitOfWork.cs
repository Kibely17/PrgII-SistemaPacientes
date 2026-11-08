using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Contracts
{
    public interface IUnitOfWork
    {
        IPacienteRepository Pacientes { get; }

        IMedicoRepository Medicos { get; }

        ICitaRepository Citas { get; }

        IConsultaRepository Consultas { get; }

        IGenericRepository<Especialidad> Especialidades { get; }

        IGenericRepository<Seguro> Seguros { get; }

        IGenericRepository<Consultorio> Consultorios { get; }

        IGenericRepository<Medicamento> Medicamentos { get; }

        IGenericRepository<Diagnostico> Diagnosticos { get; }

        IGenericRepository<Receta> Recetas { get; }

        void Complete();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
