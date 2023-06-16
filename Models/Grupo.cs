// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE MODELO CRIADA PARA SER A BASE/RAIZ DO PROGRAMA E DA CRIAÇÃO DA BASE DE DADOS
// FUNDAMENTAL PARA CRIAÇÃO DOS CONTROLADORES E VIEWS COM ENTITY FRAMEWORK


// ÁREA DOS IMPORTS/USINGS
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

// NAMESPACE DO MODELO
namespace WebRPGCreation.Models
{
    // CLASSE PRINCIPAL PARA SERVIR DE ENTIDADE
    public class Grupo
    {
        // PROPRIEDADES DA CLASSE
        // TAMBÉM UTILIZADO PARA CHAVE PRIMÁRIA, ATRIBUTOS, REFERÊNCIAS, LISTAS E CHAVES ESTRANGEIRAS
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<Personagem>? Personagens { get; set; }
    }
}
