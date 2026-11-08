import Mantenimiento from "../componentes/Mantenimiento";

export default function Consultorios() {
  return (
    <Mantenimiento
      titulo="Consultorios"
      recurso="consultorios"
      campos={[
        { nombre: "numero", etiqueta: "Numero", obligatorio: true },
        { nombre: "piso", etiqueta: "Piso" },
      ]}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "numero", etiqueta: "Numero" },
        { campo: "piso", etiqueta: "Piso" },
      ]}
    />
  );
}
