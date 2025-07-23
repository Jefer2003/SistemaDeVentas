using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.DTOs.DetalleVenta
{
    public class DetalleVentaRequest
    {
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio")]
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "La venta es obligatoria")]
        public int VentaId { get; set; }
    }
} 