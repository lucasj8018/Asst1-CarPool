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
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarPoolMvc.Controllers
{
    [Authorize(Roles = "Admin, Owner, Passenger")]
    [Route("Trips")]
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TripsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trips!.Include(t => t.Vehicle!.Member).Include(t => t.Members);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Trips/Details/1/1
        [HttpGet("Details/{tripId}/{vehicleId}")]
        public async Task<IActionResult> Details(int? tripId, int? vehicleId)
        {
            if (tripId == null || vehicleId == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips!
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(m => m.TripId == tripId && m.VehicleId == vehicleId);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        // Admin can create a trip for any vehicle
        // Owner can only create a trip for their own vehicles
        [Authorize(Roles = "Admin, Owner")]
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            // check if the logged-in user is an Admin
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user!, "Admin");
            if (isAdmin) 
            {
                // Return a list of all vehicles to the View by the vehicle ID, 
                // but display the name of the vehicle full name instead of the vehicle ID
                ViewData["VehicleId"] = new SelectList(_context.Vehicles?.Include(v => v.Member), "VehicleId", "FullName");
            }
            else
            {
                var currentMember = await _context.Members!.FirstOrDefaultAsync(m => m.Email == user!.Email);
                // Return a list of vehicles owned by the logged-in user
                ViewData["VehicleId"] = new SelectList(_context.Vehicles?
                    .Include(v => v.Member)
                    .Where(v => v.MemberId == currentMember!.MemberId), "VehicleId", "FullName");
            }
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Owner")]
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,VehicleId,Date,Time,Destination,MeetingAddress,Created,Modified,CreatedBy,ModifiedBy,Members")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                // Add create by, modified by info
                var user = await _userManager.GetUserAsync(User);
                trip.CreatedBy = user!.Id;
                trip.ModifiedBy = user!.Id;
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trips/Edit/5/5
        [Authorize(Roles = "Admin, Owner")]
        [HttpGet("Edit/{tripId}/{vehicleId}")]
        public async Task<IActionResult> Edit(int? tripId, int? vehicleId)
        {
            if (tripId == null || vehicleId == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips!
                .Where(t => t.TripId == tripId && t.VehicleId == vehicleId)
                .FirstOrDefaultAsync();

            if (trip == null)
            {
                return NotFound();
            }
            // Return a list of all vehicles to the View by the vehicle ID, 
            // but display the name of the vehicle full name instead of the vehicle ID
            ViewData["VehicleId"] = new SelectList(_context.Vehicles?.Include(v => v.Member), "VehicleId", "FullName", trip.VehicleId);
            return View(trip);
        }

        // POST: Trips/Edit/5/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Owner")]
        [HttpPost("Edit/{tripId}/{vehicleId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int tripId, int vehicleId, [Bind("TripId,VehicleId,Date,Time,Destination,MeetingAddress,Created,Modified,CreatedBy,ModifiedBy,Members")] Trip trip)
        {
            if (tripId != trip.TripId || vehicleId != trip.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Add modified by info
                    var user = await _userManager.GetUserAsync(User);
                    trip.ModifiedBy = user!.Id;
                    trip.Modified = DateTime.Now;
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.TripId, trip.VehicleId))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Model", trip.VehicleId);
            return View(trip);
        }

        // GET: Trips/Delete/5/5
        [Authorize(Roles = "Admin, Owner")]
        [HttpGet("Delete/{tripId}/{vehicleId}")]
        public async Task<IActionResult> Delete(int? tripId, int? vehicleId)
        {
            if (tripId == null || vehicleId == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips!
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(m => m.TripId == tripId && m.VehicleId == vehicleId);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5/5
        [Authorize(Roles = "Admin, Owner")]
        [HttpPost("Delete/{tripId}/{vehicleId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int tripId, int vehicleId)
        {
            var trip = await _context.Trips!
                .FirstOrDefaultAsync(m => m.TripId == tripId && m.VehicleId == vehicleId);
            if (trip != null)
            {
                _context.Trips!.Remove(trip);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int tripId, int vehicleId)
        {
            return _context.Trips!.Any(e => e.TripId == tripId && e.VehicleId == vehicleId);
        }

        // GET: Trips/Register/{tripId}
        [Authorize(Roles = "Admin, Passenger")]
        [HttpGet("Register/{tripId}")]
        public async Task<IActionResult> Register(int? tripId)
        {
            var trip = await _context.Trips!
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(m => m.TripId == tripId);
                
            if (tripId == null || trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Register
        // Allow a Passenger to register for a trip
        // Redirect to Members/Create if they are not already a member
        [Authorize(Roles = "Admin, Passenger"), ActionName("Register")]
        [HttpPost("Register/{tripId}")]
        public async Task<IActionResult> RegisterPassenger(int tripId)
        {
            // Find the Member associated with the logged-in user
            var user = await _userManager.GetUserAsync(User);
            var email = user?.Email;
            if (email == null)
            {
                return NotFound("User email not found.");
            }
            var member = await _context.Members!.Include(m => m.Trips).FirstOrDefaultAsync(m => m.Email == email);
            if (member == null)
            {
                return RedirectToAction("Create", "Members");
            }
            
            var currentTrip = await _context.Trips!.Include(t => t.Members).FirstOrDefaultAsync(t => t.TripId == tripId);
            // Only Passenger members can register for trips
            if (member != null && currentTrip != null)
            {
                // Add modified by info
                currentTrip.ModifiedBy = user!.Id;
                currentTrip.Modified = DateTime.Now;
                member.Trips!.Add(currentTrip);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // create a method to unregister a passenger from a trip
        // GET: Trips/Unregister/{tripId}
        [Authorize(Roles = "Admin, Passenger")]
        [HttpGet("Unregister/{tripId}")]
        public async Task<IActionResult> Unregister(int? tripId)
        {
            var trip = await _context.Trips!
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(m => m.TripId == tripId);
                
            if (tripId == null || trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Unregister
        // Allow a Passenger to unregister from a trip
        [Authorize(Roles = "Admin, Passenger"), ActionName("Unregister")]
        [HttpPost("Unregister/{tripId}")]
        public async Task<IActionResult> UnregisterPassenger(int tripId)
        {
            // Find the Member associated with the logged-in user
            var user = await _userManager.GetUserAsync(User);
            var email = user?.Email;
            if (email == null)
            {
                return NotFound("User email not found.");
            }
            var member = await _context.Members!.Include(m => m.Trips).FirstOrDefaultAsync(m => m.Email == email);
            if (member == null)
            {
                return RedirectToAction("Create", "Members");
            }
            
            var currentTrip = await _context.Trips!.Include(t => t.Members).FirstOrDefaultAsync(t => t.TripId == tripId);
            // Only Passenger members can unregister from trips
            if (member != null && currentTrip != null)
            {
                // Add modified by info
                currentTrip.ModifiedBy = user!.Id;
                currentTrip.Modified = DateTime.Now;
                member.Trips!.Remove(currentTrip);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
