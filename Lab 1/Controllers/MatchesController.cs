using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_1.Data;
using Lab_1.Models;
using Lab_1.Data.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace Lab_1.Controllers
{
    
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Matches.Include(m => m.TeamOne).Include(m => m.TeamTwo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.TeamOne)
                .Include(m => m.TeamTwo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [Authorize(Roles = "Referee, Admin")]
        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["TeamOneID"] = new SelectList(_context.Teams, "Id", "TeamName");
            ViewData["TeamTwoID"] = new SelectList(_context.Teams, "Id", "TeamName");
            ViewData["Arenas"] = new SelectList(_context.Arenas, "Id", "Name");
            return View();
        }

        [Authorize(Roles = "Referee, Admin")]
        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamOneID,TeamTwoID,TeamOneScore,TeamTwoScore,MatchDate")] Match match)
        {
            if (ModelState.IsValid)
            {

                if (match.TeamOneID == match.TeamTwoID)
                {
                    return BadRequest("Error, Team A and Team B have the same ID");
                }

                Team teamOne = await _context.Teams.FindAsync(match.TeamOneID);
                Team teamTwo = await _context.Teams.FindAsync(match.TeamTwoID);
                Task updateScores = UpdateScoring(teamOne, teamTwo, match);
                await updateScores;

                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamOneID"] = new SelectList(_context.Teams, "Id", "TeamName", match.TeamOneID);
            ViewData["TeamTwoID"] = new SelectList(_context.Teams, "Id", "TeamName", match.TeamTwoID);
            return View(match);
        }

        [Authorize(Roles = "Referee, Admin")]
        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);

            Team teamOne = await _context.Teams.FindAsync(match.TeamOneID);
            Team teamTwo = await _context.Teams.FindAsync(match.TeamTwoID);
            Task updateScores = UndoScoring(teamOne, teamTwo, match);
            await updateScores;

            if (match == null)
            {
                return NotFound();
            }

            ViewData["TeamOneID"] = new SelectList(_context.Teams, "Id", "TeamName", match.TeamOneID);
            ViewData["TeamTwoID"] = new SelectList(_context.Teams, "Id", "TeamName", match.TeamTwoID);
            ViewData["Arenas"] = new SelectList(_context.Arenas, "Id", "Name");
            return View(match);
        }

        [Authorize(Roles = "Referee, Admin")]
        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamOneID,TeamTwoID,TeamOneScore,TeamTwoScore,MatchDate")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Team teamOne = await _context.Teams.FindAsync(match.TeamOneID);
                    Team teamTwo = await _context.Teams.FindAsync(match.TeamTwoID);
                    Task updateScores = UpdateScoring(teamOne, teamTwo, match);
                    await updateScores;
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["TeamOneID"] = new SelectList(_context.Teams, "Id", "TeamName", match.TeamOneID);
            ViewData["TeamTwoID"] = new SelectList(_context.Teams, "Id", "TeamName", match.TeamTwoID);
            ViewData["Arenas"] = new SelectList(_context.Arenas, "Id", "Name");
            return View(match);
        }

        [Authorize(Roles = "Referee, Admin")]
        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.TeamOne)
                .Include(m => m.TeamTwo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [Authorize(Roles = "Referee, Admin")]
        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Matches'  is null.");
            }
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                Team teamOne = await _context.Teams.FindAsync(match.TeamOneID);
                Team teamTwo = await _context.Teams.FindAsync(match.TeamTwoID);
                Task updateScores = UndoScoring(teamOne, teamTwo, match);
                await updateScores;
                _context.Matches.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }

        private async Task<IActionResult> UpdateScoring(Team a, Team b, Match match)
        {
            if (match.TeamOneScore > match.TeamTwoScore)
            {
                a.Wins++;
                b.Losses++;
            }
            else if (match.TeamOneScore < match.TeamTwoScore)
            {
                b.Wins++;
                a.Losses++;
            }
            else
            {
                a.Ties++;
                b.Ties++;
            }
            try
            {
                _context.Update(a);
                _context.Update(b);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
                throw;
            }
            return Ok();
        }

        private async Task<IActionResult> UndoScoring(Team a, Team b, Match match)
        {
            if (match.TeamOneScore > match.TeamTwoScore)
            {
                a.Wins--;
                b.Losses--;
            }
            else if (match.TeamOneScore < match.TeamTwoScore)
            {
                b.Wins--;
                b.Losses--;
            }
            else
            {
                a.Ties--;
                b.Ties--;
            }
            try
            {
                _context.Update(a);
                _context.Update(b);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
                throw;
            }
            return Ok();
        }
    }
}
