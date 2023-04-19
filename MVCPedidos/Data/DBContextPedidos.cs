using Microsoft.EntityFrameworkCore;
using MVCPedidos.Models;

namespace MVCPedidos.Data
{
    public class DBContextPedidos:DbContext
    {
        public DBContextPedidos(DbContextOptions<DBContextPedidos> options):base(options){

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetalleOrdenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Orden>().ToTable("Ordenes");
            modelBuilder.Entity<DetalleOrden>().ToTable("DetallesOrden");
        }
    }
}
