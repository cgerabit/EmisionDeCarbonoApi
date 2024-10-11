using AutoMapper;

using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Domain.Contratos;
using EmisionDeCarbonoApi.Domain.Entidades;
using EmisionDeCarbonoApi.Infraestructure.Services;

using Moq;

namespace EmisionDeCarbonoApi.Infraestructure.Tests.Services
{
    public class EmisionDeCarbonoServicioTests
    {
        private readonly Mock<IEmisionCarbonoRepositorio> _mockRepositorio;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EmisionDeCarbonoServicio _servicio;

        public EmisionDeCarbonoServicioTests()
        {
            _mockRepositorio = new Mock<IEmisionCarbonoRepositorio>();
            _mockMapper = new Mock<IMapper>();
            _servicio = new EmisionDeCarbonoServicio(_mockRepositorio.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ObtenerEmisionDeCarbonoPorId_ReturnsEmision_WhenEmisionExists()
        {
            // Arrange
            EmisionCarbono emision = new()
            {
                Id = 1,
                Empresa = new Empresa { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            EmisionDeCarbonoDTO emisionDto = new()
            {
                Id = 1,
                Empresa = new EmpresaDTO { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _ = _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
                .ReturnsAsync(emision);
            _ = _mockMapper.Setup(m => m.Map<EmisionDeCarbonoDTO>(emision))
                .Returns(emisionDto);

            // Act
            EmisionDeCarbonoDTO? result = await _servicio.ObtenerEmisionDeCarbonoPorId(1);

            // Assert
            Assert.Equal(emisionDto, result);
        }

        [Fact]
        public async Task ObtenerEmisionDeCarbonoPorId_ReturnsNull_WhenEmisionDoesNotExist()
        {
            // Arrange
            _ = _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
                .ReturnsAsync((EmisionCarbono)null);

            // Act
            EmisionDeCarbonoDTO? result = await _servicio.ObtenerEmisionDeCarbonoPorId(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ObtenerEmisionesDeCarbono_ReturnsListOfEmisiones()
        {
            // Arrange
            List<EmisionCarbono> emisiones = new()
            {
                new EmisionCarbono
                {
                    Id = 1,
                    Empresa = new Empresa { Id = 1, Nombre = "Empresa 1" },
                    Descripcion = "Descripción 1",
                    Cantidad = 100,
                    FechaEmision = DateTime.UtcNow,
                    TipoEmision = "Tipo 1"
                },
                new EmisionCarbono
                {
                    Id = 2,
                    Empresa = new Empresa { Id = 2, Nombre = "Empresa 2" },
                    Descripcion = "Descripción 2",
                    Cantidad = 200,
                    FechaEmision = DateTime.UtcNow,
                    TipoEmision = "Tipo 2"
                }
            };
            List<EmisionDeCarbonoDTO> emisionesDto = new()
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
            _ = _mockRepositorio.Setup(m => m.ObtenerEmisionesDeCarbono(null))
                .ReturnsAsync(emisiones);
            _ = _mockMapper.Setup(m => m.Map<IEnumerable<EmisionDeCarbonoDTO>>(emisiones))
                .Returns(emisionesDto);

            // Act
            IEnumerable<EmisionDeCarbonoDTO> result = await _servicio.ObtenerEmisionesDeCarbono(null);

            // Assert
            Assert.Equal(emisionesDto, result);
        }

        [Fact]
        public async Task CrearEmisionDeCarbono_CreatesEmision_AndReturnsDto()
        {
            // Arrange
            CrearEmisionDeCarbonoDTO crearEmisionDto = new()
            {
                EmpresaId = 1,
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            EmisionCarbono emision = new()
            {
                Empresa = new Empresa { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            EmisionDeCarbonoDTO emisionDto = new()
            {
                Id = 1,
                Empresa = new EmpresaDTO { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _ = _mockMapper.Setup(m => m.Map<EmisionCarbono>(crearEmisionDto))
                .Returns(emision);
            _ = _mockMapper.Setup(m => m.Map<EmisionDeCarbonoDTO>(emision))
                .Returns(emisionDto);

            // Act
            EmisionDeCarbonoDTO result = await _servicio.CrearEmisionDeCarbono(crearEmisionDto);

            // Assert
            _mockRepositorio.Verify(m => m.AgregarEmisionDeCarbono(emision), Times.Once);
            Assert.Equal(emisionDto, result);
        }

        [Fact]
        public async Task ActualizarEmisionDeCarbono_UpdatesEmision()
        {
            // Arrange
            ActualizarEmisionDeCarbonoDTO actualizarEmisionDto = new()
            {
                EmpresaId = 1,
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            EmisionCarbono emision = new()
            {
                Id = 1,
                Empresa = new Empresa { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _ = _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
                .ReturnsAsync(emision);

            // Act
            await _servicio.ActualizarEmisionDeCarbono(1, actualizarEmisionDto);

            // Assert
            _mockRepositorio.Verify(m => m.ActualizarEmisionDeCarbono(emision), Times.Once);
        }

        [Fact]
        public async Task ActualizarEmisionDeCarbono_ThrowsException_WhenEmisionDoesNotExist()
        {
            // Arrange
            ActualizarEmisionDeCarbonoDTO actualizarEmisionDto = new()
            {
                EmpresaId = 1,
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _ = _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
                .ReturnsAsync((EmisionCarbono)null);

            // Act & Assert
            _ = await Assert.ThrowsAsync<NullReferenceException>(() => _servicio.ActualizarEmisionDeCarbono(1, actualizarEmisionDto));
        }

        [Fact]
        public async Task EliminarEmisionDeCarbono_DeletesEmision()
        {
            // Act
            await _servicio.EliminarEmisionDeCarbono(1);

            // Assert
            _mockRepositorio.Verify(m => m.EliminarEmisionDeCarbono(1), Times.Once);
        }
    }
}