using EmisionDeCarbonoApi.Infraestructure.Persistencia;

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
    }
}
