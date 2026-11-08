using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<MedicoDTO>> GetAllMedicos()
        {
            var medicos = _unitOfWork.Medicos.GetAllMedicosWithEspecialidad().Select(Mapear);
            return ApiResponse<IEnumerable<MedicoDTO>>.SuccessResponse(medicos);
        }

        public ApiResponse<MedicoDTO> GetMedicoById(int id)
        {
            var medico = _unitOfWork.Medicos.GetMedicoWithEspecialidad(id);
            if (medico == null)
                return ApiResponse<MedicoDTO>.ErrorResponse("El medico no existe.", 404);

            return ApiResponse<MedicoDTO>.SuccessResponse(Mapear(medico));
        }

        public ApiResponse<MedicoDTO> CreateMedico(CreateMedicoDTO request)
        {
            var error = Validar(request.Nombre, request.Apellido, request.Exequatur, request.EspecialidadId);
            if (error != null)
                return ApiResponse<MedicoDTO>.ErrorResponse(error);

            var nuevo = new Medico
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Exequatur = request.Exequatur,
                Telefono = request.Telefono,
                EspecialidadId = request.EspecialidadId
            };

            _unitOfWork.Medicos.Create(nuevo);
            _unitOfWork.Complete();

            return ApiResponse<MedicoDTO>.SuccessResponse(Mapear(nuevo), "El medico fue registrado.", 201);
        }

        public ApiResponse<bool> UpdateMedico(int id, UpdateMedicoDTO request)
        {
            var medico = _unitOfWork.Medicos.GetById(id);
            if (medico == null)
                return ApiResponse<bool>.ErrorResponse("El medico no existe.", 404);

            var error = Validar(request.Nombre, request.Apellido, request.Exequatur, request.EspecialidadId);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            medico.Nombre = request.Nombre;
            medico.Apellido = request.Apellido;
            medico.Exequatur = request.Exequatur;
            medico.Telefono = request.Telefono;
            medico.EspecialidadId = request.EspecialidadId;

            _unitOfWork.Medicos.Update(medico);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteMedico(int id)
        {
            var medico = _unitOfWork.Medicos.GetById(id);
            if (medico == null)
                return ApiResponse<bool>.ErrorResponse("El medico no existe.", 404);

            if (_unitOfWork.Medicos.TieneCitas(id))
                return ApiResponse<bool>.ErrorResponse("No se puede eliminar el medico porque tiene citas registradas.");

            _unitOfWork.Medicos.Delete(medico);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private string? Validar(string nombre, string apellido, string exequatur, int especialidadId)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "El nombre del medico es obligatorio.";
            if (string.IsNullOrWhiteSpace(apellido)) return "El apellido del medico es obligatorio.";
            if (string.IsNullOrWhiteSpace(exequatur)) return "El exequatur es obligatorio.";
            if (_unitOfWork.Especialidades.GetById(especialidadId) == null) return "La especialidad indicada no existe.";
            return null;
        }

        private static MedicoDTO Mapear(Medico m) => new MedicoDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Apellido = m.Apellido,
            Exequatur = m.Exequatur,
            Telefono = m.Telefono,
            EspecialidadId = m.EspecialidadId,
            EspecialidadNombre = m.Especialidad?.Nombre
        };
    }
}
