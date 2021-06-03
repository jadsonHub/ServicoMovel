using Microsoft.EntityFrameworkCore;

namespace PBL.Models
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Movel> Movels { get; set; }
    }
}