import { useEffect, useState } from "react";
import type { TicketDetalle } from "../../Servicios/Tickets/Models/Types";
import { ticketServicio } from "../../Servicios/Tickets/EndPoints";
import "./DetalleTicket.css";

interface Props {
  id: string;
  onCerrar: () => void;
}

const formatearFecha = (iso: string) =>
  new Date(iso).toLocaleDateString("es-CR", {
    day: "2-digit",
    month: "long",
    year: "numeric",
  });

function DetalleTicket({ id, onCerrar }: Props) {
  const [ticket, setTicket] = useState<TicketDetalle | null>(null);
  const [error, setError] = useState("");

  useEffect(() => {
    let activo = true;
    ticketServicio
      .detalle(id)
      .then((t) => activo && setTicket(t))
      .catch(
        (e) =>
          activo &&
          setError(
            e instanceof Error && e.message
              ? e.message
              : "Ocurrio un error al cargar el detalle",
          ),
      );
    return () => {
      activo = false;
    };
  }, [id]);

  useEffect(() => {
    const alTeclear = (e: KeyboardEvent) => e.key === "Escape" && onCerrar();
    window.addEventListener("keydown", alTeclear);
    return () => window.removeEventListener("keydown", alTeclear);
  }, [onCerrar]);

  return (
    <div className="detalle__fondo" role="presentation" onClick={onCerrar}>
      <section
        className="detalle__modal"
        role="dialog"
        aria-modal="true"
        aria-label="Detalle del ticket"
        onClick={(e) => e.stopPropagation()}
      >
        {error ? (
          <div className="soporte__error" role="alert">
            {error}
          </div>
        ) : !ticket ? (
          <p className="detalle__cargando">Cargando...</p>
        ) : (
          <>
            <header className="detalle__cabecera">
              <h2>{ticket.asunto}</h2>
              <span className="ticket__estado ticket__estado--terminar">
                {ticket.estado}
              </span>
            </header>

            <p className="detalle__descripcion">{ticket.descripcion}</p>

            <dl className="detalle__datos">
              <div>
                <dt>Solicitante</dt>
                <dd>{ticket.emisor}</dd>
              </div>
              <div>
                <dt>Soporte</dt>
                <dd>{ticket.soporte}</dd>
              </div>
              <div>
                <dt>Creado</dt>
                <dd>{formatearFecha(ticket.fechaCreacion)}</dd>
              </div>
            </dl>
          </>
        )}

        <button type="button" className="detalle__cerrar" onClick={onCerrar}>
          Cerrar
        </button>
      </section>
    </div>
  );
}

export default DetalleTicket;
