import Auth from "./Paginas/Auth/Auth";
import { Route, Routes, Navigate } from "react-router";
import RutaSegura from "./components/RutaSegura";
import TicketsSoporte from "./Paginas/Tickets/TicketsSoporte";
import TicketsCliente from "./Paginas/Tickets/TicketsCliente";

function App() {
  return (
    <>
      <Routes>
        <Route path="/login" element={<Auth />} />
        <Route element={<RutaSegura roles={[1]}></RutaSegura>}>
          <Route
            path="/ticketSoporte"
            element={<TicketsSoporte></TicketsSoporte>}
          ></Route>
        </Route>
        <Route element={<RutaSegura roles={[2]}></RutaSegura>}>
          <Route path="/ticketCliente" element={<TicketsCliente />} />
        </Route>
        <Route
          path="*"
          element={<Navigate to="/login" replace></Navigate>}
        ></Route>
      </Routes>
    </>
  );
}

export default App;
