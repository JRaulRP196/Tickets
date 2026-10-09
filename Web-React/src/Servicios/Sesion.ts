export interface Sesion {
  id: string;
  nombre: string;
  correo: string;
  rol: number;
  expiracion: number;
}

const CLAIMS = {
  id: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
  nombre: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
  correo: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
  rol: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
};

export function leerSesion(token: string): Sesion | null {
  try {
    const payload = JSON.parse(
      atob(token.split(".")[1].replace(/-/g, "+").replace(/_/g, "/")),
    );
    const sesion = {
      id: payload[CLAIMS.id],
      nombre: payload[CLAIMS.nombre],
      correo: payload[CLAIMS.correo],
      rol: Number(payload[CLAIMS.rol]),
      expiracion: payload.exp,
    };
    return sesion.expiracion * 1000 > Date.now() ? sesion : null;
  } catch {
    return null;
  }
}
