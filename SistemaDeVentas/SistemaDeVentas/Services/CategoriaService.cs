using SistemaDeVentas.DTOs.Categoria;
using SistemaDeVentas.Models;
using SistemaDeVentas.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly SistemaVentasContext _context;
        private readonly IMapper _mapper;

        public CategoriaService(SistemaVentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaResponse>> GetAllAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return _mapper.Map<List<CategoriaResponse>>(categorias);
        }

        public async Task<CategoriaResponse?> GetByIdAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            return categoria == null ? null : _mapper.Map<CategoriaResponse>(categoria);
        }

        public async Task<CategoriaResponse> CreateAsync(CategoriaRequest categoriaRequest)
        {
            var categoria = _mapper.Map<Categoria>(categoriaRequest);
            categoria.FechaCreacion = DateTime.Now;
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoriaResponse>(categoria);
        }

        public async Task<bool> UpdateAsync(int id, CategoriaRequest categoriaRequest)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;
            _mapper.Map(categoriaRequest, categoria);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 