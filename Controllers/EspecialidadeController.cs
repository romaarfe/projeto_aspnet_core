// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE CRIADA AUTOMÁTICA A PARTIR DO ENTITY
// ATUA COMO CONTROLADOR (PÁGINA) QUANDO SE TRATA DAS ESPECIALIDADES

// ÁREA DOS USINGS/IMPORTS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRPGCreation.Data;
using WebRPGCreation.Models;

// NAMESPACE DO CONTROLLER ENTITY
namespace WebRPGCreation.Controllers
{
    // HERANÇA A PARTIR DE CLASSE BASE PARA ATUAR COMO CONTROLADOR COM VIEW ASSOCIADA
    [Authorize(Roles = "GM")]
    public class EspecialidadeController : Controller
    {
        // VARIÁVEIS DA CLASSE PARA ENCAPSULAR E GERIR INFORMAÇÕES DA BASE DE DADOS
        private readonly ApplicationDbContext _context;

        // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS
        public EspecialidadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DA RAZOR VIEW INDEX
        // GET: Especialidade
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Especialidade.Include(e => e.Personagem);
            return View(await applicationDbContext.ToListAsync());
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DOS DETALHES DA ESPECIALIDADE
        // GET: Especialidade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Especialidade == null)
            {
                return NotFound();
            }

            var especialidade = await _context.Especialidade
                .Include(e => e.Personagem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialidade == null)
            {
                return NotFound();
            }

            return View(especialidade);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA CRIAÇÃO DE UMA ESPECIALIDADE
        // GET: Especialidade/Create
        public IActionResult Create()
        {
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome");
            return View();
        }

        // MÉTODO ASSÍNCRONO PARA CRIAÇÃO DE UMA ESPECIALIDADE
        // POST: Especialidade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,PersonagemId")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome", especialidade.PersonagemId);
            return View(especialidade);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA EDIÇÃO DE UMA ESPECIALIDADE
        // GET: Especialidade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Especialidade == null)
            {
                return NotFound();
            }

            var especialidade = await _context.Especialidade.FindAsync(id);
            if (especialidade == null)
            {
                return NotFound();
            }
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome", especialidade.PersonagemId);
            return View(especialidade);
        }

        // MÉTODO ASSÍNCRONO PARA EDIÇÃO DE UMA ESPECIALIDADE
        // POST: Especialidade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,PersonagemId")] Especialidade especialidade)
        {
            if (id != especialidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadeExists(especialidade.Id))
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
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome", especialidade.PersonagemId);
            return View(especialidade);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA REMOÇÃO DE UMA ESPECIALIDADE
        // GET: Especialidade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Especialidade == null)
            {
                return NotFound();
            }

            var especialidade = await _context.Especialidade
                .Include(e => e.Personagem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialidade == null)
            {
                return NotFound();
            }

            return View(especialidade);
        }

        // MÉTODO ASSÍNCRONO PARA CONFIRMAR REMOÇÃO DE UMA ESPECIALIDADE
        // POST: Especialidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Especialidade == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Especialidade'  is null.");
            }
            var especialidade = await _context.Especialidade.FindAsync(id);
            if (especialidade != null)
            {
                _context.Especialidade.Remove(especialidade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // MÉTODO PARA VERIFICAR SE A TAL ESPECIALIDADE JÁ EXISTE
        private bool EspecialidadeExists(int id)
        {
          return (_context.Especialidade?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
