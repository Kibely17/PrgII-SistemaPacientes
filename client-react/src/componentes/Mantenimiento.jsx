import { useEffect, useState } from "react";
import { api } from "../servicios/api";

function valorInicial(campos) {
  const inicial = {};
  campos.forEach((campo) => {
    inicial[campo.nombre] = campo.tipo === "number" ? "" : "";
  });
  return inicial;
}

export default function Mantenimiento({ titulo, recurso, campos, columnas, catalogos = {} }) {
  const [filas, setFilas] = useState([]);
  const [formulario, setFormulario] = useState(valorInicial(campos));
  const [editando, setEditando] = useState(null);
  const [error, setError] = useState("");
  const [cargando, setCargando] = useState(true);

  const cargar = async () => {
    try {
      setCargando(true);
      setFilas(await api.listar(recurso));
      setError("");
    } catch (e) {
      setError(e.message);
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    cargar();
  }, [recurso]);

  const cambiar = (nombre, valor) =>
    setFormulario((actual) => ({ ...actual, [nombre]: valor }));

  const limpiar = () => {
    setFormulario(valorInicial(campos));
    setEditando(null);
    setError("");
  };

  const guardar = async (evento) => {
    evento.preventDefault();
    const datos = {};
    campos.forEach((campo) => {
      const valor = formulario[campo.nombre];
      if (campo.tipo === "number") {
        datos[campo.nombre] = valor === "" ? null : Number(valor);
      } else {
        datos[campo.nombre] = valor === "" ? null : valor;
      }
    });

    try {
      if (editando) {
        await api.actualizar(recurso, editando, datos);
      } else {
        await api.crear(recurso, datos);
      }
      limpiar();
      cargar();
    } catch (e) {
      setError(e.message);
    }
  };

  const editar = (fila) => {
    const valores = {};
    campos.forEach((campo) => {
      const valor = fila[campo.nombre];
      if (campo.tipo === "date" && valor) {
        valores[campo.nombre] = valor.substring(0, 10);
      } else if (campo.tipo === "datetime-local" && valor) {
        valores[campo.nombre] = valor.substring(0, 16);
      } else {
        valores[campo.nombre] = valor ?? "";
      }
    });
    setFormulario(valores);
    setEditando(fila.id);
    setError("");
  };

  const eliminar = async (id) => {
    if (!confirm("Confirma que desea eliminar el registro?")) return;
    try {
      await api.eliminar(recurso, id);
      cargar();
    } catch (e) {
      setError(e.message);
    }
  };

  return (
    <section>
      <h2>{titulo}</h2>

      {error && <p className="error">{error}</p>}

      <form onSubmit={guardar} className="formulario">
        {campos.map((campo) => (
          <label key={campo.nombre}>
            <span>{campo.etiqueta}</span>
            {catalogos[campo.nombre] ? (
              <select
                value={formulario[campo.nombre]}
                onChange={(e) => cambiar(campo.nombre, e.target.value)}
                required={campo.obligatorio}
              >
                <option value="">Seleccione</option>
                {catalogos[campo.nombre].map((opcion) => (
                  <option key={opcion.id} value={opcion.id}>
                    {opcion.texto}
                  </option>
                ))}
              </select>
            ) : (
              <input
                type={campo.tipo || "text"}
                value={formulario[campo.nombre]}
                onChange={(e) => cambiar(campo.nombre, e.target.value)}
                required={campo.obligatorio}
              />
            )}
          </label>
        ))}

        <div className="acciones">
          <button type="submit">{editando ? "Guardar cambios" : "Agregar"}</button>
          {editando && (
            <button type="button" onClick={limpiar} className="secundario">
              Cancelar
            </button>
          )}
        </div>
      </form>

      {cargando ? (
        <p>Cargando...</p>
      ) : (
        <table>
          <thead>
            <tr>
              {columnas.map((c) => (
                <th key={c.campo}>{c.etiqueta}</th>
              ))}
              <th></th>
            </tr>
          </thead>
          <tbody>
            {filas.length === 0 && (
              <tr>
                <td colSpan={columnas.length + 1}>No hay registros.</td>
              </tr>
            )}
            {filas.map((fila) => (
              <tr key={fila.id}>
                {columnas.map((c) => (
                  <td key={c.campo}>{c.formato ? c.formato(fila) : fila[c.campo]}</td>
                ))}
                <td className="celda-acciones">
                  <button onClick={() => editar(fila)}>Editar</button>
                  <button onClick={() => eliminar(fila.id)} className="peligro">
                    Eliminar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </section>
  );
}
