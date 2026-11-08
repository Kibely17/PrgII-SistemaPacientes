using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class ConsultorioService : IConsultorioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultorioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<ConsultorioDTO>> GetAllConsultorios()
        {
            var lista = _unitOfWork.Consultorios.GetAll().Select(Mapear);
            return ApiResponse<IEnumerable<ConsultorioDTO>>.SuccessResponse(lista);
        }

        public ApiResponse<ConsultorioDTO> GetConsultorioById(int id)
        {
            var entidad = _unitOfWork.Consultorios.GetById(id);
            if (entidad == null)
                return ApiResponse<ConsultorioDTO>.ErrorResponse("El Consultorio no existe.", 404);

            return ApiResponse<ConsultorioDTO>.SuccessResponse(Mapear(entidad));
        }

        public ApiResponse<ConsultorioDTO> CreateConsultorio(CreateConsultorioDTO request)
        {
            var error = Validar(request.Numero);
            if (error != null)
                return ApiResponse<ConsultorioDTO>.ErrorResponse(error);

            var nuevo = new Consultorio
            {
                Numero = request.Numero,
                Piso = request.Piso
            };

            _unitOfWork.Consultorios.Create(nuevo);
            _unitOfWork.Complete();

            return ApiResponse<ConsultorioDTO>.SuccessResponse(Mapear(nuevo), "El Consultorio fue creado.", 201);
        }

        public ApiResponse<bool> UpdateConsultorio(int id, UpdateConsultorioDTO request)
        {
            var entidad = _unitOfWork.Consultorios.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("El Consultorio no existe.", 404);

            var error = Validar(request.Numero);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            entidad.Numero = request.Numero;
            entidad.Piso = request.Piso;

            _unitOfWork.Consultorios.Update(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteConsultorio(int id)
        {
            var entidad = _unitOfWork.Consultorios.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("El Consultorio no existe.", 404);

            _unitOfWork.Consultorios.Delete(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private static string? Validar(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                return "El numero del consultorio es obligatorio.";

            return null;
        }

        private static ConsultorioDTO Mapear(Consultorio e) => new ConsultorioDTO
        {
            Id = e.Id,
            Numero = e.Numero,
            Piso = e.Piso
        };
    }
}
