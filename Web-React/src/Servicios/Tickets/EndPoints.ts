import { request } from "../Cliente";
import type { TicketResponse } from "./Models/Types";

const BASE = import.meta.env.VITE_URL_API_TICKETS;

export const ticketServicio = {
  pendientes: () =>
    request<TicketResponse[] | null>(`${BASE}/api/Ticket/Tickets/Pendientes`),
  asigandos: (id: string) =>
    request<TicketResponse[] | null>(
      `${BASE}/api/Ticket/Tickets/Asignados/${id}`,
    ),
};
