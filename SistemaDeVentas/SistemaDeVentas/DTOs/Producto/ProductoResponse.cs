namespace SistemaDeVentas.DTOs.Producto
{
    public class ProductoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
} 