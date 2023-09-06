using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Session.Data;
using Session.Models;

namespace Session.Controllers
{
    public class StagiairesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StagiairesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stagiaires
        public async Task<IActionResult> Index()
        {
              return _context.Stagiaire != null ? 
                          View(await _context.Stagiaire.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Stagiaire'  is null.");
        }

        // GET: Stagiaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stagiaire == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Stagiaire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stagiaire == null)
            {
                return NotFound();
            }

            return View(stagiaire);
        }

        // GET: Stagiaires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stagiaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Gender,Birthday,Address,City,Cp,email,tel")] Stagiaire stagiaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stagiaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stagiaire);
        }

        // GET: Stagiaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stagiaire == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Stagiaire.FindAsync(id);
            if (stagiaire == null)
            {
                return NotFound();
            }
            return View(stagiaire);
        }

        // POST: Stagiaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,Gender,Birthday,Address,City,Cp,email,tel")] Stagiaire stagiaire)
        {
            if (id != stagiaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stagiaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StagiaireExists(stagiaire.Id))
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
            return View(stagiaire);
        }

        // GET: Stagiaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stagiaire == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Stagiaire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stagiaire == null)
            {
                return NotFound();
            }

            return View(stagiaire);
        }

        // POST: Stagiaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stagiaire == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Stagiaire'  is null.");
            }
            var stagiaire = await _context.Stagiaire.FindAsync(id);
            if (stagiaire != null)
            {
                _context.Stagiaire.Remove(stagiaire);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StagiaireExists(int id)
        {
          return (_context.Stagiaire?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
