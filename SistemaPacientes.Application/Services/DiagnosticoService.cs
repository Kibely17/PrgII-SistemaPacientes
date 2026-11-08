using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class DiagnosticoService : IDiagnosticoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiagnosticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<DiagnosticoDTO>> GetAllDiagnosticos()
        {
            var diagnosticos = _unitOfWork.Diagnosticos.GetAll().Select(Mapear);
            return ApiResponse<IEnumerable<DiagnosticoDTO>>.SuccessResponse(diagnosticos);
        }

        public ApiResponse<DiagnosticoDTO> GetDiagnosticoById(int id)
        {
            var diagnostico = _unitOfWork.Diagnosticos.GetById(id);
            if (diagnostico == null)
                return ApiResponse<DiagnosticoDTO>.ErrorResponse("El diagnostico no existe.", 404);

            return ApiResponse<DiagnosticoDTO>.SuccessResponse(Mapear(diagnostico));
        }

        public ApiResponse<DiagnosticoDTO> CreateDiagnostico(CreateDiagnosticoDTO request)
        {
            var error = Validar(request.ConsultaId, request.Descripcion);
            if (error != null)
                return ApiResponse<DiagnosticoDTO>.ErrorResponse(error);

            var nuevo = new Diagnostico
            {
                ConsultaId = request.ConsultaId,
                Descripcion = request.Descripcion,
                Codigo = request.Codigo
            };

            _unitOfWork.Diagnosticos.Create(nuevo);
            _unitOfWork.Complete();

            return ApiResponse<DiagnosticoDTO>.SuccessResponse(Mapear(nuevo), "El diagnostico fue registrado.", 201);
        }

        public ApiResponse<bool> UpdateDiagnostico(int id, UpdateDiagnosticoDTO request)
        {
            var diagnostico = _unitOfWork.Diagnosticos.GetById(id);
            if (diagnostico == null)
                return ApiResponse<bool>.ErrorResponse("El diagnostico no existe.", 404);

            var error = Validar(request.ConsultaId, request.Descripcion);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            diagnostico.ConsultaId = request.ConsultaId;
            diagnostico.Descripcion = request.Descripcion;
            diagnostico.Codigo = request.Codigo;

            _unitOfWork.Diagnosticos.Update(diagnostico);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteDiagnostico(int id)
        {
            var diagnostico = _unitOfWork.Diagnosticos.GetById(id);
            if (diagnostico == null)
                return ApiResponse<bool>.ErrorResponse("El diagnostico no existe.", 404);

            _unitOfWork.Diagnosticos.Delete(diagnostico);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private string? Validar(int consultaId, string descripcion)
        {
            if (_unitOfWork.Consultas.GetById(consultaId) == null) return "La consulta indicada no existe.";
            if (string.IsNullOrWhiteSpace(descripcion)) return "La descripcion del diagnostico es obligatoria.";
            return null;
        }

        private static DiagnosticoDTO Mapear(Diagnostico d) => new DiagnosticoDTO
        {
            Id = d.Id,
            ConsultaId = d.ConsultaId,
            Descripcion = d.Descripcion,
            Codigo = d.Codigo
        };
    }
}
