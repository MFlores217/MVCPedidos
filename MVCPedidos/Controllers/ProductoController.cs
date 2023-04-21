using Microsoft.AspNetCore.Mvc;
using MVCPedidos.Models;
using MVCPedidos.Services;

namespace MVCPedidos.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;
        public ProductoController(ProductoService service)
        {
            _productoService= service;
        }
        public async Task<IActionResult> Index()
        {
            List<Producto> productos = await _productoService.ListarProductos();
            return View(productos);
        }
        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nuevo(Producto producto) {
            bool resultado;
            if (ModelState.IsValid){
                resultado = await _productoService.Guardar(producto);
                if (resultado){
                    return RedirectToAction("Index");
                } else {
                    return BadRequest("Error al agregar el producto");
                }
            } else {
                return View(producto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar (int? id)
        {
            ViewData["Mensaje"] = string.Empty;
            if(id != null)
            {
                Producto producto = await _productoService.ObtenerProducto(id);
                if (producto != null){
                    return View(producto);
                } else {
                    return BadRequest("El producto no se encuentra");
                }
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            ViewData["Mensaje"] = string.Empty;
            int resultado;
            if (ModelState.IsValid)
            {
                resultado = await _productoService.Eliminar(id);
                if (resultado == 1 || resultado == 0)
                {
                    return RedirectToAction("Index");
                }
                else if (resultado == 0)
                {
                    ViewData["Mensaje"] = "No se puede eliminar el producto ya que tiene pedidos asociados";
                    return View(await _productoService.ObtenerProducto(id));
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int? id)
        {
            if (id != null)
            {
                Producto producto = await _productoService.ObtenerProducto(id);
                if (producto != null)
                {
                    return View(producto);
                } else
                {
                    return BadRequest("El producto no se encuentra");
                }
            } else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (await _productoService.Modificar(producto))
                {
                    return RedirectToAction("Index");
                } else {
                    return BadRequest("Error al modificar el producto");
                }
            }
            return View(producto);
        }

    }
}
