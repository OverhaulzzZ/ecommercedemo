using EcommerceDemo.Data;
using EcommerceDemo.Models;
using EcommerceDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDemo.Controllers;

public class CheckoutController : BaseController
{
    private readonly AppDbContext _db;

    public CheckoutController(AppDbContext db, CarritoService carritoService) : base(carritoService)
        => _db = db;

    public IActionResult Index()
    {
        var items = _carritoService.ObtenerCarrito();
        if (!items.Any()) return RedirectToAction("Index", "Carrito");
        ViewBag.Items = items;
        ViewBag.Total = _carritoService.Total();
        return View(new Orden());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirmar(Orden orden)
    {
        var items = _carritoService.ObtenerCarrito();
        if (!items.Any()) return RedirectToAction("Index", "Carrito");

        if (!ModelState.IsValid)
        {
            ViewBag.Items = items;
            ViewBag.Total = _carritoService.Total();
            return View("Index", orden);
        }

        orden.NumeroOrden = $"ORD-{DateTime.Now:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";
        orden.Fecha = DateTime.Now;
        orden.Total = _carritoService.Total();
        orden.Estado = EstadoOrden.Pendiente;

        foreach (var item in items)
        {
            orden.Items.Add(new OrdenItem
            {
                ProductoId = item.ProductoId,
                ProductoNombre = item.Nombre,
                PrecioUnitario = item.Precio,
                Cantidad = item.Cantidad,
                Subtotal = item.Subtotal
            });

            var producto = await _db.Productos.FindAsync(item.ProductoId);
            if (producto != null && producto.Stock >= item.Cantidad)
                producto.Stock -= item.Cantidad;
        }

        _db.Ordenes.Add(orden);
        await _db.SaveChangesAsync();
        _carritoService.Limpiar();

        return RedirectToAction("Confirmacion", new { id = orden.Id });
    }

    public async Task<IActionResult> Confirmacion(int id)
    {
        var orden = await _db.Ordenes.FindAsync(id);
        if (orden == null) return NotFound();

        var items = await _db.OrdenItems.Where(i => i.OrdenId == id).ToListAsync();
        ViewBag.Items = items;
        return View(orden);
    }
}
