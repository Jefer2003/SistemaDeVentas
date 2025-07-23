using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.DTOs.Usuario
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede tener más de 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres")]
        public string? Direccion { get; set; }
    }
} 