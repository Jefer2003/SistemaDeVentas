using SistemaDeVentas.DTOs.DetalleVenta;

namespace SistemaDeVentas.DTOs.Venta
{
    public class VentaResponse
    {
        public int Id { get; set; }
        public string NumeroVenta { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public List<DetalleVentaResponse> Detalles { get; set; } = new();
        public DateTime FechaCreacion { get; set; }
    }
} 