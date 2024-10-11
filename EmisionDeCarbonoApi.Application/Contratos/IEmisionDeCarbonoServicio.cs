using EmisionDeCarbonoApi.Application.DTOs;

namespace EmisionDeCarbonoApi.Application.Contratos
{
    public interface IEmisionDeCarbonoServicio
    {
        Task ActualizarEmisionDeCarbono(int id, ActualizarEmisionDeCarbonoDTO actualizarEmisionDeCarbonoDTO);
        Task<EmisionDeCarbonoDTO> CrearEmisionDeCarbono(CrearEmisionDeCarbonoDTO crearEmisionDeCarbonoDTO);
        Task EliminarEmisionDeCarbono(int id);
        Task<EmisionDeCarbonoDTO?> ObtenerEmisionDeCarbonoPorId(int Id);
        Task<IEnumerable<EmisionDeCarbonoDTO>> ObtenerEmisionesDeCarbono(int? empresaId);
    }
}
