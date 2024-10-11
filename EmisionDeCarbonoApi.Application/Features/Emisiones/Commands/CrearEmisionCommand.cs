using EmisionDeCarbonoApi.Application.DTOs;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Commands
{
    public class CrearEmisionCommand : IRequest<EmisionDeCarbonoDTO>
    {
        public CrearEmisionDeCarbonoDTO Dto { get; set; }
    }
}
