using SistemaDeVentas.DTOs.DetalleVenta;
using SistemaDeVentas.Models;
using SistemaDeVentas.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Services
{
    public class DetalleVentaService : IDetalleVentaService
    {
        private readonly SistemaVentasContext _context;
        private readonly IMapper _mapper;

        public DetalleVentaService(SistemaVentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetalleVentaResponse>> GetAllAsync()
        {
            var detalles = await _context.DetallesVenta.Include(d => d.Producto).Include(d => d.Venta).ToListAsync();
            return _mapper.Map<List<DetalleVentaResponse>>(detalles);
        }

        public async Task<DetalleVentaResponse?> GetByIdAsync(int id)
        {
            var detalle = await _context.DetallesVenta.Include(d => d.Producto).Include(d => d.Venta).FirstOrDefaultAsync(d => d.Id == id);
            return detalle == null ? null : _mapper.Map<DetalleVentaResponse>(detalle);
        }

        public async Task<IEnumerable<DetalleVentaResponse>> GetByVentaAsync(int ventaId)
        {
            var detalles = await _context.DetallesVenta.Include(d => d.Producto).Where(d => d.VentaId == ventaId).ToListAsync();
            return _mapper.Map<List<DetalleVentaResponse>>(detalles);
        }

        public async Task<IEnumerable<DetalleVentaResponse>> GetByProductoAsync(int productoId)
        {
            var detalles = await _context.DetallesVenta.Include(d => d.Venta).Where(d => d.ProductoId == productoId).ToListAsync();
            return _mapper.Map<List<DetalleVentaResponse>>(detalles);
        }

        public async Task<DetalleVentaResponse> CreateAsync(DetalleVentaRequest detalleRequest)
        {
            // Obtener el producto para calcular precios automáticamente
            var producto = await _context.Productos.FindAsync(detalleRequest.ProductoId);
            if (producto == null)
                throw new ArgumentException($"El producto con ID {detalleRequest.ProductoId} no existe");

            // Validar stock
            if (producto.Stock < detalleRequest.Cantidad)
                throw new ArgumentException($"Stock insuficiente para el producto {producto.Nombre}. Disponible: {producto.Stock}");

            // Calcular automáticamente el subtotal
            var subtotal = producto.Precio * detalleRequest.Cantidad;

            // Crear el detalle de venta con precios calculados automáticamente
            var detalle = new DetalleVenta
            {
                VentaId = detalleRequest.VentaId,
                ProductoId = detalleRequest.ProductoId,
                Cantidad = detalleRequest.Cantidad,
                PrecioUnitario = producto.Precio, // Precio actual del producto
                Subtotal = subtotal // Calculado automáticamente
            };

            _context.DetallesVenta.Add(detalle);
            await _context.SaveChangesAsync();

            // En CreateAsync, UpdateAsync y DeleteAsync después de guardar el detalle:
            var venta = await _context.Ventas.Include(v => v.DetallesVenta).FirstOrDefaultAsync(v => v.Id == detalle.VentaId);
            if (venta != null)
            {
                venta.Total = venta.DetallesVenta.Sum(d => d.Subtotal);
                venta.FechaVenta = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<DetalleVentaResponse>(detalle);
        }

        public async Task<bool> UpdateAsync(int id, DetalleVentaRequest detalleRequest)
        {
            var detalle = await _context.DetallesVenta.FindAsync(id);
            if (detalle == null) return false;

            // Obtener el producto para calcular precios automáticamente
            var producto = await _context.Productos.FindAsync(detalleRequest.ProductoId);
            if (producto == null)
                throw new ArgumentException($"El producto con ID {detalleRequest.ProductoId} no existe");

            // Validar stock (considerando la cantidad actual del detalle)
            var stockNecesario = detalleRequest.Cantidad - detalle.Cantidad;
            if (producto.Stock < stockNecesario)
                throw new ArgumentException($"Stock insuficiente para el producto {producto.Nombre}. Disponible: {producto.Stock}");

            // Actualizar campos básicos
            detalle.ProductoId = detalleRequest.ProductoId;
            detalle.Cantidad = detalleRequest.Cantidad;
            
            // Calcular automáticamente precios
            detalle.PrecioUnitario = producto.Precio;
            detalle.Subtotal = producto.Precio * detalleRequest.Cantidad;

            await _context.SaveChangesAsync();

            // En CreateAsync, UpdateAsync y DeleteAsync después de guardar el detalle:
            var venta = await _context.Ventas.Include(v => v.DetallesVenta).FirstOrDefaultAsync(v => v.Id == detalle.VentaId);
            if (venta != null)
            {
                venta.Total = venta.DetallesVenta.Sum(d => d.Subtotal);
                venta.FechaVenta = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var detalle = await _context.DetallesVenta.FindAsync(id);
            if (detalle == null) return false;
            _context.DetallesVenta.Remove(detalle);
            await _context.SaveChangesAsync();

            // En CreateAsync, UpdateAsync y DeleteAsync después de guardar el detalle:
            var venta = await _context.Ventas.Include(v => v.DetallesVenta).FirstOrDefaultAsync(v => v.Id == detalle.VentaId);
            if (venta != null)
            {
                venta.Total = venta.DetallesVenta.Sum(d => d.Subtotal);
                venta.FechaVenta = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
} 