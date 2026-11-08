const BASE = import.meta.env.VITE_API_URL || "http://localhost:5210/api";

async function pedir(ruta, opciones = {}) {
  const respuesta = await fetch(`${BASE}/${ruta}`, {
    headers: { "Content-Type": "application/json" },
    ...opciones,
  });

  if (!respuesta.ok) {
    const texto = await respuesta.text();
    throw new Error(texto || `Error ${respuesta.status}`);
  }

  if (respuesta.status === 204) {
    return null;
  }

  return respuesta.json();
}

export const api = {
  listar: (recurso) => pedir(recurso),
  obtener: (recurso, id) => pedir(`${recurso}/${id}`),
  crear: (recurso, datos) =>
    pedir(recurso, { method: "POST", body: JSON.stringify(datos) }),
  actualizar: (recurso, id, datos) =>
    pedir(`${recurso}/${id}`, { method: "PUT", body: JSON.stringify(datos) }),
  eliminar: (recurso, id) => pedir(`${recurso}/${id}`, { method: "DELETE" }),
};
