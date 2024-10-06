namespace EmisionDeCarbonoApi.Domain.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        public List<EmisionCarbono> EmisionesDeCarbono { get; set; } = new();
    }
}
