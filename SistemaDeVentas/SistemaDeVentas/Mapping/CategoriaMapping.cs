using AutoMapper;
using SistemaDeVentas.Models;
using SistemaDeVentas.DTOs.Categoria;

namespace SistemaDeVentas.Mapping
{
    public class CategoriaMapping : Profile
    {
        public CategoriaMapping()
        {
            // Model → Response (para GET)
            CreateMap<Categoria, CategoriaResponse>();

            // Request → Model (para POST/PUT)
            CreateMap<CategoriaRequest, Categoria>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
                .ForMember(dest => dest.Productos, opt => opt.Ignore());
        }
    }
} 