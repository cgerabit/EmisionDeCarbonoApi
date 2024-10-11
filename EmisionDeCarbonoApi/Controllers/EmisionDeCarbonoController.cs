using AutoMapper;
using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Commands.ActualizarEmision;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Commands;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Queries;

namespace EmisionDeCarbonoApi.Controllers
{
    [ApiController]
    [Route("emissions")]
    public class EmisionDeCarbonoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmisionDeCarbonoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET /emissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmisionDeCarbonoDTO>>> ObtenerEmisiones(int? empresaId)
        {
            var query = new ObtenerEmisionQuery { EmpresaId = empresaId};
            var emisiones = await _mediator.Send(query);
            return Ok(emisiones);
        }

        // GET /emissions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmisionDeCarbonoDTO>> ObtenerEmisionPorId(int id)
        {
            var query = new ObtenerEmisionPorIdQuery { Id = id };
            var emision = await _mediator.Send(query);

            if (emision == null)
            {
                return NotFound();
            }

            return Ok(emision);
        }

        // POST /emissions
        [HttpPost]
        public async Task<IActionResult> CrearEmision(CrearEmisionDeCarbonoDTO crearEmisionDto)
        {
            var command = _mapper.Map<CrearEmisionCommand>(crearEmisionDto);
            var emision = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObtenerEmisionPorId), new { id = emision.Id }, emision);
        }

        // PUT /emissions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEmision(int id, ActualizarEmisionDeCarbonoDTO actualizarEmisionDto)
        {
            var command = new ActualizarEmisionCommand { Id = id, Dto = actualizarEmisionDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE /emissions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEmision(int id)
        {
            var command = new EliminarEmisionCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}