using EmisionDeCarbonoApi.Application.DTOs;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Queries
{
    public class ObtenerEmisionPorIdQuery : IRequest<EmisionDeCarbonoDTO?>
    {
        public int Id { get; set; }
    }
}