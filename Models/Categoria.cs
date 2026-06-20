using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = "";

    public string Icono { get; set; } = "fa-cog";

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
