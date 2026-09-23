namespace Abstracciones.Modelos
{
    public class Ticket
    {

        public string Asunto { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

    }

    public class TicketRequest : Ticket
    {
        public Guid IdEmisor { get; set; }
        public Guid IdSoporte { get; set; }
    }

    public class TicketResponse : TicketRequest
    {
        public Guid Id { get; set; }
        public string Emisor { get; set; }
        public string Soporte { get; set; }
    }

}
