using EmisionDeCarbonoApi.Domain.Contratos;
using EmisionDeCarbonoApi.Domain.Entidades;

using Microsoft.EntityFrameworkCore;

namespace EmisionDeCarbonoApi.Infraestructure.Persistencia
{
    public class EmisionCarbonoRepositorio : IEmisionCarbonoRepositorio
    {
        private readonly EmisionesDbContext _emisionesDbContext;

        public EmisionCarbonoRepositorio(EmisionesDbContext emisionesDbContext)
        {
            _emisionesDbContext = emisionesDbContext;
        }

        public async Task<IEnumerable<EmisionCarbono>> ObtenerEmisionesDeCarbono(int? empresaId)
        {
            var queryable = _emisionesDbContext
                .EmisionesDeCarbono
                .Include(e => e.Empresa)
                .AsQueryable();

            if(empresaId.HasValue)
            {
                queryable = queryable.Where(e => e.EmpresaId == empresaId);
            }


            return await queryable.ToListAsync();
        }

        public async Task<EmisionCarbono?> ObtenerEmisionDeCarbonoPorId(int id)
        {
            return await _emisionesDbContext.EmisionesDeCarbono.FindAsync(id);
        }

        public async Task AgregarEmisionDeCarbono(EmisionCarbono emisionCarbono)
        {
            await _emisionesDbContext.EmisionesDeCarbono.AddAsync(emisionCarbono);

            await _emisionesDbContext.SaveChangesAsync();
        }

        public async Task ActualizarEmisionDeCarbono(EmisionCarbono emisionCarbono)
        {
            _emisionesDbContext.EmisionesDeCarbono.Update(emisionCarbono);
            await _emisionesDbContext.SaveChangesAsync();
        }

        public async Task EliminarEmisionDeCarbono(int id)
        {
            var emisionCarbono = await _emisionesDbContext.EmisionesDeCarbono.FindAsync(id)
                ?? throw new NullReferenceException("Emision de carbono no existe");

            if (emisionCarbono != null)
            {
                _emisionesDbContext.EmisionesDeCarbono.Remove(emisionCarbono);
                await _emisionesDbContext.SaveChangesAsync();
            }


        }



    }
}
