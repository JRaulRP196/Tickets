import { useAuth } from "../Servicios/Context";
import { Outlet, Navigate } from "react-router";

type Props = {
  roles: number[];
};

function RutaSegura({ roles }: Props) {
  const { sesion } = useAuth();
  if (!sesion) return <Navigate to={"/login"} replace></Navigate>;
  if (roles && !roles.includes(sesion.rol))
    return <Navigate to={"/404"}></Navigate>;
  return <Outlet></Outlet>;
}

export default RutaSegura;
