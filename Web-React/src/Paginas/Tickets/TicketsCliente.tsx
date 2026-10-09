import { useCallback, useEffect, useState, type FormEvent } from "react";
import { useNavigate } from "react-router";
import type { TicketResponse } from "../../Servicios/Tickets/Models/Types";
import DetalleTicket from "./DetalleTicket";
import { ticketServicio } from "../../Servicios/Tickets/EndPoints";
import { useAuth } from "../../Servicios/Context";
import "./TicketsSoporte.css";
import "./TicketsCliente.css";

const ESTADO_PENDIENTE = "Pendiente";
const SIN_SOPORTE = "00000000-0000-0000-0000-000000000000";

const formatearFecha = (iso: string) =>
  new Date(iso).toLocaleDateString("es-CR", {
    day: "2-digit",
    month: "short",
    year: "numeric",
  });

const mensajeError = (e: unknown, defecto: string) =>
  e instanceof Error && e.message ? e.message : defecto;

function TicketsCliente() {
  const { sesion, cerrarSesion } = useAuth();
  const navigate = useNavigate();
  const [idDetalle, setIdDetalle] = useState<string | null>(null);
  const cerrarDetalle = useCallback(() => setIdDetalle(null), []);

  const salir = () => {
    cerrarSesion();
    navigate("/login", { replace: true });
  };
  const [tickets, setTickets] = useState<TicketResponse[]>([]);
  const [error, setError] = useState("");
  // null = formulario cerrado, "nuevo" = crear, ticket = editar
  const [formulario, setFormulario] = useState<TicketResponse | "nuevo" | null>(
    null,
  );
  const [asunto, setAsunto] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [guardando, setGuardando] = useState(false);

  const cargar = useCallback(() => {
    if (!sesion) return;
    ticketServicio
      .creados(sesion.id)
      .then((lista) => {
        setTickets(lista ?? []);
        setError("");
      })
      .catch((e) =>
        setError(mensajeError(e, "Ocurrio un error al cargar los tickets")),
      );
  }, [sesion]);

  useEffect(() => {
    cargar();
  }, [cargar]);

  const abrirNuevo = () => {
    setAsunto("");
    setDescripcion("");
    setFormulario("nuevo");
  };

  const abrirEdicion = (ticket: TicketResponse) => {
    setAsunto(ticket.asunto);
    setDescripcion(ticket.descripcion);
    setFormulario(ticket);
  };

  const cerrarFormulario = () => setFormulario(null);

  const guardar = async (e: FormEvent) => {
    e.preventDefault();
    if (!sesion || !formulario) return;
    setGuardando(true);
    try {
      if (formulario === "nuevo") {
        await ticketServicio.agregar({
          asunto,
          descripcion,
          estado: ESTADO_PENDIENTE,
          fechaCreacion: new Date().toISOString(),
          idEmisor: sesion.id,
          idSoporte: SIN_SOPORTE,
        });
      } else {
        await ticketServicio.editar(formulario.id, {
          asunto,
          descripcion,
          estado: formulario.estado,
          fechaCreacion: formulario.fechaCreacion,
          idEmisor: formulario.idEmisor,
          idSoporte: formulario.idSoporte,
        });
      }
      setFormulario(null);
      cargar();
    } catch (e) {
      setError(mensajeError(e, "Ocurrio un error al guardar el ticket"));
      setFormulario(null);
    } finally {
      setGuardando(false);
    }
  };

  const eliminar = async (ticket: TicketResponse) => {
    if (!window.confirm(`¿Eliminar el ticket "${ticket.asunto}"?`)) return;
    try {
      await ticketServicio.eliminar(ticket.id);
      cargar();
    } catch (e) {
      setError(mensajeError(e, "Ocurrio un error al eliminar el ticket"));
    }
  };

  return (
    <main className="soporte">
      <header className="soporte__encabezado">
        <div>
          <h1>Mis tickets</h1>
          <p>Crea y da seguimiento a tus solicitudes.</p>
        </div>
        <div className="soporte__acciones">
          <button type="button" className="cliente__nuevo" onClick={abrirNuevo}>
            + Nuevo ticket
          </button>
          <button type="button" className="soporte__salir" onClick={salir}>
            Cerrar sesión
          </button>
        </div>
      </header>

      {error && (
        <div className="soporte__error" role="alert">
          {error}
        </div>
      )}

      {tickets.length === 0 ? (
        <div className="soporte__vacio">
          <strong>No has creado tickets</strong>
          <span>Pulsa "Nuevo ticket" para crear tu primera solicitud.</span>
        </div>
      ) : (
        <section className="soporte__lista">
          {tickets.map((ticket) => {
            const pendiente = ticket.estado === ESTADO_PENDIENTE;
            return (
              <article className="ticket" key={ticket.id}>
                <header className="ticket__cabecera">
                  <h3>{ticket.asunto}</h3>
                  <span
                    className={`ticket__estado ${
                      pendiente
                        ? "ticket__estado--asignar"
                        : "ticket__estado--terminar"
                    }`}
                  >
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
                  <div className="cliente__acciones">
                    <button
                      type="button"
                      className="ticket__detalle"
                      onClick={() => setIdDetalle(ticket.id)}
                    >
                      Ver detalle
                    </button>
                    {pendiente && (
                      <>
                        <button
                          type="button"
                          className="ticket__accion ticket__accion--asignar"
                          onClick={() => abrirEdicion(ticket)}
                        >
                          Editar
                        </button>
                        <button
                          type="button"
                          className="ticket__accion cliente__eliminar"
                          onClick={() => eliminar(ticket)}
                        >
                          Eliminar
                        </button>
                      </>
                    )}
                  </div>
                </footer>
              </article>
            );
          })}
        </section>
      )}

      {idDetalle && <DetalleTicket id={idDetalle} onCerrar={cerrarDetalle} />}

      {formulario && (
        <div
          className="cliente__fondo"
          role="presentation"
          onClick={cerrarFormulario}
        >
          <form
            className="cliente__modal"
            role="dialog"
            aria-modal="true"
            aria-label={formulario === "nuevo" ? "Nuevo ticket" : "Editar ticket"}
            onClick={(e) => e.stopPropagation()}
            onSubmit={guardar}
          >
            <h2>{formulario === "nuevo" ? "Nuevo ticket" : "Editar ticket"}</h2>

            <label className="cliente__campo">
              <span>Asunto</span>
              <input
                value={asunto}
                onChange={(e) => setAsunto(e.target.value)}
                maxLength={100}
                required
                autoFocus
              />
            </label>

            <label className="cliente__campo">
              <span>Detalle</span>
              <textarea
                value={descripcion}
                onChange={(e) => setDescripcion(e.target.value)}
                rows={5}
                required
              />
            </label>

            <div className="cliente__botones">
              <button
                type="button"
                className="cliente__cancelar"
                onClick={cerrarFormulario}
              >
                Cancelar
              </button>
              <button
                type="submit"
                className="ticket__accion ticket__accion--asignar"
                disabled={guardando}
              >
                {guardando ? "Guardando..." : "Guardar"}
              </button>
            </div>
          </form>
        </div>
      )}
    </main>
  );
}

export default TicketsCliente;
