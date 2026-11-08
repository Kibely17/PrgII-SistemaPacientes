# Sistema de Pacientes

Tarea complementaria de Programacion II.
Kibely Altagracia Fulgencio Valdez

Sistema para un centro medico. Se registran pacientes y medicos, se agendan citas
y de cada cita atendida queda una consulta con sus diagnosticos y recetas.

El API esta hecho en ASP.NET Core 8 con Entity Framework y SQL Server.
El frontend esta en React.

Las tablas son Especialidades, Medicos, Seguros, Pacientes, Consultorios, Citas,
Consultas, Diagnosticos, Medicamentos y Recetas.

## Proyectos

- SistemaPacientes.Domain: las entidades
- SistemaPacientes.Infrastructure: el contexto, las migraciones y los repositorios
- SistemaPacientes.Application: los servicios y los DTOs
- SistemaPacientes.API: los controladores
- client-react: el frontend

Los repositorios y los servicios se usan por interfaz.

## Para correrlo

Poner la clave de SQL Server en appsettings.Development.json donde dice TU_PASSWORD.

Crear la base:

    dotnet ef database update --project SistemaPacientes.Infrastructure --startup-project SistemaPacientes.API

Levantar el API:

    dotnet run --project SistemaPacientes.API

Levantar el frontend:

    cd client-react
    npm install
    npm run dev

El API queda en el puerto 5210 y el frontend en el 5173.

## Endpoints

Hay GET, POST, PUT y DELETE para cada recurso.

    /api/pacientes
    /api/medicos
    /api/citas
    /api/consultas
    /api/diagnosticos
    /api/recetas
    /api/especialidades
    /api/seguros
    /api/consultorios
    /api/medicamentos
