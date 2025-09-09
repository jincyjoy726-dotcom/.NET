
using KeralaDirectory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaDirectory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                });
            }
            return View(userViewModels);
        }

        // GET: Admin/ManageRoles/some-user-id
        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ManageUserRolesViewModel { UserId = user.Id, UserEmail = user.Email };
            var roles = new List<RoleCheckBox>();

            foreach (var role in await _roleManager.Roles.ToListAsync())
            {
                roles.Add(new RoleCheckBox
                {
                    RoleName = role.Name,
                    // Check the box if the user already has this role
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                });
            }
            model.Roles = roles;
            return View(model);
        }

        // POST: Admin/ManageRoles
        [HttpPost]
        public async Task<IActionResult> ManageRoles(ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Get the list of roles the user currently has
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Get the list of roles that were selected in the form
            var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName);

            // Add the new roles
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(currentRoles));
            if (!result.Succeeded)
            {
                // Handle errors
                return View(model);
            }

            // Remove the old roles that were un-checked
            result = await _userManager.RemoveFromRolesAsync(user, currentRoles.Except(selectedRoles));
            if (!result.Succeeded)
            {
                // Handle errors
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}