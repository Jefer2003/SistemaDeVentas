using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVentas.Models
{
    public class Venta
    {
        public int Id { get; set; }
        
        [Required]
        public string NumeroVenta { get; set; } = string.Empty;
        
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        
        [MaxLength(20)]
        public string Estado { get; set; } = "Completada"; // Completada, Pendiente, Cancelada
        
        [MaxLength(500)]
        public string? Observaciones { get; set; }
        
        // Foreign key
        public int UsuarioId { get; set; }
        
        // Navigation properties
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
    }
} 