using MediatR;


namespace EmisionDeCarbonoApi.Application.Features.Emisiones.Commands
{
    public class EliminarEmisionCommand : IRequest
    {
        public int Id { get; set; }

    }
}
