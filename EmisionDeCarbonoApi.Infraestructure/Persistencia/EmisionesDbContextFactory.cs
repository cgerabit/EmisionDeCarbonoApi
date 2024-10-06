using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;



namespace EmisionDeCarbonoApi.Infraestructure.Persistencia
{
    // Es necesario para poder crear la base de datos en tiempo de diseño desde el dotnet CLI
    public class EmisionesDbContextFactory : IDesignTimeDbContextFactory<EmisionesDbContext>
    {
        public EmisionesDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EmisionDeCarbonoApi"))
          .AddJsonFile("appsettings.json")
          .Build();

            DbContextOptionsBuilder<EmisionesDbContext> optionsBuilder = new DbContextOptionsBuilder<EmisionesDbContext>();
            string? connectionString = configuration.GetConnectionString("EmisionesDb");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no se encontró en el archivo de configuración.");
            }

            optionsBuilder.UseMySQL(connectionString);

            return new EmisionesDbContext(optionsBuilder.Options);
        }
    }
}
