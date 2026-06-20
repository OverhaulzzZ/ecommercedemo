using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDemo.Models;

public class Orden
{
    public int Id { get; set; }

    public string NumeroOrden { get; set; } = "";

    public DateTime Fecha { get; set; }

    [Required]
    public string ClienteNombre { get; set; } = "";

    [Required, EmailAddress]
    public string ClienteEmail { get; set; } = "";

    [Required]
    public string ClienteTelefono { get; set; } = "";

    public string ClienteDireccion { get; set; } = "";

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;

    public string Notas { get; set; } = "";

    public ICollection<OrdenItem> Items { get; set; } = new List<OrdenItem>();
}

public enum EstadoOrden
{
    Pendiente,
    Confirmada,
    EnProceso,
    Enviada,
    Entregada,
    Cancelada
}
