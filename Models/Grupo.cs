using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebRPGCreation.Models
{
    public class Grupo
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<Personagem>? Personagens { get; set; }
    }
}
