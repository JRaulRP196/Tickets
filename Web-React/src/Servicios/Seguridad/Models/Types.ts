export interface Usuario {
  id: string;
  nombre: string;
  apellido1: string;
  apellido2: string;
  passwordHash: string;
  estado: boolean;
  correo: string;
}

export interface UsuarioRequest extends Omit<Usuario, "id"> {
  idRol: number;
}

export interface UsuarioResponse extends UsuarioRequest {
  rol: string;
}

export interface Login {
  passwordHash: string;
  correo: string;
}

export interface Token {
  accessToken: string;
  validacionExitosa: boolean;
}

export interface Rol {
  id: number;
  nombre: string;
}
