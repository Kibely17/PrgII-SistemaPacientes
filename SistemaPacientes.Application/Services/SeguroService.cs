using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class SeguroService : ISeguroService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeguroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<SeguroDTO>> GetAllSeguros()
        {
            var lista = _unitOfWork.Seguros.GetAll().Select(Mapear);
            return ApiResponse<IEnumerable<SeguroDTO>>.SuccessResponse(lista);
        }

        public ApiResponse<SeguroDTO> GetSeguroById(int id)
        {
            var entidad = _unitOfWork.Seguros.GetById(id);
            if (entidad == null)
                return ApiResponse<SeguroDTO>.ErrorResponse("El Seguro no existe.", 404);

            return ApiResponse<SeguroDTO>.SuccessResponse(Mapear(entidad));
        }

        public ApiResponse<SeguroDTO> CreateSeguro(CreateSeguroDTO request)
        {
            var error = Validar(request.Nombre);
            if (error != null)
                return ApiResponse<SeguroDTO>.ErrorResponse(error);

            var nuevo = new Seguro
            {
                Nombre = request.Nombre,
                Cobertura = request.Cobertura
            };

            _unitOfWork.Seguros.Create(nuevo);
            _unitOfWork.Complete();

            return ApiResponse<SeguroDTO>.SuccessResponse(Mapear(nuevo), "El Seguro fue creado.", 201);
        }

        public ApiResponse<bool> UpdateSeguro(int id, UpdateSeguroDTO request)
        {
            var entidad = _unitOfWork.Seguros.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("El Seguro no existe.", 404);

            var error = Validar(request.Nombre);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            entidad.Nombre = request.Nombre;
            entidad.Cobertura = request.Cobertura;

            _unitOfWork.Seguros.Update(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteSeguro(int id)
        {
            var entidad = _unitOfWork.Seguros.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("El Seguro no existe.", 404);

            _unitOfWork.Seguros.Delete(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private static string? Validar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre del seguro es obligatorio.";

            return null;
        }

        private static SeguroDTO Mapear(Seguro e) => new SeguroDTO
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Cobertura = e.Cobertura
        };
    }
}
