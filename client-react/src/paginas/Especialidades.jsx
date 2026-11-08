import Mantenimiento from "../componentes/Mantenimiento";

export default function Especialidades() {
  return (
    <Mantenimiento
      titulo="Especialidades"
      recurso="especialidades"
      campos={[
        { nombre: "nombre", etiqueta: "Nombre", obligatorio: true },
        { nombre: "descripcion", etiqueta: "Descripcion" },
      ]}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "nombre", etiqueta: "Nombre" },
        { campo: "descripcion", etiqueta: "Descripcion" },
      ]}
    />
  );
}
