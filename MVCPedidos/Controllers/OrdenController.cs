using Microsoft.AspNetCore.Mvc;
using MVCPedidos.Models;
using MVCPedidos.Services;

namespace MVCPedidos.Controllers
{
    public class OrdenController : Controller
    {
        private readonly OrdenService _ordenService;

        public OrdenController(OrdenService ordenService)
        {
            _ordenService = ordenService;
        }
        public async Task<IActionResult> Index()
        {
            List<Orden> ordenes = await _ordenService.ListarOrdenes();
            return View(ordenes);
        }
    }
}
