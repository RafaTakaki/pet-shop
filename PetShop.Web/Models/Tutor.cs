namespace PetShop.Web.Models
{
    public class Tutor
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Tutor";

        public List<Pet> Pets { get; set; } = new();
    }
}