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
    [Authorize(Roles = "Admin, Owner")]
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public VehiclesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user!, "Admin");

            List<Vehicle> vehicles;

            if (isAdmin)
            {
                vehicles = await _context.Vehicles!.ToListAsync();
            }
            else
            {
                var email = user?.Email; // Fetching Email of the logged-in user
                if (email == null)
                {
                    return NotFound("User email not found.");
                }

                var member = await _context.Members!.FirstOrDefaultAsync(m => m.Email == email);
                if (member == null)
                {
                    return NotFound("Member not found for the current user.");
                }

                vehicles = await _context.Vehicles!.Where(v => v.MemberId == member.MemberId).ToListAsync();
            }

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles!
                .Include(v => v.Member)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // Define a set of default vehicle types
        public List<string> DefaultVehicleTypes()
        {
            return ["Sedan", "SUV", "Truck", "Hatchback", "Minivan", "Compact", "Other"];
        }

        // GET: Vehicles/Add
        // Let admin add a vehicle for any owner
        // Let owner add a vehicle for themselves only
        public async Task<IActionResult> Add()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user!, "Admin");

            // show all Owners if logged-in user is Admin
            var owners = await _userManager.GetUsersInRoleAsync("Owner");
            var ownerEmails = owners.Select(o => o.Email).ToList();
            var ownerMembers = await _context.Members!
                .Where(member => ownerEmails.Contains(member.Email))
                .ToListAsync();
            var loggedInMember = ownerMembers.Find(m => m.Email == user?.Email);
            if (isAdmin)
            {
                // Return a list of Owners by their member ID to the View, but display the owners' full name
                // Ensure the default selected value is the logged-in user's member ID
                ViewData["Owners"] = new SelectList(ownerMembers, "MemberId", "FullName", loggedInMember?.MemberId);
            }
            else
            {
                // If the logged-in user is not an Admin, show only the logged-in users member
                ViewData["Owners"] = new SelectList(new List<Member> { loggedInMember! }, "MemberId", "FullName", loggedInMember?.MemberId);
            }
            // Pass the list of defined vehicle types to the view
            ViewData["DefaultVehicleTypes"] = new SelectList(DefaultVehicleTypes());
            return View();
        }

        // POST: Vehicles/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("VehicleId,Model,Make,Year,NumberOfSeats,VehicleType,MemberId,Created,Modified,CreatedBy,ModifiedBy")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                // Add create by, modified by info
                var user = await _userManager.GetUserAsync(User);
                vehicle.CreatedBy = user!.Id;
                vehicle.ModifiedBy = user!.Id;
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        // Let Admin change the vehicle owner
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles!.Include(v => v.Member).FirstOrDefaultAsync(v => v.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            // check if the logged-in user is an Admin
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user!, "Admin");
            if (isAdmin)
            {
                // show all Owners
                var owners = await _userManager.GetUsersInRoleAsync("Owner");
                var ownerEmails = owners.Select(o => o.Email).ToList();
                var ownerMembers = await _context.Members!
                    .Where(member => ownerEmails.Contains(member.Email))
                    .ToListAsync();
                // Return a list of Owners to the View, but display the owners' full name instead of the member ID
                // Default selected value is the current vehicle owner's member ID
                ViewData["Owners"] = new SelectList(ownerMembers, "MemberId", "FullName", vehicle.MemberId);
            }
            else
            {
                // Return vehicle owner to the View, but display the owners' full name instead of the member ID
                // Default selected value is the vehicle owner's member ID
                ViewData["Owners"] = new SelectList(new List<Member> { vehicle.Member! }, "MemberId", "FullName", vehicle.MemberId);
            }
            ViewData["DefaultVehicleTypes"] = new SelectList(DefaultVehicleTypes());
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,Model,Make,Year,NumberOfSeats,VehicleType,MemberId,Created,Modified,CreatedBy,ModifiedBy")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Add modified by info
                    var user = await _userManager.GetUserAsync(User);
                    vehicle.ModifiedBy = user!.Id;
                    vehicle.Modified = DateTime.Now;
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
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
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles!
                .Include(v => v.Member)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles!.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles!.Any(e => e.VehicleId == id);
        }
    }
}

