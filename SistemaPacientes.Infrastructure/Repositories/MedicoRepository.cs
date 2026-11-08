using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Context;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Infrastructure.Repositories
{
    public class MedicoRepository : GenericRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Medico> GetAllMedicosWithEspecialidad()
        {
            return _context.Medicos.Include(m => m.Especialidad).ToList();
        }

        public Medico? GetMedicoWithEspecialidad(int id)
        {
            return _context.Medicos.Include(m => m.Especialidad).FirstOrDefault(m => m.Id == id);
        }

        public bool TieneCitas(int medicoId)
        {
            return _context.Citas.Any(c => c.MedicoId == medicoId);
        }
    }
}
