// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE CRIADA AUTOMÁTICA A PARTIR DO ENTITY
// ATUA COMO CONTROLADOR (PÁGINA) QUANDO SE TRATA DOS PODERES PROFANOS

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
    public class PoderProfanoController : Controller
    {
        // VARIÁVEIS DA CLASSE PARA ENCAPSULAR E GERIR INFORMAÇÕES DA BASE DE DADOS
        private readonly ApplicationDbContext _context;

        // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS
        public PoderProfanoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DA RAZOR VIEW INDEX
        // GET: PoderProfano
        public async Task<IActionResult> Index()
        {
              return _context.PoderProfano != null ? 
                          View(await _context.PoderProfano.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PoderProfano'  is null.");
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DOS DETALHES DO PODER PROFANO
        // GET: PoderProfano/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PoderProfano == null)
            {
                return NotFound();
            }

            var poderProfano = await _context.PoderProfano
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poderProfano == null)
            {
                return NotFound();
            }

            return View(poderProfano);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA CRIAÇÃO DE UM PODER PROFANO
        // GET: PoderProfano/Create
        public IActionResult Create()
        {
            return View();
        }

        // MÉTODO ASSÍNCRONO PARA CRIAÇÃO DE UM PODER PROFANO
        // POST: PoderProfano/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] PoderProfano poderProfano)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poderProfano);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poderProfano);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA EDIÇÃO DE UM PODER PROFANO
        // GET: PoderProfano/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PoderProfano == null)
            {
                return NotFound();
            }

            var poderProfano = await _context.PoderProfano.FindAsync(id);
            if (poderProfano == null)
            {
                return NotFound();
            }
            return View(poderProfano);
        }

        // MÉTODO ASSÍNCRONO PARA EDIÇÃO DE UM PODER PROFANO
        // POST: PoderProfano/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] PoderProfano poderProfano)
        {
            if (id != poderProfano.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poderProfano);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoderProfanoExists(poderProfano.Id))
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
            return View(poderProfano);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA REMOÇÃO DE UM PODER PROFANO
        // GET: PoderProfano/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PoderProfano == null)
            {
                return NotFound();
            }

            var poderProfano = await _context.PoderProfano
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poderProfano == null)
            {
                return NotFound();
            }

            return View(poderProfano);
        }

        // MÉTODO ASSÍNCRONO PARA CONFIRMAR REMOÇÃO DE UM PODER PROFANO
        // POST: PoderProfano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PoderProfano == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PoderProfano'  is null.");
            }
            var poderProfano = await _context.PoderProfano.FindAsync(id);
            if (poderProfano != null)
            {
                _context.PoderProfano.Remove(poderProfano);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // MÉTODO PARA VERIFICAR SE O TAL PODER PROFANO JÁ EXISTE
        private bool PoderProfanoExists(int id)
        {
          return (_context.PoderProfano?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
