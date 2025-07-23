using SistemaDeVentas.DTOs.Usuario;
using SistemaDeVentas.Models;
using SistemaDeVentas.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SistemaVentasContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(SistemaVentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioResponse>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return _mapper.Map<List<UsuarioResponse>>(usuarios);
        }

        public async Task<UsuarioResponse?> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario == null ? null : _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task<UsuarioResponse> CreateAsync(UsuarioRequest usuarioRequest)
        {
            var usuario = _mapper.Map<Usuario>(usuarioRequest);
            usuario.FechaCreacion = DateTime.Now;
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task<bool> UpdateAsync(int id, UsuarioRequest usuarioRequest)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;
            _mapper.Map(usuarioRequest, usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 