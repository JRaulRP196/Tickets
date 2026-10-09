import { request } from "../Cliente";
import type {
  TicketDetalle,
  TicketRequest,
  TicketResponse,
} from "./Models/Types";

const BASE = import.meta.env.VITE_URL_API_TICKETS;

export const ticketServicio = {
  pendientes: () =>
    request<TicketResponse[] | null>(`${BASE}/api/Ticket/Tickets/Pendientes`),
  asigandos: (id: string) =>
    request<TicketResponse[] | null>(
      `${BASE}/api/Ticket/Tickets/Asignados/${id}`,
    ),
  asignar: (id: string, idSoporte: string) =>
    request(`${BASE}/api/Ticket?id=${id}&idSoporte=${idSoporte}`, "", "PATCH"),
  detalle: (id: string) =>
    request<TicketDetalle>(`${BASE}/api/Ticket/${id}`),
  creados:(idEmisor: string) =>
    request<TicketResponse[] | null>(
      `${BASE}/api/Ticket/Tickets/Creados/${idEmisor}`,
    ),
  agregar: (ticket: TicketRequest) =>
    request(`${BASE}/api/Ticket`, ticket, "POST"),
  editar: (id: string, ticket: TicketRequest) =>
    request(`${BASE}/api/Ticket/${id}`, ticket, "PUT"),
  eliminar: (id: string) => request(`${BASE}/api/Ticket/${id}`, undefined, "DELETE"),
  terminar: (id: string) =>
    request(`${BASE}/api/Ticket/Terminar/${id}`, "", "PATCH"),
};
