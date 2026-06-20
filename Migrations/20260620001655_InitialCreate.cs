using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Icono = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroOrden = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClienteNombre = table.Column<string>(type: "TEXT", nullable: false),
                    ClienteEmail = table.Column<string>(type: "TEXT", nullable: false),
                    ClienteTelefono = table.Column<string>(type: "TEXT", nullable: false),
                    ClienteDireccion = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    Notas = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    SKU = table.Column<string>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Marca = table.Column<string>(type: "TEXT", nullable: false),
                    ImagenUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CompatibilidadAutos = table.Column<string>(type: "TEXT", nullable: false),
                    Destacado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrdenId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoNombre = table.Column<string>(type: "TEXT", nullable: false),
                    ProductoSKU = table.Column<string>(type: "TEXT", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenItems_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Icono", "Nombre" },
                values: new object[,]
                {
                    { 1, "fa-engine", "Motor" },
                    { 2, "fa-circle-stop", "Frenos" },
                    { 3, "fa-car-side", "Suspensión" },
                    { 4, "fa-bolt", "Eléctrico" },
                    { 5, "fa-filter", "Filtros" },
                    { 6, "fa-gears", "Transmisión" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Activo", "CategoriaId", "CompatibilidadAutos", "Descripcion", "Destacado", "FechaCreacion", "ImagenUrl", "Marca", "Nombre", "Precio", "SKU", "Stock" },
                values: new object[,]
                {
                    { 1, true, 1, "Toyota Corolla 2015-2023, Honda Civic 2014-2022, Nissan Sentra 2016-2023", "Filtro de aceite de alta eficiencia para motores gasolina y diésel. Compatible con múltiples marcas.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/filtro-aceite.png", "Bosch", "Filtro de Aceite Bosch", 8.50m, "BOC-FIL-001", 150 },
                    { 2, true, 1, "Toyota Camry 2012-2023, Chevrolet Aveo 2011-2021, Hyundai Elantra 2013-2023", "Bujía de iridio de larga duración con electrodo ultrafino para máxima eficiencia de combustión.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/bujia.png", "NGK", "Bujía NGK Iridium IX", 12.75m, "NGK-BUJ-002", 200 },
                    { 3, true, 1, "Volkswagen Jetta 2011-2019, Audi A3 2013-2020", "Correa de distribución reforzada con fibra de vidrio. Resistente al calor y al aceite.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/correa.png", "Gates", "Correa de Distribución Gates", 45.00m, "GAT-COR-003", 60 },
                    { 4, true, 1, "Universal - todos los vehículos a gasolina", "Aceite de motor sintético completo para protección extrema y rendimiento máximo.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/aceite.png", "Mobil 1", "Aceite Motor Mobil 1 5W-30 1L", 18.90m, "MOB-ACE-004", 300 },
                    { 5, true, 2, "Toyota Corolla 2014-2023, Honda Civic 2016-2023, Mazda 3 2014-2023", "Pastillas de freno de alto rendimiento con baja emisión de polvo y nula chirriación.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/pastillas.png", "Brembo", "Pastillas de Freno Brembo Delanteras", 35.00m, "BRE-PAS-005", 80 },
                    { 6, true, 2, "Chevrolet Cruze 2011-2019, Kia Sportage 2011-2021", "Disco de freno ventilado de alta performance con ranuras para mejor disipación de calor.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/disco.png", "ATE", "Disco de Freno Ventilado", 55.00m, "ATE-DIS-006", 40 },
                    { 7, true, 2, "Universal - todos los vehículos", "Líquido de frenos DOT4 con alto punto de ebullición para frenado seguro en condiciones extremas.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/liquido-frenos.png", "Motul", "Líquido de Frenos DOT4 500ml", 9.50m, "MOT-LIQ-007", 120 },
                    { 8, true, 3, "Nissan Frontier 2005-2021, Toyota Hilux 2005-2021", "Amortiguador de gas de alta presión para conducción suave y estable en todo tipo de terreno.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/amortiguador.png", "Monroe", "Amortiguador Monroe Delantero", 65.00m, "MON-AMO-008", 50 },
                    { 9, true, 3, "Ford Focus 2012-2018, Chevrolet Onix 2013-2022", "Rótula de suspensión inferior con grasa de por vida y bota reforzada anti-polvo.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/rotula.png", "Moog", "Rótula Inferior Moog", 28.00m, "MOO-ROT-009", 70 },
                    { 10, true, 4, "Universal - vehículos con sistema de 12V", "Batería de arranque libre de mantenimiento con tecnología AGM para alta demanda eléctrica.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/bateria.png", "Bosch", "Batería Bosch 60Ah 12V", 95.00m, "BOC-BAT-010", 35 },
                    { 11, true, 4, "Toyota Corolla 2009-2019, Honda Civic 2009-2015", "Alternador remanufacturado de 90 amperios con garantía de 12 meses.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/alternador.png", "Remy", "Alternador Remy 90A", 120.00m, "REM-ALT-011", 20 },
                    { 12, true, 5, "Universal - con kit de adaptación incluido", "Filtro de aire lavable y reutilizable que incrementa el flujo de aire hasta 50% vs filtros estándar.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/filtro-aire.png", "K&N", "Filtro de Aire K&N", 55.00m, "KN-FIL-012", 45 },
                    { 13, true, 5, "Volkswagen Polo 2010-2022, Seat Ibiza 2010-2022", "Filtro de combustible con elemento filtrante de alta capacidad para proteger la bomba de inyección.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/filtro-combustible.png", "Mann", "Filtro de Combustible Mann", 15.00m, "MAN-FIL-013", 90 },
                    { 14, true, 6, "Renault Sandero 2013-2022, Dacia Logan 2013-2022", "Kit completo de embrague: disco, plato y rodamiento de empuje. Instalación directa OEM.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/embrague.png", "LUK", "Kit de Embrague LUK", 185.00m, "LUK-KIT-014", 15 },
                    { 15, true, 6, "Universal - cajas manuales", "Aceite sintético para cajas de cambio manuales y ejes traseros. API GL-4/GL-5.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/aceite-caja.png", "Motul", "Aceite Caja Manual 75W-90 1L", 22.00m, "MOT-CAJ-015", 85 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItems_OrdenId",
                table: "OrdenItems",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenItems");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
