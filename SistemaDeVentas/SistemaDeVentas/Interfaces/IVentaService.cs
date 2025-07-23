using SistemaDeVentas.DTOs.Venta;

namespace SistemaDeVentas.Interfaces
{
    public interface IVentaService
    {
        Task<IEnumerable<VentaResponse>> GetAllVentasAsync();
        Task<VentaResponse?> GetVentaByIdAsync(int id);
        Task<IEnumerable<VentaResponse>> GetVentasByUsuarioAsync(int usuarioId);
        Task<VentaResponse> CreateVentaAsync(VentaRequest ventaRequest);
        Task<bool> UpdateEstadoVentaAsync(int id, string estado);
        Task<bool> DeleteVentaAsync(int id);
        string GenerarNumeroVenta();
    }
} 