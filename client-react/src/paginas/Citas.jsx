import { useEffect, useState } from "react";
import Mantenimiento from "../componentes/Mantenimiento";
import { api } from "../servicios/api";

export default function Citas() {
  const [pacientes, setPacientes] = useState([]);
  const [medicos, setMedicos] = useState([]);
  const [consultorios, setConsultorios] = useState([]);

  useEffect(() => {
    api.listar("pacientes").then((d) =>
      setPacientes(d.map((p) => ({ id: p.id, texto: `${p.nombreCompleto} (${p.cedula})` })))
    );
    api.listar("medicos").then((d) =>
      setMedicos(d.map((m) => ({ id: m.id, texto: `${m.nombre} ${m.apellido}` })))
    );
    api.listar("consultorios").then((d) =>
      setConsultorios(d.map((c) => ({ id: c.id, texto: `Consultorio ${c.numero}` })))
    );
  }, []);

  return (
    <Mantenimiento
      titulo="Citas"
      recurso="citas"
      campos={[
        { nombre: "pacienteId", etiqueta: "Paciente", tipo: "number", obligatorio: true },
        { nombre: "medicoId", etiqueta: "Medico", tipo: "number", obligatorio: true },
        { nombre: "consultorioId", etiqueta: "Consultorio", tipo: "number", obligatorio: true },
        { nombre: "fecha", etiqueta: "Fecha y hora", tipo: "datetime-local", obligatorio: true },
        { nombre: "estado", etiqueta: "Estado" },
        { nombre: "motivo", etiqueta: "Motivo" },
      ]}
      catalogos={{ pacienteId: pacientes, medicoId: medicos, consultorioId: consultorios }}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "pacienteNombre", etiqueta: "Paciente" },
        { campo: "medicoNombre", etiqueta: "Medico" },
        { campo: "consultorioNumero", etiqueta: "Consultorio" },
        {
          campo: "fecha",
          etiqueta: "Fecha",
          formato: (f) => new Date(f.fecha).toLocaleString("es-DO"),
        },
        { campo: "estado", etiqueta: "Estado" },
      ]}
    />
  );
}
