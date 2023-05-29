namespace WebRPGCreation.Models
{
    public class Equipamento
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Quantidade { get; set; }

        public string? Valor { get; set; }

        public string? Tipo { get; set; }

        public string? Ataque { get; set; }

        public string? Protecao { get; set; }

        public PoderProfano? PoderProfano { get; set; }
        public int PoderProfanoId { get; set; }


        public Personagem? Personagem { get; set; }
        public int PersonagemId { get; set; }

    }
}
