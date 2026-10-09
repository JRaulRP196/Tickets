import type { TicketDetalle } from '../../Servicios/Tickets/Models/Types'

// Datos temporales solo para ver el diseño. Bórralos cuando conectes los servicios.
const SIN_SOPORTE = '00000000-0000-0000-0000-000000000000'

export const sinAsignarMuestra: TicketDetalle[] = [
  {
    id: '1',
    asunto: 'No puedo acceder al correo institucional',
    descripcion: 'Desde ayer el correo me pide la contraseña en bucle aunque la escribo bien.',
    estado: 'Pendiente',
    fechaCreacion: '2026-10-06T09:15:00',
    idEmisor: 'a1',
    idSoporte: SIN_SOPORTE,
    emisor: 'María Fernández',
    soporte: 'Sin asignar',
  },
  {
    id: '2',
    asunto: 'La impresora del segundo piso no responde',
    descripcion: 'Aparece como conectada pero los trabajos se quedan en cola y nunca se imprimen.',
    estado: 'Pendiente',
    fechaCreacion: '2026-10-07T14:40:00',
    idEmisor: 'a2',
    idSoporte: SIN_SOPORTE,
    emisor: 'Carlos Jiménez',
    soporte: 'Sin asignar',
  },
  {
    id: '3',
    asunto: 'Solicitud de acceso a la VPN',
    descripcion: 'Necesito conectarme a la red interna para trabajar desde casa esta semana.',
    estado: 'Pendiente',
    fechaCreacion: '2026-10-08T08:05:00',
    idEmisor: 'a3',
    idSoporte: SIN_SOPORTE,
    emisor: 'Lucía Vargas',
    soporte: 'Sin asignar',
  },
]

export const asignadosMuestra: TicketDetalle[] = [
  {
    id: '4',
    asunto: 'Pantalla azul al iniciar el equipo',
    descripcion: 'El portátil muestra una pantalla azul con un código de error justo después del logo.',
    estado: 'Asignado',
    fechaCreacion: '2026-10-05T11:20:00',
    idEmisor: 'a4',
    idSoporte: 'yo',
    emisor: 'Andrés Mora',
    soporte: 'Tú',
  },
  {
    id: '5',
    asunto: 'Instalación de Docker Desktop',
    descripcion: 'Requiero Docker Desktop instalado y configurado para el proyecto del curso.',
    estado: 'Asignado',
    fechaCreacion: '2026-10-07T16:10:00',
    idEmisor: 'a5',
    idSoporte: 'yo',
    emisor: 'Sofía Rojas',
    soporte: 'Tú',
  },
]
