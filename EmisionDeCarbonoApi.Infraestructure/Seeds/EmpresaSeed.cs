using EmisionDeCarbonoApi.Domain.Entidades;

using Microsoft.EntityFrameworkCore;

namespace EmisionDeCarbonoApi.Infraestructure.Seeds
{
    public static class EmpresaSeed
    {
        public static ModelBuilder SeedEmpresa(this ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Empresa>().HasData(new List<Empresa>
            {
                new() {
                    Id=1,
                    Nombre="Apple",
                },
                new() {
                    Id=2,
                    Nombre="Google",
                }
            });

            return modelBuilder;
        }
    }
}
