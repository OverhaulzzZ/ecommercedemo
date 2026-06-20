using EcommerceDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceDemo.Controllers;

public class BaseController : Controller
{
    protected readonly CarritoService _carritoService;

    public BaseController(CarritoService carritoService) => _carritoService = carritoService;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewData["CantidadCarrito"] = _carritoService.CantidadTotal();
        base.OnActionExecuting(context);
    }
}
