using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Data;
using SistemaDeVentas.DTOs.Venta;
using SistemaDeVentas.Models;
using AutoMapper;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Services
{
    public class VentaService : IVentaService
    {
        private readonly SistemaVentasContext _context;
        private readonly IMapper _mapper;

        public VentaService(SistemaVentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VentaResponse>> GetAllVentasAsync()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.DetallesVenta)
                    .ThenInclude(dv => dv.Producto)
                .OrderByDescending(v => v.FechaVenta)
                .ToListAsync();

            return _mapper.Map<List<VentaResponse>>(ventas);
        }

        public async Task<VentaResponse?> GetVentaByIdAsync(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.DetallesVenta)
                    .ThenInclude(dv => dv.Producto)
                .FirstOrDefaultAsync(v => v.Id == id);

            return venta == null ? null : _mapper.Map<VentaResponse>(venta);
        }

        public async Task<IEnumerable<VentaResponse>> GetVentasByUsuarioAsync(int usuarioId)
        {
            var ventas = await _context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.DetallesVenta)
                    .ThenInclude(dv => dv.Producto)
                .Where(v => v.UsuarioId == usuarioId)
                .OrderByDescending(v => v.FechaVenta)
                .ToListAsync();

            return _mapper.Map<List<VentaResponse>>(ventas);
        }

        public async Task<VentaResponse> CreateVentaAsync(VentaRequest ventaRequest)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // Validar que el usuario existe
                var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == ventaRequest.UsuarioId);
                if (!usuarioExiste)
                    throw new ArgumentException("El usuario especificado no existe");

                // Validar que hay productos en la venta
                if (ventaRequest.Detalles == null || !ventaRequest.Detalles.Any())
                    throw new ArgumentException("La venta debe tener al menos un producto");

                // Crear la venta
                var venta = new Venta
                {
                    NumeroVenta = GenerarNumeroVenta(),
                    FechaVenta = DateTime.Now,
                    UsuarioId = ventaRequest.UsuarioId,
                    Estado = "Completada",
                    Observaciones = ventaRequest.Observaciones,
                    Total = 0,
                };

                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                decimal totalVenta = 0;

                // Procesar cada detalle de la venta
                foreach (var detalleRequest in ventaRequest.Detalles)
                {
                    // Obtener el producto con su precio actual
                    var producto = await _context.Productos.FindAsync(detalleRequest.ProductoId);
                    if (producto == null)
                        throw new ArgumentException($"El producto con ID {detalleRequest.ProductoId} no existe");

                    // Validar stock
                    if (producto.Stock < detalleRequest.Cantidad)
                        throw new ArgumentException($"Stock insuficiente para el producto {producto.Nombre}. Disponible: {producto.Stock}");

                    // Calcular automáticamente el subtotal
                    var subtotal = producto.Precio * detalleRequest.Cantidad;

                    // Crear el detalle de venta con precios calculados automáticamente
                    var detalleVenta = new DetalleVenta
                    {
                        VentaId = venta.Id,
                        ProductoId = detalleRequest.ProductoId,
                        Cantidad = detalleRequest.Cantidad,
                        PrecioUnitario = producto.Precio, // Precio actual del producto
                        Subtotal = subtotal // Calculado automáticamente
                    };

                    _context.DetallesVenta.Add(detalleVenta);

                    // Actualizar stock del producto
                    producto.Stock -= detalleRequest.Cantidad;

                    totalVenta += subtotal;
                }

                // Actualizar el total de la venta (calculado automáticamente)
                venta.Total = totalVenta;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Retornar la venta creada con todos sus detalles
                return await GetVentaByIdAsync(venta.Id) ?? throw new InvalidOperationException("Error al obtener la venta creada");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateEstadoVentaAsync(int id, string estado)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return false;

            venta.Estado = estado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVentaAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var venta = await _context.Ventas
                    .Include(v => v.DetallesVenta)
                    .FirstOrDefaultAsync(v => v.Id == id);

                if (venta == null) return false;

                // Restaurar stock de los productos
                foreach (var detalle in venta.DetallesVenta)
                {
                    var producto = await _context.Productos.FindAsync(detalle.ProductoId);
                    if (producto != null)
                    {
                        producto.Stock += detalle.Cantidad;
                    }
                }

                // Eliminar la venta (los detalles se eliminan en cascada)
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public string GenerarNumeroVenta()
        {
            var fecha = DateTime.Now.ToString("yyyyMMdd");
            var ultimaVenta = _context.Ventas
                .Where(v => v.NumeroVenta.StartsWith($"V{fecha}"))
                .OrderByDescending(v => v.NumeroVenta)
                .FirstOrDefault();

            int siguienteNumero = 1;
            if (ultimaVenta != null)
            {
                var ultimoNumero = ultimaVenta.NumeroVenta.Substring(9); // V20241222 + número
                if (int.TryParse(ultimoNumero, out int numero))
                    siguienteNumero = numero + 1;
            }

            return $"V{fecha}{siguienteNumero:D4}";
        }
    }
} 