import { useEffect, useState } from "react";
import Mantenimiento from "../componentes/Mantenimiento";
import { api } from "../servicios/api";

export default function Diagnosticos() {
  const [consultas, setConsultas] = useState([]);

  useEffect(() => {
    api.listar("consultas").then((d) =>
      setConsultas(d.map((c) => ({ id: c.id, texto: `Consulta ${c.id}` })))
    );
  }, []);

  return (
    <Mantenimiento
      titulo="Diagnosticos"
      recurso="diagnosticos"
      campos={[
        { nombre: "consultaId", etiqueta: "Consulta", tipo: "number", obligatorio: true },
        { nombre: "descripcion", etiqueta: "Descripcion", obligatorio: true },
        { nombre: "codigo", etiqueta: "Codigo" },
      ]}
      catalogos={{ consultaId: consultas }}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "consultaId", etiqueta: "Consulta" },
        { campo: "descripcion", etiqueta: "Descripcion" },
        { campo: "codigo", etiqueta: "Codigo" },
      ]}
    />
  );
}
