namespace WebRPGCreation.Models
{
    public class PoderProfano
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public List<Equipamento>? Equipamentos { get; set; }
    }
}
