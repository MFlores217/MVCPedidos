using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPedidos.Data;
using MVCPedidos.Models;
using MVCPedidos.Services;

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
            return RedirectToAction("Index");
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

    }
}
