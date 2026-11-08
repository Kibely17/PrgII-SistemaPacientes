import Mantenimiento from "../componentes/Mantenimiento";

export default function Medicamentos() {
  return (
    <Mantenimiento
      titulo="Medicamentos"
      recurso="medicamentos"
      campos={[
        { nombre: "nombre", etiqueta: "Nombre", obligatorio: true },
        { nombre: "presentacion", etiqueta: "Presentacion" },
      ]}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "nombre", etiqueta: "Nombre" },
        { campo: "presentacion", etiqueta: "Presentacion" },
      ]}
    />
  );
}
