using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PacienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<PacienteDTO>> GetAllPacientes()
        {
            var pacientes = _unitOfWork.Pacientes.GetAllPacientesWithSeguro().Select(Mapear);
            return ApiResponse<IEnumerable<PacienteDTO>>.SuccessResponse(pacientes);
        }

        public ApiResponse<PacienteDTO> GetPacienteById(int id)
        {
            var paciente = _unitOfWork.Pacientes.GetPacienteWithSeguro(id);
            if (paciente == null)
                return ApiResponse<PacienteDTO>.ErrorResponse("El paciente no existe.", 404);

            return ApiResponse<PacienteDTO>.SuccessResponse(Mapear(paciente));
        }

        public ApiResponse<PacienteDTO> CreatePaciente(CreatePacienteDTO request)
        {
            var error = Validar(request.Cedula, request.Nombre, request.Apellido,
                request.FechaNacimiento, request.Sexo, request.Correo, request.SeguroId);
            if (error != null)
                return ApiResponse<PacienteDTO>.ErrorResponse(error);

            if (_unitOfWork.Pacientes.GetPacienteByCedula(request.Cedula) != null)
                return ApiResponse<PacienteDTO>.ErrorResponse("Ya hay un paciente registrado con esa cedula.");

            var nuevo = new Paciente
            {
                Cedula = request.Cedula,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                Sexo = request.Sexo,
                Telefono = request.Telefono,
                Correo = request.Correo,
                Direccion = request.Direccion,
                SeguroId = request.SeguroId
            };

            _unitOfWork.Pacientes.Create(nuevo);
            _unitOfWork.Complete();

            return ApiResponse<PacienteDTO>.SuccessResponse(Mapear(nuevo), "El paciente fue registrado.", 201);
        }

        public ApiResponse<bool> UpdatePaciente(int id, UpdatePacienteDTO request)
        {
            var paciente = _unitOfWork.Pacientes.GetById(id);
            if (paciente == null)
                return ApiResponse<bool>.ErrorResponse("El paciente no existe.", 404);

            var error = Validar(request.Cedula, request.Nombre, request.Apellido,
                request.FechaNacimiento, request.Sexo, request.Correo, request.SeguroId);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            var repetido = _unitOfWork.Pacientes.GetPacienteByCedula(request.Cedula);
            if (repetido != null && repetido.Id != id)
                return ApiResponse<bool>.ErrorResponse("Ya hay otro paciente con esa cedula.");

            paciente.Cedula = request.Cedula;
            paciente.Nombre = request.Nombre;
            paciente.Apellido = request.Apellido;
            paciente.FechaNacimiento = request.FechaNacimiento;
            paciente.Sexo = request.Sexo;
            paciente.Telefono = request.Telefono;
            paciente.Correo = request.Correo;
            paciente.Direccion = request.Direccion;
            paciente.SeguroId = request.SeguroId;

            _unitOfWork.Pacientes.Update(paciente);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeletePaciente(int id)
        {
            var paciente = _unitOfWork.Pacientes.GetById(id);
            if (paciente == null)
                return ApiResponse<bool>.ErrorResponse("El paciente no existe.", 404);

            if (_unitOfWork.Pacientes.TieneCitas(id))
                return ApiResponse<bool>.ErrorResponse("No se puede eliminar el paciente porque tiene citas registradas.");

            _unitOfWork.Pacientes.Delete(paciente);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private string? Validar(string cedula, string nombre, string apellido,
            DateTime fechaNacimiento, string sexo, string? correo, int? seguroId)
        {
            if (string.IsNullOrWhiteSpace(cedula)) return "La cedula es obligatoria.";
            if (string.IsNullOrWhiteSpace(nombre)) return "El nombre es obligatorio.";
            if (string.IsNullOrWhiteSpace(apellido)) return "El apellido es obligatorio.";
            if (fechaNacimiento > DateTime.Today) return "La fecha de nacimiento no puede ser futura.";
            if (fechaNacimiento < DateTime.Today.AddYears(-120)) return "La fecha de nacimiento no es valida.";
            if (sexo != "M" && sexo != "F") return "El sexo debe ser M o F.";
            if (!string.IsNullOrWhiteSpace(correo) && !correo.Contains('@')) return "El correo no tiene un formato valido.";
            if (seguroId.HasValue && _unitOfWork.Seguros.GetById(seguroId.Value) == null) return "El seguro indicado no existe.";
            return null;
        }

        private static PacienteDTO Mapear(Paciente p) => new PacienteDTO
        {
            Id = p.Id,
            Cedula = p.Cedula,
            Nombre = p.Nombre,
            Apellido = p.Apellido,
            FechaNacimiento = p.FechaNacimiento,
            Sexo = p.Sexo,
            Telefono = p.Telefono,
            Correo = p.Correo,
            Direccion = p.Direccion,
            SeguroId = p.SeguroId,
            NombreCompleto = p.NombreCompleto(),
            Edad = p.Edad(),
            SeguroNombre = p.Seguro?.Nombre
        };
    }
}
