import { useEffect, useState } from "react";
import type { TicketResponse } from "../../Servicios/Tickets/Models/Types";
import TarjetaTicket from "./TarjetaTicket";
import { asignadosMuestra, sinAsignarMuestra } from "./datosMuestra";
import "./TicketsSoporte.css";
import { ticketServicio } from "../../Servicios/Tickets/EndPoints";
import { useAuth } from "../../Servicios/Context";

type Pestana = "sinAsignar" | "asignados";

function TicketsSoporte() {
  const [pestana, setPestana] = useState<Pestana>("sinAsignar");
  const [sinAsignar, setSinAsignar] = useState<TicketResponse[] | null>(
    sinAsignarMuestra,
  );
  const [asignados, setAsignados] = useState<TicketResponse[] | null>(
    asignadosMuestra,
  );
  const [error, setError] = useState("");
  const { sesion } = useAuth();

  useEffect(() => {
    ticketServicio
      .pendientes()
      .then((tickets) => {
        setSinAsignar(tickets);
      })
      .catch((error) => {
        setError(
          error instanceof Error
            ? error.message
            : "Ocurrio un error al cargar los tickets",
        );
      });
  }, []);

  useEffect(() => {
    ticketServicio
      .asigandos(sesion ? sesion.id : "")
      .then((tickets) => {
        setAsignados(tickets);
      })
      .catch((error) => {
        setError(
          error instanceof Error
            ? error.message
            : "Ocurrio un error al cargar los tickets",
        );
      });
  }, [sesion]);

  const asignar = (id: string) => {
    setError("");
    // TODO: llamar al servicio para asignarse el ticket (Editar con idSoporte = usuario actual)
  };

  const terminar = (id: string) => {
    setError("");
    // TODO: llamar al servicio para terminar el ticket (Editar con el estado final)
  };

  const visibles = pestana === "sinAsignar" ? sinAsignar : asignados;

  return (
    <main className="soporte">
      <header className="soporte__encabezado">
        <div>
          <h1>Panel de soporte</h1>
          <p>Gestiona las solicitudes de tu equipo.</p>
        </div>
        <div className="soporte__usuario">
          <span className="soporte__avatar" aria-hidden="true">
            S
          </span>
          Soporte
        </div>
      </header>

      <div className="soporte__tabs" role="tablist">
        <button
          type="button"
          role="tab"
          aria-selected={pestana === "sinAsignar"}
          className={pestana === "sinAsignar" ? "activo" : ""}
          onClick={() => setPestana("sinAsignar")}
        >
          Sin asignar
          <span className="soporte__contador">
            {sinAsignar ? sinAsignar.length : 0}
          </span>
        </button>
        <button
          type="button"
          role="tab"
          aria-selected={pestana === "asignados"}
          className={pestana === "asignados" ? "activo" : ""}
          onClick={() => setPestana("asignados")}
        >
          Asignados
          <span className="soporte__contador">
            {asignados ? asignados.length : 0}
          </span>
        </button>
      </div>

      {error && (
        <div className="soporte__error" role="alert">
          {error}
        </div>
      )}

      {visibles?.length === 0 ? (
        <div className="soporte__vacio">
          <strong>
            {pestana === "sinAsignar"
              ? "No hay tickets sin asignar"
              : "No tienes tickets asignados"}
          </strong>
          <span>
            {pestana === "sinAsignar"
              ? "Cuando alguien cree una solicitud, aparecerá aquí."
              : 'Asígnate un ticket desde la pestaña "Sin asignar".'}
          </span>
        </div>
      ) : (
        <section className="soporte__lista" role="tabpanel">
          {visibles?.map((ticket) => (
            <TarjetaTicket
              key={ticket.id}
              ticket={ticket}
              variante={pestana === "sinAsignar" ? "asignar" : "terminar"}
              etiquetaAccion={
                pestana === "sinAsignar" ? "Asignar" : "Terminar ticket"
              }
              onAccion={pestana === "sinAsignar" ? asignar : terminar}
            />
          ))}
        </section>
      )}
    </main>
  );
}

export default TicketsSoporte;
