namespace PetShop.Web.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public int PetId { get; set; }

        public Pet? Pet { get; set; }

        public string Servico { get; set; } = string.Empty;

        public DateTime Data { get; set; }

        public string Horario { get; set; } = string.Empty;

        public string Status { get; set; } = "Agendado";
    }
}