using System.ComponentModel.DataAnnotations;

namespace EmisionDeCarbonoApi.Domain.Entidades
{
    public class EmisionCarbono
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }

        [StringLength(1200)]
        public required string Descripcion { get; set; }
        public decimal Cantidad { get; set; }

        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        [StringLength(100)]
        public required string TipoEmision { get; set; }
    }
}
