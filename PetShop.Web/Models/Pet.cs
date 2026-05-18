namespace PetShop.Web.Models
{
    public class Pet
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Especie { get; set; } = string.Empty;

        public string Raca { get; set; } = string.Empty;

        public int Idade { get; set; }

        public int TutorId { get; set; }

        public Tutor? Tutor { get; set; }

        public List<Reserva> Reservas { get; set; } = new();
    }
}