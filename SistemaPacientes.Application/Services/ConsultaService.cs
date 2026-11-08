using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<ConsultaDTO>> GetAllConsultas()
        {
            var consultas = _unitOfWork.Consultas.GetAllConsultasWithDetalle().Select(Mapear);
            return ApiResponse<IEnumerable<ConsultaDTO>>.SuccessResponse(consultas);
        }

        public ApiResponse<ConsultaDTO> GetConsultaById(int id)
        {
            var consulta = _unitOfWork.Consultas.GetById(id);
            if (consulta == null)
                return ApiResponse<ConsultaDTO>.ErrorResponse("La consulta no existe.", 404);

            return ApiResponse<ConsultaDTO>.SuccessResponse(Mapear(consulta));
        }

        public ApiResponse<ConsultaDTO> CreateConsulta(CreateConsultaDTO request)
        {
            var cita = _unitOfWork.Citas.GetById(request.CitaId);
            if (cita == null)
                return ApiResponse<ConsultaDTO>.ErrorResponse("La cita indicada no existe.");

            if (cita.Estado == "Cancelada")
                return ApiResponse<ConsultaDTO>.ErrorResponse("No se puede registrar una consulta de una cita cancelada.");

            if (_unitOfWork.Consultas.CitaYaTieneConsulta(request.CitaId))
                return ApiResponse<ConsultaDTO>.ErrorResponse("Esa cita ya tiene una consulta registrada.");

            var nueva = new Consulta
            {
                CitaId = request.CitaId,
                Fecha = request.Fecha == default ? DateTime.Now : request.Fecha,
                Sintomas = request.Sintomas,
                Observaciones = request.Observaciones
            };

            cita.Estado = "Atendida";

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.Consultas.Create(nueva);
                _unitOfWork.Citas.Update(cita);
                _unitOfWork.Complete();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                return ApiResponse<ConsultaDTO>.ErrorResponse("No se pudo registrar la consulta.", 500);
            }

            return ApiResponse<ConsultaDTO>.SuccessResponse(Mapear(nueva), "La consulta fue registrada.", 201);
        }

        public ApiResponse<bool> UpdateConsulta(int id, UpdateConsultaDTO request)
        {
            var consulta = _unitOfWork.Consultas.GetById(id);
            if (consulta == null)
                return ApiResponse<bool>.ErrorResponse("La consulta no existe.", 404);

            consulta.Sintomas = request.Sintomas;
            consulta.Observaciones = request.Observaciones;
            if (request.Fecha != default)
            {
                consulta.Fecha = request.Fecha;
            }

            _unitOfWork.Consultas.Update(consulta);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteConsulta(int id)
        {
            var consulta = _unitOfWork.Consultas.GetById(id);
            if (consulta == null)
                return ApiResponse<bool>.ErrorResponse("La consulta no existe.", 404);

            if (_unitOfWork.Diagnosticos.GetAll().Any(d => d.ConsultaId == id))
                return ApiResponse<bool>.ErrorResponse("No se puede eliminar la consulta porque tiene diagnosticos.");

            if (_unitOfWork.Recetas.GetAll().Any(r => r.ConsultaId == id))
                return ApiResponse<bool>.ErrorResponse("No se puede eliminar la consulta porque tiene recetas.");

            _unitOfWork.Consultas.Delete(consulta);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private static ConsultaDTO Mapear(Consulta c) => new ConsultaDTO
        {
            Id = c.Id,
            CitaId = c.CitaId,
            Fecha = c.Fecha,
            Sintomas = c.Sintomas,
            Observaciones = c.Observaciones
        };
    }
}
