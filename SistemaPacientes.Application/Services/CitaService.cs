using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Core;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Contracts;

namespace SistemaPacientes.Application.Services
{
    public class CitaService : ICitaService
    {
        private static readonly string[] EstadosValidos = { "Pendiente", "Atendida", "Cancelada" };

        private readonly IUnitOfWork _unitOfWork;

        public CitaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<CitaDTO>> GetAllCitas()
        {
            var citas = _unitOfWork.Citas.GetAllCitasWithDetalle().Select(Mapear);
            return ApiResponse<IEnumerable<CitaDTO>>.SuccessResponse(citas);
        }

        public ApiResponse<CitaDTO> GetCitaById(int id)
        {
            var cita = _unitOfWork.Citas.GetCitaWithDetalle(id);
            if (cita == null)
                return ApiResponse<CitaDTO>.ErrorResponse("La cita no existe.", 404);

            return ApiResponse<CitaDTO>.SuccessResponse(Mapear(cita));
        }

        public ApiResponse<CitaDTO> CreateCita(CreateCitaDTO request)
        {
            var error = Validar(request.PacienteId, request.MedicoId, request.ConsultorioId,
                request.Fecha, request.Estado, 0);
            if (error != null)
                return ApiResponse<CitaDTO>.ErrorResponse(error);

            var nueva = new Cita
            {
                PacienteId = request.PacienteId,
                MedicoId = request.MedicoId,
                ConsultorioId = request.ConsultorioId,
                Fecha = request.Fecha,
                Estado = string.IsNullOrWhiteSpace(request.Estado) ? "Pendiente" : request.Estado,
                Motivo = request.Motivo
            };

            _unitOfWork.Citas.Create(nueva);
            _unitOfWork.Complete();

            var creada = _unitOfWork.Citas.GetCitaWithDetalle(nueva.Id) ?? nueva;
            return ApiResponse<CitaDTO>.SuccessResponse(Mapear(creada), "La cita fue agendada.", 201);
        }

        public ApiResponse<bool> UpdateCita(int id, UpdateCitaDTO request)
        {
            var cita = _unitOfWork.Citas.GetById(id);
            if (cita == null)
                return ApiResponse<bool>.ErrorResponse("La cita no existe.", 404);

            var error = Validar(request.PacienteId, request.MedicoId, request.ConsultorioId,
                request.Fecha, request.Estado, id);
            if (error != null)
                return ApiResponse<bool>.ErrorResponse(error);

            cita.PacienteId = request.PacienteId;
            cita.MedicoId = request.MedicoId;
            cita.ConsultorioId = request.ConsultorioId;
            cita.Fecha = request.Fecha;
            cita.Estado = request.Estado;
            cita.Motivo = request.Motivo;

            _unitOfWork.Citas.Update(cita);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public ApiResponse<bool> DeleteCita(int id)
        {
            var cita = _unitOfWork.Citas.GetById(id);
            if (cita == null)
                return ApiResponse<bool>.ErrorResponse("La cita no existe.", 404);

            if (_unitOfWork.Consultas.CitaYaTieneConsulta(id))
                return ApiResponse<bool>.ErrorResponse("No se puede eliminar la cita porque ya tiene una consulta registrada.");

            _unitOfWork.Citas.Delete(cita);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        private string? Validar(int pacienteId, int medicoId, int consultorioId,
            DateTime fecha, string estado, int citaIdExcluida)
        {
            if (_unitOfWork.Pacientes.GetById(pacienteId) == null) return "El paciente indicado no existe.";
            if (_unitOfWork.Medicos.GetById(medicoId) == null) return "El medico indicado no existe.";
            if (_unitOfWork.Consultorios.GetById(consultorioId) == null) return "El consultorio indicado no existe.";
            if (fecha == default) return "La fecha de la cita es obligatoria.";

            if (!string.IsNullOrWhiteSpace(estado) && !EstadosValidos.Contains(estado))
                return "El estado debe ser Pendiente, Atendida o Cancelada.";

            if (_unitOfWork.Citas.MedicoOcupado(medicoId, fecha, citaIdExcluida))
                return "El medico ya tiene una cita a esa hora.";

            if (_unitOfWork.Citas.ConsultorioOcupado(consultorioId, fecha, citaIdExcluida))
                return "El consultorio ya esta ocupado a esa hora.";

            return null;
        }

        private static CitaDTO Mapear(Cita c) => new CitaDTO
        {
            Id = c.Id,
            PacienteId = c.PacienteId,
            MedicoId = c.MedicoId,
            ConsultorioId = c.ConsultorioId,
            Fecha = c.Fecha,
            Estado = c.Estado,
            Motivo = c.Motivo,
            PacienteNombre = c.Paciente == null ? null : c.Paciente.NombreCompleto(),
            MedicoNombre = c.Medico == null ? null : c.Medico.Nombre + " " + c.Medico.Apellido,
            ConsultorioNumero = c.Consultorio?.Numero
        };
    }
}
