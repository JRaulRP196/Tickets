// En desarrollo se usan las VITE_* del .env (APIs en localhost).
// En producción no existen y se usan rutas relativas que nginx redirige a las APIs.
export const config = {
  urlApiSeguridad: import.meta.env.VITE_URL_API_SEGURIDAD ?? "/api-seguridad",
  urlApiTickets: import.meta.env.VITE_URL_API_TICKETS ?? "/api-tickets",
};
