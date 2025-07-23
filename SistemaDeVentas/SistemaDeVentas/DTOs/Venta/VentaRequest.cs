using System.ComponentModel.DataAnnotations;
using SistemaDeVentas.DTOs.DetalleVenta;

namespace SistemaDeVentas.DTOs.Venta
{
    public class VentaRequest
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden tener más de 500 caracteres")]
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "Los detalles de la venta son obligatorios")]
        [MinLength(1, ErrorMessage = "Debe tener al menos un producto en la venta")]
        public List<DetalleVentaRequest> Detalles { get; set; } = new();
    }
} 