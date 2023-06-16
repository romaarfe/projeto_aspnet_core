// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE CRIADA AUTOMÁTICA A PARTIR DO ENTITY
// ATUA COMO CONTROLADOR (PÁGINA) QUANDO SE TRATA DOS GRUPOS

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
    public class GrupoController : Controller
    {
        // VARIÁVEIS DA CLASSE PARA ENCAPSULAR E GERIR INFORMAÇÕES DA BASE DE DADOS
        private readonly ApplicationDbContext _context;

        // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS
        public GrupoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DA RAZOR VIEW INDEX
        // GET: Grupo
        public async Task<IActionResult> Index()
        {
              return _context.Grupo != null ? 
                          View(await _context.Grupo.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Grupo'  is null.");
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DOS DETALHES DO GRUPO
        // GET: Grupo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grupo == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA CRIAÇÃO DE UM GRUPO
        // GET: Grupo/Create
        public IActionResult Create()
        {
            return View();
        }

        // MÉTODO ASSÍNCRONO PARA CRIAÇÃO DE UM GRUPO
        // POST: Grupo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,DataInicio,DataFim")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupo);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA EDIÇÃO DE UM GRUPO
        // GET: Grupo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grupo == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupo.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            return View(grupo);
        }

        // MÉTODO ASSÍNCRONO PARA EDIÇÃO DE UM GRUPO
        // POST: Grupo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,DataInicio,DataFim")] Grupo grupo)
        {
            if (id != grupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.Id))
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
            return View(grupo);
        }

        // MÉTODO ASSÍNCRONO PARA APRESENTAÇÃO DAS INFORMAÇÕES BÁSICAS REFERENTES À FUTURA REMOÇÃO DE UM GRUPO
        // GET: Grupo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grupo == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // MÉTODO ASSÍNCRONO PARA CONFIRMAR REMOÇÃO DE UM GRUPO
        // POST: Grupo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grupo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Grupo'  is null.");
            }
            var grupo = await _context.Grupo.FindAsync(id);
            if (grupo != null)
            {
                _context.Grupo.Remove(grupo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // MÉTODO PARA VERIFICAR SE O TAL GRUPO JÁ EXISTE
        private bool GrupoExists(int id)
        {
          return (_context.Grupo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
