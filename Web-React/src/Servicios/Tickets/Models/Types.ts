export interface Ticket {
  asunto: string;
  estado: string;
  descripcion: string;
  fechaCreacion: string;
}

export interface TicketRequest extends Ticket {
  idEmisor: string;
  idSoporte: string;
}

export interface TicketResponse extends TicketRequest {
  id: string;
}

export interface TicketDetalle extends TicketResponse {
  emisor: string;
  soporte: string;
}
