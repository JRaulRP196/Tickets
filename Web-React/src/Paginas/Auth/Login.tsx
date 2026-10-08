import { useState, type FormEvent } from "react";
import { seguridadServicio } from "../../Servicios/Seguridad/EndPoints";
import { tokenStorage } from "../../Servicios/TokenStorage";

interface Props {
  irARegistro: () => void;
}

function Login({ irARegistro }: Props) {
  const [correo, setCorreo] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const enviar = async (e: FormEvent) => {
    e.preventDefault();
    try {
      const token = await seguridadServicio.login({
        correo,
        passwordHash: password,
      });
      tokenStorage.set(token);
    } catch (error) {
      if (error instanceof Error) {
        setError(error.message);
      } else {
        setError("Error al iniciar sesión");
      }
    }
    console.log({ correo, password });
  };

  return (
    <form className="auth__form" onSubmit={enviar}>
      <header>
        <h2>Bienvenido de nuevo</h2>
        <p>Ingresa tus credenciales para continuar.</p>
      </header>

      {error && (
        <div className="auth__error" role="alert">
          {error}
        </div>
      )}

      <label className="campo">
        <span>Correo electrónico</span>
        <input
          type="email"
          autoComplete="email"
          placeholder="tucorreo@ejemplo.com"
          value={correo}
          onChange={(e) => setCorreo(e.target.value)}
          required
        />
      </label>

      <label className="campo">
        <span>Contraseña</span>
        <input
          type="password"
          autoComplete="current-password"
          placeholder="••••••••"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </label>

      <button type="submit" className="boton">
        Iniciar sesión
      </button>

      <p className="auth__cambio">
        ¿No tienes cuenta?{" "}
        <button type="button" onClick={irARegistro}>
          Regístrate
        </button>
      </p>
    </form>
  );
}

export default Login;
