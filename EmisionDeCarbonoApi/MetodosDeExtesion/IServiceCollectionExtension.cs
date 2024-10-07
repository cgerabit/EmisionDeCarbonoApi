using EmisionDeCarbonoApi.Application.Contratos;
using EmisionDeCarbonoApi.Domain.Contratos;
using EmisionDeCarbonoApi.Infraestructure.Persistencia;
using EmisionDeCarbonoApi.Infraestructure.Services;

using Microsoft.EntityFrameworkCore;

namespace EmisionDeCarbonoApi.MetodosDeExtesion
{
    public static class IServiceCollectionExtension
    {

        public static IServiceCollection AgregarDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EmisionesDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            return services;
        }

        public static IServiceCollection AgregarInfraestructura(this IServiceCollection services)
        {
            services.AddScoped<IEmisionCarbonoRepositorio, EmisionCarbonoRepositorio>();


            return services;
        }

        public static IServiceCollection AgregarPersistencia(this IServiceCollection services)
        {
            services.AddScoped<IEmisionDeCarbonoServicio, EmisionDeCarbonoServicio>();

            return services;

        }
    }
}
