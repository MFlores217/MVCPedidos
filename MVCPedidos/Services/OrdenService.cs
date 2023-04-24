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

        public async Task<Orden> Guardar(Orden orden){
            orden.FechaPedido = DateTime.Now.Date;
            orden.FechaEntrega = DateTime.Now.Date.AddDays(5);
            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();
            return orden;
        }

        public async Task<Orden> ObtenerOrden(int id) {
            Orden? orden = await (_context.Ordenes.Include(c => c.Cliente).Include(d => d.ProductosOrden).ThenInclude(p => p.producto)).FirstOrDefaultAsync(o=>o.Id==id);
            return orden;
        }

        public async Task<bool> Eliminar(int id) {
            Orden orden = await ObtenerOrden(id);
            if(orden != null) {
                if (orden.ProductosOrden != null){
                    if (!orden.ProductosOrden.Count.Equals(0))
                    {
                        _context.DetalleOrdenes.RemoveRange(_context.DetalleOrdenes.Where(d => d.OrdenId == id));
                    }
                }
                _context.Remove(orden);
                await _context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }

    }
}
