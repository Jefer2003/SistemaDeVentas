using SistemaDeVentas.DTOs.Usuario;

namespace SistemaDeVentas.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponse>> GetAllAsync();
        Task<UsuarioResponse?> GetByIdAsync(int id);
        Task<UsuarioResponse> CreateAsync(UsuarioRequest usuarioRequest);
        Task<bool> UpdateAsync(int id, UsuarioRequest usuarioRequest);
        Task<bool> DeleteAsync(int id);
    }
} 