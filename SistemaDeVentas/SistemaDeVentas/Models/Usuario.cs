using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? Direccion { get; set; }
        
        public bool Estado { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
} 