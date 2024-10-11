using AutoMapper;
using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Commands.ActualizarEmision;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Commands;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Queries;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Retorna todas las emisiones registradas.",
            Description = "Permite obtener una lista de todas las emisiones de carbono registradas en el sistema."
        )]
        [SwaggerResponse(200, "Retorna una lista de emisiones de carbono.", typeof(IEnumerable<EmisionDeCarbonoDTO>))]
        public async Task<ActionResult<IEnumerable<EmisionDeCarbonoDTO>>> ObtenerEmisiones(int? empresaId)
        {
            var query = new ObtenerEmisionQuery { EmpresaId = empresaId };
            var emisiones = await _mediator.Send(query);
            return Ok(emisiones);
        }

        // GET /emissions/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retorna una emisión específica por su Id.",
            Description = "Permite obtener una emisión de carbono específica por su identificador único."
        )]
        [SwaggerResponse(200, "Retorna la emisión de carbono solicitada.", typeof(EmisionDeCarbonoDTO))]
        [SwaggerResponse(404, "No se encontró la emisión de carbono con el Id especificado.")]
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
        [SwaggerOperation(
            Summary = "Registra una nueva emisión.",
            Description = "Permite registrar una nueva emisión de carbono en el sistema."
        )]
        [SwaggerResponse(201, "Retorna la emisión de carbono creada.", typeof(EmisionDeCarbonoDTO))]
        public async Task<IActionResult> CrearEmision(CrearEmisionDeCarbonoDTO crearEmisionDto)
        {
            var command = _mapper.Map<CrearEmisionCommand>(crearEmisionDto);
            var emision = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObtenerEmisionPorId), new { id = emision.Id }, emision);
        }

        // PUT /emissions/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza una emisión existente.",
            Description = "Permite actualizar una emisión de carbono existente en el sistema."
        )]
        [SwaggerResponse(204, "La emisión de carbono se actualizó correctamente.")]
        public async Task<IActionResult> ActualizarEmision(int id, ActualizarEmisionDeCarbonoDTO actualizarEmisionDto)
        {
            var command = new ActualizarEmisionCommand { Id = id, Dto = actualizarEmisionDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE /emissions/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina una emisión por su Id.",
            Description = "Permite eliminar una emisión de carbono del sistema por su identificador único."
        )]
        [SwaggerResponse(204, "La emisión de carbono se eliminó correctamente.")]
        public async Task<IActionResult> EliminarEmision(int id)
        {
            var command = new EliminarEmisionCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        // GET /emissions/company/{companyId}
        [HttpGet("company/{companyId}")]
        [SwaggerOperation(
            Summary = "Retorna todas las emisiones de una empresa específica.",
            Description = "Permite obtener una lista de todas las emisiones de carbono registradas para una empresa específica."
        )]
        [SwaggerResponse(200, "Retorna una lista de emisiones de carbono de la empresa solicitada.", typeof(IEnumerable<EmisionDeCarbonoDTO>))]
        public async Task<ActionResult<IEnumerable<EmisionDeCarbonoDTO>>> ObtenerEmisionesPorEmpresa(int companyId)
        {
            var query = new ObtenerEmisionQuery { EmpresaId = companyId };
            var emisiones = await _mediator.Send(query);
            return Ok(emisiones);
        }
    }
}