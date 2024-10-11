using EmisionDeCarbonoApi.Application.Contratos;
using EmisionDeCarbonoApi.Application.DTOs;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Queries
{
    public class ObtenerEmsionPorIdQueryHandler : IRequestHandler<ObtenerEmisionPorIdQuery, EmisionDeCarbonoDTO?>
    {
        private readonly IEmisionDeCarbonoServicio _emisionDeCarbonoServicio;

        public ObtenerEmsionPorIdQueryHandler(IEmisionDeCarbonoServicio emisionDeCarbonoServicio)
        {
            _emisionDeCarbonoServicio = emisionDeCarbonoServicio;
        }
        public async Task<EmisionDeCarbonoDTO?> Handle(ObtenerEmisionPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _emisionDeCarbonoServicio.ObtenerEmisionDeCarbonoPorId(request.Id);
        }
    }
}
