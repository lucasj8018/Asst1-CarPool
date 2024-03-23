using Microsoft.AspNetCore.Mvc;
using CarPoolLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using CarPoolLibrary.Data;

namespace CarPoolMvc.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ILogger<UsersController> logger)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var usersWithRoles = await GetUsersWithRolesAsync();
        var sortedUsersWithRoles = usersWithRoles
        .OrderBy(uwr => string.Join(", ", uwr.Roles))
        .ToList();
        return View(sortedUsersWithRoles);
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

        // Get the currently logged in user
        var loggedInUser = await _userManager.GetUserAsync(User);
        if (loggedInUser != null && loggedInUser.Id == user.Id)
        {
            // Return an error message if they are the same
            ModelState.AddModelError(string.Empty, "You cannot delete the account of the currently logged in user.");
            _logger.LogWarning("User tried to delete their own account.");
            return RedirectToAction(nameof(Index));
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            // Find the Member with the same email as the User
            var member = await _context.Members!.FirstOrDefaultAsync(m => m.Email == user.Email);
            if (member != null)
            {
                // Delete the Member with the same email as the User
                _context.Members!.Remove(member);
                await _context.SaveChangesAsync();
            }

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

