using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDemo.Models;

public class OrdenItem
{
    public int Id { get; set; }
    public int OrdenId { get; set; }
    public Orden? Orden { get; set; }

    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; } = "";
    public string ProductoSKU { get; set; } = "";

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecioUnitario { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
}
