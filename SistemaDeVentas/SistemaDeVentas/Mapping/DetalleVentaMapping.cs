using AutoMapper;
using SistemaDeVentas.Models;
using SistemaDeVentas.DTOs.DetalleVenta;

namespace SistemaDeVentas.Mapping
{
    public class DetalleVentaMapping : Profile
    {
        public DetalleVentaMapping()
        {
            // Model → Response (para GET)
            CreateMap<DetalleVenta, DetalleVentaResponse>()
                .ForMember(dest => dest.ProductoNombre, opt => opt.MapFrom(src => src.Producto != null ? src.Producto.Nombre : string.Empty));

            // Request → Model (para POST/PUT) - Con lógica personalizada
            CreateMap<DetalleVentaRequest, DetalleVenta>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PrecioUnitario, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore()) 
                .ForMember(dest => dest.Venta, opt => opt.Ignore())
                .ForMember(dest => dest.Producto, opt => opt.Ignore());
        }
    }
} 