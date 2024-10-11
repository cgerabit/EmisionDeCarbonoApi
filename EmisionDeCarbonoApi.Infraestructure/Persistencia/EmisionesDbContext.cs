using EmisionDeCarbonoApi.Domain.Entidades;
using EmisionDeCarbonoApi.Infraestructure.Seeds;

using Microsoft.EntityFrameworkCore;

namespace EmisionDeCarbonoApi.Infraestructure.Persistencia
{
    public class EmisionesDbContext : DbContext
    {
        public EmisionesDbContext(DbContextOptions<EmisionesDbContext> options) : 
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SeedEmpresa();
        }
        public DbSet<EmisionCarbono> EmisionesDeCarbono{ get; set; }

        public DbSet<Empresa> Empresas { get; set; }
    }
}
