using SistemaDeVentas.DTOs.DetalleVenta;

namespace SistemaDeVentas.Interfaces
{
    public interface IDetalleVentaService
    {
        Task<IEnumerable<DetalleVentaResponse>> GetAllAsync();
        Task<DetalleVentaResponse?> GetByIdAsync(int id);
        Task<IEnumerable<DetalleVentaResponse>> GetByVentaAsync(int ventaId);
        Task<IEnumerable<DetalleVentaResponse>> GetByProductoAsync(int productoId);
        Task<DetalleVentaResponse> CreateAsync(DetalleVentaRequest detalleRequest);
        Task<bool> UpdateAsync(int id, DetalleVentaRequest detalleRequest);
        Task<bool> DeleteAsync(int id);
    }
} 