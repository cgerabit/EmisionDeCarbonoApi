using EmisionDeCarbonoApi.Domain.Entidades;

namespace EmisionDeCarbonoApi.Domain.Contratos
{
    public interface IEmisionCarbonoRepositorio
    {
        Task ActualizarEmisionDeCarbono(EmisionCarbono emisionCarbono);
        Task AgregarEmisionDeCarbono(EmisionCarbono emisionCarbono);
        Task EliminarEmisionDeCarbono(int id);
        Task<EmisionCarbono?> ObtenerEmisionDeCarbonoPorId(int id);
        Task<IEnumerable<EmisionCarbono>> ObtenerEmisionesDeCarbono(int? empresaId);
    }
}
