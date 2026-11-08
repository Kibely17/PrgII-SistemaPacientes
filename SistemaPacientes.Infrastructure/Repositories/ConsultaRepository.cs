using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Context;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Infrastructure.Repositories
{
    public class ConsultaRepository : GenericRepository<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Consulta> GetAllConsultasWithDetalle()
        {
            return _context.Consultas
                .Include(c => c.Cita)
                .Include(c => c.Diagnosticos)
                .Include(c => c.Recetas)
                .ToList();
        }

        public bool CitaYaTieneConsulta(int citaId)
        {
            return _context.Consultas.Any(c => c.CitaId == citaId);
        }
    }
}
