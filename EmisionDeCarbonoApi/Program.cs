using EmisionDeCarbonoApi.Application.Contratos;
using EmisionDeCarbonoApi.Infraestructure.Profiles;
using EmisionDeCarbonoApi.MetodosDeExtesion;

using Microsoft.OpenApi.Models;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmisionDeCarbonoApi", Version = "v1" });
    c.EnableAnnotations();

});

var connectionString = builder.Configuration.GetConnectionString("EmisionesDb");
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula o vacía.");
}
builder.Services.AgregarDbContext(connectionString);

builder.Services
    .AgregarInfraestructura()
    .AgregarPersistencia();

builder.Services.AddAutoMapper(typeof(EmisionDeCarbonoProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
 "EmisionDeCarbonoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
