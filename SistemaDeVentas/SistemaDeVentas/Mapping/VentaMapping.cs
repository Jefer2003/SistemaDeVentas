using AutoMapper;
using SistemaDeVentas.Models;
using SistemaDeVentas.DTOs.Venta;
using SistemaDeVentas.DTOs.DetalleVenta;

namespace SistemaDeVentas.Mapping
{
    public class VentaMapping : Profile
    {
        public VentaMapping()
        {
            // Model → Response (para GET)
            CreateMap<Venta, VentaResponse>()
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.Nombre : string.Empty))
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.DetallesVenta));

            // Request → Model (para POST/PUT)
            CreateMap<VentaRequest, Venta>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NumeroVenta, opt => opt.Ignore())
                .ForMember(dest => dest.FechaVenta, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore())
                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                .ForMember(dest => dest.DetallesVenta, opt => opt.Ignore());
        }
    }
} 