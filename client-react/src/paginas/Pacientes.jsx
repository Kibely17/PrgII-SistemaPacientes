import { useEffect, useState } from "react";
import Mantenimiento from "../componentes/Mantenimiento";
import { api } from "../servicios/api";

export default function Pacientes() {
  const [seguros, setSeguros] = useState([]);

  useEffect(() => {
    api
      .listar("seguros")
      .then((datos) => setSeguros(datos.map((s) => ({ id: s.id, texto: s.nombre }))))
      .catch(() => setSeguros([]));
  }, []);

  return (
    <Mantenimiento
      titulo="Pacientes"
      recurso="pacientes"
      campos={[
        { nombre: "cedula", etiqueta: "Cedula", obligatorio: true },
        { nombre: "nombre", etiqueta: "Nombre", obligatorio: true },
        { nombre: "apellido", etiqueta: "Apellido", obligatorio: true },
        { nombre: "fechaNacimiento", etiqueta: "Fecha de nacimiento", tipo: "date", obligatorio: true },
        { nombre: "sexo", etiqueta: "Sexo (M o F)", obligatorio: true },
        { nombre: "telefono", etiqueta: "Telefono" },
        { nombre: "correo", etiqueta: "Correo" },
        { nombre: "direccion", etiqueta: "Direccion" },
        { nombre: "seguroId", etiqueta: "Seguro", tipo: "number" },
      ]}
      catalogos={{ seguroId: seguros }}
      columnas={[
        { campo: "id", etiqueta: "ID" },
        { campo: "cedula", etiqueta: "Cedula" },
        { campo: "nombreCompleto", etiqueta: "Nombre" },
        { campo: "edad", etiqueta: "Edad" },
        { campo: "sexo", etiqueta: "Sexo" },
        { campo: "seguroNombre", etiqueta: "Seguro" },
        { campo: "telefono", etiqueta: "Telefono" },
      ]}
    />
  );
}
