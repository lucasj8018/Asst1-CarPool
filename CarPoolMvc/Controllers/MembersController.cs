using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarPoolLibrary.Models;
using CarPoolLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CarPoolMvc.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UsersController> _logger;

        public MembersController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ILogger<UsersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET: Members
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members!.ToListAsync());
        }

        // GET: Members/Details/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members!
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        [Authorize(Roles="Admin, Passenger")]
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin, Passenger")]
        public async Task<IActionResult> Create([Bind("MemberId,FirstName,LastName,Email,Mobile,Street,City,PostalCode,Country,Created,Modified,CreatedBy,ModifiedBy")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members!.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FirstName,LastName,Email,Mobile,Street,City,PostalCode,Country,Created,Modified,CreatedBy,ModifiedBy")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members!
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members!.FindAsync(id);
            if (member != null)
            {

                // Get the email of the currently logged in user
                var loggedInUserEmail = User.Identity?.Name;
                // Compare the email of the Member to be deleted with the email of the currently logged in user
                if (member.Email == loggedInUserEmail)
                {
                    // Return an error message if they are the same
                    ModelState.AddModelError(string.Empty, "You cannot delete the account of the currently logged in user.");
                    _logger.LogWarning("User tried to delete their own account.");
                    return RedirectToAction(nameof(Index));
                }

                _context.Members.Remove(member);
                await _context.SaveChangesAsync();

                // Find the User with the same email as the Member
                var user = await _userManager.FindByEmailAsync(member.Email!);
                if (user != null)
                {
                    // Delete the User with the same email as the Member
                    var result = await _userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View();
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members!.Any(e => e.MemberId == id);
        }
    }
}
