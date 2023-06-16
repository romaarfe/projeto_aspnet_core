// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE CRIADA AUTOMÁTICA A PARTIR DO ENTITY
// ATUA COMO CONTROLADOR (PÁGINA) QUANDO SE TRATA DOS EQUIPAMENTOS


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
    public class EquipamentoController : Controller
    {
        // VARIÁVEIS DA CLASSE PARA ENCAPSULAR E GERIR INFORMAÇÕES DA BASE DE DADOS

        private readonly ApplicationDbContext _context;

        // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS

        public EquipamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DA RAZOR VIEW INDEX
        // GET: Equipamento
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Equipamento.Include(e => e.Personagem).Include(e => e.PoderProfano);
            return View(await applicationDbContext.ToListAsync());
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DOS DETALHES DO EQUIPAMENTO
        // GET: Equipamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Equipamento == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento
                .Include(e => e.Personagem)
                .Include(e => e.PoderProfano)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA CRIAÇÃO DE UM EQUIPAMENTO
        // GET: Equipamento/Create
        public IActionResult Create()
        {
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome");
            ViewData["PoderProfanoId"] = new SelectList(_context.PoderProfano, "Id", "Nome");
            return View();
        }

        // MÉTODO ASSÍNCRONO PARA CRIAÇÃO DE UM EQUIPAMENTO
        // POST: Equipamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Quantidade,Valor,Tipo,Ataque,Protecao,PoderProfanoId,PersonagemId")] Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome", equipamento.PersonagemId);
            ViewData["PoderProfanoId"] = new SelectList(_context.PoderProfano, "Id", "Nome", equipamento.PoderProfanoId);
            return View(equipamento);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA EDIÇÃO DE UM EQUIPAMENTO
        // GET: Equipamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Equipamento == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento.FindAsync(id);
            if (equipamento == null)
            {
                return NotFound();
            }
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome", equipamento.PersonagemId);
            ViewData["PoderProfanoId"] = new SelectList(_context.PoderProfano, "Id", "Nome", equipamento.PoderProfanoId);
            return View(equipamento);
        }

        // MÉTODO ASSÍNCRONO PARA EDIÇÃO DE UM EQUIPAMENTO
        // POST: Equipamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Quantidade,Valor,Tipo,Ataque,Protecao,PoderProfanoId,PersonagemId")] Equipamento equipamento)
        {
            if (id != equipamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoExists(equipamento.Id))
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
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome", equipamento.PersonagemId);
            ViewData["PoderProfanoId"] = new SelectList(_context.PoderProfano, "Id", "Nome", equipamento.PoderProfanoId);
            return View(equipamento);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA REMOÇÃO DE UM EQUIPAMENTO
        // GET: Equipamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Equipamento == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento
                .Include(e => e.Personagem)
                .Include(e => e.PoderProfano)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // MÉTODO ASSÍNCRONO PARA CONFIRMAR REMOÇÃO DE UM EQUIPAMENTO
        // POST: Equipamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Equipamento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Equipamento'  is null.");
            }
            var equipamento = await _context.Equipamento.FindAsync(id);
            if (equipamento != null)
            {
                _context.Equipamento.Remove(equipamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // MÉTODO PARA VERIFICAR SE O TAL EQUIPAMENTO JÁ EXISTE
        private bool EquipamentoExists(int id)
        {
          return (_context.Equipamento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
