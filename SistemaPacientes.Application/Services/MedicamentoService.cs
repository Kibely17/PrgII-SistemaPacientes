using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicamentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<MedicamentoDTO>> GetAllMedicamentos()
        {
            var lista = _unitOfWork.Medicamentos.GetAll().Select(Mapear);
            return ApiResponse<IEnumerable<MedicamentoDTO>>.SuccessResponse(lista);
        }

        public ApiResponse<MedicamentoDTO> GetMedicamentoById(int id)
        {
            var entidad = _unitOfWork.Medicamentos.GetById(id);
            if (entidad == null)
                return ApiResponse<MedicamentoDTO>.ErrorResponse("El Medicamento no existe.", 404);

            return ApiResponse<MedicamentoDTO>.SuccessResponse(Mapear(entidad));
        }

        public ApiResponse<MedicamentoDTO> CreateMedicamento(CreateMedicamentoDTO request)
        {
            var error = Validar(request.Nombre);
            if (error != null)
                return ApiResponse<MedicamentoDTO>.ErrorResponse(error);

            var nuevo = new Medicamento
            {
                Nombre = request.Nombre,
                Presentacion = request.Presentacion
            };

            _unitOfWork.Medicamentos.Create(nuevo);
            _unitOfWork.Complete();

            return ApiResponse<MedicamentoDTO>.SuccessResponse(Mapear(nuevo), "El Medicamento fue creado.", 201);
        }

        public ApiResponse<bool> UpdateMedicamento(int id, UpdateMedicamentoDTO request)
        {
            var entidad = _unitOfWork.Medicamentos.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("El Medicamento no existe.", 404);

            var error = Validar(request.Nombre);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            entidad.Nombre = request.Nombre;
            entidad.Presentacion = request.Presentacion;

            _unitOfWork.Medicamentos.Update(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteMedicamento(int id)
        {
            var entidad = _unitOfWork.Medicamentos.GetById(id);
            if (entidad == null)
                return ApiResponse<bool>.ErrorResponse("El Medicamento no existe.", 404);

            _unitOfWork.Medicamentos.Delete(entidad);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private static string? Validar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre del medicamento es obligatorio.";

            return null;
        }

        private static MedicamentoDTO Mapear(Medicamento e) => new MedicamentoDTO
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Presentacion = e.Presentacion
        };
    }
}
