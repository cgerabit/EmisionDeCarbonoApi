using EmisionDeCarbonoApi.Application.Contratos;
using EmisionDeCarbonoApi.Application.DTOs;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Queries
{
    public class ObtenerEmisionQueryHandler : IRequestHandler<ObtenerEmisionQuery, IEnumerable<EmisionDeCarbonoDTO>>
    {
        private readonly IEmisionDeCarbonoServicio _emisionCarbonoServicio;

        public ObtenerEmisionQueryHandler(IEmisionDeCarbonoServicio emisionCarbonoRepositorio)
        {
            this._emisionCarbonoServicio = emisionCarbonoRepositorio;
        }
        public async Task<IEnumerable<EmisionDeCarbonoDTO>> Handle(ObtenerEmisionQuery request, CancellationToken cancellationToken)
        {
            return await _emisionCarbonoServicio.ObtenerEmisionesDeCarbono(request.EmpresaId);
        }
    }
}
