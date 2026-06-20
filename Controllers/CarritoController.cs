using EcommerceDemo.Data;
using EcommerceDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDemo.Controllers;

public class CarritoController : BaseController
{
    private readonly AppDbContext _db;

    public CarritoController(AppDbContext db, CarritoService carritoService) : base(carritoService)
        => _db = db;

    public IActionResult Index()
    {
        var items = _carritoService.ObtenerCarrito();
        ViewBag.Total = _carritoService.Total();
        return View(items);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar(int productoId, int cantidad = 1)
    {
        var producto = await _db.Productos.FindAsync(productoId);
        if (producto != null)
            _carritoService.Agregar(producto, cantidad);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Actualizar(int productoId, int cantidad)
    {
        _carritoService.ActualizarCantidad(productoId, cantidad);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Eliminar(int productoId)
    {
        _carritoService.Eliminar(productoId);
        return RedirectToAction("Index");
    }
}
