using SistemaDeVentas.DTOs.Categoria;

namespace SistemaDeVentas.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponse>> GetAllAsync();
        Task<CategoriaResponse?> GetByIdAsync(int id);
        Task<CategoriaResponse> CreateAsync(CategoriaRequest categoriaRequest);
        Task<bool> UpdateAsync(int id, CategoriaRequest categoriaRequest);
        Task<bool> DeleteAsync(int id);
    }
} 