using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebRPGCreation.Data;
using WebRPGCreation.Models;

namespace WebRPGCreation.Controllers;

public class HomeController : Controller
{
    
    private readonly ApplicationDbContext db;

    public HomeController(ApplicationDbContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
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




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

