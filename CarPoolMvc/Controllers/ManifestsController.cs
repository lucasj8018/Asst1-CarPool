using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarPoolLibrary.Models;
using CarPoolLibrary.Data;

namespace CarPoolMvc.Controllers
{
    [Route("Manifests")]
    public class ManifestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManifestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Manifests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Manifests!.Include(m => m.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Manifests/Details/5/5
        [HttpGet("Details/{manifestId}/{memberId}")]
        public async Task<IActionResult> Details(int? manifestId, int? memberId)
        {
            if (manifestId == null || memberId == null)
            {
                return NotFound();
            }

            var manifest = await _context.Manifests!
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.ManifestId == manifestId && m.MemberId == memberId);
            if (manifest == null)
            {
                return NotFound();
            }

            return View(manifest);
        }

        // GET: Manifests/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Email");
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId");
            return View();
        }

        // POST: Manifests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManifestId,MemberId,TripId,Notes,Created,Modified,CreatedBy,ModifiedBy")] Manifest manifest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manifest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Email", manifest.MemberId);
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId", manifest.TripId);
            return View(manifest);
        }

        // GET: Manifests/Edit/5/5
        [HttpGet("Edit/{manifestId}/{memberId}")]
        public async Task<IActionResult> Edit(int? manifestId, int? memberId)
        {
            if (manifestId == null || memberId == null)
            {
                return NotFound();
            }

            var manifest = await _context.Manifests!
                .FirstOrDefaultAsync(m => m.ManifestId == manifestId && m.MemberId == memberId);
            if (manifest == null)
            {
                return NotFound();
            }

            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Email", manifest.MemberId);
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId", manifest.TripId);
            return View(manifest);
        }

        // POST: Manifests/Edit/5/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{manifestId}/{memberId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int manifestId, int memberId, [Bind("ManifestId,MemberId,TripId,Notes,Created,Modified,CreatedBy,ModifiedBy")] Manifest manifest)
        {
            if (manifestId != manifest.ManifestId || memberId != manifest.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manifest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManifestExists(manifest.ManifestId, manifest.MemberId))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Email", manifest.MemberId);
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId", manifest.TripId);
            return View(manifest);
        }

        // GET: Manifests/Delete/5/5
        [HttpGet("Delete/{manifestId}/{memberId}")]
        public async Task<IActionResult> Delete(int? manifestId, int? memberId)
        {
            if (manifestId == null || memberId == null)
            {
                return NotFound();
            }

            var manifest = await _context.Manifests!
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.ManifestId == manifestId && m.MemberId == memberId);
            if (manifest == null)
            {
                return NotFound();
            }

            return View(manifest);
        }

        // POST: Manifests/Delete/5/5
        [HttpPost("Delete/{manifestId}/{memberId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int manifestId, int memberId)
        {
            var manifest = await _context.Manifests!
                .FirstOrDefaultAsync(m => m.ManifestId == manifestId && m.MemberId == memberId);
            if (manifest != null)
            {
                _context.Manifests!.Remove(manifest);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ManifestExists(int manifestId, int memberId)
        {
            return _context.Manifests!.Any(e => e.ManifestId == manifestId && e.MemberId == memberId);
        }
    }
}
