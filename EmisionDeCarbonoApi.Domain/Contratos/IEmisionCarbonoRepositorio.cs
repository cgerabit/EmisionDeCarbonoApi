using EmisionDeCarbonoApi.Domain.Entidades;

namespace EmisionDeCarbonoApi.Domain.Contratos
{
    public interface IEmisionCarbonoRepositorio
    {
        Task<IEnumerable<EmisionCarbono>> ObtenerEmisionesDeCarbono(int? empresaId);
    }
}
