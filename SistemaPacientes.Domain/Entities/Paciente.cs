using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities
{
    public class Paciente : BaseEntity
    {
        public string Cedula { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Direccion { get; set; }

        public int? SeguroId { get; set; }

        public Seguro? Seguro { get; set; }

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();

        public string NombreCompleto()
        {
            return Nombre + " " + Apellido;
        }

        public int Edad()
        {
            var edad = DateTime.Today.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }
            return edad;
        }
    }
}
