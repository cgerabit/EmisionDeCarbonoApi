using EmisionDeCarbonoApi.Application.DTOs;

using MediatR;

namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Commands.ActualizarEmision
{
    public class ActualizarEmisionCommand : IRequest
    {
        public int Id { get; set; }
        public ActualizarEmisionDeCarbonoDTO Dto { get; set; }
    }
}