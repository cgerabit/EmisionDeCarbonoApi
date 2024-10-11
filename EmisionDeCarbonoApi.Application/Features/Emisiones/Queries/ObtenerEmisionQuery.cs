using EmisionDeCarbonoApi.Application.DTOs;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Queries
{
    public class ObtenerEmisionQuery : IRequest<IEnumerable<EmisionDeCarbonoDTO>>

    {
        public int? EmpresaId { get; set; }
    }
}
