// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE CRIADA AUTOMÁTICA A PARTIR DO ENTITY
// ATUA COMO CONTROLADOR (PÁGINA) QUANDO SE TRATA DO ESSENCIAL QUANTO À BASE DE DADOS


// ÁREA DOS USINGS/IMPORTS
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebRPGCreation.Models;

// NAMESPACE DO DATA
namespace WebRPGCreation.Data;

// HERANÇA A PARTIR DE CLASSE BASE PARA ATUAR COMO "DBWORKER"
// USA IDENTITY TAMBÉM PELO TRABALHO COM O IDENTITY FRAMEWORK (AO INVÉS DO DBCONTEXT PADRÃO)
public class ApplicationDbContext : IdentityDbContext
{
    // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // PROPRIEDADES PARA GERIR/CRIAR/CONTROLAR A BASE DE DADOS (ACESSO)
    public DbSet<Personagem>? Personagem { get; set; }
    public DbSet<Grupo>? Grupo { get; set; }
    public DbSet<Equipamento>? Equipamento { get; set; }
    public DbSet<Especialidade>? Especialidade { get; set; }
    public DbSet<PoderProfano>? PoderProfano { get; set; }
}

