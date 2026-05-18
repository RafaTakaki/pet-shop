using Microsoft.EntityFrameworkCore;
using PetShop.Web.Models;

namespace PetShop.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tutor> Tutores { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Servico> Servicos { get; set; }
    }
}