import { useState } from "react";
import Especialidades from "./paginas/Especialidades";
import Seguros from "./paginas/Seguros";
import Consultorios from "./paginas/Consultorios";
import Medicamentos from "./paginas/Medicamentos";
import Medicos from "./paginas/Medicos";
import Pacientes from "./paginas/Pacientes";
import Citas from "./paginas/Citas";
import Consultas from "./paginas/Consultas";
import Diagnosticos from "./paginas/Diagnosticos";
import Recetas from "./paginas/Recetas";

const secciones = [
  { clave: "pacientes", texto: "Pacientes", componente: Pacientes },
  { clave: "medicos", texto: "Medicos", componente: Medicos },
  { clave: "citas", texto: "Citas", componente: Citas },
  { clave: "consultas", texto: "Consultas", componente: Consultas },
  { clave: "diagnosticos", texto: "Diagnosticos", componente: Diagnosticos },
  { clave: "recetas", texto: "Recetas", componente: Recetas },
  { clave: "especialidades", texto: "Especialidades", componente: Especialidades },
  { clave: "seguros", texto: "Seguros", componente: Seguros },
  { clave: "consultorios", texto: "Consultorios", componente: Consultorios },
  { clave: "medicamentos", texto: "Medicamentos", componente: Medicamentos },
];

export default function App() {
  const [activa, setActiva] = useState("pacientes");
  const Seccion = secciones.find((s) => s.clave === activa).componente;

  return (
    <div className="aplicacion">
      <header>
        <h1>Sistema de Gestion de Pacientes</h1>
      </header>

      <nav>
        {secciones.map((s) => (
          <button
            key={s.clave}
            onClick={() => setActiva(s.clave)}
            className={activa === s.clave ? "activo" : ""}
          >
            {s.texto}
          </button>
        ))}
      </nav>

      <main>
        <Seccion />
      </main>
    </div>
  );
}
