using SistemaDeVentas.DTOs.Producto;

namespace SistemaDeVentas.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoResponse>> GetAllAsync();
        Task<ProductoResponse?> GetByIdAsync(int id);
        Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int categoriaId);
        Task<ProductoResponse> CreateAsync(ProductoRequest productoRequest);
        Task<bool> UpdateAsync(int id, ProductoRequest productoRequest);
        Task<bool> DeleteAsync(int id);
    }
} 