using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPoolLibrary.Data;
using CarPoolLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPoolMvc.Controllers
{
    public class DriverRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<RolesController> _logger;

        public DriverRequestsController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RolesController> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> DriverRequest()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = user?.Email;
            var loggedInMember = await _context.Members!.FirstOrDefaultAsync(m => m.Email == email);
            if (loggedInMember != null)
            {
                return View(loggedInMember);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> RequestDriverRole()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = user?.Email;
            var loggedInMember = await _context.Members!.FirstOrDefaultAsync(m => m.Email == email);
            if (loggedInMember != null)
            {
                loggedInMember.DriverRequest = true;
                _context.Update(loggedInMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DriverRequest));
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminApproval()
        {
            return View(await _context.Members!.Where(m => m.DriverRequest == true).ToListAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveRequestDriverRole(int memberId)
        {
            var member = await _context.Members!.FindAsync(memberId);
            if (member == null)
            {
                return NotFound();
            }
            member.DriverRequest = false;
            _context.Update(member);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByEmailAsync(member.Email!);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            const string newRole = "Owner";
            result = await _userManager.AddToRoleAsync(user, newRole);
            if (!result.Succeeded)
            {
                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(AdminApproval));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeclineRequestDriverRole(int memberId)
        {
            var member = await _context.Members!.FindAsync(memberId);
            if (member == null)
            {
                return NotFound();
            }
            member.DriverRequest = false;
            _context.Update(member);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AdminApproval));
        }
    }
}