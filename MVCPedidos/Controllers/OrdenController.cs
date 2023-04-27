using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPedidos.Data;
using MVCPedidos.Models;
using MVCPedidos.Services;
using MVCPedidos.ViewModels;

namespace MVCPedidos.Controllers
{
    public class OrdenController : Controller
    {
        private readonly OrdenService _ordenService;
        private readonly DBContextPedidos _context;

        public OrdenController(OrdenService ordenService, DBContextPedidos context)
        {
            _ordenService = ordenService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Orden> ordenes = await _ordenService.ListarOrdenes();
            return View(ordenes);
        }

        [HttpGet]
        public async Task<IActionResult> Nuevo() {
            List<Cliente> clientes = await _context.Clientes.ToListAsync();
            ViewData["Clientes"] = clientes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nuevo(Orden orden){
            Orden newOrden = await _ordenService.Guardar(orden);
            OrdenCliente? ordenCliente = await _ordenService.ObtenerOrdenCliente(newOrden.Id);
            //Carga los datos de los modales
            List<Cliente> clientes = _context.Clientes.ToList();
            ViewData["Clientes"] = clientes;
            List<Producto> productos = _context.Productos.ToList();
            ViewData["Productos"] = productos;
            return View("Ordenar", ordenCliente);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool resultado = await _ordenService.Eliminar(id);
            if (resultado){
                return RedirectToAction("Index");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            OrdenCliente? orden = await _ordenService.ObtenerOrdenCliente(id);
            List<Cliente> clientes = _context.Clientes.ToList();
            ViewData["Clientes"] = clientes;
            List<Producto> productos = _context.Productos.ToList();
            ViewData["Productos"] = productos;
            return View("Ordenar",orden);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarOrden(OrdenCliente orden) {
            List<Cliente> clientes = _context.Clientes.ToList();
            ViewData["Clientes"] = clientes;
            List<Producto> productos = _context.Productos.ToList();
            ViewData["Productos"] = productos;
            Orden nuevaOrden = new Orden { 
                Id= orden.Id,
                ClienteId= orden.ClienteId,
                FechaEntrega = orden.FechaEntrega,
                FechaPedido= orden.FechaPedido,
            };
            int resultado = await _ordenService.EditarOrden(nuevaOrden);
            if (resultado < 0){
                return BadRequest("Error al modificar la orden, revise las fechas");
            } else {
                orden = await _ordenService.ObtenerOrdenCliente(orden.Id);
                return View("Ordenar",orden);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarProducto(DetalleOrden detalle){
            bool resultado = await _ordenService.AgregarProducto(detalle);
            OrdenCliente? orden;
            if(!resultado) {
                return BadRequest("Error al agregar el producto");
            }
            orden = await _ordenService.ObtenerOrdenCliente(detalle.OrdenId);
            List<Cliente> clientes = _context.Clientes.ToList();
            ViewData["Clientes"] = clientes;
            List<Producto> productos = _context.Productos.ToList();
            ViewData["Productos"] = productos;
            return View("Ordenar", orden);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarDetalle(int id) {
            int ordenId = await _ordenService.EliminarDetalle(id);
            OrdenCliente? orden = await _ordenService.ObtenerOrdenCliente(ordenId);
            if(orden.Detalles != null)
            {
                if (orden.Detalles.Count.Equals(0)){
                    await _ordenService.Eliminar(ordenId);
                    RedirectToAction("Index");
                }
            }
            List<Cliente> clientes = _context.Clientes.ToList();
            ViewData["Clientes"] = clientes;
            List<Producto> productos = _context.Productos.ToList();
            ViewData["Productos"] = productos;
            return View("Ordenar", orden);
        }

    }
}
