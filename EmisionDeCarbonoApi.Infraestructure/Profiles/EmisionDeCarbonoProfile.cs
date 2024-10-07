using AutoMapper;

using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Domain.Entidades;

namespace EmisionDeCarbonoApi.Infraestructure.Profiles
{
    public class EmisionDeCarbonoProfile : Profile
    {
        public EmisionDeCarbonoProfile()
        {
            CreateMap<EmisionCarbono, EmisionDeCarbonoDTO>();
            CreateMap<ActualizarEmisionDeCarbonoDTO, EmisionCarbono>();
            CreateMap<CrearEmisionDeCarbonoDTO, EmisionCarbono>();
            CreateMap<Empresa, EmpresaDTO>();

        }
    }
}
