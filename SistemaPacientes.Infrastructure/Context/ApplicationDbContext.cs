using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Especialidad> Especialidades { get; set; } = null!;
        public DbSet<Seguro> Seguros { get; set; } = null!;
        public DbSet<Consultorio> Consultorios { get; set; } = null!;
        public DbSet<Medicamento> Medicamentos { get; set; } = null!;
        public DbSet<Medico> Medicos { get; set; } = null!;
        public DbSet<Paciente> Pacientes { get; set; } = null!;
        public DbSet<Cita> Citas { get; set; } = null!;
        public DbSet<Consulta> Consultas { get; set; } = null!;
        public DbSet<Diagnostico> Diagnosticos { get; set; } = null!;
        public DbSet<Receta> Recetas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>().HasIndex(p => p.Cedula).IsUnique();
            modelBuilder.Entity<Medico>().HasIndex(m => m.Exequatur).IsUnique();
            modelBuilder.Entity<Consultorio>().HasIndex(c => c.Numero).IsUnique();

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Cita)
                .WithOne(c => c.Consulta)
                .HasForeignKey<Consulta>(c => c.CitaId)
                .OnDelete(DeleteBehavior.Restrict);

            foreach (var fk in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys())
                         .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
