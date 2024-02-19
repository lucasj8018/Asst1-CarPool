using Microsoft.AspNetCore.Mvc;
using CarPoolLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarPoolMvc.Controllers;
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var usersWithRoles = await GetUsersWithRolesAsync();
        return View(usersWithRoles);
    }

    private async Task<List<UserWithRole>> GetUsersWithRolesAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var usersWithRoles = new List<UserWithRole>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            usersWithRoles.Add(new UserWithRole
            {
                User = user,
                Roles = roles
            });
        }
        return usersWithRoles;
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var userWithRole = new UserWithRole
        {
            User = user,
            Roles = roles.ToList()
        };

        return View(userWithRole);
    }

    public async Task<IActionResult> Delete(string id)
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
        var userWithRole = new UserWithRole
        {
            User = user,
            Roles = roles.ToList()
        };

        return View(userWithRole);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(nameof(Index), await GetUsersWithRolesAsync());
    }


    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList();

        var userWithRole = new UserWithRole
        {
            User = user,
            Roles = roles.ToList(),
        };

        return View(userWithRole);
    }


    // POST: Users/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserWithRole model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByIdAsync(model.User!.Id);
        if (user == null)
        {
            return NotFound();
        }

        user.Email = model.User.Email;

        var currentRoles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!result.Succeeded)
        {
            // Handle errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        foreach (var role in model.Roles)
        {
            result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        // Save changes to the user
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            // Handle errors
            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

}

