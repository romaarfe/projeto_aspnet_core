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
    public class PersonagemController : Controller
    {
        private readonly ApplicationDbContext _context;

        private static string nivel = "1";
        private static string xp = "0";
        private static string moeda = "0";
        private static string poderAtual;
        private static string agirAtual;
        private static string menteAtual;
        private static string hpAtual;

        public PersonagemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personagem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Personagem.Include(p => p.Grupo);
            return View(await applicationDbContext.ToListAsync());
        }

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

        // GET: Personagem/Create
        public IActionResult Create()
        {
            Random random = new Random();
            Personagem personagem = new Personagem();

            personagem.Nivel = nivel;
            personagem.XP = xp;
            personagem.Moeda = moeda;

            hpAtual = Convert.ToString(random.Next(1, 7));
            personagem.HpAtual = hpAtual;

            menteAtual = Convert.ToString(random.Next(3, 19));
            personagem.MenteAtual = menteAtual;

            poderAtual = Convert.ToString(random.Next(3, 19));
            personagem.PoderAtual = poderAtual;

            agirAtual = Convert.ToString(random.Next(3, 19));
            personagem.AgirAtual = agirAtual;


            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Nome");
            return View(personagem);
        }

        // POST: Personagem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,GrupoId")] Personagem personagem)
        {
            personagem.Nivel = nivel;
            personagem.XP = xp;
            personagem.Moeda = moeda;
            personagem.PoderMaximo = personagem.PoderAtual = poderAtual;
            personagem.AgirMaximo = personagem.AgirAtual = agirAtual;
            personagem.MenteMaximo = personagem.MenteAtual = menteAtual;
            personagem.HpMaximo = personagem.HpAtual = hpAtual;

            if (ModelState.IsValid)
            {
                _context.Add(personagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Nome", personagem.GrupoId);
            return View(personagem);
        }

        // GET: Personagem/Edit/5
        [Authorize]
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

        // POST: Personagem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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

        // GET: Personagem/Delete/5
        [Authorize]
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

        // POST: Personagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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

        private bool PersonagemExists(int id)
        {
            return (_context.Personagem?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
