using System.Security.Cryptography.X509Certificates;
using EstoqueBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace EstoqueBackend.Data
{

    public class EstoqueContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Estoque;Username=postgres;Password=root;");
        }
    }
}
