import type { UsuarioRequest, Login, Token, Rol } from "./Models/Types";
import { request } from "../Cliente";
import { config } from "../../config";

const BASE = config.urlApiSeguridad;

export const seguridadServicio = {
  registrar: (usuario: UsuarioRequest) =>
    request<string>(`${BASE}/api/Usuario/Registrar`, usuario, "POST"),
  login: async (login: Login): Promise<string> => {
    const resp = await request<Token>(
      `${BASE}/api/Autenticacion/Login`,
      login,
      "POST",
    );
    if (!resp.validacionExitosa) throw new Error("Credenciales inválidas");
    return resp.accessToken;
  },
  roles: () => request<Rol[] | null>(`${BASE}/api/Rol`),
};
