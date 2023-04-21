using Microsoft.Build.Utilities;
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
        public async Task<bool> Guardar(Producto producto)
        {
            bool bandera = false;
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            bandera = true;
            return bandera;
        }

        public async Task<Producto> ObtenerProducto(int? id)
        {
            Producto? producto;
            producto = await (
                        from p in _context.Productos
                        where p.Id == id
                        select new Producto {
                            Id = p.Id,
                            Nombre = p.Nombre,
                            Precio = p.Precio,
                            ProductosOrden = (
                                    from d in _context.DetalleOrdenes
                                    select new DetalleOrden {
                                            Id = d.Id,
                                            Cantidad = d.Cantidad,
                                            OrdenId = d.OrdenId,
                                            ProductoId = d.ProductoId
                                    }).ToList()
                        }).FirstOrDefaultAsync();
            return producto;
        }

        public async Task<int> Eliminar(int id)
        {
            int resultado;
            Producto? producto = await ObtenerProducto(id);
            if (producto!=null)
            {
                if(producto.ProductosOrden != null)
                    if (producto.ProductosOrden.Count.Equals(0))
                    {
                        _context.Remove(producto);
                        await _context.SaveChangesAsync();
                        resultado = 1; //Se eliminó
                    } else
                    {
                        resultado = 0;
                    }
                else
                {
                    _context.Remove(producto);
                    await _context.SaveChangesAsync();
                    resultado = 1;
                }
            } else
            {
                resultado = 2;
            }
            return resultado;
        }

        public async Task<bool> Modificar(Producto producto)
        {
            _context.Update(producto);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
