using EcommerceDemo.Data;
using EcommerceDemo.Models;
using EcommerceDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDemo.Controllers;

public class AdminController : BaseController
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db, CarritoService carritoService) : base(carritoService)
        => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalProductos = await _db.Productos.CountAsync(p => p.Activo);
        ViewBag.TotalOrdenes = await _db.Ordenes.CountAsync();
        ViewBag.OrdenesHoy = await _db.Ordenes.CountAsync(o => o.Fecha.Date == DateTime.Today);
        ViewBag.VentasHoy = await _db.Ordenes.Where(o => o.Fecha.Date == DateTime.Today).SumAsync(o => (decimal?)o.Total) ?? 0m;
        ViewBag.VentasTotal = await _db.Ordenes.SumAsync(o => (decimal?)o.Total) ?? 0m;
        ViewBag.StockBajo = await _db.Productos.CountAsync(p => p.Stock < 10 && p.Activo);
        ViewBag.UltimasOrdenes = await _db.Ordenes.OrderByDescending(o => o.Fecha).Take(5).ToListAsync();
        ViewBag.ProductosStockBajo = await _db.Productos.Where(p => p.Stock < 10 && p.Activo).OrderBy(p => p.Stock).Take(5).ToListAsync();
        return View();
    }

    // ── Productos ──────────────────────────────────────────────────────────────

    public async Task<IActionResult> Productos(string? q)
    {
        var query = _db.Productos.Include(p => p.Categoria).AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.Nombre.Contains(q) || p.SKU.Contains(q) || p.Marca.Contains(q));
        ViewBag.Busqueda = q;
        return View(await query.OrderBy(p => p.Nombre).ToListAsync());
    }

    public async Task<IActionResult> CrearProducto()
    {
        ViewBag.Categorias = await _db.Categorias.ToListAsync();
        return View(new Producto());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearProducto(Producto producto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = await _db.Categorias.ToListAsync();
            return View(producto);
        }
        producto.FechaCreacion = DateTime.Now;
        _db.Productos.Add(producto);
        await _db.SaveChangesAsync();
        TempData["Ok"] = $"Producto '{producto.Nombre}' creado exitosamente.";
        return RedirectToAction(nameof(Productos));
    }

    public async Task<IActionResult> EditarProducto(int id)
    {
        var producto = await _db.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        ViewBag.Categorias = await _db.Categorias.ToListAsync();
        return View(producto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarProducto(Producto producto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = await _db.Categorias.ToListAsync();
            return View(producto);
        }
        _db.Productos.Update(producto);
        await _db.SaveChangesAsync();
        TempData["Ok"] = $"Producto '{producto.Nombre}' actualizado.";
        return RedirectToAction(nameof(Productos));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarProducto(int id)
    {
        var producto = await _db.Productos.FindAsync(id);
        if (producto != null) { producto.Activo = false; await _db.SaveChangesAsync(); }
        TempData["Ok"] = "Producto desactivado.";
        return RedirectToAction(nameof(Productos));
    }

    // ── Órdenes ────────────────────────────────────────────────────────────────

    public async Task<IActionResult> Ordenes(string? estado)
    {
        var query = _db.Ordenes.AsQueryable();
        if (!string.IsNullOrWhiteSpace(estado) && Enum.TryParse<EstadoOrden>(estado, out var e))
            query = query.Where(o => o.Estado == e);
        ViewBag.EstadoFiltro = estado;
        return View(await query.OrderByDescending(o => o.Fecha).ToListAsync());
    }

    public async Task<IActionResult> DetalleOrden(int id)
    {
        var orden = await _db.Ordenes.FindAsync(id);
        if (orden == null) return NotFound();
        ViewBag.Items = await _db.OrdenItems.Where(i => i.OrdenId == id).ToListAsync();
        return View(orden);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(int id, EstadoOrden estado)
    {
        var orden = await _db.Ordenes.FindAsync(id);
        if (orden != null) { orden.Estado = estado; await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(DetalleOrden), new { id });
    }
}
