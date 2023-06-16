﻿// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE MODELO CRIADA PARA SER A BASE/RAIZ DO PROGRAMA E DA CRIAÇÃO DA BASE DE DADOS
// FUNDAMENTAL PARA CRIAÇÃO DOS CONTROLADORES E VIEWS COM ENTITY FRAMEWORK


// NAMESPACE DO MODELO
namespace WebRPGCreation.Models
{
    // CLASSE PRINCIPAL PARA SERVIR DE ENTIDADE
    public class Equipamento
    {
        // PROPRIEDADES DA CLASSE
        // TAMBÉM UTILIZADO PARA CHAVE PRIMÁRIA, ATRIBUTOS, REFERÊNCIAS, LISTAS E CHAVES ESTRANGEIRAS
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
