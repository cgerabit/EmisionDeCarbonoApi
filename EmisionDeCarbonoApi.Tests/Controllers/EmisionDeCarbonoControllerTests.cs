using AutoMapper;

using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Commands;
using EmisionDeCarbonoApi.Application.Features.Emisiones.Queries;
using EmisionDeCarbonoApi.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;




namespace EmisionDeCarbonoApi.Tests.Controllers
{
    public class EmisionDeCarbonoControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EmisionDeCarbonoController _controller;

        public EmisionDeCarbonoControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _controller = new EmisionDeCarbonoController(_mockMediator.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ObtenerEmisiones_ReturnsOkResult_WithListOfEmisiones()
        {
            // Arrange
            var emisiones = new List<EmisionDeCarbonoDTO>
            {
                new EmisionDeCarbonoDTO
                {
                    Id = 1,
                    Empresa = new EmpresaDTO { Id = 1, Nombre = "Empresa 1" },
                    Descripcion = "Descripción 1",
                    Cantidad = 100,
                    FechaEmision = DateTime.UtcNow,
                    TipoEmision = "Tipo 1"
                },
                new EmisionDeCarbonoDTO
                {
                    Id = 2,
                    Empresa = new EmpresaDTO { Id = 2, Nombre = "Empresa 2" },
                    Descripcion = "Descripción 2",
                    Cantidad = 200,
                    FechaEmision = DateTime.UtcNow,
                    TipoEmision = "Tipo 2"
                }
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<ObtenerEmisionQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(emisiones);

            // Act
            var result = await _controller.ObtenerEmisiones(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmisiones = Assert.IsAssignableFrom<IEnumerable<EmisionDeCarbonoDTO>>(okResult.Value);
            Assert.Equal(emisiones.Count, returnedEmisiones.Count());
        }

        [Fact]
        public async Task ObtenerEmisionPorId_ReturnsOkResult_WithEmision()
        {
            // Arrange
            var emision = new EmisionDeCarbonoDTO
            {
                Id = 1,
                Empresa = new EmpresaDTO { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<ObtenerEmisionPorIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(emision);

            // Act
            var result = await _controller.ObtenerEmisionPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmision = Assert.IsType<EmisionDeCarbonoDTO>(okResult.Value);
            Assert.Equal(emision, returnedEmision);
        }

        [Fact]
        public async Task ObtenerEmisionPorId_ReturnsNotFound_WhenEmisionNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<ObtenerEmisionPorIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((EmisionDeCarbonoDTO)null);

            // Act
            var result = await _controller.ObtenerEmisionPorId(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CrearEmision_ReturnsCreatedAtActionResult_WithCreatedEmision()
        {
            // Arrange
            var crearEmisionDto = new CrearEmisionDeCarbonoDTO
            {
                EmpresaId = 1,
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            var crearEmisionCommand = new CrearEmisionCommand();
            var emision = new EmisionDeCarbonoDTO { Id = 1,Descripcion ="123",TipoEmision ="123" };
            _mockMapper.Setup(m => m.Map<CrearEmisionCommand>(crearEmisionDto))
                .Returns(crearEmisionCommand);
            _mockMediator.Setup(m => m.Send(crearEmisionCommand, It.IsAny<CancellationToken>()))
                .ReturnsAsync(emision);

            // Act
            var result = await _controller.CrearEmision(crearEmisionDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(EmisionDeCarbonoController.ObtenerEmisionPorId), createdAtActionResult.ActionName);
            Assert.Equal(emision, createdAtActionResult.Value);
        }

        [Fact]
        public async Task ActualizarEmision_ReturnsNoContentResult()
        {
            // Arrange
            var actualizarEmisionDto = new ActualizarEmisionDeCarbonoDTO
            {
                EmpresaId = 1,
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };

            // Act
            var result = await _controller.ActualizarEmision(1, actualizarEmisionDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task EliminarEmision_ReturnsNoContentResult()
        {
            // Act
            var result = await _controller.EliminarEmision(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}