namespace SistemaDeVentas.DTOs.DetalleVenta
{
    public class DetalleVentaResponse
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int VentaId { get; set; }
    }
} 