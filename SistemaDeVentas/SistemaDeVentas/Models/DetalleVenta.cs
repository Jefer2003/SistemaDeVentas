using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVentas.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        
        public int Cantidad { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        
        // Foreign keys
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        
        // Navigation properties
        public virtual Venta Venta { get; set; } = null!;
        public virtual Producto Producto { get; set; } = null!;
    }
} 