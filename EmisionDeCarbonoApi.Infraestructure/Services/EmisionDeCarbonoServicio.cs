using AutoMapper;

using EmisionDeCarbonoApi.Application.Contratos;
using EmisionDeCarbonoApi.Application.DTOs;
using EmisionDeCarbonoApi.Domain.Contratos;
using EmisionDeCarbonoApi.Domain.Entidades;

namespace EmisionDeCarbonoApi.Infraestructure.Services
{
    public class EmisionDeCarbonoServicio : IEmisionDeCarbonoServicio
    {
        private readonly IEmisionCarbonoRepositorio _emisionCarbonoRepositorio;
        private readonly IMapper _mapper;

        public EmisionDeCarbonoServicio(IEmisionCarbonoRepositorio emisionCarbonoRepositorio,
            IMapper mapper)
        {
            this._emisionCarbonoRepositorio = emisionCarbonoRepositorio;
            this._mapper = mapper;
        }

        public async Task<EmisionDeCarbonoDTO?> ObtenerEmisionDeCarbonoPorId(int Id)
        {
            var emisionCarbono = await _emisionCarbonoRepositorio.ObtenerEmisionDeCarbonoPorId(Id);

            if (emisionCarbono == null)
            {
                return null;
            }

            return _mapper.Map<EmisionDeCarbonoDTO>(emisionCarbono);
        }

        public async Task<IEnumerable<EmisionDeCarbonoDTO>> ObtenerEmisionesDeCarbono(int? empresaId)
        {
            var emisiones = await _emisionCarbonoRepositorio.ObtenerEmisionesDeCarbono(empresaId);

            return _mapper.Map<IEnumerable<EmisionDeCarbonoDTO>>(emisiones);
        }

        public async Task CrearEmisionDeCarbono(CrearEmisionDeCarbonoDTO crearEmisionDeCarbonoDTO)
        {
            var emisionCarbono = _mapper.Map<EmisionCarbono>(crearEmisionDeCarbonoDTO);

            await _emisionCarbonoRepositorio.AgregarEmisionDeCarbono(emisionCarbono);

        }

        public async Task ActualizarEmisionDeCarbono(int id, ActualizarEmisionDeCarbonoDTO actualizarEmisionDeCarbonoDTO)
        {
            var emisionCarbono = await _emisionCarbonoRepositorio.ObtenerEmisionDeCarbonoPorId(id);
            if (emisionCarbono == null)
            {
                throw new NullReferenceException("La emisión de carbono no existe");
            }

            _mapper.Map(actualizarEmisionDeCarbonoDTO, emisionCarbono);

            await _emisionCarbonoRepositorio.ActualizarEmisionDeCarbono(emisionCarbono);
        }

        public async Task EliminarEmisionDeCarbono(int id)
        {
            await _emisionCarbonoRepositorio.EliminarEmisionDeCarbono(id);
        }

    }
}
