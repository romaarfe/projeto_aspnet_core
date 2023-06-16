// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE CRIADA AUTOMÁTICA A PARTIR DO ENTITY
// ATUA COMO CONTROLADOR (PÁGINA) QUANDO SE TRATA DOS PERSONAGENS

// ÁREA DOS USINGS/IMPORTS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRPGCreation.Data;
using WebRPGCreation.Models;

// NAMESPACE DO CONTROLLER ENTITY
namespace WebRPGCreation.Controllers
{
    // HERANÇA A PARTIR DE CLASSE BASE PARA ATUAR COMO CONTROLADOR COM VIEW ASSOCIADA
    [Authorize]
    public class PersonagemController : Controller
    {
        // VARIÁVEIS DA CLASSE PARA ENCAPSULAR E GERIR INFORMAÇÕES DA BASE DE DADOS
        private readonly ApplicationDbContext _context;

        // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS
        public PersonagemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DA RAZOR VIEW INDEX
        // GET: Personagem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Personagem.Include(p => p.Grupo);
            return View(await applicationDbContext.ToListAsync());
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DOS DETALHES DO GRUPO
        // GET: Personagem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personagem == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem
                .Include(p => p.Grupo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA CRIAÇÃO DE UM PERSONAGEM
        // GET: Personagem/Create
        public IActionResult Create()
        {
            Personagem personagem = new Personagem();

            personagem.Nivel = "1";
            personagem.XP = "0";
            personagem.Moeda = "0";
            personagem.HpAtual = "???";
            personagem.MenteAtual = "???";
            personagem.PoderAtual = "???";
            personagem.AgirAtual = "???";

            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Nome");

            return View(personagem);
        }

        // MÉTODO ASSÍNCRONO PARA CRIAÇÃO DE UM PERSONAGEM COM CARACTERÍSTICAS RANDÔMICAS ASSOCIADAS AO MÉTODO RANDOM ROLAR D6
        // POST: Personagem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,GrupoId")] Personagem personagem)
        {
            personagem.PoderMaximo = personagem.PoderAtual = Convert.ToString(RolarD6() + RolarD6() + RolarD6());
            personagem.Nivel = "1";
            personagem.AgirMaximo = personagem.AgirAtual = Convert.ToString(RolarD6() + RolarD6() + RolarD6());
            personagem.XP = "0";
            personagem.MenteMaximo = personagem.MenteAtual = Convert.ToString(RolarD6() + RolarD6() + RolarD6());
            personagem.Moeda = "0";
            personagem.HpMaximo = personagem.HpAtual = Convert.ToString(RolarD6());

            if (ModelState.IsValid)
            {
                _context.Add(personagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Nome", personagem.GrupoId);
            return View(personagem);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA EDIÇÃO DE UM PERSONAGEM
        // GET: Personagem/Edit/5
        [Authorize(Roles = "GM")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personagem == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem.FindAsync(id);
            if (personagem == null)
            {
                return NotFound();
            }
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Nome", personagem.GrupoId);
            return View(personagem);
        }

        // MÉTODO ASSÍNCRONO PARA EDIÇÃO DE UM PERSONAGEM
        // POST: Personagem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GM")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Nivel,XP,PoderAtual,PoderMaximo,AgirAtual,AgirMaximo,MenteAtual,MenteMaximo,HpAtual,HpMaximo,Moeda,GrupoId")] Personagem personagem)
        {
            if (id != personagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonagemExists(personagem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Nome", personagem.GrupoId);
            return View(personagem);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA REMOÇÃO DE UM PERSONAGEM
        // GET: Personagem/Delete/5
        [Authorize(Roles = "GM")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personagem == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem
                .Include(p => p.Grupo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // MÉTODO ASSÍNCRONO PARA CONFIRMAR REMOÇÃO DE UM PERSONAGEM
        // POST: Personagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GM")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personagem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Personagem'  is null.");
            }
            var personagem = await _context.Personagem.FindAsync(id);
            if (personagem != null)
            {
                _context.Personagem.Remove(personagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // MÉTODO PARA VERIFICAR SE O TAL PERSONAGEM JÁ EXISTE
        private bool PersonagemExists(int id)
        {
            return (_context.Personagem?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // MÉTODO PARA GERAR UM VALOR "RANDÔMICO"
        public int RolarD6()
        {
            Random random = new Random();

            int d6 = random.Next(1, 7);

            return d6;
        }
    }
}
