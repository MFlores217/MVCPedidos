using Microsoft.EntityFrameworkCore;
using MVCPedidos.Data;
using MVCPedidos.Models;

namespace MVCPedidos.Services
{
    public class ProductoService
    {
        private readonly DBContextPedidos _context;

        public ProductoService(DBContextPedidos context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ListarProductos()
        {
            List<Producto> lista = await(
                    from p in _context.Productos
                    select new Producto {
                        Id= p.Id,
                        Nombre= p.Nombre,
                        Precio= p.Precio,
                    }
                 ).ToListAsync();
            return lista;
        }

    }
}
