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

namespace CarPoolMvc.Controllers
{
    [Authorize(Roles = "Admin, Owner, Passenger")]
    [Route("Trips")]
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trips!.Include(t => t.Vehicle);
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
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Model");
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,VehicleId,Date,Time,Destination,MeetingAddress,Created,Modified,CreatedBy,ModifiedBy")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Model", trip.VehicleId);
            return View(trip);
        }

        // GET: Trips/Edit/5/5
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

            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Model", trip.VehicleId);
            return View(trip);
        }

        // POST: Trips/Edit/5/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{tripId}/{vehicleId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int tripId, int vehicleId, [Bind("TripId,VehicleId,Date,Time,Destination,MeetingAddress,Created,Modified,CreatedBy,ModifiedBy")] Trip trip)
        {
            if (tripId != trip.TripId || vehicleId != trip.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
    }
}
