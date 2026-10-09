import {
  createContext,
  useContext,
  useEffect,
  useState,
  type ReactNode,
} from "react";
import { leerSesion, type Sesion } from "./Sesion";
import { tokenStorage } from "./TokenStorage";

interface AuthValor {
  sesion: Sesion | null;
  iniciarSesion: (token: string) => void;
  cerrarSesion: () => void;
}

const AuthContext = createContext<AuthValor | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [sesion, setSesion] = useState<Sesion | null>(() => {
    const token = tokenStorage.get();
    return token ? leerSesion(token) : null;
  });

  const iniciarSesion = (token: string) => {
    tokenStorage.set(token);
    setSesion(leerSesion(token));
  };

  const cerrarSesion = () => {
    tokenStorage.clear();
    setSesion(null);
  };

  useEffect(() => {
    if (!sesion) return;
    const milisegundos = sesion.expiracion * 1000 - Date.now();
    const id = setTimeout(cerrarSesion, milisegundos);
    return () => clearTimeout(id);
  }, [sesion]);

  useEffect(() => {
    window.addEventListener("sesion-expirada", cerrarSesion);
    return () => window.removeEventListener("sesion-expirada", cerrarSesion);
  }, []);

  return (
    <AuthContext value={{ sesion, iniciarSesion, cerrarSesion }}>
      {children}
    </AuthContext>
  );
}

export function useAuth() {
  const contexto = useContext(AuthContext);
  if (!contexto) throw new Error("Debe usarse dentro de <AuthProvider>");
  return contexto;
}
