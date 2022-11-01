using Lab_1.Data;
using Lab_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab_1.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlayersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var playersViewModel = new List<PlayerViewModel>();
            foreach (ApplicationUser user in users)
            {
                var viewModel = new PlayerViewModel();
                viewModel.Id = user.Id;
                viewModel.FirstName = user.FirstName;
                viewModel.LastName = user.LastName;
                viewModel.Email = user.Email;
                if (user.TeamId != null)
                {
                    viewModel.TeamId = (int)user.TeamId;
                    viewModel.Team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == viewModel.TeamId);
                }
                viewModel.ProfilePicture = user.ProfilePicture;
                playersViewModel.Add(viewModel);
            }
            return View(playersViewModel);
        }

        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            List<SelectListItem> teamsList = new SelectList(_context.Teams, "Id", "TeamName").ToList();
            teamsList.Insert(0, new SelectListItem { Text = "No team", Value = "0" });
            ViewData["Teams"] = teamsList;
            
            var playerViewModel = new PlayerViewModel();
            playerViewModel.Id = user.Id;
            playerViewModel.FirstName = user.FirstName;
            playerViewModel.LastName = user.LastName;
            playerViewModel.Email = user.Email;
            if (user.TeamId != null)
            {
                playerViewModel.TeamId = (int)user.TeamId;
                playerViewModel.Team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == playerViewModel.TeamId);
            }
            playerViewModel.ProfilePicture = user.ProfilePicture;

            return View(playerViewModel);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerViewModel playerViewModel, string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (playerViewModel.TeamId == 0)
            {
                user.TeamId = null;
            }
            else
            {
                user.TeamId = playerViewModel.TeamId;
            }
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected user to team");
                return View(playerViewModel);
            }
            return RedirectToAction("Index");
        }
    }
}
