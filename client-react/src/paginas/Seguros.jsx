import Mantenimiento from "../componentes/Mantenimiento";

export default function Seguros() {
  return (
    <Mantenimiento
      titulo="Seguros"
      recurso="seguros"
      campos={[
        { nombre: "nombre", etiqueta: "Nombre", obligatorio: true },
        { nombre: "cobertura", etiqueta: "Cobertura" },
      ]}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "nombre", etiqueta: "Nombre" },
        { campo: "cobertura", etiqueta: "Cobertura" },
      ]}
    />
  );
}
