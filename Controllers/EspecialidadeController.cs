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
    public class EspecialidadeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EspecialidadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Especialidade
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Especialidade.Include(e => e.Personagem);
            return View(await applicationDbContext.ToListAsync());
        }

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

        // GET: Especialidade/Create
        public IActionResult Create()
        {
            ViewData["PersonagemId"] = new SelectList(_context.Personagem, "Id", "Nome");
            return View();
        }

        // POST: Especialidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Especialidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        private bool EspecialidadeExists(int id)
        {
          return (_context.Especialidade?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
