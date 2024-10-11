using AutoMapper;
using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Application.Contratos;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Commands.CrearEmision
{
    public class CrearEmisionCommandHandler : IRequestHandler<CrearEmisionCommand, EmisionDeCarbonoDTO>
    {
        private readonly IEmisionDeCarbonoServicio _emisionDeCarbonoServicio;

        public CrearEmisionCommandHandler(IEmisionDeCarbonoServicio emisionDeCarbonoServicio)
        {
            this._emisionDeCarbonoServicio = emisionDeCarbonoServicio;
        }

        public async Task<EmisionDeCarbonoDTO> Handle(CrearEmisionCommand request, CancellationToken cancellationToken)
        {
           var emisionCarbono =  await _emisionDeCarbonoServicio.CrearEmisionDeCarbono(request.Dto);
            
            return emisionCarbono;
        }
    }
}