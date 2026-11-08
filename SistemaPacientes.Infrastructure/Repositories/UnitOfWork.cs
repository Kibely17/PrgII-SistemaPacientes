using Microsoft.EntityFrameworkCore.Storage;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Context;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(
            ApplicationDbContext context,
            IPacienteRepository pacientes,
            IMedicoRepository medicos,
            ICitaRepository citas,
            IConsultaRepository consultas,
            IGenericRepository<Especialidad> especialidades,
            IGenericRepository<Seguro> seguros,
            IGenericRepository<Consultorio> consultorios,
            IGenericRepository<Medicamento> medicamentos,
            IGenericRepository<Diagnostico> diagnosticos,
            IGenericRepository<Receta> recetas)
        {
            _context = context;
            Pacientes = pacientes;
            Medicos = medicos;
            Citas = citas;
            Consultas = consultas;
            Especialidades = especialidades;
            Seguros = seguros;
            Consultorios = consultorios;
            Medicamentos = medicamentos;
            Diagnosticos = diagnosticos;
            Recetas = recetas;
        }

        public IPacienteRepository Pacientes { get; }

        public IMedicoRepository Medicos { get; }

        public ICitaRepository Citas { get; }

        public IConsultaRepository Consultas { get; }

        public IGenericRepository<Especialidad> Especialidades { get; }

        public IGenericRepository<Seguro> Seguros { get; }

        public IGenericRepository<Consultorio> Consultorios { get; }

        public IGenericRepository<Medicamento> Medicamentos { get; }

        public IGenericRepository<Diagnostico> Diagnosticos { get; }

        public IGenericRepository<Receta> Recetas { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction = null;
        }
    }
}
