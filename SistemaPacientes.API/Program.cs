using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Services;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Infrastructure.Context;
using SistemaPacientes.Infrastructure.Contracts;
using SistemaPacientes.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IPacienteRepository, PacienteRepository>();
builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();
builder.Services.AddTransient<ICitaRepository, CitaRepository>();
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
builder.Services.AddTransient<IGenericRepository<Especialidad>, GenericRepository<Especialidad>>();
builder.Services.AddTransient<IGenericRepository<Seguro>, GenericRepository<Seguro>>();
builder.Services.AddTransient<IGenericRepository<Consultorio>, GenericRepository<Consultorio>>();
builder.Services.AddTransient<IGenericRepository<Medicamento>, GenericRepository<Medicamento>>();
builder.Services.AddTransient<IGenericRepository<Diagnostico>, GenericRepository<Diagnostico>>();
builder.Services.AddTransient<IGenericRepository<Receta>, GenericRepository<Receta>>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IEspecialidadService, EspecialidadService>();
builder.Services.AddTransient<ISeguroService, SeguroService>();
builder.Services.AddTransient<IConsultorioService, ConsultorioService>();
builder.Services.AddTransient<IMedicamentoService, MedicamentoService>();
builder.Services.AddTransient<IMedicoService, MedicoService>();
builder.Services.AddTransient<IPacienteService, PacienteService>();
builder.Services.AddTransient<ICitaService, CitaService>();
builder.Services.AddTransient<IConsultaService, ConsultaService>();
builder.Services.AddTransient<IDiagnosticoService, DiagnosticoService>();
builder.Services.AddTransient<IRecetaService, RecetaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClienteReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ClienteReact");
app.UseAuthorization();
app.MapControllers();

app.Run();
