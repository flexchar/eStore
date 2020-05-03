using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eStore.Data;
using eStore.Models;

namespace eStore.Controllers
{
    public class DronesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DronesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Drones
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Drones";

            var applicationDbContext = _context.Drone.Include(d => d.series);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Drones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Title = "Drone details";

            if (id == null)
            {
                return NotFound();
            }

            var drone = await _context.Drone
                .Include(d => d.series)
                .FirstOrDefaultAsync(m => m.id == id);
            if (drone == null)
            {
                return NotFound();
            }

            return View(drone);
        }

        // GET: Drones/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create drone";

            ViewData["series_id"] = new SelectList(_context.Series, "id", "name");
            return View();
        }

        // POST: Drones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,price,year,series_id")] Drone drone)
        {
            ViewBag.Title = "Create drone";

            if (ModelState.IsValid)
            {
                _context.Add(drone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["series_id"] = new SelectList(_context.Series, "id", "name", drone.series_id);
            return View(drone);
        }

        // GET: Drones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Title = "Edit drone";

            if (id == null)
            {
                return NotFound();
            }

            var drone = await _context.Drone.FindAsync(id);
            if (drone == null)
            {
                return NotFound();
            }
            ViewData["series_id"] = new SelectList(_context.Series, "id", "name", drone.series_id);
            return View(drone);
        }

        // POST: Drones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,price,year,series_id")] Drone drone)
        {
            ViewBag.Title = "Edit drone";

            if (id != drone.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DroneExists(drone.id))
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
            ViewData["series_id"] = new SelectList(_context.Series, "id", "name", drone.series_id);
            return View(drone);
        }

        // GET: Drones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Title = "Delete drone";

            if (id == null)
            {
                return NotFound();
            }

            var drone = await _context.Drone
                .Include(d => d.series)
                .FirstOrDefaultAsync(m => m.id == id);
            if (drone == null)
            {
                return NotFound();
            }

            return View(drone);
        }

        // POST: Drones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drone = await _context.Drone.FindAsync(id);
            _context.Drone.Remove(drone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DroneExists(int id)
        {
            return _context.Drone.Any(e => e.id == id);
        }
    }
}
