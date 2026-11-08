using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecetaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<RecetaDTO>> GetAllRecetas()
        {
            var recetas = _unitOfWork.Recetas.GetAll().Select(Mapear);
            return ApiResponse<IEnumerable<RecetaDTO>>.SuccessResponse(recetas);
        }

        public ApiResponse<RecetaDTO> GetRecetaById(int id)
        {
            var receta = _unitOfWork.Recetas.GetById(id);
            if (receta == null)
                return ApiResponse<RecetaDTO>.ErrorResponse("La receta no existe.", 404);

            return ApiResponse<RecetaDTO>.SuccessResponse(Mapear(receta));
        }

        public ApiResponse<RecetaDTO> CreateReceta(CreateRecetaDTO request)
        {
            var error = Validar(request.ConsultaId, request.MedicamentoId, request.Dosis, request.DiasTratamiento);
            if (error != null)
                return ApiResponse<RecetaDTO>.ErrorResponse(error);

            var nueva = new Receta
            {
                ConsultaId = request.ConsultaId,
                MedicamentoId = request.MedicamentoId,
                Dosis = request.Dosis,
                Indicaciones = request.Indicaciones,
                DiasTratamiento = request.DiasTratamiento
            };

            _unitOfWork.Recetas.Create(nueva);
            _unitOfWork.Complete();

            return ApiResponse<RecetaDTO>.SuccessResponse(Mapear(nueva), "La receta fue registrada.", 201);
        }

        public ApiResponse<bool> UpdateReceta(int id, UpdateRecetaDTO request)
        {
            var receta = _unitOfWork.Recetas.GetById(id);
            if (receta == null)
                return ApiResponse<bool>.ErrorResponse("La receta no existe.", 404);

            var error = Validar(request.ConsultaId, request.MedicamentoId, request.Dosis, request.DiasTratamiento);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            receta.ConsultaId = request.ConsultaId;
            receta.MedicamentoId = request.MedicamentoId;
            receta.Dosis = request.Dosis;
            receta.Indicaciones = request.Indicaciones;
            receta.DiasTratamiento = request.DiasTratamiento;

            _unitOfWork.Recetas.Update(receta);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteReceta(int id)
        {
            var receta = _unitOfWork.Recetas.GetById(id);
            if (receta == null)
                return ApiResponse<bool>.ErrorResponse("La receta no existe.", 404);

            _unitOfWork.Recetas.Delete(receta);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private string? Validar(int consultaId, int medicamentoId, string dosis, int diasTratamiento)
        {
            if (_unitOfWork.Consultas.GetById(consultaId) == null) return "La consulta indicada no existe.";
            if (_unitOfWork.Medicamentos.GetById(medicamentoId) == null) return "El medicamento indicado no existe.";
            if (string.IsNullOrWhiteSpace(dosis)) return "La dosis es obligatoria.";
            if (diasTratamiento <= 0) return "Los dias de tratamiento deben ser mayor que cero.";
            return null;
        }

        private static RecetaDTO Mapear(Receta r) => new RecetaDTO
        {
            Id = r.Id,
            ConsultaId = r.ConsultaId,
            MedicamentoId = r.MedicamentoId,
            Dosis = r.Dosis,
            Indicaciones = r.Indicaciones,
            DiasTratamiento = r.DiasTratamiento
        };
    }
}
