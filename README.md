Markdown
# EmisionDeCarbonoApi

Esta API proporciona funcionalidades para gestionar las emisiones de carbono de diferentes empresas.

## Arquitectura

La API sigue una arquitectura limpia, separando las responsabilidades en diferentes capas:

* **Presentación:** Contiene los controladores que manejan las solicitudes HTTP.
* **Aplicación:** Contiene la lógica de negocio de la aplicación, incluyendo los casos de uso y los DTOs.
* **Dominio:** Contiene las entidades y reglas de negocio del dominio.
* **Infraestructura:** Contiene la implementación de la persistencia, los servicios externos y otros detalles de infraestructura.

## Cómo ejecutar la API

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/cgerabit/EmisionDeCarbonoApi


   Configurar la base de datos:
2. **Restaurar las dependencias:**
  ```
cd EmisionDeCarbonoApi
dotnet restore
  ```
3. **Configurar la base de datos:**

   
Modificar el archivo appsettings.json con la cadena de conexión a la base de datos.

Abre el archivo appsettings.json y busca la sección ConnectionStrings. Ahí encontrarás una propiedad llamada EmisionesDbContext, que define la cadena de conexión a la base de datos. Reemplázala con la cadena de conexión correcta para tu entorno. Por ejemplo:
  ```
JSON
"ConnectionStrings": {
  "EmisionesDbContext": "Server=(localdb)\\mssqllocaldb;Database=EmisionesDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
  ```

Ejecutar las migraciones para crear la base de datos:

Bash
dotnet ef database update --project EmisionDeCarbonoApi.Infraestructure

Este comando aplica las migraciones de Entity Framework Core, que son un conjunto de instrucciones para crear o actualizar el esquema de la base de datos. Las migraciones se definen en la carpeta Migrations del proyecto EmisionDeCarbonoApi.Infraestructure.

4. **Ejecutar la API:**
  ```
dotnet run
  ```
La API estará disponible en la URL especificada en el archivo launchSettings.json.

5. **Pruebas unitarias**
El proyecto incluye pruebas unitarias para las capas de aplicación e infraestructura. Para ejecutar las pruebas, usar:
  ```
dotnet test
  ```
Tecnologías utilizadas
.NET 8
ASP.NET Core
Entity Framework Core
AutoMapper
MediatR
xUnit
