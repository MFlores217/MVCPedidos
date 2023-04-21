using Microsoft.EntityFrameworkCore;
using MVCPedidos.Data;
using MVCPedidos.Models;

namespace MVCPedidos.Services
{
    public class OrdenService
    {
        private readonly DBContextPedidos _context;
        public OrdenService(DBContextPedidos context)
        {
            _context = context;
        }

        public async Task<List<Orden>> ListarOrdenes()
        {
            List<Orden> lista = await (
                    from o in _context.Ordenes
                    orderby o.FechaPedido descending
                    select new Orden {
                        Id = o.Id,
                        Cliente = (from c in _context.Clientes.Where(i=>i.Id==o.ClienteId)
                                   select new Cliente
                                   {
                                       Id= c.Id,
                                       Nombre = c.Nombre,
                                       Apellido= c.Apellido,
                                       Direccion= c.Direccion,
                                       Email= c.Email,
                                       Telefono= c.Telefono,
                                   }).FirstOrDefault(),
                        FechaEntrega=o.FechaEntrega,
                        FechaPedido=o.FechaPedido,
                        ProductosOrden=(from d in o.ProductosOrden
                                        select new DetalleOrden
                                        {
                                            Id= d.Id,
                                            Cantidad= d.Cantidad,
                                            ProductoId= d.ProductoId,
                                            OrdenId= d.OrdenId,
                                            producto = (from p in _context.Productos.Where(x=>x.Id==d.ProductoId)
                                                        select new Producto
                                                        {
                                                            Id = p.Id,
                                                            Nombre = p.Nombre,
                                                            Precio = p.Precio,
                                                        }).FirstOrDefault()
                                        }
                                        ).ToList()
                    }
                ).ToListAsync();
            return lista;
        }
    }
}
