import { useEffect, useState } from "react";
import Mantenimiento from "../componentes/Mantenimiento";
import { api } from "../servicios/api";

export default function Recetas() {
  const [consultas, setConsultas] = useState([]);
  const [medicamentos, setMedicamentos] = useState([]);

  useEffect(() => {
    api.listar("consultas").then((d) =>
      setConsultas(d.map((c) => ({ id: c.id, texto: `Consulta ${c.id}` })))
    );
    api.listar("medicamentos").then((d) =>
      setMedicamentos(d.map((m) => ({ id: m.id, texto: m.nombre })))
    );
  }, []);

  return (
    <Mantenimiento
      titulo="Recetas"
      recurso="recetas"
      campos={[
        { nombre: "consultaId", etiqueta: "Consulta", tipo: "number", obligatorio: true },
        { nombre: "medicamentoId", etiqueta: "Medicamento", tipo: "number", obligatorio: true },
        { nombre: "dosis", etiqueta: "Dosis", obligatorio: true },
        { nombre: "indicaciones", etiqueta: "Indicaciones" },
        { nombre: "diasTratamiento", etiqueta: "Dias de tratamiento", tipo: "number", obligatorio: true },
      ]}
      catalogos={{ consultaId: consultas, medicamentoId: medicamentos }}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "consultaId", etiqueta: "Consulta" },
        { campo: "medicamentoId", etiqueta: "Medicamento" },
        { campo: "dosis", etiqueta: "Dosis" },
        { campo: "diasTratamiento", etiqueta: "Dias" },
      ]}
    />
  );
}
