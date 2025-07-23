using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVentas.Models
{
    public class Producto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Descripcion { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        
        public int Stock { get; set; }
        
        public bool Estado { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Foreign key
        public int CategoriaId { get; set; }
        
        // Navigation properties
        public virtual Categoria Categoria { get; set; } = null!;
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
    }
} 