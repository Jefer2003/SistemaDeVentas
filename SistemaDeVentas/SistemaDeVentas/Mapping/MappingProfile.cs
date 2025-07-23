using AutoMapper;

namespace SistemaDeVentas.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Este perfil principal registra todos los perfiles de mapeo específicos
            // Los mapeos específicos están en archivos separados para mejor organización
            
            // Los perfiles se registran automáticamente por AutoMapper
            // cuando están en el mismo assembly y heredan de Profile
        }
    }
} 