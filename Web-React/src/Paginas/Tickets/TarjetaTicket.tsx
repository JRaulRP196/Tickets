import type { TicketResponse } from "../../Servicios/Tickets/Models/Types";

interface Props {
  ticket: TicketResponse;
  etiquetaAccion: string;
  variante: "asignar" | "terminar";
  onAccion: (id: string) => void;
}

const formatearFecha = (iso: string) =>
  new Date(iso).toLocaleDateString("es-CR", {
    day: "2-digit",
    month: "short",
    year: "numeric",
  });

function TarjetaTicket({ ticket, etiquetaAccion, variante, onAccion }: Props) {
  return (
    <article className="ticket">
      <header className="ticket__cabecera">
        <h3>{ticket.asunto}</h3>
        <span className={`ticket__estado ticket__estado--${variante}`}>
          {ticket.estado}
        </span>
      </header>

      <p className="ticket__descripcion">{ticket.descripcion}</p>

      <footer className="ticket__pie">
        <div className="ticket__meta">
          <span>
            <small>Creado</small>
            {formatearFecha(ticket.fechaCreacion)}
          </span>
        </div>
        <button
          type="button"
          className={`ticket__accion ticket__accion--${variante}`}
          onClick={() => onAccion(ticket.id)}
        >
          {etiquetaAccion}
        </button>
      </footer>
    </article>
  );
}

export default TarjetaTicket;
