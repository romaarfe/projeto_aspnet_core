namespace WebRPGCreation.Models
{
    public class Especialidade
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public Personagem? Personagem { get; set; }
        public int PersonagemId { get; set; }
    }
}
