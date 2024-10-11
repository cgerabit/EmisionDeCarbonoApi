using EmisionDeCarbonoApi.Domain.Entidades;
using EmisionDeCarbonoApi.Infraestructure.Persistencia;

using Microsoft.EntityFrameworkCore;

namespace EmisionDeCarbonoApi.Infraestructure.Tests.Repositories
{
    public class EmisionCarbonoRepositorioTests
    {
        private readonly EmisionesDbContext _context;
        private readonly EmisionCarbonoRepositorio _repositorio;

        public EmisionCarbonoRepositorioTests()
        {
            DbContextOptions<EmisionesDbContext> options = new DbContextOptionsBuilder<EmisionesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new EmisionesDbContext(options);
            _repositorio = new EmisionCarbonoRepositorio(_context);
        }

        [Fact]
        public async Task ObtenerEmisionesDeCarbono_ReturnsAllEmisiones()
        {
            // Arrange


            Empresa empresa1 = new Empresa
            {
                Nombre = "Empresa 1",
                Id = 1
            };
            Empresa empresa2 = new Empresa
            {
                Nombre = "Empresa 2",
                Id = 2
            };

            await _context.Empresas.AddRangeAsync(empresa1, empresa2);
            await _context.SaveChangesAsync();

            List<EmisionCarbono> emisiones = new()
            {
                new EmisionCarbono { Id = 1, EmpresaId = 1, Descripcion = "Descripción 1", Cantidad = 100, TipoEmision = "Tipo 1" },
                new EmisionCarbono { Id = 2, EmpresaId = 2, Descripcion = "Descripción 2", Cantidad = 200, TipoEmision = "Tipo 2" }
            };
            await _context.EmisionesDeCarbono.AddRangeAsync(emisiones);
            _ = await _context.SaveChangesAsync();

            // Act
            IEnumerable<EmisionCarbono> result = await _repositorio.ObtenerEmisionesDeCarbono(null);

            // Assert
            Assert.Equal(emisiones.Count, result.Count());
        }

        [Fact]
        public async Task ObtenerEmisionesDeCarbono_ReturnsEmisionesByEmpresaId()
        {

            // Arrange

            Empresa empresa1 = new Empresa
            {
                Nombre = "Empresa 1",
                Id = 1
            };
            Empresa empresa2 = new Empresa
            {
                Nombre = "Empresa 2",
                Id = 2
            };

             await _context.Empresas.AddRangeAsync(empresa1, empresa2);
            await _context.SaveChangesAsync();
            List<EmisionCarbono> emisiones = new()
            {
                new EmisionCarbono { Id = 1, EmpresaId = 1, Descripcion = "Descripción 1", Cantidad = 100, TipoEmision = "Tipo 1" },
                new EmisionCarbono { Id = 2, EmpresaId = 1, Descripcion = "Descripción 2", Cantidad = 200, TipoEmision = "Tipo 2" },
                new EmisionCarbono { Id = 3, EmpresaId = 2, Descripcion = "Descripción 3", Cantidad = 300, TipoEmision = "Tipo 3" }
            };
            await _context.EmisionesDeCarbono.AddRangeAsync(emisiones);
            _ = await _context.SaveChangesAsync();

            // Act
            IEnumerable<EmisionCarbono> result = await _repositorio.ObtenerEmisionesDeCarbono(1);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ObtenerEmisionDeCarbonoPorId_ReturnsEmision()
        {
            // Arrange
            EmisionCarbono emision = new() { Id = 1, EmpresaId = 1, Descripcion = "Descripción 1", Cantidad = 100, TipoEmision = "Tipo 1" };
            _ = await _context.EmisionesDeCarbono.AddAsync(emision);
            _ = await _context.SaveChangesAsync();

            // Act
            EmisionCarbono? result = await _repositorio.ObtenerEmisionDeCarbonoPorId(1);

            // Assert
            Assert.Equal(emision, result);
        }

        [Fact]
        public async Task AgregarEmisionDeCarbono_AddsEmision()
        {
            // Arrange
            EmisionCarbono emision = new() { Id = 1, EmpresaId = 1, Descripcion = "Descripción 1", Cantidad = 100, TipoEmision = "Tipo 1" };

            // Act
            await _repositorio.AgregarEmisionDeCarbono(emision);

            // Assert
            EmisionCarbono? result = await _context.EmisionesDeCarbono.FindAsync(1);
            Assert.Equal(emision, result);
        }

        [Fact]
        public async Task ActualizarEmisionDeCarbono_UpdatesEmision()
        {
            // Arrange
            EmisionCarbono emision = new() { Id = 1, EmpresaId = 1, Descripcion = "Descripción 1", Cantidad = 100, TipoEmision = "Tipo 1" };
            _ = await _context.EmisionesDeCarbono.AddAsync(emision);
            _ = await _context.SaveChangesAsync();
            emision.Descripcion = "Descripción actualizada";

            // Act
            await _repositorio.ActualizarEmisionDeCarbono(emision);

            // Assert
            EmisionCarbono? result = await _context.EmisionesDeCarbono.FindAsync(1);
            Assert.Equal("Descripción actualizada", result.Descripcion);
        }

        [Fact]
        public async Task EliminarEmisionDeCarbono_DeletesEmision()
        {
            // Arrange
            EmisionCarbono emision = new() { Id = 1, EmpresaId = 1, Descripcion = "Descripción 1", Cantidad = 100, TipoEmision = "Tipo 1" };
            _ = await _context.EmisionesDeCarbono.AddAsync(emision);
            _ = await _context.SaveChangesAsync();

            // Act
            await _repositorio.EliminarEmisionDeCarbono(1);

            // Assert
            EmisionCarbono? result = await _context.EmisionesDeCarbono.FindAsync(1);
            Assert.Null(result);
        }
    }
}