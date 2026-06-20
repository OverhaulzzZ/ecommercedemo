using EcommerceDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<OrdenItem> OrdenItems => Set<OrdenItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Motor", Icono = "fa-engine" },
            new Categoria { Id = 2, Nombre = "Frenos", Icono = "fa-circle-stop" },
            new Categoria { Id = 3, Nombre = "Suspensión", Icono = "fa-car-side" },
            new Categoria { Id = 4, Nombre = "Eléctrico", Icono = "fa-bolt" },
            new Categoria { Id = 5, Nombre = "Filtros", Icono = "fa-filter" },
            new Categoria { Id = 6, Nombre = "Transmisión", Icono = "fa-gears" }
        );

        modelBuilder.Entity<Producto>().HasData(
            // Motor
            new Producto { Id = 1, Nombre = "Filtro de Aceite Bosch", SKU = "BOC-FIL-001", Precio = 8.50m, Stock = 150, Marca = "Bosch", CategoriaId = 1, Descripcion = "Filtro de aceite de alta eficiencia para motores gasolina y diésel. Compatible con múltiples marcas.", CompatibilidadAutos = "Toyota Corolla 2015-2023, Honda Civic 2014-2022, Nissan Sentra 2016-2023", Destacado = true, ImagenUrl = "/img/filtro-aceite.png" },
            new Producto { Id = 2, Nombre = "Bujía NGK Iridium IX", SKU = "NGK-BUJ-002", Precio = 12.75m, Stock = 200, Marca = "NGK", CategoriaId = 1, Descripcion = "Bujía de iridio de larga duración con electrodo ultrafino para máxima eficiencia de combustión.", CompatibilidadAutos = "Toyota Camry 2012-2023, Chevrolet Aveo 2011-2021, Hyundai Elantra 2013-2023", Destacado = true, ImagenUrl = "/img/bujia.png" },
            new Producto { Id = 3, Nombre = "Correa de Distribución Gates", SKU = "GAT-COR-003", Precio = 45.00m, Stock = 60, Marca = "Gates", CategoriaId = 1, Descripcion = "Correa de distribución reforzada con fibra de vidrio. Resistente al calor y al aceite.", CompatibilidadAutos = "Volkswagen Jetta 2011-2019, Audi A3 2013-2020", Destacado = false, ImagenUrl = "/img/correa.png" },
            new Producto { Id = 4, Nombre = "Aceite Motor Mobil 1 5W-30 1L", SKU = "MOB-ACE-004", Precio = 18.90m, Stock = 300, Marca = "Mobil 1", CategoriaId = 1, Descripcion = "Aceite de motor sintético completo para protección extrema y rendimiento máximo.", CompatibilidadAutos = "Universal - todos los vehículos a gasolina", Destacado = true, ImagenUrl = "/img/aceite.png" },
            // Frenos
            new Producto { Id = 5, Nombre = "Pastillas de Freno Brembo Delanteras", SKU = "BRE-PAS-005", Precio = 35.00m, Stock = 80, Marca = "Brembo", CategoriaId = 2, Descripcion = "Pastillas de freno de alto rendimiento con baja emisión de polvo y nula chirriación.", CompatibilidadAutos = "Toyota Corolla 2014-2023, Honda Civic 2016-2023, Mazda 3 2014-2023", Destacado = true, ImagenUrl = "/img/pastillas.png" },
            new Producto { Id = 6, Nombre = "Disco de Freno Ventilado", SKU = "ATE-DIS-006", Precio = 55.00m, Stock = 40, Marca = "ATE", CategoriaId = 2, Descripcion = "Disco de freno ventilado de alta performance con ranuras para mejor disipación de calor.", CompatibilidadAutos = "Chevrolet Cruze 2011-2019, Kia Sportage 2011-2021", Destacado = false, ImagenUrl = "/img/disco.png" },
            new Producto { Id = 7, Nombre = "Líquido de Frenos DOT4 500ml", SKU = "MOT-LIQ-007", Precio = 9.50m, Stock = 120, Marca = "Motul", CategoriaId = 2, Descripcion = "Líquido de frenos DOT4 con alto punto de ebullición para frenado seguro en condiciones extremas.", CompatibilidadAutos = "Universal - todos los vehículos", Destacado = false, ImagenUrl = "/img/liquido-frenos.png" },
            // Suspensión
            new Producto { Id = 8, Nombre = "Amortiguador Monroe Delantero", SKU = "MON-AMO-008", Precio = 65.00m, Stock = 50, Marca = "Monroe", CategoriaId = 3, Descripcion = "Amortiguador de gas de alta presión para conducción suave y estable en todo tipo de terreno.", CompatibilidadAutos = "Nissan Frontier 2005-2021, Toyota Hilux 2005-2021", Destacado = true, ImagenUrl = "/img/amortiguador.png" },
            new Producto { Id = 9, Nombre = "Rótula Inferior Moog", SKU = "MOO-ROT-009", Precio = 28.00m, Stock = 70, Marca = "Moog", CategoriaId = 3, Descripcion = "Rótula de suspensión inferior con grasa de por vida y bota reforzada anti-polvo.", CompatibilidadAutos = "Ford Focus 2012-2018, Chevrolet Onix 2013-2022", Destacado = false, ImagenUrl = "/img/rotula.png" },
            // Eléctrico
            new Producto { Id = 10, Nombre = "Batería Bosch 60Ah 12V", SKU = "BOC-BAT-010", Precio = 95.00m, Stock = 35, Marca = "Bosch", CategoriaId = 4, Descripcion = "Batería de arranque libre de mantenimiento con tecnología AGM para alta demanda eléctrica.", CompatibilidadAutos = "Universal - vehículos con sistema de 12V", Destacado = true, ImagenUrl = "/img/bateria.png" },
            new Producto { Id = 11, Nombre = "Alternador Remy 90A", SKU = "REM-ALT-011", Precio = 120.00m, Stock = 20, Marca = "Remy", CategoriaId = 4, Descripcion = "Alternador remanufacturado de 90 amperios con garantía de 12 meses.", CompatibilidadAutos = "Toyota Corolla 2009-2019, Honda Civic 2009-2015", Destacado = false, ImagenUrl = "/img/alternador.png" },
            // Filtros
            new Producto { Id = 12, Nombre = "Filtro de Aire K&N", SKU = "KN-FIL-012", Precio = 55.00m, Stock = 45, Marca = "K&N", CategoriaId = 5, Descripcion = "Filtro de aire lavable y reutilizable que incrementa el flujo de aire hasta 50% vs filtros estándar.", CompatibilidadAutos = "Universal - con kit de adaptación incluido", Destacado = true, ImagenUrl = "/img/filtro-aire.png" },
            new Producto { Id = 13, Nombre = "Filtro de Combustible Mann", SKU = "MAN-FIL-013", Precio = 15.00m, Stock = 90, Marca = "Mann", CategoriaId = 5, Descripcion = "Filtro de combustible con elemento filtrante de alta capacidad para proteger la bomba de inyección.", CompatibilidadAutos = "Volkswagen Polo 2010-2022, Seat Ibiza 2010-2022", Destacado = false, ImagenUrl = "/img/filtro-combustible.png" },
            // Transmisión
            new Producto { Id = 14, Nombre = "Kit de Embrague LUK", SKU = "LUK-KIT-014", Precio = 185.00m, Stock = 15, Marca = "LUK", CategoriaId = 6, Descripcion = "Kit completo de embrague: disco, plato y rodamiento de empuje. Instalación directa OEM.", CompatibilidadAutos = "Renault Sandero 2013-2022, Dacia Logan 2013-2022", Destacado = false, ImagenUrl = "/img/embrague.png" },
            new Producto { Id = 15, Nombre = "Aceite Caja Manual 75W-90 1L", SKU = "MOT-CAJ-015", Precio = 22.00m, Stock = 85, Marca = "Motul", CategoriaId = 6, Descripcion = "Aceite sintético para cajas de cambio manuales y ejes traseros. API GL-4/GL-5.", CompatibilidadAutos = "Universal - cajas manuales", Destacado = false, ImagenUrl = "/img/aceite-caja.png" }
        );
    }
}
