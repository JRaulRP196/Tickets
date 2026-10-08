import { useEffect, useState, type FormEvent } from "react";
import type { Rol } from "../../Servicios/Seguridad/Models/Types";
import { seguridadServicio } from "../../Servicios/Seguridad/EndPoints";

interface Props {
  irALogin: () => void;
}

function Registro({ irALogin }: Props) {
  const [nombre, setNombre] = useState("");
  const [apellido1, setApellido1] = useState("");
  const [apellido2, setApellido2] = useState("");
  const [correo, setCorreo] = useState("");
  const [password, setPassword] = useState("");
  const [confirmar, setConfirmar] = useState("");
  const [idRol, setIdRol] = useState("");
  const [roles, setRoles] = useState<Rol[] | null>([]);
  const [error, setError] = useState("");

  useEffect(() => {
    seguridadServicio
      .roles()
      .then((r) => setRoles(r ?? []))
      .catch((err) =>
        setError(
          err instanceof Error
            ? err.message
            : "No se pudieron cargar los roles",
        ),
      );
  }, []);

  const enviar = (e: FormEvent) => {
    e.preventDefault();
    if (password !== confirmar) {
      setError("Las contraseñas no coinciden.");
      return;
    }
    seguridadServicio
      .registrar({
        correo,
        nombre,
        apellido1,
        apellido2,
        passwordHash: password,
        idRol: Number(idRol),
        estado: true,
      })
      .then(irALogin)
      .catch((error) =>
        setError(
          error instanceof Error
            ? error.message
            : "Ocurrio un error al registrar el usuario",
        ),
      );
    console.log({
      nombre,
      apellido1,
      apellido2,
      correo,
      password,
      idRol: Number(idRol),
    });
  };

  return (
    <form className="auth__form" onSubmit={enviar}>
      <header>
        <h2>Crea tu cuenta</h2>
        <p>Solo toma un minuto.</p>
      </header>

      {error && (
        <div className="auth__error" role="alert">
          {error}
        </div>
      )}

      <label className="campo">
        <span>Nombre</span>
        <input
          type="text"
          autoComplete="given-name"
          placeholder="Juan"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
          required
        />
      </label>

      <div className="fila">
        <label className="campo">
          <span>Primer apellido</span>
          <input
            type="text"
            autoComplete="family-name"
            placeholder="Pérez"
            value={apellido1}
            onChange={(e) => setApellido1(e.target.value)}
            required
          />
        </label>
        <label className="campo">
          <span>Segundo apellido</span>
          <input
            type="text"
            placeholder="Mora"
            value={apellido2}
            onChange={(e) => setApellido2(e.target.value)}
          />
        </label>
      </div>

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
        <span>Rol</span>
        <select
          value={idRol}
          onChange={(e) => setIdRol(e.target.value)}
          disabled={roles?.length === 0}
          required
        >
          <option value="" disabled>
            {roles?.length === 0 ? "Cargando roles…" : "Selecciona un rol"}
          </option>
          {roles?.map((rol) => (
            <option key={rol.id} value={rol.id}>
              {rol.nombre}
            </option>
          ))}
        </select>
      </label>

      <div className="fila">
        <label className="campo">
          <span>Contraseña</span>
          <input
            type="password"
            autoComplete="new-password"
            placeholder="••••••••"
            minLength={8}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </label>
        <label className="campo">
          <span>Confirmar contraseña</span>
          <input
            type="password"
            autoComplete="new-password"
            placeholder="••••••••"
            value={confirmar}
            onChange={(e) => setConfirmar(e.target.value)}
            required
          />
        </label>
      </div>

      <button type="submit" className="boton">
        Crear cuenta
      </button>

      <p className="auth__cambio">
        ¿Ya tienes cuenta?{" "}
        <button type="button" onClick={irALogin}>
          Inicia sesión
        </button>
      </p>
    </form>
  );
}

export default Registro;
