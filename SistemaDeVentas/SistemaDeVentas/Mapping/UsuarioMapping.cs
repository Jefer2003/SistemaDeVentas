using AutoMapper;
using SistemaDeVentas.Models;
using SistemaDeVentas.DTOs.Usuario;

namespace SistemaDeVentas.Mapping
{
    public class UsuarioMapping : Profile
    {
        public UsuarioMapping()
        {
            // Model → Response (para GET)
            CreateMap<Usuario, UsuarioResponse>();

            // Request → Model (para POST/PUT)
            CreateMap<UsuarioRequest, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
                .ForMember(dest => dest.Ventas, opt => opt.Ignore());
        }
    }
} 