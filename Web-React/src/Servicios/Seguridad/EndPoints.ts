import type { UsuarioRequest, Login, Token } from "./Models/Types";
import { request } from "../Cliente";

const BASE = import.meta.env.VITE_URL_API_SEGURIDAD;

export const seguridadServicio = {
  registrar: async (usuario: UsuarioRequest) =>
    await request<string>(`${BASE}/api/Usuario/Registrar`, usuario, "POST"),
  login: async (login: Login): Promise<string> => {
    const resp = await request<Token>(
      `${BASE}/api/Autenticacion/Login`,
      login,
      "POST",
    );
    if (!resp.validacionExitosa) throw new Error("Credenciales inválidas");
    return resp.accessToken;
  },
};
