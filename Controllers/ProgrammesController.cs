using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Session.Data;
using Session.Models;

namespace Session.Controllers
{
    public class ProgrammesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgrammesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Programmes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Programme.Include(p => p.Module).Include(p => p.Session);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Programmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Programme == null)
            {
                return NotFound();
            }

            var programme = await _context.Programme
                .Include(p => p.Module)
                .Include(p => p.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programme == null)
            {
                return NotFound();
            }

            return View(programme);
        }

        [Authorize]
        // GET: Programmes/Create/{sessionId}
        public IActionResult Create(int? sessionId)
        {

            Sessions sessions = _context.Session.FirstOrDefault(s => s.Id == sessionId);

            ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "SessionName", sessions.Id);
            return View();
        }

        [Authorize]
        // POST: Programmes/Create/{sessionId}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SessionId,ModuleId,Duree")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programme);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Sessions", new { Id = programme.SessionId });
            }
            ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Name", programme.ModuleId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "SessionName", programme.SessionId);
            return View(programme);
        }

        [Authorize]
        // GET: Programmes/Edit/5/{sessionId}
        public async Task<IActionResult> Edit(int? id, int sessionId)
        {
            if (id == null || _context.Programme == null)
            {
                return NotFound();
            }

            var programme = await _context.Programme.FindAsync(id);
            Sessions session = _context.Session.FirstOrDefault(s => s.Id == sessionId);

            if (programme == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Name", programme.ModuleId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "SessionName", programme.SessionId);
            return View(programme);
        }

        [Authorize]
        // POST: Programmes/Edit/5/{sessionId}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionId,ModuleId,Duree")] Programme programme)
        {
            if (id != programme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgrammeExists(programme.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Sessions", new { Id = programme.SessionId});
            }
            ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Name", programme.ModuleId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "SessionName", programme.SessionId);
            return View(programme);
        }

        [Authorize]
        // GET: Programmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Programme == null)
            {
                return NotFound();
            }

            var programme = await _context.Programme
                .Include(p => p.Module)
                .Include(p => p.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programme == null)
            {
                return NotFound();
            }

            return View(programme);
        }

        [Authorize]
        // POST: Programmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Programme == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Programme'  is null.");
            }
            var programme = await _context.Programme.FindAsync(id);
            if (programme != null)
            {
                _context.Programme.Remove(programme);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Sessions", new { Id = programme.SessionId });
        }

        private bool ProgrammeExists(int id)
        {
          return (_context.Programme?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
