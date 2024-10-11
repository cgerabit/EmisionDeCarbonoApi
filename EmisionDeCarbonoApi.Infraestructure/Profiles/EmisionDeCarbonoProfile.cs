using AutoMapper;

using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Commands;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Commands.ActualizarEmision;
using EmisionDeCarbonoApi.Domain.Entidades;

using Microsoft.Extensions.Options;

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
            CreateMap<CrearEmisionDeCarbonoDTO, CrearEmisionCommand>()
                .ForMember(m => m.Dto,options => 
                options.MapFrom(src => src));

            CreateMap<ActualizarEmisionDeCarbonoDTO, ActualizarEmisionCommand>()
                .ForMember(m => m.Dto, options => options.MapFrom(m => m));

        }
    }
}
