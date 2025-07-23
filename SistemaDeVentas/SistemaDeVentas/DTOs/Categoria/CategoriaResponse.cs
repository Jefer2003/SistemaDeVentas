namespace SistemaDeVentas.DTOs.Categoria
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
} 