using AutoMapper;
using SistemaDeVentas.Models;
using SistemaDeVentas.DTOs.Producto;

namespace SistemaDeVentas.Mapping
{
    public class ProductoMapping : Profile
    {
        public ProductoMapping()
        {
            // Model → Response (para GET)
            CreateMap<Producto, ProductoResponse>()
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nombre : string.Empty));

            // Request → Model (para POST/PUT)
            CreateMap<ProductoRequest, Producto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
                .ForMember(dest => dest.Categoria, opt => opt.Ignore())
                .ForMember(dest => dest.DetallesVenta, opt => opt.Ignore());
        }
    }
} 