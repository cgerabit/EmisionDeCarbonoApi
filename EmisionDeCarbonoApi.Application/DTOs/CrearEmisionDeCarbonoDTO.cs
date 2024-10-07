using System.ComponentModel.DataAnnotations;

namespace EmisionDeCarbonoApi.Application.DTOs
{
    public class CrearEmisionDeCarbonoDTO
    {
        [Required]
        public int EmpresaId { get; set; }
        
        [StringLength(1200)]
        [Required]
        public required string Descripcion { get; set; }
        [Required]
        public decimal Cantidad { get; set; }
        [Required]
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        [StringLength(100)]
        [Required]
        public required string TipoEmision { get; set; }
    }
}
