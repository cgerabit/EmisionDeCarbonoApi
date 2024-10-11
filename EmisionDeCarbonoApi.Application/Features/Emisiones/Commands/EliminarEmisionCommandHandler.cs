using EmisionDeCarbonoApi.Application.Contratos;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Commands
{
    public class EliminarEmisionCommandHandler : IRequestHandler<EliminarEmisionCommand>
    {
        private readonly IEmisionDeCarbonoServicio _emisionDeCarbonoServicio;

        public EliminarEmisionCommandHandler(IEmisionDeCarbonoServicio emisionDeCarbonoServicio)
        {
            this._emisionDeCarbonoServicio = emisionDeCarbonoServicio;
        }

        public async Task Handle(EliminarEmisionCommand request, CancellationToken cancellationToken)
        {
            var emisionCarbono = await _emisionDeCarbonoServicio.ObtenerEmisionDeCarbonoPorId(request.Id);
            if (emisionCarbono == null)
            {
                throw new NullReferenceException("La emisión de carbono no existe");
            }

            await _emisionDeCarbonoServicio.EliminarEmisionDeCarbono(request.Id);
        }
    }
}
