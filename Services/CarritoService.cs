using System.Text.Json;
using EcommerceDemo.Models;

namespace EcommerceDemo.Services;

public class CarritoService
{
    private readonly IHttpContextAccessor _http;
    private const string SessionKey = "Carrito";

    public CarritoService(IHttpContextAccessor http) => _http = http;

    public List<CarritoItem> ObtenerCarrito()
    {
        var session = _http.HttpContext?.Session;
        var json = session?.GetString(SessionKey);
        return json == null ? new List<CarritoItem>() : JsonSerializer.Deserialize<List<CarritoItem>>(json)!;
    }

    public void Agregar(Producto producto, int cantidad = 1)
    {
        var carrito = ObtenerCarrito();
        var item = carrito.FirstOrDefault(c => c.ProductoId == producto.Id);
        if (item != null)
            item.Cantidad += cantidad;
        else
            carrito.Add(new CarritoItem { ProductoId = producto.Id, Nombre = producto.Nombre, Precio = producto.Precio, Cantidad = cantidad, ImagenUrl = producto.ImagenUrl });
        Guardar(carrito);
    }

    public void ActualizarCantidad(int productoId, int cantidad)
    {
        var carrito = ObtenerCarrito();
        var item = carrito.FirstOrDefault(c => c.ProductoId == productoId);
        if (item != null) { if (cantidad <= 0) carrito.Remove(item); else item.Cantidad = cantidad; }
        Guardar(carrito);
    }

    public void Eliminar(int productoId)
    {
        var carrito = ObtenerCarrito();
        carrito.RemoveAll(c => c.ProductoId == productoId);
        Guardar(carrito);
    }

    public void Limpiar() => _http.HttpContext?.Session.Remove(SessionKey);

    public int CantidadTotal() => ObtenerCarrito().Sum(c => c.Cantidad);

    public decimal Total() => ObtenerCarrito().Sum(c => c.Subtotal);

    private void Guardar(List<CarritoItem> carrito) =>
        _http.HttpContext?.Session.SetString(SessionKey, JsonSerializer.Serialize(carrito));
}
