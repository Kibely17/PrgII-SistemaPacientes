import { useEffect, useState } from "react";
import Mantenimiento from "../componentes/Mantenimiento";
import { api } from "../servicios/api";

export default function Medicos() {
  const [especialidades, setEspecialidades] = useState([]);

  useEffect(() => {
    api
      .listar("especialidades")
      .then((datos) => setEspecialidades(datos.map((e) => ({ id: e.id, texto: e.nombre }))))
      .catch(() => setEspecialidades([]));
  }, []);

  return (
    <Mantenimiento
      titulo="Medicos"
      recurso="medicos"
      campos={[
        { nombre: "nombre", etiqueta: "Nombre", obligatorio: true },
        { nombre: "apellido", etiqueta: "Apellido", obligatorio: true },
        { nombre: "exequatur", etiqueta: "Exequatur", obligatorio: true },
        { nombre: "telefono", etiqueta: "Telefono" },
        { nombre: "especialidadId", etiqueta: "Especialidad", tipo: "number", obligatorio: true },
      ]}
      catalogos={{ especialidadId: especialidades }}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "nombre", etiqueta: "Nombre", formato: (f) => `${f.nombre} ${f.apellido}` },
        { campo: "exequatur", etiqueta: "Exequatur" },
        { campo: "especialidadNombre", etiqueta: "Especialidad" },
        { campo: "telefono", etiqueta: "Telefono" },
      ]}
    />
  );
}
