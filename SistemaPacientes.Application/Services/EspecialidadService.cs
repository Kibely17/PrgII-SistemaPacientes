using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EspecialidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<EspecialidadDTO>> GetAllEspecialidades()
        {
            var lista = _unitOfWork.Especialidades.GetAll().Select(Mapear);
            return ApiResponse<IEnumerable<EspecialidadDTO>>.SuccessResponse(lista);
        }

        public ApiResponse<EspecialidadDTO> GetEspecialidadById(int id)
        {
            var entidad = _unitOfWork.Especialidades.GetById(id);
            if (entidad == null)
                return ApiResponse<EspecialidadDTO>.ErrorResponse("La Especialidad no existe.", 404);

            return ApiResponse<EspecialidadDTO>.SuccessResponse(Mapear(entidad));
        }

        public ApiResponse<EspecialidadDTO> CreateEspecialidad(CreateEspecialidadDTO request)
        {
            var error = Validar(request.Nombre);
            if (error != null)
                return ApiResponse<EspecialidadDTO>.ErrorResponse(error);

            var nuevo = new Especialidad
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };

            _unitOfWork.Especialidades.Create(nuevo);
            _unitOfWork.Complete();

            return ApiResponse<EspecialidadDTO>.SuccessResponse(Mapear(nuevo), "La Especialidad fue creada.", 201);
        }

        public ApiResponse<bool> UpdateEspecialidad(int id, UpdateEspecialidadDTO request)
        {
            var entidad = _unitOfWork.Especialidades.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("La Especialidad no existe.", 404);

            var error = Validar(request.Nombre);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            entidad.Nombre = request.Nombre;
            entidad.Descripcion = request.Descripcion;

            _unitOfWork.Especialidades.Update(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteEspecialidad(int id)
        {
            var entidad = _unitOfWork.Especialidades.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("La Especialidad no existe.", 404);

            _unitOfWork.Especialidades.Delete(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private static string? Validar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre de la especialidad es obligatorio.";

            return null;
        }

        private static EspecialidadDTO Mapear(Especialidad e) => new EspecialidadDTO
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Descripcion = e.Descripcion
        };
    }
}
