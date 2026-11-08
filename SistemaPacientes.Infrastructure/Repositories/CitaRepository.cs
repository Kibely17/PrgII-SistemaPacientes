using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Context;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Infrastructure.Repositories
{
    public class CitaRepository : GenericRepository<Cita>, ICitaRepository
    {
        public CitaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Cita> GetAllCitasWithDetalle()
        {
            return _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Consultorio)
                .ToList();
        }

        public Cita? GetCitaWithDetalle(int id)
        {
            return _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Consultorio)
                .FirstOrDefault(c => c.Id == id);
        }

        public bool ConsultorioOcupado(int consultorioId, DateTime fecha, int citaIdExcluida)
        {
            return _context.Citas.Any(c => c.ConsultorioId == consultorioId
                && c.Fecha == fecha
                && c.Id != citaIdExcluida
                && c.Estado != "Cancelada");
        }

        public bool MedicoOcupado(int medicoId, DateTime fecha, int citaIdExcluida)
        {
            return _context.Citas.Any(c => c.MedicoId == medicoId
                && c.Fecha == fecha
                && c.Id != citaIdExcluida
                && c.Estado != "Cancelada");
        }
    }
}
