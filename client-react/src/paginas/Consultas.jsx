import { useEffect, useState } from "react";
import Mantenimiento from "../componentes/Mantenimiento";
import { api } from "../servicios/api";

export default function Consultas() {
  const [citas, setCitas] = useState([]);

  useEffect(() => {
    api.listar("citas").then((d) =>
      setCitas(
        d.map((c) => ({
          id: c.id,
          texto: `${c.pacienteNombre} - ${new Date(c.fecha).toLocaleString("es-DO")}`,
        }))
      )
    );
  }, []);

  return (
    <Mantenimiento
      titulo="Consultas"
      recurso="consultas"
      campos={[
        { nombre: "citaId", etiqueta: "Cita", tipo: "number", obligatorio: true },
        { nombre: "fecha", etiqueta: "Fecha", tipo: "datetime-local" },
        { nombre: "sintomas", etiqueta: "Sintomas" },
        { nombre: "observaciones", etiqueta: "Observaciones" },
      ]}
      catalogos={{ citaId: citas }}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "citaId", etiqueta: "Cita" },
        {
          campo: "fecha",
          etiqueta: "Fecha",
          formato: (f) => new Date(f.fecha).toLocaleString("es-DO"),
        },
        { campo: "sintomas", etiqueta: "Sintomas" },
        { campo: "observaciones", etiqueta: "Observaciones" },
      ]}
    />
  );
}
