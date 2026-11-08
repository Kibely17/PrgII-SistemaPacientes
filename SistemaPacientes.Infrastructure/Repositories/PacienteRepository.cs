using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Context;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Infrastructure.Repositories
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Paciente> GetAllPacientesWithSeguro()
        {
            return _context.Pacientes.Include(p => p.Seguro).ToList();
        }

        public Paciente? GetPacienteWithSeguro(int id)
        {
            return _context.Pacientes.Include(p => p.Seguro).FirstOrDefault(p => p.Id == id);
        }

        public Paciente? GetPacienteByCedula(string cedula)
        {
            return _context.Pacientes.FirstOrDefault(p => p.Cedula == cedula);
        }

        public bool TieneCitas(int pacienteId)
        {
            return _context.Citas.Any(c => c.PacienteId == pacienteId);
        }
    }
}
