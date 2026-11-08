# Sistema de Gestion de Pacientes

Tarea complementaria de Programacion II.

**Estudiante:** Kibely Altagracia Fulgencio Valdez

## Descripcion

Sistema para administrar la consulta de un centro medico: se registran los
pacientes y los medicos, se agendan las citas, y de cada cita atendida queda una
consulta con sus diagnosticos y sus recetas.

El backend es un API REST en ASP.NET Core separado en capas, y el frontend es
una aplicacion de React que lo consume.

## Tablas

| Tabla | Descripcion |
|-------|-------------|
| Especialidades | Las especialidades medicas del centro. |
| Medicos | Cada medico pertenece a una especialidad. |
| Seguros | Las ARS con las que trabaja el centro. |
| Pacientes | Los pacientes. Pueden tener un seguro o ser privados. |
| Consultorios | Los consultorios disponibles. |
| Citas | Reserva de un paciente con un medico en un consultorio. |
| Consultas | Lo que ocurrio en una cita atendida. |
| Diagnosticos | Los diagnosticos de una consulta. |
| Medicamentos | Los medicamentos que se pueden recetar. |
| Recetas | El medicamento recetado en una consulta, con su dosis. |

## Tecnologias

- ASP.NET Core 8 Web API
- Entity Framework Core 8 con migraciones
- SQL Server 2022
- React 19 con Vite
- Swagger / OpenAPI

## Arquitectura

```
SistemaPacientes.Domain/              Capa de dominio
├── Core/BaseEntity.cs
└── Entities/                         Las 10 entidades

SistemaPacientes.Infrastructure/      Capa de acceso a datos
├── Context/ApplicationDbContext.cs
├── Contracts/                        Interfaces de los repositorios
├── Migrations/
└── Repositories/                     Implementaciones y unidad de trabajo

SistemaPacientes.Application/         Capa de servicios
├── Core/ApiResponse.cs
├── Contracts/                        Interfaces de los servicios
├── Dtos/
└── Services/                         Un servicio por recurso

SistemaPacientes.API/                 Capa de presentacion
└── Controllers/                      Un controlador por recurso

client-react/                         Frontend que consume el API
```

Cada proyecto referencia solo al siguiente: API, Application, Infrastructure y
Domain en ese orden.

### Sobre las interfaces

Tanto los repositorios como los servicios se registran por su interfaz. El
controlador recibe `IPacienteService` y no sabe cual es la implementacion, y el
servicio recibe `IUnitOfWork` y no sabe como se guardan los datos. Eso permite
cambiar una pieza sin tocar las demas.

### Sobre la unidad de trabajo

Los repositorios agregan, actualizan y eliminan, pero no guardan. Quien guarda es
`UnitOfWork.Complete()`. Asi se pueden encadenar varias operaciones y deshacerlas
todas si una falla: al registrar una consulta se crea la consulta y ademas se
marca la cita como atendida, y las dos cosas entran juntas o no entra ninguna.

## Reglas de negocio

- La cedula del paciente no se puede repetir.
- La fecha de nacimiento no puede ser futura ni mayor a 120 anios.
- El sexo del paciente debe ser M o F.
- El exequatur del medico no se puede repetir.
- Un medico no puede tener dos citas a la misma hora.
- Un consultorio no puede estar ocupado por dos citas a la misma hora.
- No se puede registrar una consulta de una cita cancelada.
- Una cita no puede tener mas de una consulta.
- Al registrar la consulta, la cita pasa a estado Atendida.
- No se puede eliminar un paciente o un medico que tenga citas.
- No se puede eliminar una consulta que tenga diagnosticos o recetas.

## Como ejecutar

### 1. Base de datos

Levantar SQL Server y poner la clave en
`SistemaPacientes.API/appsettings.Development.json`, donde dice `TU_PASSWORD`.
Luego crear la base con la migracion:

```bash
dotnet ef database update --project SistemaPacientes.Infrastructure --startup-project SistemaPacientes.API
```

### 2. API

```bash
dotnet run --project SistemaPacientes.API
```

Queda en `http://localhost:5210` y Swagger en `/swagger`.

### 3. Frontend

```bash
cd client-react
npm install
npm run dev
```

Queda en `http://localhost:5173`. La direccion del API se configura en el
archivo `.env` del cliente.

El API habilita CORS unicamente para `http://localhost:5173`, que es el puerto
donde Vite levanta el cliente.

## Endpoints

Los diez recursos exponen el mismo CRUD:

| Metodo | Ruta | Respuesta |
|--------|------|-----------|
| GET | /api/{recurso} | 200 lista |
| GET | /api/{recurso}/{id} | 200 o 404 |
| POST | /api/{recurso} | 201 + Location |
| PUT | /api/{recurso}/{id} | 204, 400 o 404 |
| DELETE | /api/{recurso}/{id} | 204, 400 o 404 |

Recursos: `pacientes`, `medicos`, `citas`, `consultas`, `diagnosticos`,
`recetas`, `especialidades`, `seguros`, `consultorios`, `medicamentos`.
