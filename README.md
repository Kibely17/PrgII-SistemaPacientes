# Sistema de Gestion de Pacientes

Tarea complementaria de Programacion II.

**Estudiante:** Kibely Altagracia Fulgencio Valdez

## Descripcion

Sistema para la consulta de un centro medico. Se registran pacientes y medicos,
se agendan citas, y de cada cita atendida queda una consulta con sus
diagnosticos y recetas.

El backend es un API en ASP.NET Core dividido en capas y el frontend es una
aplicacion de React que lo consume.

## Tablas

Especialidades, Medicos, Seguros, Pacientes, Consultorios, Citas, Consultas,
Diagnosticos, Medicamentos y Recetas.

## Tecnologias

- ASP.NET Core 8 Web API
- Entity Framework Core 8 (Code First)
- SQL Server 2022
- React 19 con Vite
- Swagger

## Estructura

```
SistemaPacientes.Domain/
  Core/BaseEntity.cs
  Entities/            Las 10 entidades

SistemaPacientes.Infrastructure/
  Context/             ApplicationDbContext
  Contracts/           Interfaces de los repositorios
  Migrations/
  Repositories/        Implementaciones y UnitOfWork

SistemaPacientes.Application/
  Core/ApiResponse.cs
  Contracts/           Interfaces de los servicios
  Dtos/
  Services/            Un servicio por recurso

SistemaPacientes.API/
  Controllers/         Un controlador por recurso

client-react/          Frontend
```

Cada proyecto referencia solo al siguiente: API, Application, Infrastructure,
Domain.

Los repositorios y los servicios se registran por su interfaz, asi que el
controlador recibe `IPacienteService` y el servicio recibe `IUnitOfWork` sin
saber cual es la implementacion.

La unidad de trabajo es la unica que guarda. Eso permite encadenar operaciones:
al registrar una consulta se crea la consulta y se marca la cita como atendida,
y las dos cosas entran juntas o no entra ninguna.

## Como ejecutar

1. En `SistemaPacientes.API/appsettings.Development.json` sustituir
   `TU_PASSWORD` por la clave de SQL Server.

2. Crear la base de datos:

```bash
dotnet ef database update --project SistemaPacientes.Infrastructure --startup-project SistemaPacientes.API
```

3. Levantar el API:

```bash
dotnet run --project SistemaPacientes.API
```

Queda en `http://localhost:5210` y Swagger en `/swagger`.

4. Levantar el cliente:

```bash
cd client-react
npm install
npm run dev
```

Queda en `http://localhost:5173`. La direccion del API se configura en el
archivo `.env`. El API solo acepta peticiones desde ese puerto.

## Endpoints

Los diez recursos tienen el mismo CRUD:

```
GET    /api/{recurso}
GET    /api/{recurso}/{id}
POST   /api/{recurso}
PUT    /api/{recurso}/{id}
DELETE /api/{recurso}/{id}
```

Recursos: `pacientes`, `medicos`, `citas`, `consultas`, `diagnosticos`,
`recetas`, `especialidades`, `seguros`, `consultorios`, `medicamentos`.

## Validaciones

- La cedula del paciente y el exequatur del medico no se repiten.
- La fecha de nacimiento no puede ser futura ni mayor a 120 anios.
- El sexo del paciente debe ser M o F.
- Un medico no puede tener dos citas a la misma hora, ni un consultorio estar
  ocupado dos veces.
- No se puede registrar una consulta de una cita cancelada, ni mas de una
  consulta por cita.
- No se puede eliminar un paciente o un medico que tenga citas, ni una consulta
  que tenga diagnosticos o recetas.
