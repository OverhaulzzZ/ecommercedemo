using EcommerceDemo.Data;
using EcommerceDemo.Models;
using EcommerceDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDemo.Controllers;

public class CatalogoController : BaseController
{
    private readonly AppDbContext _db;

    public CatalogoController(AppDbContext db, CarritoService carritoService) : base(carritoService)
        => _db = db;

    public async Task<IActionResult> Index(string? q, int? categoriaId, string? marca, string? orden)
    {
        var query = _db.Productos.Include(p => p.Categoria).Where(p => p.Activo).AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(p => p.Nombre.Contains(q) || p.Marca.Contains(q) ||
                                     p.Descripcion.Contains(q) || p.CompatibilidadAutos.Contains(q));
            ViewData["BusquedaActual"] = q;
        }
        if (categoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == categoriaId);
        if (!string.IsNullOrWhiteSpace(marca))
            query = query.Where(p => p.Marca == marca);

        query = orden switch
        {
            "precio_asc" => query.OrderBy(p => p.Precio),
            "precio_desc" => query.OrderByDescending(p => p.Precio),
            "nombre" => query.OrderBy(p => p.Nombre),
            _ => query.OrderByDescending(p => p.Destacado).ThenBy(p => p.Nombre)
        };

        var productos = await query.ToListAsync();
        var categorias = await _db.Categorias.ToListAsync();
        var marcas = await _db.Productos.Where(p => p.Activo).Select(p => p.Marca).Distinct().OrderBy(m => m).ToListAsync();

        ViewBag.Categorias = categorias;
        ViewBag.Marcas = marcas;
        ViewBag.CategoriaSeleccionada = categoriaId;
        ViewBag.MarcaSeleccionada = marca;
        ViewBag.OrdenSeleccionado = orden;
        ViewBag.Busqueda = q;

        return View(productos);
    }

    public async Task<IActionResult> Detalle(int id)
    {
        var producto = await _db.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        if (producto == null) return NotFound();

        var relacionados = await _db.Productos
            .Where(p => p.CategoriaId == producto.CategoriaId && p.Id != id && p.Activo)
            .Take(4).ToListAsync();

        ViewBag.Relacionados = relacionados;
        return View(producto);
    }
}
