using SistemaDeVentas.DTOs.Producto;
using SistemaDeVentas.Models;
using SistemaDeVentas.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Services
{
    public class ProductoService : IProductoService
    {
        private readonly SistemaVentasContext _context;
        private readonly IMapper _mapper;

        public ProductoService(SistemaVentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductoResponse>> GetAllAsync()
        {
            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();
            return _mapper.Map<List<ProductoResponse>>(productos);
        }

        public async Task<ProductoResponse?> GetByIdAsync(int id)
        {
            var producto = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
            return producto == null ? null : _mapper.Map<ProductoResponse>(producto);
        }

        public async Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int categoriaId)
        {
            var productos = await _context.Productos.Include(p => p.Categoria).Where(p => p.CategoriaId == categoriaId).ToListAsync();
            return _mapper.Map<List<ProductoResponse>>(productos);
        }

        public async Task<ProductoResponse> CreateAsync(ProductoRequest productoRequest)
        {
            var producto = _mapper.Map<Producto>(productoRequest);
            producto.FechaCreacion = DateTime.Now;
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductoResponse>(producto);
        }

        public async Task<bool> UpdateAsync(int id, ProductoRequest productoRequest)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;
            _mapper.Map(productoRequest, producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 