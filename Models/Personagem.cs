using System.ComponentModel.DataAnnotations;

namespace WebRPGCreation.Models
{
    public class Personagem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de Personagem é obrigatório", AllowEmptyStrings = false)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A descrição do Personagem é obrigatória", AllowEmptyStrings = false)]
        public string? Descricao { get; set; }

        public string? Nivel { get; set; }

        public string? XP { get; set; }

        public string? PoderAtual { get; set; }

        public string? PoderMaximo { get; set; }

        public string? AgirAtual { get; set; }

        public string? AgirMaximo { get; set; }

        public string? MenteAtual { get; set; }

        public string? MenteMaximo { get; set; }

        public string? HpAtual { get; set; }

        public string? HpMaximo { get; set; }

        public string? Moeda { get; set; }

        public Grupo? Grupo { get; set; }
        public int GrupoId { get; set; }

        
        public List<Especialidade>? Especialidades { get; set; }

        public List<Equipamento>? Equipamentos { get; set; }
        
    }
}
