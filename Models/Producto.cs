using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDemo.Models;

public class Producto
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; } = "";

    [MaxLength(1000)]
    public string Descripcion { get; set; } = "";

    [Required]
    public string SKU { get; set; } = "";

    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public string Marca { get; set; } = "";

    public string ImagenUrl { get; set; } = "/img/repuesto-default.png";

    public string CompatibilidadAutos { get; set; } = "";

    public bool Destacado { get; set; } = false;

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; }

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
