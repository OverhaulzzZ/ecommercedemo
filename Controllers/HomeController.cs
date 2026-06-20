using System.Diagnostics;
using EcommerceDemo.Data;
using EcommerceDemo.Models;
using EcommerceDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDemo.Controllers;

public class HomeController : BaseController
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db, CarritoService carritoService) : base(carritoService)
        => _db = db;

    public async Task<IActionResult> Index()
    {
        var categorias = await _db.Categorias.ToListAsync();
        var destacados = await _db.Productos
            .Include(p => p.Categoria)
            .Where(p => p.Destacado && p.Activo)
            .Take(8)
            .ToListAsync();
        ViewBag.Categorias = categorias;
        ViewBag.Destacados = destacados;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
