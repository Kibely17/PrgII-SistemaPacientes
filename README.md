# Sistema de Pacientes

Tarea complementaria de Programacion II
Kibely Fulgencio

API en ASP.NET Core 8 con Entity Framework y SQL Server, y un frontend en React.
Maneja pacientes, medicos, citas, consultas, diagnosticos, recetas,
especialidades, seguros, consultorios y medicamentos.

Poner la clave de SQL Server en appsettings.Development.json donde dice TU_PASSWORD.

Crear la base:

    dotnet ef database update --project SistemaPacientes.Infrastructure --startup-project SistemaPacientes.API

Correr el API:

    dotnet run --project SistemaPacientes.API

Correr el frontend:

    cd client-react
    npm install
    npm run dev
