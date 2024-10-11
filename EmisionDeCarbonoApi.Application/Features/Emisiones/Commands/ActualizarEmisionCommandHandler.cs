using AutoMapper;

using EmisionDeCarbonoApi.Application.Contratos;
using EmisionDeCarbonoApi.Domain.Contratos;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Commands.ActualizarEmision
{
    public class ActualizarEmisionCommandHandler :IRequestHandler<ActualizarEmisionCommand>
    {
        private readonly IEmisionDeCarbonoServicio _emisionDeCarbonoServicio;
        private readonly IMapper _mapper;

        public ActualizarEmisionCommandHandler(IEmisionDeCarbonoServicio emisionDeCarbonoServicio, IMapper mapper)
        {
            this._emisionDeCarbonoServicio = emisionDeCarbonoServicio;
            _mapper = mapper;
        }

        public async Task Handle(ActualizarEmisionCommand request, CancellationToken cancellationToken)
        {
            var emisionCarbono = await _emisionDeCarbonoServicio.ObtenerEmisionDeCarbonoPorId(request.Id);
            if (emisionCarbono == null)
            {
                throw new NullReferenceException("La emisión de carbono no existe");
            }

            await _emisionDeCarbonoServicio.ActualizarEmisionDeCarbono(request.Id,request.Dto);
        }

    }
}