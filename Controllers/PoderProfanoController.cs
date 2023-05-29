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
    public class PoderProfanoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoderProfanoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PoderProfano
        public async Task<IActionResult> Index()
        {
              return _context.PoderProfano != null ? 
                          View(await _context.PoderProfano.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PoderProfano'  is null.");
        }

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

        // GET: PoderProfano/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PoderProfano/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: PoderProfano/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        private bool PoderProfanoExists(int id)
        {
          return (_context.PoderProfano?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
