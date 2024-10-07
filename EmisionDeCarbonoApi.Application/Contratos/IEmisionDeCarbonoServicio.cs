using EmisionDeCarbonoApi.Application.DTOs;

namespace EmisionDeCarbonoApi.Application.Contratos
{
    public interface IEmisionDeCarbonoServicio
    {
        Task<EmisionDeCarbonoDTO?> ObtenerEmisionDeCarbonoPorId(int Id);
    }
}
