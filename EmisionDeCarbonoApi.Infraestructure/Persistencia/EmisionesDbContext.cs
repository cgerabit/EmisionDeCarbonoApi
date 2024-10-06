using EmisionDeCarbonoApi.Domain.Entidades;

using Microsoft.EntityFrameworkCore;

namespace EmisionDeCarbonoApi.Infraestructure.Persistencia
{
    public class EmisionesDbContext : DbContext
    {
        public EmisionesDbContext(DbContextOptions<EmisionesDbContext> options) : 
            base(options)
        {

        }

        public DbSet<EmisionCarbono> EmisionesDeCarbono{ get; set; }

        public DbSet<Empresa> Empresas { get; set; }
    }
}
