using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_1.Data;
using Lab_1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab_1.Controllers
{
    [Authorize(Roles = "Admin, Referee")]
    public class ArenasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArenasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Arenas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Arenas.ToListAsync());
        }

        // GET: Arenas/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Arenas == null)
            {
                return NotFound();
            }

            var arena = await _context.Arenas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arena == null)
            {
                return NotFound();
            }

            return View(arena);
        }

        // GET: Arenas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Arenas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] Arena arena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(arena);
        }

        // GET: Arenas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Arenas == null)
            {
                return NotFound();
            }

            var arena = await _context.Arenas.FindAsync(id);
            if (arena == null)
            {
                return NotFound();
            }
            return View(arena);
        }

        // POST: Arenas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Arena arena)
        {
            if (id != arena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArenaExists(arena.Id))
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
            return View(arena);
        }

        // GET: Arenas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Arenas == null)
            {
                return NotFound();
            }

            var arena = await _context.Arenas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arena == null)
            {
                return NotFound();
            }

            return View(arena);
        }

        // POST: Arenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Arenas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Arenas'  is null.");
            }
            var arena = await _context.Arenas.FindAsync(id);
            if (arena != null)
            {
                _context.Arenas.Remove(arena);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArenaExists(int id)
        {
            return _context.Arenas.Any(e => e.Id == id);
        }
    }
}
