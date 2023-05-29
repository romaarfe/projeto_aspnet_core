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

namespace WebRPGCreation.Controllers
{
    [Authorize]
    public class EquipamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipamento
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Equipamento.Include(e => e.Personagem).Include(e => e.PoderProfano);
            return View(await applicationDbContext.ToListAsync());
        }

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

        // GET: Equipamento/Create
        public IActionResult Create()
        {
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome");
            ViewData["PoderProfanoId"] = new SelectList(_context.PoderProfano, "Id", "Nome");
            return View();
        }

        // POST: Equipamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Equipamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        private bool EquipamentoExists(int id)
        {
          return (_context.Equipamento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
