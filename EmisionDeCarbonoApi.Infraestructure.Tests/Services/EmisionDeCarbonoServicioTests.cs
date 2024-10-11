using AutoMapper;

using EmisionDeCarbonoApi.Application.Contratos;
using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Domain.Contratos;
using EmisionDeCarbonoApi.Domain.Entidades;
using EmisionDeCarbonoApi.Infraestructure.Services;

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace EmisionDeCarbonoApi.Tests.Services
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
            var emision = new EmisionCarbono
            {
                Id = 1,
                Empresa = new Empresa { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            var emisionDto = new EmisionDeCarbonoDTO
            {
                Id = 1,
                Empresa = new EmpresaDTO { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
                .ReturnsAsync(emision);
            _mockMapper.Setup(m => m.Map<EmisionDeCarbonoDTO>(emision))
                .Returns(emisionDto);

            // Act
            var result = await _servicio.ObtenerEmisionDeCarbonoPorId(1);

            // Assert
            Assert.Equal(emisionDto, result);
        }

        [Fact]
        public async Task ObtenerEmisionDeCarbonoPorId_ReturnsNull_WhenEmisionDoesNotExist()
        {
            // Arrange
            _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
                .ReturnsAsync((EmisionCarbono)null);

            // Act
            var result = await _servicio.ObtenerEmisionDeCarbonoPorId(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ObtenerEmisionesDeCarbono_ReturnsListOfEmisiones()
        {
            // Arrange
            var emisiones = new List<EmisionCarbono>
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
            var emisionesDto = new List<EmisionDeCarbonoDTO>
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
            _mockRepositorio.Setup(m => m.ObtenerEmisionesDeCarbono(null))
                .ReturnsAsync(emisiones);
            _mockMapper.Setup(m => m.Map<IEnumerable<EmisionDeCarbonoDTO>>(emisiones))
                .Returns(emisionesDto);

            // Act
            var result = await _servicio.ObtenerEmisionesDeCarbono(null);

            // Assert
            Assert.Equal(emisionesDto, result);
        }

        [Fact]
        public async Task CrearEmisionDeCarbono_CreatesEmision_AndReturnsDto()
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
            var emision = new EmisionCarbono
            {
                Empresa = new Empresa { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            var emisionDto = new EmisionDeCarbonoDTO
            {
                Id = 1,
                Empresa = new EmpresaDTO { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _mockMapper.Setup(m => m.Map<EmisionCarbono>(crearEmisionDto))
                .Returns(emision);
            _mockMapper.Setup(m => m.Map<EmisionDeCarbonoDTO>(emision))
                .Returns(emisionDto);

            // Act
            var result = await _servicio.CrearEmisionDeCarbono(crearEmisionDto);

            // Assert
            _mockRepositorio.Verify(m => m.AgregarEmisionDeCarbono(emision), Times.Once);
            Assert.Equal(emisionDto, result);
        }

        [Fact]
        public async Task ActualizarEmisionDeCarbono_UpdatesEmision()
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
            var emision = new EmisionCarbono
            {
                Id = 1,
                Empresa = new Empresa { Id = 1, Nombre = "Empresa 1" },
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
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
            var actualizarEmisionDto = new ActualizarEmisionDeCarbonoDTO
            {
                EmpresaId = 1,
                Descripcion = "Descripción 1",
                Cantidad = 100,
                FechaEmision = DateTime.UtcNow,
                TipoEmision = "Tipo 1"
            };
            _mockRepositorio.Setup(m => m.ObtenerEmisionDeCarbonoPorId(1))
                .ReturnsAsync((EmisionCarbono)null);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _servicio.ActualizarEmisionDeCarbono(1, actualizarEmisionDto));
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