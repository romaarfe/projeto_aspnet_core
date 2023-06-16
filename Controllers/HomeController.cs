// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE CRIADA POR PADRÃO
// ATUA COMO CONTROLADOR (PÁGINA) QUANDO SE TRATA DA PÁGINA INICIAL/PRINCIPAL

// NAMESPACE DO CONTROLLER ENTITY
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebRPGCreation.Data;
using WebRPGCreation.Models;

// ÁREA DOS USINGS/IMPORTS
namespace WebRPGCreation.Controllers;

// HERANÇA A PARTIR DE CLASSE BASE PARA ATUAR COMO CONTROLADOR COM VIEW ASSOCIADA
public class HomeController : Controller
{
    // VARIÁVEIS DA CLASSE PARA ENCAPSULAR E GERIR INFORMAÇÕES DA BASE DE DADOS
    private readonly ApplicationDbContext db;

    // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS
    public HomeController(ApplicationDbContext context)
    {
        db = context;
    }

    // MÉTODO ÚNICO PARA APRESENTAÇÃO DA RAZOR VIEW INDEX
    // ESSENCIALMENTE TRABALHA COM FILTRAGEM DE INFORMAÇÕES E LISTAS RELACIONADAS AOS MODELOS/CONTROLADORES
    public IActionResult Index(string codigo)
    {
        SelectList listaNome = new SelectList(db.Personagem, "Id", "Nome");
        ViewBag.NOME = listaNome;
        ViewBag.ID = Convert.ToInt32(codigo);

        List<Personagem> listaPersonagem = new List<Personagem>();
        listaPersonagem = db.Personagem.ToList();
        ViewBag.PERSONAGEM = listaPersonagem;

        List<Especialidade> listaEspecialidade = new List<Especialidade>();
        listaEspecialidade = db.Especialidade.Where(e => e.Personagem.Id == e.PersonagemId).ToList();
        ViewBag.ESPECIALIDADE = listaEspecialidade;

        List<Equipamento> listaEquipamento = new List<Equipamento>();
        listaEquipamento = db.Equipamento.Where(e => e.Personagem.Id == e.PersonagemId).ToList();
        ViewBag.EQUIPAMENTO = listaEquipamento;

        List<PoderProfano> listaProfano = new List<PoderProfano>();
        listaProfano = db.PoderProfano.ToList();
        ViewBag.PROFANO = listaProfano;


        return View();
    }

    // MÉTODO TRATAMENTO DO ERRO
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

