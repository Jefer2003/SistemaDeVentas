using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Descripcion { get; set; }
        
        public bool Estado { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
} 