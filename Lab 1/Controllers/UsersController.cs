using Lab_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Lab_1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var UsersViewModel = new List<UsersViewModel>();

            foreach (ApplicationUser user in users)
            {
                var viewModel = new UsersViewModel();
                viewModel.Id = user.Id;
                viewModel.FirstName = user.FirstName;
                viewModel.LastName = user.LastName;
                viewModel.Email = user.Email;
                viewModel.Roles = await GetUserRoles(user);
                UsersViewModel.Add(viewModel);
            }

            return View(UsersViewModel);
        }

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
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Email = user.Email;
            var roleViewModel = new List<RoleViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var ViewModel = new RoleViewModel 
                { 
                    Id = role.Id, Name = role.Name 
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    ViewModel.IsActive = true;
                }
                else
                {
                    ViewModel.IsActive = false;
                }
                roleViewModel.Add(ViewModel);
            }
            return View(roleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<RoleViewModel> roleViewModel, string id)
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
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(roleViewModel);
            }
            
            result = await _userManager.AddToRolesAsync(user, roleViewModel.Where(x => x.IsActive).Select(y => y.Name));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(roleViewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));

        }
    }
}
